using Architecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DialogMenu : UIElement
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _speakerImage;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _parentObject;

        protected override void LoadData(Step step)
        {
            if (!step.ContainsFeature(StepFeature.DialogMenu)) _parentObject.SetActive(false);
            else
            {
                _parentObject.SetActive(true);
                UpdateContent(step.DialogMenuData);
            }
        }

        private void UpdateContent(DialogMenuData data) => _text.SetText(data._text);
    }
}