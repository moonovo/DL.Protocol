using Friendcom.Protocol.MicroPower.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol.MicroPower
{
    /// <summary>
    /// Mac帧
    /// </summary>
    public class MacFrame : NonLeafField<Byte>
    {
        public MacFrame()
            : base(6)
        {
            IsLengthFixed = false;
        }

        private MacCtrl _ctrl = new MacCtrl();

        /// <summary>
        /// 帧控制域
        /// </summary>
        public MacCtrl Ctrl
        {
            get { return _ctrl; }
        }

        private Sequence _seq = new Sequence(0);

        /// <summary>
        /// 帧序号
        /// </summary>
        public Sequence Seq
        {
            get { return _seq; }
            set { _seq = value; }
        }

        private LeafField<Byte> _panID = new LeafField<byte>(2);

        /// <summary>
        /// 网络号 PanID
        /// </summary>
        public ushort PanID
        {
            get
            {
                return (ushort)(_panID.Value[0] + (_panID.Value[1] << 8));
            }
            set
            {
                _panID.Value[0] = (byte)(value & 0xff);
                _panID.Value[1] = (byte)(value >> 8);
            }
        }

        private NetAddr _targetAddr = new NetAddr();

        /// <summary>
        /// 目标地址
        /// </summary>
        public NetAddr TargetAddr
        {
            get { return _targetAddr; }
            set { _targetAddr = value; }
        }

        private NetAddr _sourceAddr = new NetAddr();

        /// <summary>
        /// 源地址
        /// </summary>
        public NetAddr SourceAddr
        {
            get { return _sourceAddr; }
            set { _sourceAddr = value; }
        }

        private MacExtension _extension = null;

        /// <summary>
        /// 扩展信息域
        /// </summary>
        public MacExtension Extension
        {
            get { return _extension; }
            set { _extension = value; }
        }

        private Field<Byte> _du = null;

        /// <summary>
        /// Mac层 载荷
        /// 如果Mac帧为信标帧 那么载荷为MacBeacon
        /// 如果Mac帧为数据帧 那么载荷为NetFrame
        /// 如果Mac帧为确认帧 那么载荷为null
        /// 如果Mac帧为命令帧 那么载荷为MacCmd
        /// </summary>
        public Field<Byte> Du
        {
            get { return _du; }
            set { _du = value; }
        }

        public MacBeacon GetDuAsMacBeacon()
        {
            return Du as MacBeacon;
        }

        public NetFrame GetDuAsNetFrame()
        {
            return Du as NetFrame;
        }

        public MacCmd GetDuAsMacCmd()
        {
            return Du as MacCmd;
        }


        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(Ctrl);
            if(Ctrl.HasSeq) 
            {
                Children.Add(Seq);
            }
            if (Ctrl.HasPanID)
            {
                Children.Add(_panID);
            }
            Children.Add(TargetAddr);
            Children.Add(SourceAddr);
            if (Ctrl.HasExtension)
            {
                Children.Add(Extension);
            }
            Children.Add(Du);
        }

        public override void SetData(byte[] data, int startIndex, int len)
        {
            CheckData(data, startIndex, len);
            int curPos = startIndex;
            Ctrl.SetData(data, curPos, 2);
            curPos += 2;

            if (Ctrl.HasSeq)
            {
                Seq.SetData(data, curPos, 1);
                curPos += 1;
            }

            if (Ctrl.HasPanID)
            {
                _panID.SetData(data, curPos, 2);
                curPos += 2;
            }

            int targetAddrLen = (Ctrl.TargetAddrMode == EmAddrLen.Two ? 2 : 6);
            TargetAddr.SetData(data, curPos, targetAddrLen);
            curPos += targetAddrLen;

            int sourceAddrLen = (Ctrl.SourceAddrMode == EmAddrLen.Two ? 2 : 6);
            SourceAddr.SetData(data, curPos, sourceAddrLen);
            curPos += sourceAddrLen;

            if (Ctrl.HasExtension)
            {
                int extLen = data[curPos];
                Extension = new MacExtension();
                Extension.SetData(data, curPos, extLen + 1);
                curPos += extLen + 1;
            }

            int duLen = startIndex + len - curPos;
            switch (Ctrl.FrameType)
            {
                case EmMacFrameType.Beacon:
                    Du = new MacBeacon();
                    Du.SetData(data, curPos, duLen);
                    break;
                case EmMacFrameType.Data:
                    Du = new NetFrame();
                    Du.SetData(data, curPos, duLen);
                    break;
                case EmMacFrameType.Ack:
                    Du = null;
                    break;
                case EmMacFrameType.Cmd:
                    Du = new MacCmd();
                    Du.SetData(data, curPos, duLen);
                    break;
            }
        }




    }
}
