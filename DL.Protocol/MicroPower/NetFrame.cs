using DL.Protocol.MicroPower.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.MicroPower
{
    public class NetFrame : NonLeafField<Byte>
    {
        public NetFrame()
            : base(6)
        {
            IsLengthFixed = false;
        }

        private NetCtrl _ctrl = new NetCtrl();

        /// <summary>
        /// 帧控制域
        /// </summary>
        public NetCtrl Ctrl
        {
            get { return _ctrl; }
        }

        private NetAddr _targetAddr = new NetAddr();

        /// <summary>
        /// 目标地址域 其中AddrMode与帧控制域Ctrl中的TargetAddrMode相等
        /// </summary>
        public NetAddr TargetAddr
        {
            get { return _targetAddr; }
        }

        private NetAddr _sourceAddr = new NetAddr();

        /// <summary>
        /// 源地址域 其中AddrMode与帧控制域Ctrl中的SourceAddrMode相等 
        /// </summary>
        public NetAddr SourceAddr
        {
            get { return _sourceAddr; }
        }

        private Sequence _seq = new Sequence(0, 0x01, 0x0F);

        /// <summary>
        /// 序列号
        /// </summary>
        public Sequence Seq
        {
            get { return _seq; }
            set { _seq = value; }
        }

        private PrimitiveField<Byte> _radius = 0;

        /// <summary>
        /// 半径域
        /// </summary>
        public PrimitiveField<Byte> Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        private NetRouteInfo _routeInfo = new NetRouteInfo();

        /// <summary>
        /// 路由信息域
        /// </summary>
        public NetRouteInfo RouteInfo
        {
            get { return _routeInfo; }
            set { _routeInfo = value; }
        }

        private NonLeafField<Byte> _du = null;

        /// <summary>
        /// 网络层载荷 如果此网络层帧是数据帧 那么网络层载荷为应用层帧ApsFrame；如果此网络层帧是命令帧，那么网络层载荷为网络命令NetCmd
        /// </summary>
        public NonLeafField<Byte> Du
        {
            get { return _du; }
            set { _du = value; }
        }

        /// <summary>
        /// 将网络层载荷当做ApsFrame获取
        /// </summary>
        /// <returns></returns>
        public ApsFrame GetDuAsApsFrame()
        {
            return Du as ApsFrame;
        }

        /// <summary>
        /// 将网络层载荷当做NetCmd获取
        /// </summary>
        /// <returns></returns>
        public NetCmd GetDuAsNetCmd()
        {
            return Du as NetCmd;
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(Ctrl);
            Children.Add(TargetAddr);
            Children.Add(SourceAddr);
            PrimitiveField<Byte> seqAndRadius = 0;
            seqAndRadius += (byte)(Seq.Value << 4);
            seqAndRadius += (byte)(Radius.Value & 0x0F);
            Children.Add(seqAndRadius);
            if (Ctrl.RoutingInstruction == 1)
            {
                Children.Add(RouteInfo);
            }
            Children.Add(Du);
        }

        public override void SetData(byte[] data, int startIndex, int len)
        {
            CheckData(data, startIndex, len);
            Ctrl.SetData(data, startIndex, 1);
            int targetAddrLen = Ctrl.TargetAddrMode == EmAddrLen.Two ? 2 : 6;
            int sourceAddrLen = Ctrl.SourceAddrMode == EmAddrLen.Two ? 2 : 6;
            TargetAddr.AddrMode = Ctrl.TargetAddrMode;
            TargetAddr.SetData(data, startIndex + 1, targetAddrLen);
            SourceAddr.AddrMode = Ctrl.SourceAddrMode;
            SourceAddr.SetData(data, startIndex + 1 + targetAddrLen, sourceAddrLen);

            byte seqAndRadius = data[startIndex + 1 + targetAddrLen + sourceAddrLen];
            Seq.Value = (byte)(seqAndRadius >> 4);
            Radius.Value = (byte)(seqAndRadius & 0x0F);

            int curPos = startIndex + 2 + targetAddrLen + sourceAddrLen;
            if (Ctrl.RoutingInstruction == 1)
            {
                byte[] routeInfoValue = new byte[3];
                Array.Copy(data, curPos, routeInfoValue, 0, 3);
                //这里不能用下面的方式赋值 因为无法更新中继列表
                //Array.Copy(data, curPos, RouteInfo.Value, 0, 3);
                RouteInfo.Value = routeInfoValue;
                curPos += 3;
                if (RouteInfo.RouteNodeNum > 6)
                {
                    throw new Exception("路由信息域 中继节点数不能超过6");
                }
                if (RouteInfo.RouteIndex > 6)
                {
                    throw new Exception("路由信息域 中继索引不能超过6");
                }
                for (int i = 0; i < RouteInfo.RouteNodeNum; i++)
                {
                    NetAddr na = RouteInfo.RouteAddrList[i];
                    if (na.AddrMode == EmAddrLen.Two)
                    {
                        na.SetData(data, curPos, 2);
                        curPos += 2;
                    }
                    else
                    {
                        na.SetData(data, curPos, 6);
                        curPos += 6;
                    }
                }
            }
            if (Ctrl.FrameType == EmNetFrameType.Command)
            {
                Du = new NetCmd();
                Du.SetData(data, curPos, startIndex + len - curPos);
            }
            else if (Ctrl.FrameType == EmNetFrameType.Data)
            {
                Du = new ApsFrame();
                Du.SetData(data, curPos, startIndex + len - curPos);
            }
        }

    }
}
