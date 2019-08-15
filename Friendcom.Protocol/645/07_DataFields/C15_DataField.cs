using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645._07_DataFields
{
    public class C15_DataField : DataField
    {
        public C15_DataField()
            : base(6)
        {
            
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FAddress);
        }

        private AddressField _fAddress = new AddressField();

        public AddressField FAddress
        {
            get { return _fAddress; }
            set { _fAddress = value; }
        }


    }
}
