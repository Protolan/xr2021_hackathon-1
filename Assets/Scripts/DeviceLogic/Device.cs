using System.Collections.Generic;
using Architecture;
using ScriptableSystem.GameEvent;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

namespace DeviceLogic
{
    public class Device: SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<string, DeviceButton> _buttons;
        [SerializeField] private StepGameEvent _onStepChanged;
        [SerializeField] private Footnote _footnote;

        private DeviceButton _currentButton;
        
        private void OnEnable() => _onStepChanged.AddAction(StartActionIfHave);

        private void OnDisable() => _onStepChanged.RemoveAction(StartActionIfHave);

        private void StartActionIfHave(Step step)
        {
            if (_currentButton != null)
            {
                _currentButton.Deactivate();
                _footnote.gameObject.SetActive(false);
                _currentButton = null;
            }
            if (!step.ContainsFeature(StepFeature.DeviceAction))
            {
                _currentButton = _buttons[step.DeviceButtonActionData._buttonID];
                _currentButton.Activate();
                _footnote.UpdateFootnote(step.DeviceButtonActionData._footnoteText, _currentButton.FootnotePoint.position);
            }
        }
    }
}