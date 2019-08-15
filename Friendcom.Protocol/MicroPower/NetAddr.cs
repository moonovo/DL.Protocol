using Friendcom.Protocol.MicroPower.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol.MicroPower
{
    /// <summary>
    /// 网络层中的地址
    /// </summary>
    public class NetAddr : ReverseLeafField<Byte>
    {
        public NetAddr() : 
            base(0, false)
        {
        }

        /// <summary>
        /// 地址模式
        /// </summary>
        public EmAddrLen AddrMode
        {
            get;
            set;
        }
    }
}
