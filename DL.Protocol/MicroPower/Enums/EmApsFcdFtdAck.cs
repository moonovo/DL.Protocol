using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.MicroPower.Enums
{
    /// <summary>
    /// APS中FCD中FTD中确认/否认标识枚举
    /// </summary>
    public enum EmApsFcdFtdAck
    {
        /// <summary>
        /// 否认
        /// </summary>
        Deny = 0,

        /// <summary>
        /// 确认
        /// </summary>
        Confirm = 1
    }
}
