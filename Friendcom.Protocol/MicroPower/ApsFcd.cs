using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DL.Protocol.MicroPower.Enums;

namespace DL.Protocol.MicroPower
{
    /// <summary>
    /// APS中帧控制域
    /// </summary>
    public class ApsFcd : LeafField<Byte>
    {
        public ApsFcd() : base(1) { }

        /// <summary>
        /// 业务扩展域标识 0标识没有 1标识有
        /// </summary>
        public byte Oei
        {
            get
            {
                return InsideData[0].GetBit(3);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBit(3, value);
            }
        }

        /// <summary>
        /// 帧类型域
        /// </summary>
        public EmApsFcdFtd Ftd
        {
            get
            {
                return (EmApsFcdFtd)InsideData[0].GetBits(0, 3);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBits(0, 3, (int)value);
            }
        }
    }
}
