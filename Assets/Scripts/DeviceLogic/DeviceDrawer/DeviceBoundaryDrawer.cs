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
        [SerializeField] private GameEvent _onConfirmButtonPressed;
        [SerializeField] private DeviceData _data;
        [SerializeField] private GameObject _boundaryObject;
        [SerializeField] private Transform _drawingPoint;

        
        private DevicePlacingBehaviour _currentBehaviour;
        private bool _isActive;
        private Vector3 _startMachineScale;
        private InputModule _inputModule;
        

        private void Start()
        {
            _isActive = false;
            _boundaryObject.SetActive(false);
            _data._washingMachine.gameObject.SetActive(false);
            _startMachineScale = _data._washingMachine.transform.localScale;
            _inputModule = new InputModule();
        }


        private void Update()
        {
            if(!_isActive) return;
            _currentBehaviour?.Invoke();
            _inputModule.Check();
        }

        private void OnEnable() => _onStepLoaded.AddAction(ActivateIfHave);
        private void OnDisable() => _onStepLoaded.RemoveAction(ActivateIfHave);

        private void ActivateIfHave(Step stepData)
        {
            if (!stepData.ContainsFeature(StepFeature.DeviceDrawer)) return;
            _isActive = true;
            _boundaryObject.SetActive(false);
            _data._washingMachine.gameObject.SetActive(false);
            _inputModule.ONButtonDown += StartDrawing;
        }

        private void StartDrawing()
        {
            _currentBehaviour = new DrawingBoundariesBehaviour(_boundaryObject, _drawingPoint, _data._scales);
            _inputModule.ONButtonDown -= StartDrawing;
            _inputModule.ONButtonUp += CheckSettingHeight;
        }

        private void CheckSettingHeight()
        {
            _currentBehaviour = null;
            _onConfirmButtonPressed.AddAction(SetDevice);
            _inputModule.ONButtonUp -= CheckSettingHeight;
            _inputModule.ONButtonDown += StartSettingHeight;
            _onBoundariesDrawn.Invoke();
        }
        private void StartSettingHeight()
        {
            _currentBehaviour = new SettingBoundariesHeightBehaviour(_boundaryObject, _drawingPoint);
            _inputModule.ONButtonDown -= StartSettingHeight;
            _inputModule.ONButtonUp += CheckSettingHeight;
        }

        private void SetDevice()
        {
            _inputModule.ONButtonDown -= CheckSettingHeight;
            _onConfirmButtonPressed.RemoveAction(SetDevice);
            _currentBehaviour = new DeviceSettingBehaviour(_data._washingMachine.gameObject, _boundaryObject,
                _data._scales, _startMachineScale);
            _inputModule.ONButtonDown -= SetDevice;
            _onDrawingComplete.Invoke();
            _isActive = false;
        }
    }
}