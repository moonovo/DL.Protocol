using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol.CJT188_2004
{
    /// <summary>
    /// 地址域
    /// </summary>
    public class AddressField : ReverseLeafField<Byte>
    {
        public AddressField()
            : base(7)
        {

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


        /// <summary>
        /// 广播地址
        /// </summary>
        public static byte[] BroadcastAddress = new byte[] { 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA };

    }
}
