using UnityEngine;

namespace DeviceLogic
{
    public abstract class DeviceButton: MonoBehaviour
    {
        [SerializeField] private Transform _footnotePoint;

        public Transform FootnotePoint => _footnotePoint;

        public virtual void Activate()
        {
            
        }

        public virtual void Deactivate()
        {
            
        }
    }
}