using DeviceLogic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ChoosingMenu: MonoBehaviour
    {
        [SerializeField] private Device[] _devices;
        [SerializeField] private TMP_Dropdown _dropdown;
    }
}