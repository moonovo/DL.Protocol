using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645
{
    /// <summary>
    /// 密码域中的密码
    /// </summary>
    public class PWInPassword : ReverseLeafField<Byte>
    {
        public PWInPassword() : base(3) { }

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
