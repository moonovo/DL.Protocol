using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol
{
    /// <summary>
    /// 叶子域 没有孩子
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LeafField<T> : Field<T>
    {
        public LeafField(bool isLengthFixed = true) : this(0, isLengthFixed) { }

        public LeafField(int length, bool isLengthFixed = true)
            : base(length)
        {
            IsLengthFixed = isLengthFixed;
        }

        public T[] Value
        {
            get
            {
                return InsideData;
            }
            set
            {
                InsideData = value;
            }
        }

        public override T[] GetData()
        {
            return InsideData;
        }

        public override void SetData(T[] data, int startIndex, int len)
        {
            CheckData(data, startIndex, len);
            T[] id = new T[len];
            Array.Copy(data, startIndex, id, 0, len);
            InsideData = id;
        }
    }
}
