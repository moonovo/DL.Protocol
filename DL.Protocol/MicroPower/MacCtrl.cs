using DL.Protocol.MicroPower.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.MicroPower
{
    /// <summary>
    /// Mac层 帧控制域
    /// </summary>
    public class MacCtrl : LeafField<Byte>
    {
        public MacCtrl() : base(2) 
        {
            // 本标准恒为true
            HasSeq = true;

            // 本标准恒为true
            HasPanID = true;

            // 默认的
            Hang = 0;
            Security = 0;

        }

        /// <summary>
        /// 源地址模式
        /// </summary>
        public EmAddrLen SourceAddrMode
        {
            get
            {
                return (EmAddrLen)InsideData.GetBits(14, 2);
            }
            set
            {
                InsideData = InsideData.SetBits(14, 2, (byte)value);
            }
        }

        /// <summary>
        /// 帧版本 
        /// </summary>
        public EmMacVersion Version 
        {
            get
            {
                return (EmMacVersion)InsideData.GetBits(12, 2);
            }
            set
            {
                InsideData = InsideData.SetBits(12, 2, (byte)value);
            }
        }

        /// <summary>
        /// 目标地址模式
        /// </summary>
        public EmAddrLen TargetAddrMode
        {
            get
            {
                return (EmAddrLen)InsideData.GetBits(10, 2);
            }
            set
            {
                InsideData = InsideData.SetBits(10, 2, (byte)value);
            }
        }

        /// <summary>
        /// 是否有扩展信息域指示
        /// </summary>
        public bool HasExtension
        {
            get
            {
                return InsideData.GetBit(9) == 1 ? true : false;
            }
            set
            {
                InsideData = InsideData.SetBit(9, value ? (byte)1 : (byte)0);
            }
        }

        /// <summary>
        /// 是否有帧序列号 本标准恒为true
        /// </summary>
        public bool HasSeq
        {
            get
            {
                return InsideData.GetBit(8) == 1 ? true : false;
            }
            private set
            {
                InsideData = InsideData.SetBit(8, value ? (byte)1 : (byte)0);
            }
        }

        /// <summary>
        /// 是否有网络号
        /// </summary>
        public bool HasPanID
        {
            get
            {
                return InsideData[0].GetBit(6) == 1 ? true : false;
            }
            private set
            {
                InsideData[0] = InsideData[0].SetBit(6, value ? (byte)1 : (byte)0);
            }
        }

        /// <summary>
        /// 确认请求 0-表示不需要确认请求帧 1-表示需要确认请求帧
        /// </summary>
        public byte ConfirmRequest
        {
            get
            {
                return InsideData[0].GetBit(5);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBit(5, value);
            }
        }

        /// <summary>
        /// 帧挂起域 0-表示无后续帧 1-表示有后续帧
        /// </summary>
        public byte Hang
        {
            get
            {
                return InsideData[0].GetBit(4);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBit(4, value);
            }
        }

        /// <summary>
        /// 安全使能 0-表示不使能安全功能 1-使能安全功能
        /// </summary>
        public byte Security
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
        /// 帧类型
        /// </summary>
        public EmMacFrameType FrameType
        {
            get
            {
                return (EmMacFrameType)InsideData[0].GetBits(0, 3);
            }
            set
            {
                InsideData[0] = InsideData[0].SetBits(0, 3, (byte)value);
            }
        }



    }
}
