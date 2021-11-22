using UnityEngine;

namespace DeviceLogic
{
    public class DeviceSettingBehaviour : DevicePlacingBehaviour
    {
        private readonly GameObject _device;
        private readonly GameObject _boundaries;
        private readonly Vector3 _aspectRatio;
        private readonly Vector3 _startMachineScale;

        public DeviceSettingBehaviour(GameObject device, GameObject boundaries, Vector3 aspectRatio, Vector3 startMachineScale)
        {
            _device = device;
            _boundaries = boundaries;
            _aspectRatio = aspectRatio;
            _startMachineScale = startMachineScale;
            SetDeice();
        }
        
        private void SetDeice()
        {
            _device.transform.position = _boundaries.transform.position;
            _device.transform.localScale = _startMachineScale * (_boundaries.transform.localScale.x / _aspectRatio.x);
            _device.transform.rotation = _boundaries.transform.rotation;
            _device.SetActive(true);
        }
        public override void Invoke()
        {
        }
    }
}