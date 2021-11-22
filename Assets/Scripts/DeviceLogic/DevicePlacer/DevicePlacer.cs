using System;
using Architecture;
using ScriptableSystem.GameEvent;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DeviceLogic.DevicePlacer
{
    public class DevicePlacer : MonoBehaviour
    {
        [SerializeField] private DemoDevice _device;
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private OVRInputModule _laser;

        private bool _isActive = false;
        private bool _isPlacing = false;
        private UserInput _userInput;
        private const int MaxDistant = 3;

        private void Awake() => _userInput = new UserInput();

        private void OnEnable() => _onStepLoaded.AddAction(StartPlacingDeviceIfHave);

        private void OnDisable() => _onStepLoaded.RemoveAction(StartPlacingDeviceIfHave);

        private void StartPlacingDeviceIfHave(Step step)
        {
            if (step.ContainsFeature(StepFeature.DevicePlacing))
            {
                _isActive = true;
                _device.gameObject.SetActive(true);
                _device.MakeTransparent();
                _userInput.ONButtonDown += ActivatePlacing;
            }
            else
            {
                _isActive = false;
                _device.MakeNormal();
            }
        }

        private void ActivatePlacing()
        {
            
            _isPlacing = true;
            _device.MakeTransparent();
            _userInput.ONButtonDown -= ActivatePlacing;
            _userInput.ONButtonUp += DeactivatePlacing;
        }

        private void DeactivatePlacing()
        {
            _isPlacing = false;
            _device.MakeNormal();
            _userInput.ONButtonUp -= DeactivatePlacing;
            _userInput.ONButtonDown += ActivatePlacing;
        }


        private void Update()
        {
            if (!_isActive) return;
            _userInput.Check();

        }

        private void FixedUpdate()
        {
            if (!_isActive) return;
            if (!_isPlacing) return;
            if (Physics.Raycast(_laser.rayTransform.position, _laser.rayTransform.forward, out var hit, MaxDistant))
            {
                if (hit.transform.TryGetComponent(out PlacingPlatform placingPlatform))
                    _device.transform.position = hit.point;
            }
        }
    }
}