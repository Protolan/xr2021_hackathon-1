using System.Linq;
using Architecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ModularMenu : UIElement
    {
        [SerializeField] private TMP_Text _mainText;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _imageText;
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private GameObject _parentObject;


        protected override void LoadData(Step step)
        {
            if (!step.ContainsFeature(StepFeature.ModularMenu)) _parentObject.SetActive(false);
            else
            {
                _parentObject.SetActive(true);
                UpdateContent(step.MenuData);
            }
        }

        private void DeactivateAll()
        {
            _mainText.gameObject.SetActive(false);
            _image.gameObject.SetActive(false);
            _imageText.gameObject.SetActive(false);
            _okButton.gameObject.SetActive(false);
            _cancelButton.gameObject.SetActive(false);
        }

        private void UpdateContent(ModularMenuData data)
        {
            DeactivateAll();
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
            
            _okButton.gameObject.SetActive(true);
            
        }
    }
}