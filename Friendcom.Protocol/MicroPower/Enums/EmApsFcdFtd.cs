using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol.MicroPower.Enums
{
    public enum EmApsFcdFtd
    {
        /// <summary>
        /// 确认/否认帧"
        /// </summary>
        Ack = 0,

        /// <summary>
        /// 命令帧
        /// </summary>
        Command = 1,

        /// <summary>
        /// 数据转发帧
        /// </summary>
        Forward = 2,

        /// <summary>
        /// 上报帧
        /// </summary>
        Report = 3,

        /// <summary>
        /// 保留
        /// </summary>
        Retain = 4
    }
}
