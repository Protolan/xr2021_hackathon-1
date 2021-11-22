using System.Collections;
using Architecture;
using ScriptableSystem.GameEvent;
using UnityEngine;
using UnityEngine.EventSystems;

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
        [SerializeField] private OVRInputModule _input;

        private DevicePlacingBehaviour _currentBehaviour;
        private bool _isActive;
        private Vector3 _startMachineScale;
        private UserInput _userInput;
        private bool _buttonSelected;


        public void ButtonSelected(bool state)
        {
            Debug.Log(state);
            _buttonSelected = state;
        }


        private void Start()
        {
            _isActive = false;
            _boundaryObject.SetActive(false);
            _data._washingMachine.gameObject.SetActive(false);
            _startMachineScale = _data._washingMachine.transform.localScale;
            _userInput = new UserInput();
        }


        private void Update()
        {
            if (!_isActive) return;
            _currentBehaviour?.Invoke();
            _userInput.Check();
        }

        private void OnEnable() => _onStepLoaded.AddAction(ActivateIfHave);
        private void OnDisable() => _onStepLoaded.RemoveAction(ActivateIfHave);

        private void ActivateIfHave(Step stepData)
        {
     
            _buttonSelected = false;
       
            if (!stepData.ContainsFeature(StepFeature.DeviceDrawer)) return;

            _userInput = new UserInput();
            _isActive = true;
        
            _boundaryObject.SetActive(false);
        
            _data._washingMachine.gameObject.SetActive(false);
            _userInput.ONButtonDown += StartDrawing;
        }
        
        private void StartDrawing()
        {
            _currentBehaviour = new DrawingBoundariesBehaviour(_boundaryObject, _drawingPoint, _data._scales);
            _userInput.ONButtonDown -= StartDrawing;
            _userInput.ONButtonUp += CheckSettingHeight;
        }

        private void CheckSettingHeight()
        {
            _currentBehaviour = null;
            _onConfirmButtonPressed.AddAction(SetDevice);
            _userInput.ONButtonUp -= CheckSettingHeight;
            _userInput.ONButtonDown += StartSettingHeight;
            _onBoundariesDrawn.Invoke();
        }

        private void StartSettingHeight()
        {
            Debug.Log(_buttonSelected);
            if (_buttonSelected) return;
            _currentBehaviour = new SettingBoundariesHeightBehaviour(_boundaryObject, _drawingPoint);
            _userInput.ONButtonDown -= StartSettingHeight;
            _userInput.ONButtonUp += CheckSettingHeight;
        }

        private bool CheckForButtonSelected()
        {
            if (Physics.Raycast(_input.rayTransform.position, _input.rayTransform.forward, 100f,
                LayerMask.GetMask("UI")))
            {
                return true;
            }

            return false;
        }


        private void SetDevice()
        {
            _userInput.ONButtonDown -= StartSettingHeight;
            _userInput.ONButtonDown -= CheckSettingHeight;
            _onConfirmButtonPressed.RemoveAction(SetDevice);
            _currentBehaviour = new DeviceSettingBehaviour(_data._washingMachine.gameObject, _boundaryObject,
                _data._scales, _startMachineScale);
            _userInput.ONButtonDown -= SetDevice;
            _onDrawingComplete.Invoke();
            _isActive = false;
        }
    }
}