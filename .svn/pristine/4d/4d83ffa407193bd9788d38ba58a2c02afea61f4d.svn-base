using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol._645._07_DataFields
{
    public class C18_DataField : DataField
    {
        public C18_DataField()
            : base(12)
        {
            
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FDataIdentifier);
            Children.Add(FPassword);
            Children.Add(FNewPassword);
        }

        private DataIdentifier07 _fDataIdentifier = new DataIdentifier07();

        public DataIdentifier07 FDataIdentifier
        {
            get { return _fDataIdentifier; }
            set { _fDataIdentifier = value; }
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
