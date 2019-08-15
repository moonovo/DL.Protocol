using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DL.Protocol.MicroPower.Enums;

namespace DL.Protocol.MicroPower
{
    /// <summary>
    /// 网络层的帧控制域
    /// </summary>
    public class NetCtrl : LeafField<Byte>
    {
        public NetCtrl() : base(1) 
        {
            
        }

        /// <summary>
        /// 路由指示 0-无路由信息域 1-有路由信息域
        /// </summary>
        public byte RoutingInstruction
        {
            get
            {
                return InsideData[0].GetBit(7);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBit(7, value);
            }
        }

        /// <summary>
        /// 源地址模式
        /// </summary>
        public EmAddrLen SourceAddrMode
        {
            get
            {
                return (EmAddrLen)InsideData[0].GetBits(4, 2);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBits(4, 2, (byte)value);
            }
        }

        /// <summary>
        /// 目标地址模式
        /// </summary>
        public EmAddrLen TargetAddrMode
        {
            get
            {
                return (EmAddrLen)InsideData[0].GetBits(2, 2);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBits(2, 2, (byte)value);
            }
        }

        /// <summary>
        /// 帧类型
        /// </summary>
        public EmNetFrameType FrameType
        {
            get
            {
                return (EmNetFrameType)InsideData[0].GetBits(0, 2);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBits(0, 2, (byte)value);
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
