using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645._07_DataFields
{
    public class C1B_DataField : DataField
    {
        public C1B_DataField()
            : base(8)
        {
            
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FPassword);
            Children.Add(FOperatorCode);
        }

        private Password _fPassword = new Password();

        public Password FPassword
        {
            get { return _fPassword; }
            set { _fPassword = value; }
        }

        private OperatorCode _fOperatorCode = new OperatorCode();

        public OperatorCode FOperatorCode
        {
            get { return _fOperatorCode; }
            set { _fOperatorCode = value; }
        }

        private DataIdentifier07 _fDataIdentifer = new DataIdentifier07();

        public DataIdentifier07 FDataIdentifer
        {
            get { return _fDataIdentifer; }
            set { _fDataIdentifer = value; }
        }



    }
}
