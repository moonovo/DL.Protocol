using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.MicroPower.Enums
{
    /// <summary>
    /// Mac层 帧类型
    /// </summary>
    public enum EmMacFrameType
    {
        /// <summary>
        /// 信标帧
        /// </summary>
        Beacon = 0,

        /// <summary>
        /// 数据帧
        /// </summary>
        Data = 1,

        /// <summary>
        /// 确认帧
        /// </summary>
        Ack = 2,

        /// <summary>
        /// 命令帧
        /// </summary>
        Cmd = 3

    }
}
