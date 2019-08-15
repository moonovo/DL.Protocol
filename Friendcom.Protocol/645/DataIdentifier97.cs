using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645
{
    /// <summary>
    /// 645_97协议的数据标识
    /// </summary>
    public class DataIdentifier97 : ReverseLeafField<byte>
    {
        public DataIdentifier97() : base(2) { }

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

        
    }
}
