using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645._07_DataFields
{
    public class CB2_DataField : DataField
    {
        public CB2_DataField()
            : base(5)
        {
            IsLengthFixed = false;
            
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FDataIdentifier);
            Children.Add(FData);
            Children.Add(FSeq);
        }

        private DataIdentifier07 _fDataIdentifier = new DataIdentifier07();

        public DataIdentifier07 FDataIdentifier
        {
            get { return _fDataIdentifier; }
            set { _fDataIdentifier = value; }
        }

        private DataInDataField _fData = new DataInDataField();

        public DataInDataField FData
        {
            get { return _fData; }
            set { _fData = value; }
        }

        private PrimitiveField<Byte> _fSeq = 0;

        public PrimitiveField<Byte> FSeq
        {
            get { return _fSeq; }
            set { _fSeq = value; }
        }

    }
}
