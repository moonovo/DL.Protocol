using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DL.Protocol._1376_1.Enum;

namespace DL.Protocol._1376_1
{
    /// <summary>
    /// 长度域
    /// </summary>
    public class LengthField : ReverseLeafField<Byte>
    {
        public LengthField() : base(2) { }

        /// <summary>
        /// 协议标识
        /// </summary>
        public EmProtocolIdentifier ProtocolIdentifier
        {
            get
            {
                byte val = InsideData[0].GetBits(0, 2);
                return (EmProtocolIdentifier)val;
            }
            set
            {
                byte val = (byte)value;
                InsideData[0] = InsideData[0].SetBits(0, 2, val);
            }
        }

        /// <summary>
        /// 长度
        /// </summary>
        public short Length
        {
            get
            {
                return (short)InsideData.GetBits(2, 14);
            }
            set
            {
                InsideData = InsideData.SetBits(2, 14, value);
            }
        }

        public byte[] Value
        {
            get
            {
                return InsideData;
            }
            set
            {
                InsideData = value;
            }
        }
    }
}
