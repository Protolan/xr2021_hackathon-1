using System;
using Architecture;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace DeviceLogic.DevicePlacer
{
    public class DevicePlacer: MonoBehaviour
    {
        [SerializeField] private DemoDevice _device;
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private Transform _originPoint;

        private bool _isActive = false;
        private bool _isPlacing = false;
        private InputModule _input;
        private const int MaxDistant = 3;

        private void Start() => _input = new InputModule();

        private void OnEnable() => _onStepLoaded.AddAction(StartPlacingDeviceIfHave);

        private void OnDisable() => _onStepLoaded.RemoveAction(StartPlacingDeviceIfHave);

        private void StartPlacingDeviceIfHave(Step step)
        {
            if (step.ContainsFeature(StepFeature.DevicePlacing))
            {
                _isActive = true;
                _device.gameObject.SetActive(true);
                _device.MakeTransparent();
                _input.ONButtonDown += ActivatePlacing;
            }
        }

        private void ActivatePlacing()
        {
            _isPlacing = true;
            _input.ONButtonDown -= ActivatePlacing;
            _input.ONButtonUp += DeactivatePlacing;
        }

        private void DeactivatePlacing()
        {
            _isPlacing = false;
            _input.ONButtonUp -= DeactivatePlacing;
            _input.ONButtonDown += ActivatePlacing;
        }

        private void FixedUpdate()
        {
            if(!_isActive) return;
            _input.Check();
            if(!_isPlacing) return;
            if (Physics.Raycast(_originPoint.position, _originPoint.forward, out var hit, 3, LayerMask.NameToLayer("PlacingPlatform")))
                _device.transform.position = hit.transform.position;
        }
    }
}