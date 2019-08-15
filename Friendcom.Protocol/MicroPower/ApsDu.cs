using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol.MicroPower
{
    /// <summary>
    /// APS中的数据单元
    /// </summary>
    public class ApsDu : LeafField<Byte>
    {
        public ApsDu()
            : base(0)
        {
            IsLengthFixed = false;
        }

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
