using UnityEngine;

namespace DeviceLogic
{
    public class Device: MonoBehaviour
    {
        [SerializeField] private DeviceButton[] _buttons;

        public static int ButtonCount;
        private void OnValidate() => ButtonCount = _buttons.Length;
    }
}