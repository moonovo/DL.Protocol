using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.MicroPower
{
    /// <summary>
    /// APS层应用帧
    /// </summary>
    public class ApsFrame : NonLeafField<Byte>
    {
        public ApsFrame()
            : base(3)
        {
            IsLengthFixed = false;
        }

        private ApsFcd _fcd = new ApsFcd();

        /// <summary>
        /// 帧控制域
        /// </summary>
        public ApsFcd Fcd
        {
            get { return _fcd; }
        }

        private Sequence _seq = new Sequence(0, 0x01, 0xFF);

        /// <summary>
        /// 帧序列域
        /// </summary>
        public Sequence Seq
        {
            get { return _seq; }
            set { _seq = value; }
        }

        private ApsOed _oed = new ApsOed();

        /// <summary>
        /// 业务扩展域
        /// </summary>
        public ApsOed Oed
        {
            get { return _oed; }
            set { _oed = value; }
        }

        private PrimitiveField<Byte> _dui = 0;

        /// <summary>
        /// 数据单元标识符
        /// </summary>
        public PrimitiveField<Byte> Dui
        {
            get { return _dui; }
            set { _dui = value; }
        }

        private ApsDu _du = new ApsDu();

        /// <summary>
        /// 数据单元
        /// </summary>
        public ApsDu Du
        {
            get { return _du; }
            set { _du = value; }
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(Fcd);
            Children.Add(Seq);
            if (Fcd.Oei == 1)
            {
                Children.Add(Oed);
            }
            Children.Add(Dui);
            Children.Add(Du);
        }

        public override byte[] GetData()
        {
            return base.GetData();
        }

        public override void SetData(byte[] data, int startIndex, int len)
        {
            CheckData(data, startIndex, len);
            Fcd.SetData(data, startIndex, 1);
            Seq.SetData(data, startIndex + 1, 1);
            int oedLength = 0;
            // 如果OEI==1 那么存在业务扩展域OED
            if (Fcd.Oei == 1)
            {
                oedLength = data[startIndex + 2] + 3;
                Oed.SetData(data, startIndex + 2, oedLength);
            }
            else
            {
                Oed = null;
            }
            Dui.SetData(data, startIndex + 2 + oedLength, 1);
            int duLength = len - 3 - oedLength;
            Du.SetData(data, startIndex + 3 + oedLength, duLength);
        }

    }
}
