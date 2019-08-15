using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol
{
    /// <summary>
    /// 只读的值类型域
    /// </summary>
    public class ReadonlyPrimitiveField<T> : PrimitiveField<T> where T : struct
    {
        public ReadonlyPrimitiveField(T value) : base(value) { }

        /// <summary>
        /// 只读的值类型域不能设置数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="len"></param>
        public override void SetData(T[] data, int startIndex, int len)
        {
            CheckData(data, startIndex, len);
            // 如果设置的数据不相同 那么抛出异常 如果设置的数据相同 那么忽略
            if (!data[startIndex].Equals(this.Value))
            {
                throw new NotSupportedException("只读的值类型域不能设置数据");
            }
            
        }

        public override T Value
        {
            get
            {
                return base.Value;
            }
        }
        

        public static implicit operator T(ReadonlyPrimitiveField<T> bf)
        {
            return bf.Value;
        }

        public static implicit operator ReadonlyPrimitiveField<T>(T b)
        {
            return new ReadonlyPrimitiveField<T>(b);
        }


    }
}
