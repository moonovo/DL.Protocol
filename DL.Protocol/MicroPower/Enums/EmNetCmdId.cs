using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.MicroPower.Enums
{
    public enum EmNetCmdID
    {
        /// <summary>
        /// 入网申请请求
        /// </summary>
        NetApplicationRequest = 0x01,

        /// <summary>
        /// 入网申请响应
        /// </summary>
        NetApplicationResponse = 0x02,

        /// <summary>
        /// 路由错误
        /// </summary>
        RoutingErrors = 0x03,

        /// <summary>
        /// 场强收集命令
        /// </summary>
        FieldStrengthCollectionCmd = 0x10,

        /// <summary>
        /// 场强收集应答
        /// </summary>
        FieldStrengthCollectionResponse = 0x11,

        /// <summary>
        /// 配置子节点
        /// </summary>
        ConfigChildNodes = 0x12,

        /// <summary>
        /// 配置子节点应答
        /// </summary>
        ConfigChildNodesResponse = 0x13,

        /// <summary>
        /// 游离节点就绪
        /// </summary>
        FreeNodeReady = 0x16,
    }
}
