using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol
{
    /// <summary>
    /// 值类型域
    /// </summary>
    public class PrimitiveField<T> : LeafField<T> where T : struct
    {
        public PrimitiveField(T value)
            : base(1)
        {
            InsideData[0] = value;
        }


        public virtual T Value
        {
            get
            {
                return InsideData[0];
            }
            set
            {
                InsideData[0] = value;
            }
        }


        public static implicit operator T(PrimitiveField<T> bf)
        {
            return bf.Value;
        }

        public static implicit operator PrimitiveField<T>(T b)
        {
            return new PrimitiveField<T>(b);
        }
    }
}
