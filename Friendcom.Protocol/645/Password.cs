using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friendcom.Protocol._645
{
    /// <summary>
    /// 密码域
    /// </summary>
    public class Password : NonLeafField<Byte>
    {
        public Password() : base(4) 
        {
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(PA);
            Children.Add(PW);
        }

        private PAInPassword _pa = new PAInPassword();

        public PAInPassword PA
        {
            get { return _pa; }
            set { _pa = value; }
        }

        private PWInPassword _pw = new PWInPassword();

        public PWInPassword PW
        {
            get { return _pw; }
            set { _pw = value; }
        }

        


    }
}
