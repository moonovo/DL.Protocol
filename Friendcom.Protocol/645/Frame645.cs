using Friendcom.Protocol._645._07_DataFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol._645
{
    /// <summary>
    /// 645协议帧
    /// </summary>
    public class Frame645 : NonLeafField<Byte>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldsNamespace">数据域的命名空间 默认为645_07协议的数据域命名空间</param>
        public Frame645(string dataFieldsNamespace = Frame645_07.DataFieldsNamespace)
            : base(12)
        {
            IsLengthFixed = false;
            DataFieldsNamespace = dataFieldsNamespace;
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(StartByte);
            Children.Add(FAddress);
            Children.Add(StartByte);
            Children.Add(FControl);
            Children.Add(FLength);
            Children.Add(FDataField);
            Children.Add(FCheckSum);
            Children.Add(EndByte);
        }

        protected string DataFieldsNamespace
        {
            get;
            set;
        }

        private static readonly string AssemblyName = "Friendcom.Protocol";

        private static readonly string DataFieldNameFormat = "C{0}_DataField";

        /// <summary>
        /// 开始字符
        /// </summary>
        public static ReadonlyPrimitiveField<Byte> StartByte = 0x68;


        private AddressField _fAddress = new AddressField();

        /// <summary>
        /// 地址域
        /// </summary>
        public AddressField FAddress
        {
            get { return _fAddress; }
            set { _fAddress = value; }
        }

        private ControlField _fControl = new ControlField();

        /// <summary>
        /// 控制域
        /// </summary>
        public ControlField FControl
        {
            get { return _fControl; }
            set { _fControl = value; }
        }

        private PrimitiveField<Byte> _fLength = 0;

        /// <summary>
        /// 长度
        /// </summary>
        public PrimitiveField<Byte> FLength
        {
            get { return _fLength; }
            set { _fLength = value; }
        }

        private DataField _fDataField = new OtherDataField();

        /// <summary>
        /// 数据域 默认为只包含数据的数据域
        /// </summary>
        public DataField FDataField
        {
            get { return _fDataField; }
            set { _fDataField = value; }
        }

        private PrimitiveField<Byte> _fCheckSum = 0;

        /// <summary>
        /// 校验和
        /// </summary>
        public PrimitiveField<Byte> FCheckSum
        {
          get { return _fCheckSum; }
          set { _fCheckSum = value; }
        }

        /// <summary>
        /// 结束字符
        /// </summary>
        public static ReadonlyPrimitiveField<Byte> EndByte = 0x16;


        /// <summary>
        /// 获取帧字节 自动计算数据域长度和校验和
        /// </summary>
        /// <returns></returns>
        public override byte[] GetData()
        {
            byte[] dataFieldData = FDataField.GetData();
            FLength = (byte)dataFieldData.Length;
            FCheckSum = (byte)(StartByte.Value + FAddress.Value.Sum(0, 6) + StartByte.Value 
                             + FControl.Value + FLength.Value + dataFieldData.Sum(0, dataFieldData.Length));
            return base.GetData();
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="len"></param>
        public override void SetData(byte[] data, int startIndex, int len)
        {
            if (!IsProtocolCommon(data, startIndex, len, DataFieldsNamespace))
            {
                throw new Exception("字节数组不符合645协议");
            }
            base.SetData(data, startIndex, len);
            byte[] dataFieldData = FDataField.GetData();
            DataField df = Tools.CreateInstance<DataField>(AssemblyName, DataFieldsNamespace, string.Format(DataFieldNameFormat, FControl.Value.ToString("X2")));
            if (df != null)
            {
                df.SetData(dataFieldData);
                FDataField = df;
            }
        }

        /// <summary>
        /// 检查字节数组是否符合645_2007协议
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="len"></param>
        /// <param name="checkDataUnit"></param>
        /// <param name="dataUnitLen"></param>
        /// <returns></returns>
        protected static Result IsProtocolCommon(byte[] data, int startIndex, int len, string dataFieldsNamespace)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            if (data.Length < startIndex)
                throw new ArgumentOutOfRangeException("startIndex", "startIndex超出范围");
            if (data.Length < startIndex + len || len < 0)
                throw new ArgumentOutOfRangeException("len", "len超出范围");

            if (len < 12)
                return false;
            if (data[startIndex] != StartByte.Value || data[startIndex + 7] != StartByte || data[startIndex + len - 1] != EndByte)
                return false;

            if (data[startIndex + 9] + 12 != len)
                return false;

            if (data.Sum(startIndex, len - 2) != data[startIndex + len - 2])
                return false;

            return true;
        }

        protected static Result IsProtocolCommon(byte[] data, string dataFieldsNamespace)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            return IsProtocolCommon(data, 0, data.Length, dataFieldsNamespace);
        }


        /// <summary>
        /// 从字节数组中提取帧
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected static byte[] ExtractFrameCommon(byte[] data, string dataFieldsNamespace)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            byte[] ret;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == StartByte)
                {
                    if (i + 7 >= data.Length || data[i + 7] != StartByte)
                        continue;
                    for (int j = i + 11; j < data.Length; j++)
                    {
                        if (data[j] == EndByte && IsProtocolCommon(data, i, j - i + 1, dataFieldsNamespace))
                        {
                            int len = j - i + 1;
                            ret = new byte[len];
                            Array.Copy(data, i, ret, 0, len);
                            return ret;
                        }
                    }
                }
            }
            return null;
        }
    }
}
