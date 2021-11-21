using System.Collections.Generic;
using ScriptableSystem.GameEvent;
using Sirenix.OdinInspector;
using UnityEngine;
using Voice;

namespace Architecture
{
    public class StepController : SerializedMonoBehaviour
    {
        [SerializeField] private Step _firstStep;
        [SerializeField] private List<Step> _steps;
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private GameEvent _onStepChanged;
        [SerializeField] private GameEvent _onStepEnded;


        [Button]
        private void CreateStepsFromFile(string filePath)
        {
            var builder = new StepBuilder();
            _steps = builder.CreateStepsFromFile(filePath);
        }

        [Button]
        private void SetButtonActionSteps(string filePath)
        {
            var builder = new StepBuilder();
            builder.SetButtonActionFromFile(filePath, _steps);
        }

        [Button]
        private void SetStepsConditions(GameEvent condition)
        {
            var builder = new StepBuilder();
            builder.SetStepTransitions(_steps, condition);
        }

        [Button]
        private void SetVoiceActor(string audioFilesFolderPath)
        {
            var builder = new StepBuilder();
            builder.SetVoiceActingFromFiles(_steps, audioFilesFolderPath);
        }

        [Button]
        private void SetListeners(string filePath, VoiceIntent intent)
        {
            var builder = new StepBuilder();
            builder.SetVoiceListenersFromFile(_steps, filePath, intent);
        }


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