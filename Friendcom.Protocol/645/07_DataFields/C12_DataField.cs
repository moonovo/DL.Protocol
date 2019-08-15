using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645._07_DataFields
{
    public class C12_DataField : DataField
    {
        public C12_DataField() : base(5) 
        {
            
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FDataIdentifier);
            Children.Add(FSeq);
        }

        private DataIdentifier07 _fDataIdentifier = new DataIdentifier07();

        public DataIdentifier07 FDataIdentifier
        {
            get { return _fDataIdentifier; }
            set { _fDataIdentifier = value; }
        }

        private PrimitiveField<Byte> _fSeq = 0;

        public PrimitiveField<Byte> FSeq
        {
            get { return _fSeq; }
            set { _fSeq = value; }
        }

    }
}
