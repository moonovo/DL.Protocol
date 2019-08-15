using Microsoft.VisualStudio.TestTools.UnitTesting;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DL.Protocol._1376_2;

namespace DL.Protocol.Sample
{
    /// <summary>
    /// Frame1376_2Sample 的摘要说明
    /// </summary>
    [TestClass]
    public class Frame1376_2Sample
    {
        public Frame1376_2Sample()
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
        public void TestMethod1()
        {
            //
            // TODO:  在此处添加测试逻辑
            //
        }


        [TestMethod]
        public void GetDataSample()
        {
            Frame1376_2Down frame = new Frame1376_2Down();
            frame.FControl.DIR = 0;
            frame.FControl.PRM = 1;
            frame.FControl.CommunicationMode = 0x0a;

            frame.FInformation.CommModuleIdentifier = 1;
            frame.FInformation.RelayLevel = 1;
            frame.FInformation.MessageSequenceNumber = 10;

            // 地址域是可变的 所以需要先new一个AddressField 规则为 
            //   如果信息域中的通信模块标识为0 那么无地址域
            //   如果通信模块表示为1 如果是下行 那么长度为 6 + 6*FrameInformationDownField.RelayLevel + 6
            //                       如果是上行 那么长度为 6 + 6
            // 当GetBytes时，如果地址域长度不符合上面的规则 那么将抛出异常
            frame.FAddress.A1.Value = new byte[] { 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
            frame.FAddress.A2.Add(new Address() { Value = new byte[] { 0x21, 0x22, 0x23, 0x24, 0x25, 0x26 } });
            frame.FAddress.A3.Value = new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 }; 


            frame.AFN = 10;
            frame.FDataUnitIdentifier.DT = 1;

            /*
            // 数据单元是可变的 对于数据单元的赋值 
            //   可以直接将 字节数据赋值给FrameDataUnit.Data 
            //   也可以创建一个新类如下AFN02_F1_DataUnit 继承数据单元DataUnit, 然后new出该类的对象 将其赋值给FrameDataUnit
            AFN02_F1_DataUnit du = new AFN02_F1_DataUnit(3);
            du.ProtocolType = 0;
            du.MessageLength = 1;
            du.Message = new byte[] { 0x01 };
            frame.FrameDataUnit = du;

            // ...
            byte[] data = frame.GetBytes();
            Assert.AreEqual(0x68, data[0]);
            */

            OtherDataUnit odu = new OtherDataUnit();
            odu.FData.Value = new byte[] { 0x00, 0x01, 0x01 };
            frame.FDataUnit = odu;

            byte[] data = frame.GetData();


            string str = String.Join(" ", data.Select(o => o.ToString("X2")));
        }

        [TestMethod]
        public void SetDataSample()
        {
            byte[] data = Tools.HexStrToByteArr("68 24 00 4A 14 00 00 00 00 0A 11 12 13 14 15 16 21 22 23 24 25 26 31 32 33 34 35 36 0A 01 00 00 01 01 F4 16", ' ');
            Frame1376_2Down frame = new Frame1376_2Down();
            frame.SetData(data);

            byte[] data1 = Tools.HexStrToByteArr("68 2F 00 4A 04 00 28 00 00 68 10 00 00 00 10 00 30 70 75 21 01 01 13 01 00 02 00 00 10 68 30 70 75 21 01 01 68 11 04 35 34 33 37 ED 16 4F 16", ' ');
            Frame1376_2Down frame1 = new Frame1376_2Down();
            frame1.SetData(data1);


        }

        [TestMethod]
        public void IsProtocolSample()
        {
            byte[] data = Tools.HexStrToByteArr("68 24 00 4A 14 00 00 00 00 0A 11 12 13 14 15 16 21 22 23 24 25 26 31 32 33 34 35 36 0A 01 00 00 01 01 F4 16", ' ');
            Result res = Frame1376_2Down.IsProtocol(data, 0, data.Length);
            Assert.AreEqual(true, res.Successful);
            res = Frame1376_2Up.IsProtocol(data, 0, data.Length);
            Assert.AreEqual(false, res.Successful);

        }

        [TestMethod]
        public void ExtractFrameSample()
        {
            byte[] data = Tools.HexStrToByteArr("FE FE FE FE 68 24 00 4A 14 00 00 00 00 0A 11 12 13 14 15 16 21 22 23 24 25 26 31 32 33 34 35 36 0A 01 00 00 01 01 F4 16 00 00 00", ' ');
            byte[] frameData = Frame1376_2Down.ExtractFrame(data);
            Assert.AreNotEqual(null, frameData);
            Assert.AreEqual(0x68, frameData[0]);

            frameData = Frame1376_2Up.ExtractFrame(data);
            Assert.AreEqual(null, frameData);
        }
    }
}
