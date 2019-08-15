using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Protocol.MicroPower
{
    /// <summary>
    /// APP层的设备管理平台帧
    /// </summary>
    public class DMPFrame : NonLeafField<Byte>
    {
        public DMPFrame()
            : base(14)
        {

        }

        private ReverseLeafField<Byte> _deviceFacAddr = new ReverseLeafField<byte>(6);

        /// <summary>
        /// 设备出厂地址
        /// </summary>
        public ReverseLeafField<Byte> DeviceFacAddr
        {
            get { return _deviceFacAddr; }
        }

        private PrimitiveField<Byte> _deviceType = 0;

        /// <summary>
        /// 设备类型
        /// </summary>
        public PrimitiveField<Byte> DeviceType
        {
            get { return _deviceType; }
            set { _deviceType = value; }
        }

        private ReverseLeafField<Byte> _facIdentifer = new ReverseLeafField<byte>(2);

        /// <summary>
        /// 厂家标识
        /// </summary>
        public ReverseLeafField<Byte> FacIdentifer
        {
            get { return _facIdentifer; }
        }

        private ReverseLeafField<Byte> _hardwareVer = new ReverseLeafField<byte>(2);

        /// <summary>
        /// 硬件版本号
        /// </summary>
        public ReverseLeafField<Byte> HardwareVer
        {
            get { return _hardwareVer; }
        }

        private ReverseLeafField<Byte> _softwareVer = new ReverseLeafField<byte>(3);

        /// <summary>
        /// 软件版本号
        /// </summary>
        public ReverseLeafField<Byte> SoftwareVer
        {
            get { return _softwareVer; }
        }


        protected override void SetChildren()
        {
            Children.Clear();
            Children.Add(DeviceFacAddr);
            Children.Add(DeviceType);
            Children.Add(FacIdentifer);
            Children.Add(HardwareVer);
            Children.Add(SoftwareVer);
        }
    }
}
