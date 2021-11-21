using Sirenix.OdinInspector;
using UnityEngine;

namespace DeviceLogic
{
    public class DeviceButton: MonoBehaviour
    {
        [ChildGameObjectsOnly]
        [SerializeField] private Transform _footnotePoint;
        public Transform FootnotePoint => _footnotePoint;

        public void SetFootnotePoint() => _footnotePoint = GetComponentsInChildren<Transform>()[1];

        public virtual void Activate() => gameObject.SetActive(true);

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}