﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.CJT188_2004
{
    /// <summary>
    /// 只包含数据的数据域
    /// </summary>
    public class OtherDataField : DataField
    {
        public OtherDataField()
            : base(0)
        {
            IsLengthFixed = false;
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FData);
        }

        private DataInDataField _fData = new DataInDataField();

        public DataInDataField FData
        {
            get { return _fData; }
            set { _fData = value; }
        }
    }
}
