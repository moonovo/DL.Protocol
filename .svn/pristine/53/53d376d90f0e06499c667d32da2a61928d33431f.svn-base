using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol
{
    /// <summary>
    /// 域
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Field<T> : IDataOperation<T>
    {
        public Field() : this(0) {}

        public Field(int length) 
        {
            _length = length;
        }

        private object _synObj = new object();

        private int _length;

        private T[] _insideData = null;

        protected T[] InsideData
        {
            get
            {
                lock (_synObj)
                {
                    if (_insideData == null)
                        _insideData = new T[_length];
                }
                return _insideData;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "value不能为空");
                }
                if (IsLengthFixed && value.Length != _length)
                {
                    throw new ArgumentOutOfRangeException("value", "value的长度与域规定的长度不相同");
                }
                _insideData = value;
            }
        }

        List<Field<T>> _children = null;

        /// <summary>
        /// 子域
        /// </summary>
        protected List<Field<T>> Children
        {
            get
            {
                lock (_synObj)
                {
                    if (_children == null)
                    {
                        _children = new List<Field<T>>();
                    }
                }
                return _children;
            }
            set
            {
                _children = value;
            }
        }

        private bool _isLengthFixed = true;

        /// <summary>
        /// 长度是否固定
        /// </summary>
        public bool IsLengthFixed
        {
            get
            {
                return _isLengthFixed;
            }
            protected set 
            {
                _isLengthFixed = value;
            }
        }

        /// <summary>
        /// 域的长度 如果长度不固定 那么此长度为最小长度
        /// </summary>
        public virtual int Length
        {
            get
            {
                return _length;
            }
        }
       

        /// <summary>
        /// 检查字节数组，开始字符，长度
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="startIndex">开始字符</param>
        /// <param name="len">长度</param>
        protected void CheckData(T[] data, int startIndex, int len)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data", "data不能为空");
            }
            if (startIndex < 0 || startIndex >= data.Length)
            {
                throw new ArgumentOutOfRangeException("startIndex", "startIndex超出范围");
            }
            if (len < 0 || startIndex + len > data.Length)
            {
                throw new ArgumentOutOfRangeException("len", "len超出范围");
            }

            if (IsLengthFixed)
            {
                if (len != this.Length)
                {
                    throw new ArgumentOutOfRangeException("len", "len与该域的长度不相等");
                }
            }
            else
            {
                if (len < this.Length)
                {
                    throw new ArgumentOutOfRangeException("len", "len比该域的最小长度小");
                }
            }
        }

        public void SetData(T[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data", "data不能为空");
            }
            SetData(data, 0, data.Length);
        }

        public abstract void SetData(T[] data, int startIndex, int len);

        public abstract T[] GetData();

        public IFormat<T> FieldFormat = null;

        

        public override string ToString()
        {
            string assembly = "Friendcom.Protocol";
            string formatsNamespace = "Friendcom.Protocol.Formats";
            if (FieldFormat == null) 
            {
                Type type = typeof(T);
                string formatClassName = type.Name + "Format";
                //IFormat<T> format = Tools.CreateInstance<IFormat<T>>(assembly, formatsNamespace, formatClassName);
                Type formatType = Type.GetType(formatsNamespace + "." + formatClassName);
                IFormat<T> format = Tools.InvokeStaticMethod<IFormat<T>>(formatType, "GetInstance", null);
                if (format == null)
                {
                    return base.ToString();
                }
                else
                {
                    return format.GetString(this, this.GetData());
                }
            }
            else
            {
                return FieldFormat.GetString(this, this.GetData());
            }
        }

    }
}
