using System;
using System.Collections.Generic;
using ScriptableSystem.GameEvent;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Voice;

namespace Architecture
{
    public class StepController : SerializedMonoBehaviour
    {
        [SerializeField] private List<Step> _steps;
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private GameEvent _onStepChanged;
        [SerializeField] private GameEvent _onStepEnded;

#if UNITY_EDITOR
        [Button]
        private void CreateStepsFromFile(VoiceIntent intent, AudioClip defaultClip)
        {
            CreateStepsFromFile("Assets/Resources/DialogMenuDictionary.txt");
            SetButtonActionSteps("Assets/Resources/AnnotationMenuDictionary.txt");
            SetStepsConditions(intent);
            SetListeners("Assets/Resources/VoiceListenerData.txt", intent, defaultClip);
            SetVoiceActor("Assets/Resources/Voice");
            SetCancelButtonSteps("Assets/Resources/CancelButton.txt");
            AssetDatabase.SaveAssets();
        }

        private void CreateStepsFromFile(string filePath)
        {
            var builder = new StepBuilder();
            _steps = builder.CreateStepsFromFile(filePath, _steps);
        }

        private void SetCancelButtonSteps(string filePath)
        {
            var builder = new StepBuilder();
            builder.SetCancelSteps(filePath, _steps);
        }

        [Button]
        private void SetMainMenuTransition(Step mainMenuStep, GameEvent condition)
        {
            var builder = new StepBuilder();
            builder.SetMainMenuExitSteps("Assets/Resources/MainMenuReturn.txt", _steps, mainMenuStep, condition);
            AssetDatabase.SaveAssets();
        }


        private void SetButtonActionSteps(string filePath)
        {
            var builder = new StepBuilder();
            builder.SetButtonActionFromFile(filePath, _steps);
        }


        private void SetStepsConditions(GameEvent condition)
        {
            var builder = new StepBuilder();
            builder.SetStepTransitions(_steps, condition);
        }

        private void SetVoiceActor(string audioFilesFolderPath)
        {
            var builder = new StepBuilder();
            builder.SetVoiceActingFromFiles(_steps, audioFilesFolderPath);
        }

        private void SetListeners(string filePath, VoiceIntent intent, AudioClip defaultClip)
        {
            var builder = new StepBuilder();
            builder.SetVoiceListenersFromFile(_steps, filePath, intent, defaultClip);
        }

        private void CreateStepsJson(string filePath)
        {
            var builder = new StepBuilder();
            builder.CreateStepsJSON(_steps, filePath);
        }

#endif


        private Step _currentStep;
        

        private void OnDisable()
        {
            UnsubscribeTransitions(_currentStep);
        }


        private void Start()
        {
            _currentStep = _steps[0];
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
                stepTransition.OnConditionComplete -= LoadNextStep;
                stepTransition.StopWaitingForCondition();
            }
        }

        private void LoadNextStep(Step step)
        {
            Debug.Log(step.name);
            UnsubscribeTransitions(_currentStep);
            Debug.Log("Unsubscribe");
            _currentStep = step;
            Debug.Log("Set Current Step");
            _onStepEnded.Invoke();
            Debug.Log("OnStepEndedInvoke");
            _onStepLoaded.Invoke(_currentStep);
            Debug.Log("OnStepLoadedInvoke");
            _onStepChanged.Invoke();
            Debug.Log("OnStepEndedChanged");
            SubscribeTransitions(_currentStep);
            Debug.Log("Subscribe");
        }
    }
}