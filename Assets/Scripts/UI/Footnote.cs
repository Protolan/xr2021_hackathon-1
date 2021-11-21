using TMPro;
using UnityEngine;

namespace UI
{
    public class Footnote: MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        public void UpdateFootnote(string text, Vector3 newPosition)
        {
            transform.position = newPosition;
            _text.SetText(text);
        }
    }
}