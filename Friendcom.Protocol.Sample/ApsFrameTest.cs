using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Friendcom.Protocol.MicroPower;
using Friendcom.Protocol.MicroPower.Enums;
using Friendcom.Protocol.Formats;

namespace Friendcom.Protocol.Sample
{
    [TestClass]
    public class ApsFrameTest
    {
        [TestMethod]
        public void ApsFrameGetDataSample()
        {
            ApsFrame frame = new ApsFrame();
            frame.Fcd.Ftd = EmApsFcdFtd.Forward;
            frame.Fcd.Oei = 0;
            frame.Seq++;
            frame.Dui = (byte)EmApsFcdFtdDti.Auto;
            frame.Du.Value = Tools.HexStrToByteArr("FE FE FE FE 68 11 11 11 11 11 11 68 11 04 34 34 33 37 1D 16", ' ');
            byte[] data = frame.GetData();
            Assert.AreEqual(0x02, data[0]);

            frame.Seq.Value = 0xFF;
            frame.Seq++;

            frame.Fcd.Oei = 1;
            frame.Oed.DataArea.Value = new byte[] {1,2,3};
            frame.Oed.DataAreaLength = 3;
            frame.Oed.ManufacturerID.Value = new byte[] { 00, 01 };
            data = frame.GetData();
        }

        [TestMethod]
        public void ApsFrameSetDataSample()
        {
            Type type = typeof(ByteFormat);
            dynamic d = type;
            ApsFrame frame = new ApsFrame();
            byte[] data = Tools.HexStrToByteArr("02 00 00 FE FE FE FE 68 11 11 11 11 11 11 68 11 04 34 34 33 37 1D 16", ' ');
            frame.SetData(data);
            string a = frame.ToString();
            ByteFormat bf = ByteFormat.GetInstance();
            bf.Suffix = "0x";
            Assert.AreEqual(EmApsFcdFtd.Forward, frame.Fcd.Ftd);


            data = Tools.HexStrToByteArr("AA 3E 00 1C 41 CD 05 FF FF AF AF AF AF AF AF 01 32 10 42 01 01 3C AF AF AF AF AF AF 01 32 10 42 01 01 31 01 03 04 91 31 09 10 14 00 03 00 00 00 00 00 00 53 00 21 00 00 00 60 00 00 00 05 00 00 EE 01 17");
            ApsFrame frame1 = new ApsFrame();
            frame1.SetData(data);

        }
    }
}
