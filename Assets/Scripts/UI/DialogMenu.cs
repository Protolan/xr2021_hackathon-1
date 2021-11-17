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

        protected override void LoadData(Step step)
        {
            if (!step.ContainsFeature(StepFeature.DialogMenu)) gameObject.SetActive(false);
            else
            {
                gameObject.SetActive(true);
                UpdateContent(step.DialogMenuData);
            }
        }

        private void UpdateContent(DialogMenuData data) => _text.SetText(data._text);
    }
}