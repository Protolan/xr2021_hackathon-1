using System;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace Architecture
{
    public class StepController: MonoBehaviour
    {
        [SerializeField] private Step _firstStep;
        [SerializeField] private Step[] _steps;
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private GameEvent _onStepChanged;
        [SerializeField] private GameEvent _onStepEnded;

        private Step _currentStep;

        private void Start()
        {
            _currentStep = _firstStep;
            _onStepLoaded.Invoke(_currentStep);
            SubscribeTransitions(_currentStep);
            _onStepChanged.Invoke();
        }

        private void SubscribeTransitions(Step step)
        {
            foreach (var stepTransition in step.Transitions)
            {
                stepTransition.WaitForCondition();
                stepTransition.OnConditionComplete += LoadNextStep;
            }
        }

        private void UnsubscribeTransitions(Step step)
        {
            foreach (var stepTransition in step.Transitions)
            {
                stepTransition.StopWaitingForCondition();
                stepTransition.OnConditionComplete -= LoadNextStep;
            }
        }

        private void OnDisable() => UnsubscribeTransitions(_currentStep);

        private void LoadNextStep(Step step)
        {
            UnsubscribeTransitions(_currentStep);
            _currentStep = step;
            _onStepEnded.Invoke();
            _onStepLoaded.Invoke(_currentStep);
            _onStepChanged.Invoke();
            SubscribeTransitions(_currentStep);
        }
    }
}