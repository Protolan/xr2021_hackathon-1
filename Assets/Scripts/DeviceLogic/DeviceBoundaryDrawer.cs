using Architecture;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace DeviceLogic
{
    public class DeviceBoundaryDrawer: MonoBehaviour
    {
        // [SerializeField] private GameEvent _onDeviceDraw;
        // [SerializeField] private StepGameEvent _onStepLoaded;
        // [SerializeField] private GameEvent _onStepChanged;
        [SerializeField] private DeviceData _data;
        [SerializeField] private Vector3 _startSize;
        [SerializeField] private GameObject _boundaryObject;
        [SerializeField] private Transform _drawingPoint;
        [SerializeField] private float _rotateMultiplier = 1f;
        [SerializeField] private float _scaleMultiplier = 1f;

        private bool _isActive = true;
        private bool _isDrawing;
        private Vector3 _startDrawPosition;
        private float _length;
        private Vector3 _startRotation;

        private void Awake()
        {
            _boundaryObject.SetActive(false);
            _isDrawing = false;
        }

        // private void OnEnable()
        // {
        //     _onStepLoaded.AddAction(ActivateIfHave);
        // }
        //
        // private void ActivateIfHave(Step stepData)
        // {
        //     if (stepData.ContainsFeature(StepFeature.DeviceDrawer)) _isActive = true;
        // }

        private void Update()
        {
            if(_isActive == false) return;
            if(CheckForActivation()) 
                StartDrawing();
            if (CheckForComplete())
                StopDrawing();
            if (!_isDrawing) return;
            UpdateLength();
            UpdateScale();
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            _boundaryObject.transform.rotation = Quaternion.Euler(_boundaryObject.transform.rotation.eulerAngles.x, 
                _startRotation.y + (_drawingPoint.transform.position.y - _startDrawPosition.y) * _rotateMultiplier,
                _boundaryObject.transform.rotation.eulerAngles.z);
        }

        private void UpdateLength() =>
            _length = _startSize.x + 
                (new Vector2(_drawingPoint.transform.position.x, _drawingPoint.transform.position.y) 
                 - new Vector2(_startDrawPosition.x, _startDrawPosition.y)).magnitude * _scaleMultiplier;

        private void UpdateScale()
        { 
            _boundaryObject.transform.localScale = 
                new Vector3(_length,
                    CalculateSide(_data._scales.y), 
                    CalculateSide(_data._scales.z));
        }

        private bool CheckForActivation()
        {
            if(_isDrawing) return false;
            return OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger);
        }

        private bool CheckForComplete()
        {
            if(!_isDrawing) return false;
            return OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger);
        }
        
        private void StartDrawing()
        {
            _isDrawing = true;
            _startDrawPosition = _drawingPoint.position;
            _boundaryObject.transform.position = _startDrawPosition;
            _boundaryObject.transform.rotation = Quaternion.Euler(_boundaryObject.transform.rotation.eulerAngles.x, 
                _drawingPoint.transform.rotation.eulerAngles.y, _boundaryObject.transform.rotation.eulerAngles.z);
            _startRotation = _boundaryObject.transform.rotation.eulerAngles;
            _boundaryObject.SetActive(true);
        }

        private void StopDrawing()
        {
            _isDrawing = false;
            // _onDeviceDraw.Invoke();
        }

        public float CalculateSide(float defaultSize) => defaultSize * _length / _data._scales.x;
    }
}