using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.MicroPower
{
    /// <summary>
    /// APS中的业务扩展域
    /// </summary>
    public class ApsOed : NonLeafField<Byte>
    {
        public ApsOed()
            : base(3)
        {
            IsLengthFixed = false;
        }

        private PrimitiveField<Byte> _dataAreaLength = 0;

        /// <summary>
        /// 扩展域数据区长度
        /// </summary>
        public PrimitiveField<Byte> DataAreaLength
        {
            get { return _dataAreaLength; }
            set { _dataAreaLength = value; }
        }

        private ApsOedManufacturerID _manufacturerID = new ApsOedManufacturerID();

        /// <summary>
        /// 厂家标识
        /// </summary>
        public ApsOedManufacturerID ManufacturerID
        {
            get { return _manufacturerID; }
            set { _manufacturerID = value; }
        }

        private ApsOedDataArea _dataArea = new ApsOedDataArea();

        /// <summary>
        /// 扩展域数据区
        /// </summary>
        public ApsOedDataArea DataArea
        {
            get { return _dataArea; }
            set { _dataArea = value; }
        }

        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(DataAreaLength);
            Children.Add(ManufacturerID);
            Children.Add(DataArea);
        }

        public override byte[] GetData()
        {
            if (DataAreaLength != DataArea.Value.Length)
            {
                throw new Exception("APS层业务扩展域中长度错误");
            }
            return base.GetData();
        }

        public override void SetData(byte[] data, int startIndex, int len)
        {
            base.SetData(data, startIndex, len);
            if (data[startIndex] + 3 != len)
            {
                throw new Exception("APS层业务扩展域中长度错误");
            }
        }
    }
}
