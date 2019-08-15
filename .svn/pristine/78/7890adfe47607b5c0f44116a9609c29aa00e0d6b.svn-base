using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Friendcom.Protocol.MicroPower;

namespace Friendcom.Protocol.Sample
{
    [TestClass]
    public class MacBeaconTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            byte[] data = Tools.HexStrToByteArr("61 01 0E 04 E1 1A 00 60 FF FF 88 88 88 88 88 88", ' ');
            MacBeacon mb = new MacBeacon();
            mb.Value = data;
        }
    }
}
