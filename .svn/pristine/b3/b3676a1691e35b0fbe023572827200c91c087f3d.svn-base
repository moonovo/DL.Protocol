using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol._1376_2
{
    /// <summary>
    /// 长度
    /// </summary>
    public class LengthField : ReverseLeafField<Byte>
    {
        public LengthField()
            : base(2)
        {

        }

        public short Value
        {
            get
            {
                return (short)((InsideData[0] << 8) + InsideData[1]);
            }
            set
            {
                InsideData[1] = (byte)(value % (1 << 8));
                InsideData[0] = (byte)(value / (1 << 8));
            }
        }
    }
}
