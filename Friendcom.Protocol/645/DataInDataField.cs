using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645
{
    /// <summary>
    /// 数据域中的数据
    /// </summary>
    public class DataInDataField : LeafField<Byte>
    {
        public DataInDataField()
            : base()
        {
            IsLengthFixed = false;
        }

        public DataInDataField(int length) : base(length) { }


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
