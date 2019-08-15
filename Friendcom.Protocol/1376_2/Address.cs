using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._1376_2
{
    /// <summary>
    /// 地址
    /// </summary>
    public class Address : LeafField<Byte>
    {
        public Address() : base(6) { }

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
