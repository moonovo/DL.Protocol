using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645
{
    /// <summary>
    /// 数据域
    /// </summary>
    public abstract class DataField : NonLeafField<Byte>
    {
        public DataField(int length) : base(length) { }

        public override byte[] GetData()
        {
            return base.GetData().Add(0x33);
        }

        public override void SetData(byte[] data, int startIndex, int len)
        {
            CheckData(data, startIndex, len);
            byte[] tmpData = new byte[len];
            Array.Copy(data, startIndex, tmpData, 0, len);
            base.SetData(tmpData.Subtract(0x33), 0, len);
        }
    }
}
