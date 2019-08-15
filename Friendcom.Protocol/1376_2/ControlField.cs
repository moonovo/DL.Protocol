using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol._1376_2
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
        /// 传输方向位
        /// </summary>
        public byte DIR
        {
            get
            {
                return InsideData.GetBit(7);
            }
            set
            {
                InsideData.SetBit(7, value);
            }
        }

        /// <summary>
        /// 启动标志位
        /// </summary>
        public byte PRM
        {
            get
            {
                return InsideData.GetBit(6);
            }
            set
            {
                InsideData.SetBit(6, value);
            }
        }

        /// <summary>
        /// 通讯方式
        /// </summary>
        public byte CommunicationMode
        {
            get
            {
                return (byte)InsideData.GetBits(0, 6);
            }
            set
            {
                InsideData.SetBits(0, 6, value);
            }
        }
    }
}
