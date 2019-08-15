using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol
{
    public class ReverseLeafField<T> : Field<T>
    {
        public ReverseLeafField(bool isLengthFixed = true) : this(0, isLengthFixed) { }

        public ReverseLeafField(int length, bool isLengthFixed = true) : base(length) 
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
            return InsideData.Reverse().ToArray();
        }

        public override void SetData(T[] data, int startIndex, int len)
        {
            CheckData(data, startIndex, len);
            T[] id = new T[len];
            Array.Copy(data, startIndex, id, 0, len);
            InsideData = id.Reverse().ToArray();
        }
    }
}
