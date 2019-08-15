using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.CJT188_2004
{
    /// <summary>
    /// CJT188_2004协议帧
    /// </summary>
    public class FrameCJT188_2004 : NonLeafField<Byte>
    {

        public FrameCJT188_2004() : base(13)
        {
            IsLengthFixed = false;
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(StartByte);
            Children.Add(FMeterType);
            Children.Add(FAddress);
            Children.Add(FControl);
            Children.Add(FLength);
            Children.Add(FDataField);
            Children.Add(FCheckSum);
            Children.Add(EndByte);
        }

        public static ReadonlyPrimitiveField<Byte> StartByte = 0x68;

        private PrimitiveField<Byte> _fMeterType = 0;

        public PrimitiveField<Byte> FMeterType
        {
            get { return _fMeterType; }
            set { _fMeterType = value; }
        }

        private AddressField _fAddress = new AddressField();

        public AddressField FAddress
        {
            get { return _fAddress; }
            set { _fAddress = value; }
        }

        private ControlField _fControl = new ControlField();

        public ControlField FControl
        {
            get { return _fControl; }
            set { _fControl = value; }
        }

        private PrimitiveField<Byte> _fLength = 0;

        public PrimitiveField<Byte> FLength
        {
            get { return _fLength; }
            set { _fLength = value; }
        }

        private DataField _fDataField = new OtherDataField();

        public DataField FDataField
        {
            get { return _fDataField; }
            set { _fDataField = value; }
        }

        private PrimitiveField<Byte> _fCheckSum = 0;

        public PrimitiveField<Byte> FCheckSum
        {
            get { return _fCheckSum; }
            set { _fCheckSum = value; }
        }

        public static ReadonlyPrimitiveField<Byte> EndByte = 0x16;

        public override byte[] GetData()
        {
            byte[] dataFieldData = FDataField.GetData();
            FLength = (byte)dataFieldData.Length;
            FCheckSum = (byte)(StartByte.Value + FMeterType.Value + FAddress.Value.Sum(0, FAddress.Length)
                             + FControl.Value + FLength.Value + dataFieldData.Sum(0, dataFieldData.Length));
            return base.GetData();
        }

        public static Result IsProtocol(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            return IsProtocol(data, 0, data.Length);
        }


        public static Result IsProtocol(byte[] data, int startIndex, int len)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            if (data.Length < startIndex)
                throw new ArgumentOutOfRangeException("startIndex", "startIndex超出范围");
            if (data.Length < startIndex + len || len < 0)
                throw new ArgumentOutOfRangeException("len", "len超出范围");
            if (len < 13)
                return (Result)"该帧长度过小";
            if (data[startIndex] != StartByte.Value || data[startIndex + len - 1] != EndByte)
                return false;
            if (data[startIndex + 10] + 13 != len)  // data[startIndex + 10]是数据域的长度
                return false;
            if (data.Sum(startIndex, len - 2) != data[startIndex + len - 2]) // 计算校验和
                return (Result)"该帧校验和错误";
            return true;
        }


        /// <summary>
        /// 从字节数组中提取帧
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ExtractFrame(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            byte[] ret;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == StartByte)
                {
                    for (int j = i + 12; j < data.Length; j++)
                    {
                        if (data[j] == EndByte && IsProtocol(data, i, j - i + 1))
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
