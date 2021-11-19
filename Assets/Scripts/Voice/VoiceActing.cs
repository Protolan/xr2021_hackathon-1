using Architecture;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace Voice
{
    public class VoiceActing: MonoBehaviour
    {
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private AudioSource _source;
        private void OnEnable() => _onStepLoaded.AddAction(StartActingIfHave);

        private void OnDisable() => _onStepLoaded.RemoveAction(StartActingIfHave);

        private void StartActingIfHave(Step stepData)
        {
            if (stepData.ContainsFeature(StepFeature.VoiceActing)) 
                StartActing(stepData.GetFeatureData(StepFeature.VoiceActing) as VoiceActingData);
        }

        private void StartActing(VoiceActingData data)
        {
            _source.clip = data._clip;
            _source.Play();
        }
    }
}