using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._1376_2
{
    /// <summary>
    /// 上行帧中的信息域
    /// </summary>
    public class InformationUpField : LeafField<Byte>
    {
        public InformationUpField() : base(6) { }

        /// <summary>
        /// 路由标识
        /// </summary>
        public byte RouteIdentifier
        {
            get
            {
                return InsideData[0].GetBit(0);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBit(0, value);
            }
        }

        /// <summary>
        /// 通信模块标识
        /// </summary>
        public byte CommModuleIdentifier
        {
            get
            {
                return InsideData[0].GetBit(2);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBit(2, value);
            }
        }

        /// <summary>
        /// 中继级别
        /// </summary>
        public byte RelayLevel
        {
            get
            {
                return InsideData[0].GetBits(4, 4);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBits(4, 4, value);
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
        /// 实测相线标识
        /// </summary>
        public byte MeasuredPhaseIdentifier
        {
            get
            {
                return InsideData[2].GetBits(0, 4);
            }
            set
            {
                InsideData[2] = InsideData[2].SetBits(0, 4, value);
            }
        }

        /// <summary>
        /// 电能表通道特征
        /// </summary>
        public byte ElectricityMeterChannelProperites
        {
            get
            {
                return InsideData[2].GetBits(4, 4);
            }
            set
            {
                InsideData[2] = InsideData[2].SetBits(4, 4, value);
            }
        }

        /// <summary>
        /// 末级命令信号品质
        /// </summary>
        public byte FinalCommandSignalQuality
        {
            get
            {
                return InsideData[3].GetBits(0, 4);
            }
            set
            {
                InsideData[3] = InsideData[3].SetBits(0, 4, value);
            }
        }

        /// <summary>
        /// 末级应答信号品质
        /// </summary>
        public byte FinalAnswerSignalQuality
        {
            get
            {
                return InsideData[3].GetBits(4, 4);
            }
            set
            {
                InsideData[3] = InsideData[3].SetBits(4, 4, value);
            }
        }

        /// <summary>
        /// 事件标志
        /// </summary>
        public byte EventFlag
        {
            get
            {
                return InsideData[4].GetBit(0);
            }
            set
            {
                InsideData[4] = InsideData[4].SetBit(0, value);
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
