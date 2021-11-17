using ScriptableSystem.GameEvent;
using UnityEngine;

namespace Voice
{
    public class VoiceListener: MonoBehaviour
    {
        [SerializeField] private GameEvent _onStartListening;
        [SerializeField] private AudioSource _source;
        private void OnEnable() => _onStartListening.AddAction(StartListening);

        private void OnDisable() => _onStartListening.RemoveAction(StartListening);

        private void StartListening()
        {
            
        }
    }
}