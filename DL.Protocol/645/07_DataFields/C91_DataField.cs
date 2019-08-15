using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645._07_DataFields
{
    public class C91_DataField : DataField
    {
        public C91_DataField()
            : base(4)
        {
            IsLengthFixed = false;
            
        }


        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FDataIdentifer);
            Children.Add(FDataInDataField);
        }

        private DataIdentifier07 _fDataIdentifer = new DataIdentifier07();

        public DataIdentifier07 FDataIdentifer
        {
            get { return _fDataIdentifer; }
            set { _fDataIdentifer = value; }
        }

        private DataInDataField _fDataInDataField = new DataInDataField();

        public DataInDataField FDataInDataField
        {
            get { return _fDataInDataField; }
            set { _fDataInDataField = value; }
        }
    }
}
