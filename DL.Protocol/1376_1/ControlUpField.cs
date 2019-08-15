using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._1376_1
{
    /// <summary>
    /// 上行帧控制域
    /// </summary>
    public class ControlUpField : LeafField<Byte>
    {
        public ControlUpField() : base(1) { }

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
        /// 要求访问位
        /// </summary>
        public byte ACD
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
                return InsideData[0].GetBits(0, 4);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBits(0, 4, value);
            }
        }
    }
}
