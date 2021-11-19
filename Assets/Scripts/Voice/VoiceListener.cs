using Architecture;
using Facebook.WitAi;
using Facebook.WitAi.CallbackHandlers;
using Facebook.WitAi.Lib;
using Oculus.Voice;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace Voice
{
    public class VoiceListener: WitResponseHandler
    {
        [SerializeField] private AppVoiceExperience _voice;
        [SerializeField] private StepGameEvent _onStepLoaded;

        private VoiceListenerData _data;
        private void OnEnable() => _onStepLoaded.AddAction(StartListeningIfHave);
        private void OnDisable() => _onStepLoaded.RemoveAction(StartListeningIfHave);
        protected override void OnHandleResponse(WitResponseNode response)
        {
            WitResponseNode handleIntent = response.GetFirstIntent();
            foreach (var intent in _data._intents)
            {
                if (IntentEquals(intent, handleIntent))
                {
                    intent.Invoke();
                    return;
                }
            }
        }

        private static bool IntentEquals(VoiceIntent intent, WitResponseNode handleIntent)
        {
            WitResponseNode response;
            return intent.Name == handleIntent["name"].Value
                   && float.Parse(handleIntent["confidence"].Value) >= intent.Confidence;
        }

        private void StartListeningIfHave(Step step)
        {
            if (step.ContainsFeature(StepFeature.VoiceListener))
            {
                _data = step.GetFeatureData(StepFeature.VoiceActing) as VoiceListenerData;
                _voice.Activate();
            }
        }

      
    }
}