using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol
{
    public abstract class ByteNonLeafField : NonLeafField<Byte>
    {
        public ByteNonLeafField() : base() { }

        public ByteNonLeafField(int length) : base(length) { }

        /// <summary>
        /// 转换成string
        /// </summary>
        /// <param name="format">每个字节的转换格式 默认为X2 即转换为16进制</param>
        /// <param name="sep">分割字符串 默认为空格</param>
        /// <param name="suffix">前缀字符串 默认为0x</param>
        /// <returns></returns>
        public override string ToString()
        {
            string format = "X2";
            string sep = " "; 
            string suffix = "0x";
            byte[] data = this.GetData();
            StringBuilder sb = new StringBuilder();
            if (data != null)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (i != 0)
                    {
                        sb.Append(sep);
                    }
                    sb.Append(suffix);
                    sb.Append(data[i].ToString(format));
                }
            }
            return sb.ToString();
        }
    }
}
