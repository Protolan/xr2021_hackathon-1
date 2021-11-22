using ScriptableSystem.GameEvent;
using UnityEngine;

namespace UI
{
    public class UIStand: MonoBehaviour
    {
        private Vector3 _startPosition;
        
        
        
        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update() => transform.position = _startPosition;
    }
}