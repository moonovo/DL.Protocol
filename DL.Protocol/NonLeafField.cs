using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol
{
    public abstract class NonLeafField<T> : Field<T>
    {
        public NonLeafField() : base(0) { }

        public NonLeafField(int length) : base(length) { }

        protected abstract void SetChildren();

        /// <summary>
        /// 获取域的数据
        /// </summary>
        /// <returns></returns>
        public override T[] GetData()
        {
            SetChildren();
            List<T[]> dataList = new List<T[]>();
            int len = 0;
            foreach (var child in Children)
            {
                T[] tmp = null;
                if (child == null)
                {
                    tmp = new T[0];
                    continue;
                }
                tmp = child.GetData();
                if (child.IsLengthFixed && child.Length != tmp.Length)
                {
                    throw new Exception("某个固定长度的子域中的Length与GetData时获取的数据的长度不相等");
                }
                len += tmp.Length;
                dataList.Add(tmp);
            }
            T[] data = new T[len];
            int curPos = 0;
            foreach (var childData in dataList)
            {
                Array.Copy(childData, 0, data, curPos, childData.Length);
                curPos += childData.Length;
            }
            return data;
        }

        public override void SetData(T[] data, int startIndex, int len)
        {
            CheckData(data, startIndex, len);
            SetChildren();
            if (Children == null || Children.Count == 0)
                return;

            // 检查子域中 有几个长度不固定 如果不固定长度的域 超过一个 那么报错
            int isNotFixedNum = 0;
            foreach (var child in Children)
            {
                isNotFixedNum += child.IsLengthFixed ? 0 : 1;
            }
            //Children.Select(o => isNotFixedNum += o.IsLengthFixed ? 0 : 1);
            if (isNotFixedNum > 1)
            {
                throw new Exception("子域中不固定长度的域超过了一个，无法对各个子域进行SetData操作，请检查各个域或者重载SetData方法");
            }
            int isNotFixedChild = -1;
            int curPreIndex = startIndex;
            for (int i = 0; i < Children.Count; i++)
            {
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


        
    }
}
