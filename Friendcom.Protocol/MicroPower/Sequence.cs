using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol.MicroPower
{
    /// <summary>
    /// 微功率无线协议中的序列号 
    ///     当执行++操作时 
    ///         如果当前值 大于等于最大值 那么序列号等于最小值 
    ///         如果当前值+1小于最小值 那么序列号等于最小值
    ///         否则 当前值+1
    /// </summary>
    public class Sequence : PrimitiveField<Byte>
    {
        /// <summary>
        /// 构造一个序列号对象 
        /// </summary>
        /// <param name="value">初始值</param>
        /// <param name="minSeq">最小值</param>
        /// <param name="maxSeq">最大值</param>
        public Sequence(byte value, byte minSeq = 0x00, byte maxSeq = 0xFF)
            : base(value)
        {
            _minSeq = minSeq;
            _maxSeq = maxSeq;
        }

        private byte _maxSeq = 0xFF;

        /// <summary>
        /// 最大值
        /// </summary>
        public byte MaxSeq
        {
          get { return _maxSeq; }
        }

        private byte _minSeq = 0x00;

        /// <summary>
        /// 最小值
        /// </summary>
        public byte MinSeq
        {
            get { return _minSeq; }
        }

        public static implicit operator byte(Sequence bf)
        {
            return bf.Value;
        }

        /// <summary>
        /// 重载++符号
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        public static Sequence operator ++(Sequence seq)
        {
            if(seq.Value >= seq.MaxSeq) 
            {
                seq.Value = seq.MinSeq;
            }
            else if (seq.Value + 1 < seq.MinSeq)
            {
                seq.Value = seq.MinSeq;
            }
            else
            {
                seq.Value++;
            }
            return seq;
        }
    }
}
