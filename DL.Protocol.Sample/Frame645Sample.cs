using Microsoft.VisualStudio.TestTools.UnitTesting;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DL.Protocol;
using DL.Protocol._645;
using DL.Protocol._645._07_DataFields;
using DL.Protocol._645._97_DataFields;

namespace DL.Protocol.Sample
{
    /// <summary>
    /// 645_07协议使用样例，645_07帧包含以下域：
    ///   地址域 6个字节
    ///   控制域 1个字节
    ///   长度域 1个字节
    ///   数据域 长度不定
    ///   校验和 1个字节
    ///   
    /// 其中数据域可能包括 数据标识（4字节） 密码（4字节） 操作者代码（4字节） 数据（长度不定） 帧序号（1字节）
    /// 根据控制域的不同 数据域可能包含上述不同的部分
    /// 数据域对于只包含数据的 比如控制码为D1H的，那么可以用OtherDataField类
    ///       对于包含其他部分的 比如控制码为11H的 格式为数据标识+数据 那么用C11_DataFiled类（当控制码为11H时，数据域为C11_DataFiled; 控制码为12H时，数据域为C12_DataField ...）
    ///       当然包含其他部分的也可以使用OtherDataField 但是格式要使用者自己拼接
    ///       
    /// 645_97协议与645_07协议相似 只是数据域中的数据标识为2个字节
    /// </summary>
    [TestClass]
    public class Frame645Sample
    {
        /// <summary>
        /// Frame645_07.IsProtocol 检查字节数组是否符合645_07协议 不检查数据域 只检查长度和数据域是否符合
        /// </summary>
        [TestMethod]
        public void IsProtocolSample()
        {
            byte[] data = Tools.HexStrToByteArr("68 06 05 04 03 02 01 68 11 05 37 36 35 34 DD AE 16");
            bool b = Frame645_07.IsProtocol(data);
            Assert.AreEqual(true, b);
        }

        /// <summary>
        /// 从字节数组中提取符合645_07协议的字节数组
        /// </summary>
        [TestMethod]
        public void ExtractFrameSample()
        {
            byte[] data = Tools.HexStrToByteArr("FE FE FE FE 68 06 05 04 03 02 01 68 11 05 37 36 35 34 DD AE 16 00 00");
            byte[] frame = Frame645_07.ExtractFrame(data);
            bool b= Frame645_07.IsProtocol(frame);
            Assert.AreEqual(0x68, frame[0]);
        }

        /// <summary>
        /// Frame645_07对象的GetData方法 
        /// 先对各个域进行赋值，然后调用GetData方法
        /// 长度和校验和无需赋值，自动计算
        /// </summary>
        [TestMethod]
        public void GetDataSample()
        {
            Frame645_07 frame = new Frame645_07();
            frame.FAddress.Value = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06 };
            frame.FControl.Value = 0x11;


            //C11_DataField dataFiled = new C11_DataField();
            //dataFiled.FDataIdentifer.Value = new byte[] { 0x01, 0x02, 0x03, 0x04 };
            //dataFiled.FData.Value = new byte[] { 0xAA };
            //frame.FDataField = dataFiled;

            // 下面两行 和 上面注释的内容 起到一样的效果
            OtherDataField odf = new OtherDataField();
            odf.FData.Value = new byte[] { 0x04, 0x03, 0x02, 0x01, 0xAA };
            frame.FDataField = odf;

            byte[] data = frame.GetData();
        }

        /// <summary>
        /// Frame645_07对象的GetData方法 
        /// </summary>
        [TestMethod]
        public void GetDataSample1()
        {
            Frame645_07 frame = new Frame645_07();
            frame.FAddress.Value = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06 };
            frame.FControl.Value = 0x19;
            C19_DataField df = new C19_DataField();
            df.FPassword.PA.Value = 0x04;
            df.FPassword.PW.Value = new byte[] { 0x11, 0x22, 0x33 };
            df.FOperatorCode.Value = new byte[] { 0xaa, 0xbb, 0xcc, 0xdd }; 
            frame.FDataField = df;
            
            byte[] data = frame.GetData();
            string str = String.Join(" ", data.Select(o => o.ToString("X2")));
        }

        /// <summary>
        /// Frame645_97对象的GetData方法
        /// </summary>
        [TestMethod]
        public void GetDataSample2()
        {
            Frame645_97 frame = new Frame645_97();
            frame.FAddress.Value = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06 };
            frame.FControl.Value = 0x0F;
            C0F_DataField df = new C0F_DataField();
            df.FPassword.PA.Value = 0x01;
            df.FPassword.PW.Value = new byte[] { 0x11, 0x22, 0x33 };
            df.FNewPassword.PA.Value = 0x02;
            df.FNewPassword.PW.Value = new byte[] { 0x011, 0x22, 0x33 };
            frame.FDataField = df;

            byte[] data = frame.GetData();
            string str = String.Join(" ", data.Select(o => o.ToString("X2")));
        }




        /// <summary>
        /// Frame645_07对象的SetData方法，根据所给的字节数组设置帧中各个域的值
        /// 数据域需要特别说明 当控制域能在DataFields文件夹中找到，比如下面的11H能在DataFields文件夹中找到C11_DataField类
        /// 那么使用C11_DataField解析数据域
        /// 如果找不到那么使用OtherDataField解析数据域
        /// </summary>
        [TestMethod]
        public void SetBytesSample()
        {
            byte[] data = Tools.HexStrToByteArr("68 06 05 04 03 02 01 68 11 05 37 36 35 34 DD AE 16");
            Frame645_07 frame = new Frame645_07();
            frame.SetData(data);

            byte[] data1 = Tools.HexStrToByteArr("68 06 05 04 03 02 01 68 19 08 37 66 55 44 10 FF EE DD 16 16");
            frame.SetData(data1);
        }

        /// <summary>
        /// Frame645_97对象的SetData方法
        /// </summary>
        [TestMethod]
        public void SetDataSample1()
        {
            byte[] data = Tools.HexStrToByteArr("68 06 05 04 03 02 01 68 0F 08 34 66 55 44 35 66 55 44 63 16");
            Frame645_97 frame = new Frame645_97();
            frame.SetData(data);
        }
    }
}
