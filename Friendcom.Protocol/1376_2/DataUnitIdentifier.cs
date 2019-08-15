using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol._1376_2
{
    /// <summary>
    /// 数据标识
    /// </summary>
    public class DataUnitIdentifier : LeafField<Byte>
    {
        public DataUnitIdentifier() : base(2) { }

        public byte DT1
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

        public byte DT2
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



        public byte DT
        {
            get
            {
                for (int i = 0; i < 7; i++)
                {
                    if (DT1.GetBit(i) > 0)
                    {
                        return (byte)(DT2 * 8 + i + 1);
                    }
                }
                return 0;
            }
            set
            {
                DT2 = (byte)((value - 1) / 8);
                DT1 = DT1.SetBit((value - 1) % 8, 1);
            }
        }
    }
}
