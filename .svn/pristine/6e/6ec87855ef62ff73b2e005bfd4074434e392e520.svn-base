using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol._645
{
    /// <summary>
    /// 控制域
    /// </summary>
    public class ControlField : LeafField<Byte>
    {
        public ControlField()
            : base(1)
        {

        }

        /// <summary>
        /// D7 传送方向
        /// </summary>
        public byte D7
        {
            get
            {
                return InsideData[0].GetBit(7);
            }
            set
            {
                InsideData[0].SetBit(7, value);
            }
        }

        /// <summary>
        /// D6 从站应答标志
        /// </summary>
        public byte D6
        {
            get
            {
                return InsideData[0].GetBit(6);
            }
            set
            {
                InsideData[0].SetBit(6, value);
            }
        }

        /// <summary>
        /// D5 后续帧标志
        /// </summary>
        public byte D5
        {
            get
            {
                return InsideData[0].GetBit(5);
            }
            set
            {
                InsideData[0].SetBit(5, value);
            }
        }

        /// <summary>
        /// 功能码
        /// </summary>
        public byte FunctionCode
        {
            get
            {
                return InsideData[0].GetBits(0, 5);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBits(0, 5, value);
            }
        }

        public byte Value
        {
            get
            {
                return InsideData[0];
            }
            set
            {
                InsideData[0] = value;
            }
        }

    }
}
