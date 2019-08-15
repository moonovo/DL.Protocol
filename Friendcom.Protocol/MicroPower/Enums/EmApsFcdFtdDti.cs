using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.MicroPower.Enums
{
    /// <summary>
    /// APS中FCD中FTD中数据转发标识枚举
    /// </summary>
    public enum EmApsFcdFtdDti
    {
        /// <summary>
        /// 自适应
        /// </summary>
        Auto = 0,

        /// <summary>
        /// 1200bps速率转发
        /// </summary>
        Bps1200 = 1,

        /// <summary>
        /// 2400bps速率转发
        /// </summary>
        Bps2400 = 2,

        /// <summary>
        /// 4800bps速率转发
        /// </summary>
        Bps4800 = 3,

        /// <summary>
        /// 9600bps速率转发
        /// </summary>
        Bps9600 = 4,

        /// <summary>
        /// 19200bps速率转发
        /// </summary>
        Bps19200 = 5,

        /// <summary>
        /// 保留
        /// </summary>
        Retain = 6
    }
}
