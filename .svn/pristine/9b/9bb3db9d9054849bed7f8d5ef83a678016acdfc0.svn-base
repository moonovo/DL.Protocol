using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol._645
{
    /// <summary>
    /// 645_07协议的数据标识
    /// </summary>
    public class DataIdentifier07 : ReverseLeafField<byte>
    {
        public DataIdentifier07() : base(4) { }

        public byte DI0
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

        public byte DI1
        {
            get
            {
                return InsideData[1];
            }
            set
            {
                InsideData[1] = value;
            }
        }

        public byte DI2
        {
            get
            {
                return InsideData[2];
            }
            set
            {
                InsideData[2] = value;
            }
        }

        public byte DI3
        {
            get
            {
                return InsideData[3];
            }
            set
            {
                InsideData[3] = value;
            }
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
