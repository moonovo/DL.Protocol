using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol._645
{
    /// <summary>
    /// 密码域中的权限
    /// </summary>
    public class PAInPassword : LeafField<Byte>
    {
        public PAInPassword() : base(1) { }

        public byte Value
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
    }
}
