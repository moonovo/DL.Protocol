using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645._07_DataFields
{
    public class C14_DataField : DataField
    {
        public C14_DataField()
            : base(5)
        {
            IsLengthFixed = false;
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FDataIdentifier);
            Children.Add(FPassword);
            Children.Add(FOperatorCode);
            Children.Add(FData);
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

        private OperatorCode _fOperatorCode = new OperatorCode();

        public OperatorCode FOperatorCode
        {
            get { return _fOperatorCode; }
            set { _fOperatorCode = value; }
        }

        private DataInDataField _fData = new DataInDataField();

        public DataInDataField FData
        {
            get { return _fData; }
            set { _fData = value; }
        }
    }
}
