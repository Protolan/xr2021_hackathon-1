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
        private InputModule _input;
        private const int MaxDistant = 3;

        private void Awake() => _input = new InputModule();

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
            _input.ONButtonDown -= ActivatePlacing;
            _input.ONButtonUp += DeactivatePlacing;
        }

        private void DeactivatePlacing()
        {
            _isPlacing = false;
            _device.MakeNormal();
            _input.ONButtonUp -= DeactivatePlacing;
            _input.ONButtonDown += ActivatePlacing;
        }


        private void Update()
        {
            if (!_isActive) return;
            _input.Check();

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