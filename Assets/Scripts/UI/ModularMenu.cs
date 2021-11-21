using System;
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
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _imageText;
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private GameObject _parentObject;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private GameObject _twoButtonModule;
        [SerializeField] private Button _redrawButton;

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
            _image.gameObject.SetActive(false);
            _imageText.gameObject.SetActive(false);
            _okButton.gameObject.SetActive(false);
            _cancelButton.gameObject.SetActive(false);
            _redrawButton.gameObject.SetActive(false);
            _title.gameObject.SetActive(false);
            _twoButtonModule.gameObject.SetActive(false);
        }

        private void UpdateContent(ModularMenuData data)
        {
            DeactivateAll();
            if (data._features.Contains(ModularMenuFeature.OkButton))
            {
                _okButton.gameObject.SetActive(true);
            }

            if (data._features.Contains(ModularMenuFeature.Title))
            {
                _title.SetText(data._titleText);
                _title.gameObject.SetActive(true);
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
                _image.sprite = data._sprite;
                _imageText.SetText(data._imageText);
                _image.gameObject.SetActive(true);
                _image.gameObject.SetActive(true);
            }
            else
            {
                _mainText.SetText(data._mainText);
                _mainText.gameObject.SetActive(true);
            }
        }
    }
}