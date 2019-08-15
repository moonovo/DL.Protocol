using Friendcom.Protocol.MicroPower.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol.MicroPower
{
    /// <summary>
    /// 网络层 路由信息域
    /// </summary>
    public class NetRouteInfo : LeafField<Byte>
    {
        public NetRouteInfo()
            : base(3, false)
        {
            RouteAddrsMode = 0x3FFF;
        }

        /// <summary>
        /// 中继节点数
        /// </summary>
        public byte RouteNodeNum
        {
            get
            {
                return (byte)InsideData.GetBits(0, 5);
            }
            set
            {
                InsideData = InsideData.SetBits(0, 5, value);
            }
        }

        /// <summary>
        /// 中继索引
        /// </summary>
        public byte RouteIndex
        {
            get
            {
                return (byte)InsideData.GetBits(5, 5);
            }
            set
            {
                InsideData = InsideData.SetBits(5, 5, value);
            }
        }

        /// <summary>
        /// 中继列表地址模式
        /// </summary>
        public short RouteAddrsMode
        {
            get
            {
                return (short)InsideData.GetBits(10, 14);
            }
            set
            {
                InsideData = InsideData.SetBits(10, 14, value);
                UpdateAddrList();
            }
        }

        public byte[] Value
        {
            get
            {
                return InsideData;
            }
            set
            {
                InsideData = value;
                UpdateAddrList();
            }
        }



        /// <summary>
        /// 中继列表地址模式变动后 更新中继列表
        /// </summary>
        private void UpdateAddrList()
        {
            RouteAddrList.Clear();
            for (int i = 0; i < RouteNodeNum; i++)
            {
                // 获取第i个中继地址模式
                var emLen = (EmAddrLen)InsideData.GetBits(10 + 2 * i, 2);
                NetAddr addr = new NetAddr();
                addr.AddrMode = emLen;
                RouteAddrList.Add(addr);
            }
        }

        private List<NetAddr> _routeAddrList = new List<NetAddr>();

        /// <summary>
        /// 中继列表
        /// </summary>
        public List<NetAddr> RouteAddrList
        {
            get { return _routeAddrList; }
            set { _routeAddrList = value; }
        }

        /// <summary>
        /// 添加中继地址 自动设置 中继列表地址模式、中继索引、中继节点数
        /// </summary>
        /// <param name="addr"></param>
        public void Add(NetAddr addr)
        {
            if (addr == null)
                throw new ArgumentNullException("addr", "addr不能为空");
            if (RouteNodeNum > 6)
                throw new Exception("中继地址已经超过了6个 不能再添加");
            var mode = addr.AddrMode;

            InsideData.SetBits(10 + 2 * RouteNodeNum, 2, (byte)mode);
            RouteNodeNum++;
            RouteIndex++;
            RouteAddrList.Add(addr);
        }

        public override byte[] GetData()
        {
            List<byte[]> datas = new List<byte[]>();
            datas.Add(InsideData);
            foreach (var addr in RouteAddrList) datas.Add(addr.GetData());

            // 计算总长度
            int totalLength = 0;
            foreach (var data in datas) totalLength += data.Length;

            byte[] retData = new byte[totalLength];
            int curPos = 0;
            foreach (var data in datas)
            {
                Array.Copy(data, 0, retData, curPos, data.Length);
                curPos += data.Length;
            }
            return retData;
        }

        public override void SetData(byte[] data, int startIndex, int len)
        {
            CheckData(data, startIndex, len);
            Array.Copy(data, startIndex, InsideData, 0, 3);
            int addrValueIndex = startIndex + 3;
            // 初始化中继地址列表
            if (RouteAddrList == null)
            {
                RouteAddrList = new List<NetAddr>();
            }
            RouteAddrList.Clear();

            for (int i = 0; i < RouteNodeNum; i++)
            {
                // 获取第i个中继地址模式
                var emLen = (EmAddrLen)InsideData.GetBits(10 + 2 * i, 2);

                NetAddr addr = new NetAddr() { AddrMode = emLen };
                int addrLen = emLen == EmAddrLen.Two ? 2 : 6;
                addr.Value = new byte[addrLen];
                if (addrValueIndex + addrLen > len)
                {
                    throw new ArgumentOutOfRangeException("len", "len的长度不够");
                }
                addr.SetData(data, addrValueIndex, addrLen);
                addrValueIndex += addrLen;
                RouteAddrList.Add(addr);
            }
        }
    }
}
