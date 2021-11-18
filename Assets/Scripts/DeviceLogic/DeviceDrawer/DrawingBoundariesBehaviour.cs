using UnityEngine;

namespace DeviceLogic
{
    public class DrawingBoundariesBehaviour : DevicePlacingBehaviour
    {
        private readonly GameObject _boundaryObject;
        private readonly Transform _drawingPoint;
        private readonly Vector3 _aspectRatio;
        private Vector3 _startDrawPosition;
        private float _length;

        public DrawingBoundariesBehaviour(GameObject boundaryObject, Transform drawingPoint, Vector3 aspectRatio)
        {
            _boundaryObject = boundaryObject;
            _drawingPoint = drawingPoint;
            _aspectRatio = aspectRatio;
            StartDrawing();
        }
        
        private void StartDrawing()
        {
            _startDrawPosition = _drawingPoint.position;
            _boundaryObject.transform.position = _startDrawPosition;
            _boundaryObject.transform.rotation = Quaternion.Euler(_boundaryObject.transform.rotation.eulerAngles.x,
                _drawingPoint.transform.rotation.eulerAngles.y, _boundaryObject.transform.rotation.eulerAngles.z);
            _boundaryObject.SetActive(true);
        }

        private void UpdateRotation()
        {
            _boundaryObject.transform.LookAt(_drawingPoint.transform.position, Vector3.up);
            _boundaryObject.transform.rotation =
                Quaternion.Euler(0, _boundaryObject.transform.rotation.eulerAngles.y - 90, 0);
        }

        private void UpdateLength()
        {
            _length = (new Vector2(_drawingPoint.position.x, _drawingPoint.position.z) -
                       new Vector2(_startDrawPosition.x, _startDrawPosition.z)).magnitude;
        }

        private void UpdateScaleRelativelyLength()
        {
            UpdateLength();
            _boundaryObject.transform.localScale =
                new Vector3(_length,
                    CalculateSideRelativelyLenght(_aspectRatio.y),
                    CalculateSideRelativelyLenght(_aspectRatio.z));
        }

        private float CalculateSideRelativelyLenght(float defaultSize) => defaultSize * _length / _aspectRatio.x;

        public override void Invoke()
        {
            UpdateRotation();
            UpdateScaleRelativelyLength();
        }
    }
}