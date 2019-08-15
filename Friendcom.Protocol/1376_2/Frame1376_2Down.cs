using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._1376_2
{
    public class Frame1376_2Down : Frame1376_2
    {
        public Frame1376_2Down()
            : base()
        {
            FControl.DIR = 0;
            FAddress.IsDown = true;
        }


        private InformationDownField _fInformation = new InformationDownField();

        /// <summary>
        /// 信息域
        /// </summary>
        public InformationDownField FInformation
        {
            get { return _fInformation; }
            set { _fInformation = value; }
        }


        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(StartByte);
            Children.Add(FLength);
            Children.Add(FControl);
            Children.Add(FInformation);
            Children.Add(FAddress);
            Children.Add(AFN);
            Children.Add(FDataUnitIdentifier);
            Children.Add(FDataUnit);
            Children.Add(FCheckSum);
            Children.Add(EndByte);
        }

        public override void SetData(byte[] data, int startIndex, int len)
        {
            CheckData(data, startIndex, len);
            SetChildren();
            if (Children == null || Children.Count == 0)
                return;

            
            int isNotFixedChild = -1;
            int curPreIndex = startIndex;
            for (int i = 0; i < Children.Count; i++)
            {
                // 如果孩子是地址域 那么需要先对其三个属性赋值
                if (Children[i] is AddressField)
                {
                    FAddress.IsDown = true;
                    FAddress.CommModuleID = FInformation.CommModuleIdentifier;
                    FAddress.RelayLevel = FInformation.RelayLevel;
                }
                if (!Children[i].IsLengthFixed)
                {
                    isNotFixedChild = i;
                    break;
                }
                Children[i].SetData(data, curPreIndex, Children[i].Length);
                curPreIndex += Children[i].Length;
            }
            // 存在不固定长度的子域
            if (isNotFixedChild != -1)
            {
                int curSufIndex = startIndex + len;
                for (int i = Children.Count - 1; i > isNotFixedChild; i--)
                {
                    curSufIndex -= Children[i].Length;
                    Children[i].SetData(data, curSufIndex, Children[i].Length);
                }
                Children[isNotFixedChild].SetData(data, curPreIndex, curSufIndex - curPreIndex);
            }
        }

        public override byte[] GetData()
        {
            FAddress.IsDown = true;
            FAddress.CommModuleID = FInformation.CommModuleIdentifier;
            FAddress.RelayLevel = FInformation.RelayLevel;
            byte[] data = base.GetData();
            FLength.Value = (short)data.Length;
            FCheckSum = data.Sum(3, data.Length - 4);
            Array.Copy(FLength.GetData(), 0, data, 1, 2);
            data[data.Length - 2] = FCheckSum.Value;
            return data;
        }

        public static Result IsProtocol(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            return IsProtocol(data, 0, data.Length);
        }


        public static Result IsProtocol(byte[] data, int startIndex, int len)
        {
            Result common = Frame1376_2.IsProtocolCommon(data, startIndex, len);
            if (common == false)
            {
                return common;
            }
            ControlField cf = new ControlField();
            cf.SetData(data, startIndex + 3, cf.Length);
            if (cf.DIR != 0)
            {
                return (Result)"此帧不是下行帧";
            }
            return true;
        }

        /// <summary>
        /// 从字节数组中提取帧
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ExtractFrame(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            byte[] ret;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == StartByte)
                {
                    LengthField lf = new LengthField();
                    lf.SetData(data, i + 1, lf.Length);
                    if (data[i + lf.Value - 1] == EndByte)
                    {
                        if (IsProtocol(data, i, lf.Value))
                        {
                            ret = new byte[lf.Value];
                            Array.Copy(data, i, ret, 0, lf.Value);
                            return ret;
                        }
                    }

                }
            }
            return null;
        }
    }
}
