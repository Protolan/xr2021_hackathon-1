using UnityEngine;

namespace DeviceLogic
{
    public class SettingBoundariesHeightBehaviour : DevicePlacingBehaviour
    {
        private readonly GameObject _boundaries;
        private readonly Transform _drawingPoint;

        public SettingBoundariesHeightBehaviour(GameObject boundaries, Transform drawingPoint)
        {
            _boundaries = boundaries;
            _drawingPoint = drawingPoint;
        }
    

        private void UpdateHeight()
        {
            _boundaries.transform.position =
                new Vector3(_boundaries.transform.position.x, _drawingPoint.transform.position.y,
                    _boundaries.transform.position.z);
        }
        public override void Invoke()
        {
            UpdateHeight();
        }
    }
}