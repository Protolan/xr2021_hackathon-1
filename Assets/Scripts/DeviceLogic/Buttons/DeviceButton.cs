using UnityEngine;

namespace DeviceLogic
{
    public abstract class DeviceButton: MonoBehaviour
    {
        [SerializeField] private Canvas _footnote;
        
        public virtual void Activate() => _footnote.gameObject.SetActive(true);

        public virtual void Deactivate() => _footnote.gameObject.SetActive(false);
    }
}