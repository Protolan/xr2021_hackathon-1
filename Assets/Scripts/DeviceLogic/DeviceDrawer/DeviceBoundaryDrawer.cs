using System;
using Architecture;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace DeviceLogic
{
    public class DeviceBoundaryDrawer : MonoBehaviour
    {
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private GameEvent _onBoundariesDrawn;
        [SerializeField] private GameEvent _onDrawingComplete;
        [SerializeField] private DeviceData _data;
        [SerializeField] private GameObject _boundaryObject;
        [SerializeField] private Transform _drawingPoint;

        
        private DevicePlacingBehaviour _currentBehaviour;
        private Action _onButtonDown;
        private Action _onButtonUp;
        private bool _isActive;
        private Vector3 _startMachineScale;

        private void Start()
        {
            _isActive = false;
            _boundaryObject.SetActive(false);
            _data._washingMachine.gameObject.SetActive(false);
            _startMachineScale = _data._washingMachine.transform.localScale;
        }


        private void Update()
        {
            if(!_isActive) return;
            _currentBehaviour?.Invoke();
            CheckForButtonDown();
            CheckForButtonUp();
        }

        private void OnEnable() => _onStepLoaded.AddAction(ActivateIfHave);
        private void OnDisable() => _onStepLoaded.RemoveAction(ActivateIfHave);

        private void ActivateIfHave(Step stepData)
        {
            if (!stepData.ContainsFeature(StepFeature.DeviceDrawer)) return;
            _isActive = true;
            _boundaryObject.SetActive(false);
            _data._washingMachine.gameObject.SetActive(false);
            _onButtonDown += StartDrawing;
        }

        private void StartDrawing()
        {
            _currentBehaviour = new DrawingBoundariesBehaviour(_boundaryObject, _drawingPoint, _data._scales);
            _onButtonDown -= StartDrawing;
            _onButtonUp += StartSettingHeight;
        }

        private void StartSettingHeight()
        {
            _currentBehaviour = new SettingBoundariesHeightBehaviour(_boundaryObject, _drawingPoint);
            _onButtonUp -= StartSettingHeight;
            _onButtonDown += SetDevice;
            _onBoundariesDrawn.Invoke();
        }

        private void SetDevice()
        {
            _currentBehaviour = new DeviceSettingBehaviour(_data._washingMachine.gameObject, _boundaryObject,
                _data._scales, _startMachineScale);
            _onButtonDown -= SetDevice;
            _onDrawingComplete.Invoke();
            _isActive = false;
        }


        private void CheckForButtonDown()
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) 
                _onButtonDown?.Invoke();
        }

        private void CheckForButtonUp()
        {
            if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)) 
                _onButtonUp?.Invoke();
        }
    }
}