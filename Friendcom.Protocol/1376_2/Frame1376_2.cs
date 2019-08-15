using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._1376_2
{
    /// <summary>
    /// 1376_2协议帧 不能实例化
    /// </summary>
    public abstract class Frame1376_2 : NonLeafField<Byte>
    {
        public Frame1376_2() : base(15)
        {
            IsLengthFixed = false;
        }

        /// <summary>
        /// 开始字符
        /// </summary>
        public static ReadonlyPrimitiveField<Byte> StartByte = 0x68;

        private LengthField _fLength = new LengthField();

        /// <summary>
        /// 长度域
        /// </summary>
        public LengthField FLength
        {
            get { return _fLength; }
            set { _fLength = value; }
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

        private AddressField _fAddress = new AddressField();

        /// <summary>
        /// 地址域
        /// </summary>
        public AddressField FAddress
        {
            get { return _fAddress; }
            set { _fAddress = value; }
        }

        private PrimitiveField<Byte> _afn = 0;

        /// <summary>
        /// AFN
        /// </summary>
        public PrimitiveField<Byte> AFN
        {
            get { return _afn; }
            set { _afn = value; }
        }

        private DataUnitIdentifier _fDataUnitIdentifier = new DataUnitIdentifier();

        /// <summary>
        /// 数据单元标识
        /// </summary>
        public DataUnitIdentifier FDataUnitIdentifier
        {
            get { return _fDataUnitIdentifier; }
            set { _fDataUnitIdentifier = value; }
        }

        private DataUnit _fDataUnit = new OtherDataUnit();

        /// <summary>
        /// 数据单元
        /// </summary>
        public DataUnit FDataUnit
        {
            get { return _fDataUnit; }
            set { _fDataUnit = value; }
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

        protected static Result IsProtocolCommon(byte[] data, int startIndex, int len)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            if (data.Length < startIndex)
                throw new ArgumentOutOfRangeException("startIndex", "startIndex超出范围");
            if (data.Length < startIndex + len || len < 0)
                throw new ArgumentOutOfRangeException("len", "len超出范围");

            // 起始字符 1、长度 2、控制域 1、信息域 6、地址域 >=0、功能码 1、信息类 2、数据单元 >=0、校验和 1、结束字符 1
            if (len < 15) return false;
            if (data[startIndex] != StartByte) return false;
            if (data[startIndex + len - 1] != EndByte) return false;

            LengthField lf = new LengthField();
            lf.SetData(data, startIndex + 1, lf.Length);
            if (lf.Value != len) return (Result)"此帧的长度与长度域不符";

            byte checkSum = data.Sum(startIndex + 3, len - 5);
            if (checkSum != data[startIndex + len - 2]) return (Result)"此帧的校验和不正确";
            return true;
        }

    }
}
