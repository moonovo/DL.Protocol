using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol.Formats
{
    /// <summary>
    /// 字节域转换为string的转换器 
    ///     必须为单例模式 
    ///     获取实例的静态方法名必须为GetInstance 参数必须为空
    ///     以后要添加XXX转换成string的转换器 类型必须为XXXFormat 如果XXX为基类型 那么要注意对应的System中的类型 例如int对应Int32
    ///     命名空间必须为 Friendcom.Protocol.Formats
    /// </summary>
    public class ByteFormat : IFormat<Byte>
    {
        private static ByteFormat _byteFormat = null;

        private static Object _sync = new object();

        private ByteFormat() { }

        public static ByteFormat GetInstance()
        {
            lock (_sync)
            {
                if (_byteFormat == null)
                {
                    _byteFormat = new ByteFormat();
                }
                return _byteFormat;
            }
        }
        
        private string _outputFormat = "X2";

        /// <summary>
        /// 每个字节的转换格式 默认为X2 即转换为16进制 长度为2
        /// </summary>
        public string OutputFormat
        {
            get { return _outputFormat; }
            set { _outputFormat = value; }
        }

        private string _separator = " ";

        /// <summary>
        /// 分割字符串 默认为空格
        /// </summary>
        public string Separator
        {
            get { return _separator; }
            set { _separator = value; }
        }

        private string _suffix = string.Empty;

        /// <summary>
        /// 前缀 默认为空
        /// </summary>
        public string Suffix
        {
            get { return _suffix; }
            set { _suffix = value; }
        }

        /// <summary>
        /// byte[]转换成string
        /// </summary>
        public string GetString(object ower, Byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            if (data != null)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (i != 0)
                    {
                        sb.Append(Separator);
                    }
                    sb.Append(Suffix);
                    sb.Append(data[i].ToString(OutputFormat));
                }
            }
            sb.Append("(" + ower.GetType() + ")");
            return sb.ToString();
        }
        
    }
}
