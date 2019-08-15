using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.MicroPower
{
    /// <summary>
    /// Mac层 信标帧载荷域
    /// </summary>
    public class MacBeacon : LeafField<Byte>
    {
        public MacBeacon() : base(16) { }

        /// <summary>
        /// 发射随机延时
        /// </summary>
        public byte SendRandomTimeDelay
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

        /// <summary>
        /// 信标轮次
        /// </summary>
        public byte BeaconRounds
        {
            get
            {
                return InsideData[1];
            }
            set
            {
                InsideData[1] = value;
            }
        }

        /// <summary>
        /// 层次号
        /// </summary>
        public byte LevelNo
        {
            get
            {
                return (byte)InsideData.GetBits(26, 4);
            }
            set
            {
                InsideData = InsideData.SetBits(26, 4, value);
            }
        }

        /// <summary>
        /// 时隙号
        /// </summary>
        public short TSNo
        {
            get
            {
                return (short)InsideData.GetBits(16, 10);
            }
            set
            {
                InsideData = InsideData.SetBits(16, 10, value);
            }
        }

        /// <summary>
        /// 信标标识
        /// </summary>
        public byte BeaconID
        {
            get
            {
                return InsideData[4];
            }
            set
            {
                InsideData[4] = value;
            }
        }

        /// <summary>
        /// 网络规模
        /// </summary>
        public ushort NetworkSize
        {
            get
            {
                return (ushort)(InsideData[5] + (InsideData[6] << 8));
            }
            set
            {
                InsideData[5] = (byte)(value & 0xff);
                InsideData[6] = (byte)(value >> 8);
            }
        }

        /// <summary>
        /// 场强门限
        /// </summary>
        public byte StrengthLimit
        {
            get
            {
                return InsideData[7];
            }
            set
            {
                InsideData[7] = value;
            }
        }

        /// <summary>
        /// 中心节点
        /// </summary>
        public ushort PanID
        {
            get
            {
                return (ushort)(InsideData[8] + (InsideData[9] << 8));
            }
            set
            {
                InsideData[8] = (byte)(value & 0xff);
                InsideData[9] = (byte)(value >> 8);
            }
        }

        /// <summary>
        /// 中心节点地址 长度为6
        /// </summary>
        public byte[] PanIDAddr
        {
            get
            {
                byte[] addr = new byte[6];
                Array.Copy(InsideData, 10, addr, 0, 6);
                return addr.Reverse().ToArray();
            }
            set
            {
                if (value == null || value.Length != 6)
                {
                    throw new ArgumentException("value", "value不能为空并且长度为6");
                }
                byte[] addr = value.Reverse().ToArray();
                Array.Copy(addr, 0, InsideData, 10, 6);
            }
        }
    }
}
