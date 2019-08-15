using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Friendcom.Protocol
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Tools
    {

        /// <summary>
        /// 获取字节数组中 从pos位置开始 长度为length的字节 将其作为Int32返回
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pos"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Int32 GetBits(this byte[] data, int pos, int length)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            if (pos >= data.Length * 8 || pos < 0)
                throw new ArgumentOutOfRangeException("pos", "字节数组无" + pos.ToString() + "位");
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", "长度不能为负");
            if (pos + length > data.Length * 8)
                throw new ArgumentOutOfRangeException("data", "字节数组无" + (pos + length).ToString() + "位");

            Int32 ret = 0;
            for (int i = 0; i < length; i++)
            {
                if (GetBit(data, pos + i) > 0)
                {
                    ret += (1 << i);
                }
            }
            return ret;
        }

        public static byte GetBits(this byte data, int pos, int length)
        {
            return (byte)GetBits(new byte[] { data }, pos, length);
        }

        /// <summary>
        /// 获取字节数组中 第pos位的数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static byte GetBit(this byte[] data, int pos)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            if (pos >= data.Length * 8 || pos < 0)
                throw new ArgumentOutOfRangeException("pos", "字节数组无" + pos.ToString() + "位");
            return GetBit(data[pos / 8], pos % 8);
        }

        /// <summary>
        /// 获取字节中 第pos位的数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static byte GetBit(this byte data, int pos)
        {
            if (pos >= 8 || pos < 0)
                throw new ArgumentOutOfRangeException("pos", "字节数组无" + pos.ToString() + "位");
            return ((1 << pos) & data) > 0 ? (byte)1 : (byte)0;
        }

        /// <summary>
        /// 设置字节数组data 从第pos位开始 长度为length的部分 设置为val
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="pos">从此位开始</param>
        /// <param name="length">长度</param>
        /// <param name="val">值</param>
        /// <returns></returns>
        public static byte[] SetBits(this byte[] data, int pos, int length, Int32 val)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            if (pos >= data.Length * 8 || pos < 0)
                throw new ArgumentOutOfRangeException("pos", "字节数组无" + pos.ToString() + "位");
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", "长度不能为负");
            if (pos + length > data.Length * 8)
                throw new ArgumentOutOfRangeException("data", "字节数组无" + (pos + length).ToString() + "位");

            for (int i = 0; i < length; i++)
            {
                byte bitVal = (val & (1 << i)) > 0 ? (byte)1 : (byte)0;
                data = data.SetBit(pos + i, bitVal);
            }
            return data;
        }

        public static byte SetBits(this byte data, int pos, int length, Int32 val)
        {
            var ret = SetBits(new byte[] { data }, pos, length, val);
            return ret[0];
        }

        /// <summary>
        /// 设置字节数组data的 第pos位为val
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="pos">某一位</param>
        /// <param name="val">值 只能为0或1</param>
        /// <returns></returns>
        public static byte[] SetBit(this byte[] data, int pos, byte val)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            if (pos >= data.Length * 8 || pos < 0)
                throw new ArgumentOutOfRangeException("pos", "字节数组无" + pos.ToString() + "位");
            if (val != 1 && val != 0)
                throw new ArgumentOutOfRangeException("val", "val只能为0或1");
            data[pos / 8] = data[pos / 8].SetBit(pos % 8, val);
            return data;
        }

        /// <summary>
        /// 设置字节data的pos为val
        /// </summary>
        /// <param name="data">字节</param>
        /// <param name="pos">某一位</param>
        /// <param name="val">值 只能为0或1</param>
        /// <returns></returns>
        public static byte SetBit(this byte data, int pos, byte val)
        {
            if (pos >= 8 || pos < 0)
                throw new ArgumentOutOfRangeException("pos", "字节数组无" + pos.ToString() + "位");
            if (val != 1 && val != 0)
                throw new ArgumentOutOfRangeException("val", "val只能为0或1");
            if (val == 1)
                return (byte)(data | (1 << pos));
            else
                return (byte)(data & (0xFF - (1 << pos)));
        }

        public static byte Sum(this byte[] data, int startIndex, int len)
        {
            if (data == null)
                throw new ArgumentNullException("data", "data不能为空");
            if (data.Length < startIndex)
                throw new ArgumentOutOfRangeException("startIndex", "startIndex超出范围");
            byte ret = 0;
            for (int i = 0; i < len && i < data.Length; i++)
            {
                ret += data[startIndex + i];
            }
            return ret;
        }

        public static byte[] HexStrToByteArr(string hexStr, params char[] sep)
        {
            string[] byteStrArr = hexStr.Split(sep);
            byte[] byteArr = new byte[byteStrArr.Length];
            for (int i = 0; i < byteStrArr.Length; i++)
            {
                byteArr[i] = Tools.HexStrToByte(byteStrArr[i]);
            }
            return byteArr;
        }

        /// <summary>
        /// 重新定义大小
        /// </summary>
        /// <param name="data"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] Resize(this byte[] data, int size)
        {
            byte[] newData = new byte[size];
            Array.Copy(data, newData, Math.Min(data.Length, newData.Length));
            return newData;
        }


        public static byte[] Add(this byte[] data, byte val)
        {
            if (data == null)
                return null;
            for (int i = 0; i < data.Length; i++)
            {
                data[i] += val;
            }
            return data;
        }

        public static byte[] Subtract(this byte[] data, byte val)
        {
            if (data == null)
                return null;
            for (int i = 0; i < data.Length; i++)
            {
                data[i] -= val;
            }
            return data;
        }


        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string assemblyName, string nameSpace, string className)
        {
            try
            {
                string fullName = nameSpace + "." + className;//命名空间.类型名
                string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
                Type o = Type.GetType(path);//加载类型
                object obj = Activator.CreateInstance(o);//根据类型创建实例
                return (T)obj;//类型转换并返回
            }
            catch
            {
                //发生异常，返回类型的默认值
                return default(T);
            }
        }

        /// <summary>
        /// 调用静态方法
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="staticMethodName">静态方法名</param>
        /// <param name="parameters">静态方法参数</param>
        /// <returns></returns>
        public static T InvokeStaticMethod<T>(Type type, string staticMethodName, object[] parameters)
        {
            try
            {
                MethodInfo mf = type.GetMethod(staticMethodName);
                T ret = (T)mf.Invoke(null, parameters);
                return ret;
            }
            catch (Exception)
            {
                //发生异常，返回类型的默认值
                return default(T);
            }
        }




        // 字符串转整数
        public static int StrToInt(string str)
        {
            int ret = 0, len = str.Length;
            for (int i = 0; i < len; i++)
            {
                ret = ret * 10 + str[i] - '0';
            }
            return ret;
        }

        // 检查字符串是否是整数
        public static bool IsNum(string str)
        {
            // 去掉空格
            Regex replaceBlackRegex = new Regex(@"\s");
            str = replaceBlackRegex.Replace(str, "");

            // 检查是否全是数字
            Regex numRegex = new Regex(@"^(\d+)$");
            return numRegex.IsMatch(str);

        }

        /// <summary>
        /// 将 表示范围的字符串 分割为两个整数
        /// 例如 123-324 分割为 123 和 324
        /// </summary>
        /// <param name="nln">要分割的字符串</param>
        /// <param name="minVal">注意out</param>
        /// <param name="maxVal">注意out</param>
        /// <returns></returns>
        public static bool NumLineNumSeg(string nln, out int minVal, out int maxVal)
        {
            // 去掉空格
            Regex replaceBlackRegex = new Regex(@"\s");
            nln = replaceBlackRegex.Replace(nln, "");

            // 是否符合 数-数 的格式
            Regex nlnRegex = new Regex(@"^(\d+)-(\d+)$");
            bool isMatch = nlnRegex.IsMatch(nln);
            if (!isMatch)
            {
                minVal = maxVal = 0;
                return false;
            }

            // 获取两数
            Match nnMatch = nlnRegex.Match(nln);
            string minStr = nnMatch.Groups[1].Value;
            string maxStr = nnMatch.Groups[2].Value;
            minVal = StrToInt(minStr);
            maxVal = StrToInt(maxStr);
            return true;

        }

        internal static void SetIntBits(ref byte p1, int p2, int p3, int value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 字节数组复制
        /// </summary>
        /// <param name="src">源数组</param>
        /// <param name="beg">开始位置</param>
        /// <param name="len">复制的长度</param>
        /// <returns></returns>
        public static byte[] Copy(byte[] src, int beg, int len)
        {
            if (beg + len > src.Length)
                throw new Exception("长度错误，无法复制");
            byte[] dest = new byte[len];

            for (int i = 0; i < len; i++)
            {
                dest[i] = src[beg + i];
            }
            return dest;
        }


        /// <summary>
        /// 字节数组求和 只保留和的低8位
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static byte SumOfBytes(byte[] src, int beg, int end)
        {
            byte sum = 0;
            for (int i = beg; i <= end; i++)
            {
                sum += src[i];
            }
            return sum;
        }

        /// <summary>
        /// 字节转换为16进制表示
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ByteToHexStr(byte value, string prefix = "")
        {
            byte high = (byte)(value >> 4);
            byte low = (byte)(value & 0x0f);
            string highStr, lowStr;
            if (high >= 10)
            {  // 10 - A , 11 - B , ...
                highStr = ((char)(high - 10 + 'A')).ToString();
            }
            else
            {
                highStr = high.ToString();
            }
            if (low >= 10)
            {
                lowStr = ((char)(low - 10 + 'A')).ToString();
            }
            else
            {
                lowStr = low.ToString();
            }
            return prefix + highStr + lowStr;
        }

        public static byte HexStrToByte(string hexStr)
        {
            if (String.IsNullOrEmpty(hexStr) || hexStr.Length != 2)
                return 0;
            char highChar = hexStr[0];
            char lowChar = hexStr[1];
            byte ret = 0;
            if (highChar >= '0' && highChar <= '9')
            {
                ret += (byte)((highChar - '0') << 4);
            }
            else if (highChar >= 'A' && highChar <= 'F')
            {
                ret += (byte)((highChar - 'A' + 10) << 4);
            }
            else if (highChar >= 'a' && highChar <= 'f')
            {
                ret += (byte)((highChar - 'a' + 10) << 4);
            }
            else
            {
                return 0;
            }

            if (lowChar >= '0' && lowChar <= '9')
            {
                ret += (byte)(lowChar - '0');
            }
            else if (lowChar >= 'A' && lowChar <= 'F')
            {
                ret += (byte)(lowChar - 'A' + 10);
            }
            else if (lowChar >= 'a' && lowChar <= 'f')
            {
                ret += (byte)(lowChar - 'a' + 10);
            }
            else
            {
                return 0;
            }
            return ret;
        }

        /// <summary>
        /// 字节数组用字符串表示
        /// </summary>
        /// <param name="byteArr">字节数组</param>
        /// <param name="separator">分割符，默认为空格</param>
        /// <param name="prefix">每个字节前缀，默认不加</param>
        /// <returns></returns>
        public static string ByteArrToString(byte[] byteArr, char separator = ' ', string prefix = "")
        {
            if (byteArr == null || byteArr.Length == 0)
            {
                //throw new Exception("参数错误");
                return "";
            }
            string ret = "";
            for (int i = 0; i < byteArr.Length; i++)
            {
                if (i == 0)
                {
                    ret += ByteToHexStr(byteArr[i], prefix);
                }
                else
                {
                    ret += separator.ToString() + ByteToHexStr(byteArr[i], prefix);
                }
            }
            return ret;
        }

        /// <summary>
        /// 字符串转字节数组
        /// </summary>
        /// <param name="frameStr">字符串</param>
        /// <param name="sep">分隔符</param>
        /// <returns></returns>
        public static byte[] StringToByteArr(string frameStr, params char[] sep)
        {
            string[] byteStrArr = frameStr.Split(sep);
            byte[] byteArr = new byte[byteStrArr.Length];
            for (int i = 0; i < byteStrArr.Length; i++)
            {
                byteArr[i] = Tools.HexStrToByte(byteStrArr[i]);
            }
            return byteArr;
        }


        public static bool IsEqual<T>(T[] data1, int startIndex1, int len1,  T[] data2, int startIndex2, int len2)
        {
            if (data1 == null && data2 == null)
                return true;
            if (data1 == null || data2 == null)
                return false;
            if (startIndex1 < 0 || startIndex1 >= data1.Length)
            {
                throw new ArgumentOutOfRangeException("startIndex1", "startIndex1超出范围");
            }
            if (len1 < 0 || startIndex1 + len1 >= data1.Length)
            {
                throw new ArgumentOutOfRangeException("len", "len超出范围");
            }
            if (startIndex2 < 0 || startIndex2 >= data2.Length)
            {
                throw new ArgumentOutOfRangeException("startIndex2", "startIndex2超出范围");
            }
            if (len2 < 0 || startIndex2 + len2 >= data2.Length)
            {
                throw new ArgumentOutOfRangeException("len2", "len2超出范围");
            }
            if (len1 != len2)
                return false;
            for (int i = 0; i < len1; i++)
            {
                if (!data1[startIndex1 + i].Equals(data2[startIndex2 + i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
