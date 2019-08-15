using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol._645
{
    /// <summary>
    /// 645_97协议帧
    /// </summary>
    public class Frame645_07 : Frame645
    {
        public Frame645_07() : base(DataFieldsNamespace) { }

        /// <summary>
        /// 数据域命名空间
        /// </summary>
        public const string DataFieldsNamespace = "Friendcom.Protocol._645._07_DataFields";

        /// <summary>
        /// 检查字节数组是否符合645_07协议
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="startIndex">开始位置</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static Result IsProtocol(byte[] data, int startIndex, int len)
        {
            return Frame645.IsProtocolCommon(data, startIndex, len, DataFieldsNamespace);
        }

        /// <summary>
        /// 检查字节数据是否符合645_07协议 
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <returns></returns>
        public static Result IsProtocol(byte[] data)
        {
            return Frame645.IsProtocolCommon(data, DataFieldsNamespace);
        }

        /// <summary>
        /// 从字节数组中提取出符合645_07协议的部分
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ExtractFrame(byte[] data)
        {
            return Frame645.ExtractFrameCommon(data, DataFieldsNamespace);
        }
    }
}
