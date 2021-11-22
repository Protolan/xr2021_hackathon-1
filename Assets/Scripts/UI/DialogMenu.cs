using System;
using System.Collections;
using Architecture;
using ScriptableSystem.GameEvent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DialogMenu: MonoBehaviour
    {
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _parentObject;
        [SerializeField] private FloatGameEvent _onNotUnderstandSpeak;


        private Step _currentStep;
        private Coroutine _currentCoroutine;
        
        private void OnEnable()
        {
            _onStepLoaded.AddAction(LoadData);
            _onNotUnderstandSpeak.AddAction(ShowNotUnderstandText);
        }

        private void OnDisable()
        {
            _onStepLoaded.RemoveAction(LoadData);
            _onNotUnderstandSpeak.RemoveAction(ShowNotUnderstandText);
        }

        private void ShowNotUnderstandText(float duration)
        {
            if (_currentStep != null) 
                _currentCoroutine = StartCoroutine(ReturnDefaultText(duration));

        }

        private IEnumerator ReturnDefaultText(float duration)
        {
            _text.SetText(_currentStep.ListenerData._notUnderStandText);
            yield return new WaitForSeconds(duration);
            _text.SetText(_currentStep.DialogMenuData._text);
        }

        private void LoadData(Step step)
        {
            if (!step.ContainsFeature(StepFeature.DialogMenu)) _parentObject.SetActive(false);
            else
            {
                if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
                _currentStep = step;
                _parentObject.SetActive(true);
                UpdateContent(step.DialogMenuData);
            }
        }

        private void UpdateContent(DialogMenuData data)
        {
            _button.gameObject.SetActive(data._hasButton);
            _text.SetText(data._text);
        }
    }
}