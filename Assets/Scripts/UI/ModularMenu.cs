﻿using System;
using System.Linq;
using Architecture;
using ScriptableSystem.GameEvent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ModularMenu : MonoBehaviour
    {
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private TMP_Text _mainText;
        [SerializeField] private TMP_Text _imageText;
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private GameObject _parentObject;
        [SerializeField] private GameObject _twoButtonModule;
        [SerializeField] private Button _redrawButton;
        [SerializeField] private Animator _imageAnimator;
        [SerializeField] private GameObject _image;
        private static readonly int Step2 = Animator.StringToHash("Step2");

        private void OnEnable() => _onStepLoaded.AddAction(LoadData);

        private void OnDisable() => _onStepLoaded.RemoveAction(LoadData);


        private void LoadData(Step step)
        {
            if (!step.ContainsFeature(StepFeature.ModularMenu)) _parentObject.SetActive(false);
            else
            {
                _parentObject.SetActive(true);
                UpdateContent(step.ModularMenuData);
            }
        }

        private void DeactivateAll()
        {
            _mainText.gameObject.SetActive(false);
            _imageText.gameObject.SetActive(false);
            _okButton.gameObject.SetActive(false);
            _cancelButton.gameObject.SetActive(false);
            _redrawButton.gameObject.SetActive(false);
            _twoButtonModule.gameObject.SetActive(false);
            _image.gameObject.SetActive(false);
        }

        private void UpdateContent(ModularMenuData data)
        {
            DeactivateAll();
            if (data._features.Contains(ModularMenuFeature.OkButton))
            {
                _okButton.gameObject.SetActive(true);
            }
            

            if (data._features.Contains(ModularMenuFeature.Cancel))
            {
                _twoButtonModule.gameObject.SetActive(true);
                _cancelButton.gameObject.SetActive(true);
            }

            if (data._features.Contains(ModularMenuFeature.Redraw))
            {
                _twoButtonModule.gameObject.SetActive(true);
                _cancelButton.gameObject.SetActive(false);
                _redrawButton.gameObject.SetActive(true);
            }

            if (data._features.Contains(ModularMenuFeature.Image))
            {
                _image.gameObject.SetActive(true);
                _imageAnimator.SetBool(Step2, data._animation == AnimationType.SettingHeight);
            }
            else
            {
                _mainText.SetText(data._mainText);
                _mainText.gameObject.SetActive(true);
            }
        }
    }
}