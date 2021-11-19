using ScriptableSystem.GameEvent;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Voice
{
    [CreateAssetMenu(
        menuName = "Create VoiceIntent",
        fileName = "VoiceIntent", 
        order = 120)]
    [InlineEditor()]
    public class VoiceIntent: GameEvent
    {
        [Range(0,1f)]
        [SerializeField] private float _confidence;

        [SerializeField] private string _name;
        
        [SerializeField] private bool _hasEntity;

        [ShowIf("_hasEntity")]
        [SerializeField] private string _entityName;
        
        public float Confidence => _confidence;

        public string Name => _name;

        public bool HasEntity => _hasEntity;

        public string EntityName => _entityName;
    }
}