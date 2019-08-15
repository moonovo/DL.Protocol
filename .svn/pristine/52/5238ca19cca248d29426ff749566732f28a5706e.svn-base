using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Friendcom.Protocol.MicroPower;
using Friendcom.Protocol.MicroPower.Enums;

namespace Friendcom.Protocol.Sample
{
    [TestClass]
    public class MacFrameTest
    {
        [TestMethod]
        public void MacFrameGetDataSample()
        {
            MacFrame frame = new MacFrame();
            frame.Ctrl.FrameType = EmMacFrameType.Beacon;
            frame.Ctrl.HasExtension = false;
            frame.Ctrl.TargetAddrMode = EmAddrLen.Six;
            frame.Ctrl.SourceAddrMode = EmAddrLen.Six;

            frame.Seq.Value = 0;
            frame.PanID = 0xffff;
            frame.TargetAddr.Value = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
            frame.SourceAddr.Value = new byte[] { 0x11, 0x11, 0x11, 0x11, 0x11, 0x12 };

            MacBeacon mb = new MacBeacon();
            mb.SendRandomTimeDelay = 97;
            mb.BeaconRounds = 1;
            mb.TSNo = 14;
            mb.LevelNo = 1;
            mb.BeaconID = 0xE1;
            mb.NetworkSize = 26;
            mb.StrengthLimit = 96;
            mb.PanID = 0xffff;
            mb.PanIDAddr = new byte[] { 0x88, 0x88, 0x88, 0x88, 0x88, 0x88 };
            frame.Du = mb;

            byte[] data = frame.GetData();
            string str = String.Join(" ", data.Select(o => o.ToString("X2")));

        }

        [TestMethod]
        public void MacFrameGetDataSample1()
        {
            MacFrame frame = new MacFrame();
            frame.Ctrl.FrameType = EmMacFrameType.Data;
            frame.Ctrl.HasExtension = true;
            frame.Ctrl.TargetAddrMode = EmAddrLen.Six;
            frame.Ctrl.SourceAddrMode = EmAddrLen.Six;

            frame.Seq.Value = 110;
            frame.PanID = 0x6771;
            frame.TargetAddr.Value = new byte[] { 0x11, 0x11, 0x11, 0x11, 0x11, 0x22 };
            frame.SourceAddr.Value = new byte[] { 0x88, 0x88, 0x88, 0x88, 0x88, 0x88 };

            frame.Extension = new MacExtension();
            frame.Extension.ExtLength = 10;
            frame.Extension.ExtContent.Value = Tools.HexStrToByteArr("46 43 20 00 07 00 F4 03 4D B9", ' ');

            NetFrame nf = new NetFrame();
            nf.Ctrl.FrameType = EmNetFrameType.Data;
            nf.Ctrl.RoutingInstruction = 1;
            nf.Ctrl.TargetAddrMode = EmAddrLen.Six;
            nf.Ctrl.SourceAddrMode = EmAddrLen.Six;
            nf.TargetAddr.Value = new byte[] { 0x11, 0x11, 0x11, 0x11, 0x11, 0x11 };
            nf.SourceAddr.Value = new byte[] { 0x88, 0x88, 0x88, 0x88, 0x88, 0x88 };
            nf.Seq.Value = 13;
            nf.Radius = 2;
            nf.RouteInfo.Add(new NetAddr() { AddrMode = EmAddrLen.Six, Value = new byte[] { 0x11, 0x11, 0x11, 0x11, 0x11, 0x22 } });
            frame.Du = nf;

            ApsFrame af = new ApsFrame();
            af.Fcd.Ftd = EmApsFcdFtd.Forward;
            af.Fcd.Oei = 0;
            af.Seq.Value = 2;
            af.Dui = (byte)EmApsFcdFtdDti.Auto;
            af.Du.Value = Tools.HexStrToByteArr("FE FE FE FE 68 11 11 11 11 11 11 68 11 04 34 34 33 37 1D 16", ' ');
            nf.Du = af;

            byte[] data = frame.GetData();
        }

        [TestMethod]
        public void MacFrameSetDataSample()
        {
            MacFrame frame = new MacFrame();
            byte[] data = Tools.HexStrToByteArr("40 CD 00 FF FF FF FF FF FF FF FF 12 11 11 11 11 11 61 01 0E 04 E1 1A 00 60 FF FF 88 88 88 88 88 88", ' ');
            frame.SetData(data);

            MacFrame frame1 = new MacFrame();
            byte[] data1 = Tools.HexStrToByteArr("41 CF 6E 71 67 22 11 11 11 11 11 88 88 88 88 88 88 0A 46 43 20 00 07 00 F4 03 4D B9 BC 11 11 11 11 11 11 88 88 88 88 88 88 D2 21 FC FF 22 11 11 11 11 11 02 00 00 FE FE FE FE 68 11 11 11 11 11 11 68 11 04 34 34 33 37 1D 16", ' ');
            frame1.SetData(data1);
        }
    }
}
