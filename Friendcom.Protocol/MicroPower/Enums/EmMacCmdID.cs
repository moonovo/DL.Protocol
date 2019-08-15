using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol.MicroPower.Enums
{
    /// <summary>
    /// Mac层命令标识枚举
    /// </summary>
    public enum EmMacCmdID
    {
        /// <summary>
        /// 网络维护请求
        /// </summary>
        NWMaintainRequest = 0x01,

        /// <summary>
        /// 网络维护响应
        /// </summary>
        NWMaintainResponse = 0x02
    }
}
