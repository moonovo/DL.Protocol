using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol._645._97_DataFields
{
    public class C8F_DataField : DataField
    {
        public C8F_DataField() : base(4) { }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FPassword);
        }

        private Password _fPassword = new Password();

        public Password FPassword
        {
            get { return _fPassword; }
            set { _fPassword = value; }
        }

    }
}
