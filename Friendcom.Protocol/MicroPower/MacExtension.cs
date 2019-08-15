using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol.MicroPower
{
    /// <summary>
    /// Mac层 扩展信息域
    /// </summary>
    public class MacExtension : NonLeafField<Byte>
    {
        public MacExtension()
            : base(1)
        {
            IsLengthFixed = false;
        }

        /// <summary>
        /// 扩展信息域长度
        /// </summary>
        private PrimitiveField<Byte> _extLength = 0;

        public PrimitiveField<Byte> ExtLength
        {
            get { return _extLength; }
            set { _extLength = value; }
        }

        private LeafField<Byte> _extContent = new LeafField<byte>(0, false);

        /// <summary>
        /// 扩展信息域内容 包括厂家标识（2字节）、帧头扩展信息域、载荷扩展信息域
        /// </summary>
        public LeafField<Byte> ExtContent
        {
            get { return _extContent; }
            set { _extContent = value; }
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(ExtLength);
            Children.Add(ExtContent);
        }

        /// <summary>
        /// 获取扩展信息域数据 会重新计算ExtLength
        /// </summary>
        /// <returns></returns>
        public override byte[] GetData()
        {
            byte[] data = base.GetData();
            data[0] = (byte)(data.Length - 1);
            ExtLength = data[0];
            return data;
        }


    }
}
