using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol.MicroPower
{
    /// <summary>
    /// APS业务扩展域中的厂家标识
    /// </summary>
    public class ApsOedManufacturerID : ReverseLeafField<Byte>
    {
        public ApsOedManufacturerID() : base(2) { }

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
