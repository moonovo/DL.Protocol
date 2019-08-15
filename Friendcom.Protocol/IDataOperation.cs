using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol
{
    /// <summary>
    /// 数据操作接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataOperation<T>
    {
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="data">数据数组</param>
        /// <param name="startIndex">数据开始位置</param>
        /// <param name="len">数据长度</param>
        void SetData(T[] data, int startIndex, int len);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        T[] GetData();
    }
}
