using Friendcom.Protocol.MicroPower.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol.MicroPower
{
    /// <summary>
    /// 网络层命令帧中的网络层载荷
    /// </summary>
    public class NetCmd : NonLeafField<Byte>
    {
        public NetCmd()
            : base(1)
        {
            IsLengthFixed = false;
        }

        private PrimitiveField<Byte> _cmdID = 0;

        /// <summary>
        /// 网络命令标识
        /// </summary>
        public EmNetCmdID CmdID
        {
            get { return (EmNetCmdID)_cmdID.Value; }
            set { _cmdID = (byte)value; }
        }

        private LeafField<Byte> _cmdDu = new LeafField<byte>(false);

        /// <summary>
        /// 网络命令载荷
        /// </summary>
        public LeafField<Byte> CmdDu
        {
            get { return _cmdDu; }
            set { _cmdDu = value; }
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(_cmdID);
            Children.Add(CmdDu);
        }

    }
}
