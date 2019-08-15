using DL.Protocol.MicroPower.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.MicroPower
{
    /// <summary>
    /// Mac层 命令帧载荷域
    /// </summary>
    public class MacCmd : NonLeafField<Byte>
    {
        public MacCmd()
            : base(1)
        {
            IsLengthFixed = false;
        }

        private PrimitiveField<Byte> _cmdID = 0x01;

        /// <summary>
        /// 命令标识
        /// </summary>
        public EmMacCmdID CmdID
        {
            get { return (EmMacCmdID)(_cmdID.Value); }
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
