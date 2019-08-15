using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._1376_2
{
    /// <summary>
    /// 下行帧中的信息域
    /// </summary>
    public class InformationDownField : LeafField<Byte>
    {
        public InformationDownField() : base(6) { }

        /// <summary>
        /// 路由标识
        /// </summary>
        public byte RouteIdentifier
        {
            get
            {
                return  InsideData.GetBit(0);
            }
            set
            {
                InsideData.SetBit(0, value);
            }
        }

        /// <summary>
        /// 附属节点标识
        /// </summary>
        public byte AttachedNodeIdentifier
        {
            get
            {
                return InsideData.GetBit(1);
            }
            set
            {
                InsideData.SetBit(1, value);
            }
        }

        /// <summary>
        /// 通信模块标识
        /// </summary>
        public byte CommModuleIdentifier
        {
            get
            {
                return InsideData.GetBit(2);
            }
            set
            {
                InsideData.SetBit(2, value);
            }
        }

        /// <summary>
        /// 冲突检测
        /// </summary>
        public byte CollisionDetection
        {
            get
            {
                return InsideData.GetBit(3);
            }
            set
            {
                InsideData.SetBit(3, value);
            }
        }

        /// <summary>
        /// 中继级别
        /// </summary>
        public byte RelayLevel
        {
            get
            {
                return (byte)InsideData.GetBits(4, 4);
            }
            set
            {
                InsideData.SetBits(4, 4, value);
            }
        }

        /// <summary>
        /// 信道标识
        /// </summary>
        public byte ChannelIdentifier
        {
            get
            {
                return InsideData[1].GetBits(0, 4);
            }
            set
            {
                InsideData[1] = InsideData[1].SetBits(0, 4, value);
            }
        }

        /// <summary>
        /// 纠错编码标识
        /// </summary>
        public byte ErrorCollectingCodeIdentifier
        {
            get
            {
                return InsideData[1].GetBits(4, 4);
            }
            set
            {
                InsideData[1] = InsideData[1].SetBits(4, 4, value);
            }
        }

        /// <summary>
        /// 预计应答字节数
        /// </summary>
        public byte EstimatedNumberOfBytes
        {
            get
            {
                return InsideData[2];
            }
            set
            {
                InsideData[2] = value;
            }
        }

        /// <summary>
        /// 通信速率
        /// </summary>
        public short CommRate
        {
            get
            {
                return (short)InsideData.GetBits(24, 15);
            }
            set
            {
                InsideData.SetBits(24, 15, value);
            }
        }

        /// <summary>
        /// 速率单位标识
        /// </summary>
        public byte RateUnitIdentifier
        {
            get
            {
                return InsideData[4].GetBit(7);
            }
            set
            {
                InsideData[4] = InsideData[4].SetBit(7, value);
            }
        }

        /// <summary>
        /// 报文序列号
        /// </summary>
        public byte MessageSequenceNumber
        {
            get
            {
                return InsideData[5];
            }
            set
            {
                InsideData[5] = value;
            }
        }
    }
}
