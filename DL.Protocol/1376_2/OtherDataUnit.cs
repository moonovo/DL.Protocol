using DL.Protocol._645;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._1376_2
{
    /// <summary>
    /// 只包含数据的数据单元
    /// </summary>
    public class OtherDataUnit : DataUnit
    {
        public OtherDataUnit()
            : base(0)
        {
            IsLengthFixed = false;
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FData);
        }

        private DataInDataUnit _fData = new DataInDataUnit();

        public DataInDataUnit FData
        {
            get { return _fData; }
            set { _fData = value; }
        }
    }
}
