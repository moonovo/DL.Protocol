using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645._07_DataFields
{
    public class C11_DataField : DataField
    {
        public C11_DataField()
            : base(4)
        {
            IsLengthFixed = false;
            
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FDataIdentifer);
            Children.Add(FData);
        }

        private DataIdentifier07 _fDataIdentifer = new DataIdentifier07();

        public DataIdentifier07 FDataIdentifer
        {
            get { return _fDataIdentifer; }
            set { _fDataIdentifer = value; }
        }

        private DataInDataField _fData = new DataInDataField();

        public DataInDataField FData
        {
            get { return _fData; }
            set { _fData = value; }
        }
    }
}
