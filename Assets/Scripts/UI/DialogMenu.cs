using System;
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


        private void OnEnable() => _onStepLoaded.AddAction(LoadData);

        private void OnDisable() => _onStepLoaded.RemoveAction(LoadData);

        private void LoadData(Step step)
        {
            if (!step.ContainsFeature(StepFeature.DialogMenu)) _parentObject.SetActive(false);
            else
            {
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