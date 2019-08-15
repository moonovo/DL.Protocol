using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._1376_2
{
    /// <summary>
    /// 数据单元
    /// </summary>
    public abstract class DataUnit : NonLeafField<Byte>
    {
        public DataUnit(int length) : base(length) { }
    }
}
