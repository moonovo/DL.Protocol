using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol._1376_2
{
    /// <summary>
    /// 地址域
    /// </summary>
    public class AddressField : NonLeafField<Byte>
    {
        public AddressField()
            : base(0)
        {
            
        }

        private bool _isDown;

        public bool IsDown
        {
            get { return _isDown; }
            set { _isDown = value; }
        }

        private byte _commModuleID;

        public byte CommModuleID
        {
            get { return _commModuleID; }
            set { _commModuleID = value; }
        }

        private byte _relayLevel;

        public byte RelayLevel
        {
            get { return _relayLevel; }
            set { _relayLevel = value; }
        }

        private Address _a1 = new Address();

        public Address A1
        {
            get { return _a1; }
            set { _a1 = value; }
        }

        private List<Address> _a2 = new List<Address>();

        public List<Address> A2
        {
          get { return _a2; }
          set { _a2 = value; }
        }

        private Address _a3 = new Address();

        public Address A3
        {
            get { return _a3; }
            set { _a3 = value; }
        }

        public override int Length
        {
            get
            {
                if (CommModuleID != 0)
                {
                    if (IsDown)
                    {
                        return 6 + 6 * RelayLevel + 6;
                    }
                    else
                    {
                        return 6 + 6;
                    }
                }
                return 0;
            }
        }

        protected override void SetChildren()
        {
            if (CommModuleID != 0)
            {
                if (IsDown)
                {
                    Children.Add(A1);
                    for (int i = 0; i < RelayLevel; i++)
                    {
                        if(A2 == null) 
                            A2 = new List<Address>();
                        if(i >= A2.Count || A2[i] == null)
                            A2.Add(new Address());
                        Children.Add(A2[i]);
                    }
                    Children.Add(A3);
                }
                else
                {
                    Children.Add(A1);
                    Children.Add(A3);
                }
            }
        }
    }
}
