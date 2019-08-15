using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645
{
    /// <summary>
    /// 地址域
    /// </summary>
    public class AddressField : ReverseLeafField<Byte>
    {
        public AddressField()
            : base(6)
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
        /// 通配地址
        /// </summary>
        public static byte[] WildcardAddress = new byte[] { 0xAA, 0xAA, 0xAA, 0xAA, 0xAA, 0xAA };

        /// <summary>
        /// 广播地址
        /// </summary>
        public static byte[] BroadcastAddress = new byte[] { 0x99, 0x99, 0x99, 0x99, 0x99, 0x99 };
       
    }
}
