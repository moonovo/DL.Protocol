using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._645._07_DataFields
{
    public class C98_DataField : DataField
    {
        public C98_DataField()
            : base(4)
        {
            
        }


        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(FPassword);
        }

        private Password _fPassword = new Password();

        public Password FPassword
        {
            get { return _fPassword; }
            set { _fPassword = value; }
        }

        
    }
}
