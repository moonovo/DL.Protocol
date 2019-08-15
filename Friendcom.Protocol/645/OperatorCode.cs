using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol._645
{
    public class OperatorCode : ReverseLeafField<Byte>
    {
        public OperatorCode() : base(4) { }

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
