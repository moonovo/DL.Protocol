using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol._1376_1
{
    /// <summary>
    /// 下行帧控制域
    /// </summary>
    public class ControlDownField : LeafField<Byte>
    {
        public ControlDownField() : base(1) { }

        /// <summary>
        /// 传输方向位
        /// </summary>
        public byte DIR
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
        /// 启动标志位
        /// </summary>
        public byte PRM
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
        /// 帧计数位
        /// </summary>
        public byte FCB
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
        /// 帧计数有效位
        /// </summary>
        public byte FCV
        {
            get
            {
                return InsideData[0].GetBit(4);
            }
            set
            {
                InsideData[0].SetBit(4, value);
            }
        }

        /// <summary>
        /// 功能码
        /// </summary>
        public byte FunctionCode
        {
            get
            {
                return InsideData[0].GetBits(0, 4);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBits(0, 4, value);
            }
        }
    }
}
