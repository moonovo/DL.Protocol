using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645._97_DataFields
{
    public class C0F_DataField : DataField
    {
        public C0F_DataField() : base(8) { }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FPassword);
            Children.Add(FNewPassword);
        }

        private Password _fPassword = new Password();

        public Password FPassword
        {
            get { return _fPassword; }
            set { _fPassword = value; }
        }

        private Password _fNewPassword = new Password();

        public Password FNewPassword
        {
            get { return _fNewPassword; }
            set { _fNewPassword = value; }
        }
    }
}
