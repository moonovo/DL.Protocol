using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Friendcom.Protocol;
using Friendcom.Protocol._645;
using Friendcom.Protocol._645._07_DataFields;

namespace Friendcom.Protocol.Sample
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void _645Password()
        {
            Password p = new Password();
            p.PA.Value = 0x04;
            p.PW.Value = new byte[] { 0x01, 0x02, 0x03 };
            var data = p.GetData();

            Password p1 = new Password();
            p1.SetData(data);

            Assert.AreEqual(0x04, p1.PA.Value);
            Assert.AreEqual(0x01, p1.PW.Value[0]);
            Assert.AreEqual(0x02, p1.PW.Value[1]);
            Assert.AreEqual(0x03, p1.PW.Value[2]);
        }

        [TestMethod]
        public void _64507C11DataField()
        {
            C11_DataField df = new C11_DataField();
            byte[] diData = new byte[] { 0x01, 0x02, 0x03, 0x04 };
            byte[] ddData = new byte[] {0x05, 0x06};
            df.FDataIdentifer.Value = diData;
            df.FData.Value = ddData;
            byte[] d = df.GetData();

            C11_DataField df1 = new C11_DataField();
            df1.SetData(d);

            Assert.AreEqual(0x01, df1.FDataIdentifer.DI0);
            Assert.AreEqual(0x02, df1.FDataIdentifer.DI1);
            Assert.AreEqual(0x03, df1.FDataIdentifer.DI2);
            Assert.AreEqual(0x04, df1.FDataIdentifer.DI3);

            Assert.AreEqual(0x05, df1.FData.Value[0]);
            Assert.AreEqual(0x06, df1.FData.Value[1]);
            
        }

        [TestMethod]
        public void Frame645()
        {
            Frame645_07 frame = new Frame645_07();
            byte[] data = Tools.HexStrToByteArr("68 06 05 04 03 02 01 68 11 05 37 36 35 34 DD AE 16");
            frame.SetData(data);

        }
    }
}
