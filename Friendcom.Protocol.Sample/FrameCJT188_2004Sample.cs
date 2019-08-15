using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Friendcom.Protocol.CJT188_2004;

namespace Friendcom.Protocol.Sample
{
    /// <summary>
    /// FrameCJT188_2004Sample 的摘要说明
    /// </summary>
    [TestClass]
    public class FrameCJT188_2004Sample
    {
        public FrameCJT188_2004Sample()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetDataSample()
        {
            FrameCJT188_2004 frame = new FrameCJT188_2004();
            frame.FMeterType = 0x10;
            frame.FAddress.Value = new byte[] { 0x00, 0x20, 0x15, 0x10, 0x27, 0x05, 0x88 };
            frame.FControl.Value = 0x01;
            OtherDataField odf = new OtherDataField();
            odf.FData.Value = new byte[] { 0x90, 0x1F, 0x01 };
            frame.FDataField = odf;
            byte[] data = frame.GetData();
            string str = String.Join(" ", data.Select(o => o.ToString("X2")));
        }

        [TestMethod]
        public void SetDataSample()
        {
            FrameCJT188_2004 frame = new FrameCJT188_2004();
            byte[] data = Tools.HexStrToByteArr("68 10 88 05 27 10 15 20 00 01 03 90 1F 01 25 16", ' ');
            frame.SetData(data);

            byte[] data1 = Tools.HexStrToByteArr("68 10 88 05 27 10 15 20 00 81 16 90 1F 01 00 00 00 00 2C 00 00 00 00 2C 06 20 17 09 01 17 20 00 00 8E 16", ' ');
            frame.SetData(data1);
        }

        [TestMethod]
        public void IsProtocolSample()
        {
            byte[] data = Tools.HexStrToByteArr("68 10 88 05 27 10 15 20 00 01 03 90 1F 01 24 16", ' ');
            byte[] data1 = Tools.HexStrToByteArr("68 10 88 05 27 10 15 20 00 81 16 90 1F 01 00 00 00 00 2C 00 00 00 00 2C 06 20 17 09 01 17 20 00 00 8E 16", ' ');

            Result is0 = FrameCJT188_2004.IsProtocol(data, 0, data.Length);
            Assert.AreEqual(false, is0.Successful);

            bool is1 = FrameCJT188_2004.IsProtocol(data1, 0, data1.Length);
            Assert.AreEqual(true, is1);
        }

        [TestMethod]
        public void ExtractFrameSample()
        {
            byte[] data = Tools.HexStrToByteArr("FE FE FE FE 68 10 88 05 27 10 15 20 00 01 03 90 1F 01 25 16 00 00 00", ' ');
            byte[] frame = FrameCJT188_2004.ExtractFrame(data);
        }
    }
}
