using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.MicroPower.Enums
{
    /// <summary>
    /// 网络层 帧控制域 帧类型枚举
    /// </summary>
    public enum EmNetFrameType
    {
        /// <summary>
        /// 数据
        /// </summary>
        Data = 0,

        /// <summary>
        /// 网络命令
        /// </summary>
        Command = 1,

        /// <summary>
        /// 保留
        /// </summary>
        Retain = 2
    }
}
