using System;
using Architecture;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace UI
{
    public class BackToMainMenu : MonoBehaviour
    {
        [SerializeField] private StepGameEvent _onStepChanged;
        
        [SerializeField] private Step _mainMenuStep;
        private bool _isActive;


        private void OnEnable() => _onStepChanged.AddAction(CheckForActivation);

        private void OnDisable() => _onStepChanged.RemoveAction(CheckForActivation);

        private void CheckForActivation(Step step)
        {
            if (step.ContainsFeature(StepFeature.EnableMainMenu)) 
                _isActive = true;
        }

        private void Update()
        {
            if(!_isActive) return;
            if (OVRInput.GetDown(OVRInput.Button.One))
                _onStepChanged.Invoke(_mainMenuStep);
        }
    }
}