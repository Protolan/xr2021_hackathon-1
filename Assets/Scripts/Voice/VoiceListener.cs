using System.Collections;
using Architecture;
using Facebook.WitAi;
using Facebook.WitAi.CallbackHandlers;
using Facebook.WitAi.Lib;
using Oculus.Voice;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace Voice
{
    public class VoiceListener : WitResponseHandler
    {
        [SerializeField] private AppVoiceExperience _voice;
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private GameEvent _onVoiceActorFinished;
        [SerializeField] private GameEvent _onNotUnderstand;
        [SerializeField] private GameObject _microphoneUI;

        private VoiceListenerData _data;
        private bool _isActive;
        private Coroutine _coroutine;

        protected override void OnEnable()
        {
            base.OnEnable();
            _onStepLoaded.AddAction(StartListeningIfHave);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _onStepLoaded.RemoveAction(StartListeningIfHave);
            _onVoiceActorFinished.RemoveAction(WaitForVoiceActorFinished);
        }

        protected override void OnHandleResponse(WitResponseNode response)
        {
            if (!_isActive) return;
            WitResponseNode handleIntent = response.GetFirstIntent();
            if (!CheckForIntent(handleIntent))
            {
                _onNotUnderstand.Invoke();
                _coroutine = StartCoroutine(StartListening());
            } 
        }

        private bool CheckForIntent(WitResponseNode handleIntent)
        {
            foreach (var intent in _data._intents)
            {
                if (IntentEquals(intent, handleIntent))
                {
                    intent.Invoke();
                    return true;
                }
            }

            return false;
        }

        private bool IntentEquals(VoiceIntent intent, WitResponseNode handleIntent)
        {
            return intent.Name == handleIntent["name"].Value
                   && handleIntent["confidence"].Value.String2Float() >= intent.Confidence;
        }

        private void StartListeningIfHave(Step step)
        {
            if (step.ContainsFeature(StepFeature.VoiceListener))
            {
                _data = step.ListenerData;
                _onVoiceActorFinished.AddAction(WaitForVoiceActorFinished);
            }
            else
            {
                if(_coroutine != null) StopCoroutine(_coroutine);
                DeactivateListener();
                _onVoiceActorFinished.RemoveAction(WaitForVoiceActorFinished);
            }
        }

        private void DeactivateListener()
        {
            _isActive = false;
            _microphoneUI.gameObject.SetActive(true);
        }

        private void WaitForVoiceActorFinished()
        {
            _isActive = true;
            _coroutine = StartCoroutine(StartListening());
            _onVoiceActorFinished.RemoveAction(WaitForVoiceActorFinished);
        }

        private IEnumerator StartListening()
        {
            yield return new WaitForSeconds(0.5f);
            _voice.Activate();
            _microphoneUI.gameObject.SetActive(false);
        }
        
    }
}