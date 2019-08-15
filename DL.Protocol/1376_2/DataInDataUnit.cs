using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645
{
    /// <summary>
    /// 数据单元中的数据
    /// </summary>
    public class DataInDataUnit : LeafField<Byte>
    {
        public DataInDataUnit()
            : base()
        {
            IsLengthFixed = false;
        }

        public DataInDataUnit(int length) : base(length) { }


        public byte[] Value
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

    }
}
