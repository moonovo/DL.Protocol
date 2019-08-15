using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.CJT188_2004
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
        /// D7 0-主站发出的控制帧 1-从站发出的应答帧
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
        /// D6 通讯状态 0-正常  1-异常 
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
        /// 功能码
        /// </summary>
        public byte FunctionCode
        {
            get
            {
                return InsideData[0].GetBits(0, 6);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBits(0, 6, value);
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
