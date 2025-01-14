﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private DeviceButton[] _buttonArray;

        [Button]
        private void MakeDictionary(string filePath)
        {
            foreach (var button in _buttonArray) button.SetFootnotePoint();
            _buttons = _buttonArray.ToDictionary(x => x.name);
        }
        
        private DeviceButton _currentButton;

        private void Start()
        {
            foreach (var button in _buttonArray)
            {
                _footnote.gameObject.SetActive(false);
                button.gameObject.SetActive(false);
            }
        }

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
            if (step.ContainsFeature(StepFeature.DeviceAction))
            {
                Debug.Log(step.DeviceButtonActionData._buttonID);
                if (!_buttons.ContainsKey(step.DeviceButtonActionData._buttonID)) return;
                _currentButton = _buttons[step.DeviceButtonActionData._buttonID];
                _currentButton.Activate();
                _footnote.gameObject.SetActive(true);
                _footnote.UpdateFootnote(step.DeviceButtonActionData._footnoteText, _currentButton.FootnotePoint.position);
            }
        }
    }
}