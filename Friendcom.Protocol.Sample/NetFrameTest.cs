using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Friendcom.Protocol.MicroPower.Enums;
using Friendcom.Protocol.MicroPower;

namespace Friendcom.Protocol.Sample
{
    [TestClass]
    public class NetFrameTest
    {
        [TestMethod]
        public void NetFrameGetDataSample()
        {
            NetFrame frame = new NetFrame();
            frame.Ctrl.RoutingInstruction = 1;
            frame.Ctrl.FrameType = EmNetFrameType.Command;
            frame.Ctrl.TargetAddrMode = EmAddrLen.Six;
            frame.Ctrl.SourceAddrMode = EmAddrLen.Six;

            frame.TargetAddr.Value = new byte[] { 0x11, 0x11, 0x11, 0x11, 0x11, 0x11 };

            frame.SourceAddr.Value = new byte[] { 0x88, 0x88, 0x88, 0x88, 0x88, 0x88 };

            frame.Seq.Value = 13;
            frame.Radius = 1;

            /*
            frame.RouteInfo.Add(new NetAddr() { AddrMode = EmNetAddrLen.Six, Value = new byte[] {0x11, 0x11, 0x11, 0x11, 0x11, 0x22} });
            frame.RouteInfo.RouteIndex = 0;
            */
            // 这里和上面两句有同样的效果
            frame.RouteInfo.SetData(Tools.HexStrToByteArr("01 FC FF 22 11 11 11 11 11", ' '));

            NetCmd netCmd = new NetCmd();
            netCmd.CmdID = EmNetCmdID.ConfigChildNodes;
            netCmd.CmdDu.Value = new byte[] { 0x11, 0x22, 0x33 };
            frame.Du = netCmd;


            byte[] data = frame.GetData();
            frame.ToString();

        }

        [TestMethod]
        public void NetFrameSetDataSample()
        {
            NetFrame frame = new NetFrame();
            byte[] data = Tools.HexStrToByteArr("BD 11 11 11 11 11 11 88 88 88 88 88 88 D1 01 FC FF 22 11 11 11 11 11 12 11 22 33", ' ');
            frame.SetData(data);
        }
    }
}
