using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.MicroPower
{
    /// <summary>
    /// APS中OED中的扩展域数据区
    /// </summary>
    public class ApsOedDataArea : LeafField<Byte>
    {
        public ApsOedDataArea() : base(0) 
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
