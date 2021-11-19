using System.ComponentModel;
using Architecture;
using Facebook.WitAi.Lib;
using Oculus.Voice;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace Voice
{
    public class VoiceListener: MonoBehaviour
    {
        [SerializeField] private AppVoiceExperience _voice;
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private string _text;
        private void OnEnable() => _onStepLoaded.AddAction(StartListeningIfHave);

        private void OnDisable() => _onStepLoaded.RemoveAction(StartListeningIfHave);

        public void OnResponse(WitResponseNode response)
        {
            if (!string.IsNullOrEmpty(response["text"]))
            {
                _text = "I heard: " + response["text"];
            }
        }

        private void StartListeningIfHave(Step step)
        {
            if (step.ContainsFeature(StepFeature.VoiceAnswer))
                _voice.Activate();
        }
    }
}