using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645._97_DataFields
{
    public class C83_DataField : DataField
    {
        public C83_DataField()
            : base(2)
        {
            IsLengthFixed = false;
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FDataIdentifer);
            Children.Add(FData);
        }

        private DataIdentifier97 _fDataIdentifer = new DataIdentifier97();

        public DataIdentifier97 FDataIdentifer
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
