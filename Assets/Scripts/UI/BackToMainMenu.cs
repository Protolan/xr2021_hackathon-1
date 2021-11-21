using System;
using Architecture;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace UI
{
    public class BackToMainMenu : MonoBehaviour
    {
        [SerializeField] private StepGameEvent _onStepChanged;
        [SerializeField] private GameEvent _onActivate;
        [SerializeField] private Step _mainMenuStep;

        private bool _isActive = false;

        private void OnEnable() => _onActivate.AddAction(Activate);
        private void OnDisable() => _onActivate.RemoveAction(Activate);
        private void Activate() => _isActive = true;


        private void Update()
        {
            if(!_isActive) return;
            if (OVRInput.GetDown(OVRInput.Button.One))
                _onStepChanged.Invoke(_mainMenuStep);
        }
    }
}