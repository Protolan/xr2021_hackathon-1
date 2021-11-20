using System;
using System.Collections.Generic;
using ScriptableSystem.GameEvent;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Architecture
{
    public class StepController : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<string, string> _dictionary;
        [SerializeField] private Step _firstStep;
        [SerializeField] private Step[] _steps;
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private GameEvent _onStepChanged;
        [SerializeField] private GameEvent _onStepEnded;


        [Button]
        public void CreateDictionaryFromFile(string filePath)
        {
            var fileReader = new FileReader(filePath);
            _dictionary = fileReader.CreateTextDictionary();
        }

        [Button]
        public void SetTextToSteps()
        {
            foreach (var step in _steps)
            {
                if(!_dictionary.ContainsKey(step.name)) continue;
                step.SetText(_dictionary[step.name]);
            }
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