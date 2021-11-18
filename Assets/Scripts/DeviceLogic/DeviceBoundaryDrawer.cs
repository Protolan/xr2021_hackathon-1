using System;
using Architecture;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace DeviceLogic
{
    public class DeviceBoundaryDrawer : MonoBehaviour
    {
        // [SerializeField] private GameEvent _onDeviceDraw;
        // [SerializeField] private StepGameEvent _onStepLoaded;
        // [SerializeField] private GameEvent _onStepChanged;
        [SerializeField] private DeviceData _data;
        [SerializeField] private GameObject _boundaryObject;
        [SerializeField] private Transform _drawingPoint;

        private bool _isActive = true;
        private bool _isDrawing;
        private Vector3 _startDrawPosition;
        private float _length;
        private Vector3 _startRotation;
        private bool _isSettingHeight;
        private Vector3 _startMachineScale;

        private void Awake()
        {
            _boundaryObject.SetActive(false);
            _isDrawing = false;
            _isSettingHeight = false;

        }

        private void Start()
        {
            _data._washingMachine.SetActive(false);
            _startMachineScale = _data._washingMachine.transform.localScale;
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
            if (_isActive == false) return;
            if (CheckForActivation())
                StartDrawing();
            if (CheckForDrawingComplete())
                StopDrawing();
            if (_isDrawing)
            {
                UpdateLength();
                UpdateScale();
                UpdateRotation();
            }

            if (!_isSettingHeight) return;
            SetHeight();
            if (CheckForSettingHeightComplete())
            {
                
                _data._washingMachine.transform.position = _boundaryObject.transform.position;
                _data._washingMachine.transform.localScale = _startMachineScale * (_length / _data._scales.x);
                _data._washingMachine.transform.rotation = _boundaryObject.transform.rotation;
                _data._washingMachine.SetActive(true);
                _boundaryObject.SetActive(false);
                _isSettingHeight = false;
            }
        }


        private void SetHeight()
        {
            if (!_isSettingHeight) return;
            _boundaryObject.transform.position =
                new Vector3(_boundaryObject.transform.position.x, _drawingPoint.transform.position.y,
                    _boundaryObject.transform.position.z);
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

        private void UpdateScale()
        {
            _boundaryObject.transform.localScale =
                new Vector3(_length,
                    CalculateSide(_data._scales.y),
                    CalculateSide(_data._scales.z));
        }

        private bool CheckForActivation()
        {
            if (_isDrawing || _isSettingHeight) return false;
            return OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger);
        }

        private bool CheckForDrawingComplete()
        {
            if (!_isDrawing) return false;
            return OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger);
        }

        private bool CheckForSettingHeightComplete()
        {
            if (!_isSettingHeight) return false;
            return OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger);
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
            _isSettingHeight = true;
        }

        public float CalculateSide(float defaultSize) => defaultSize * _length / _data._scales.x;
    }
}