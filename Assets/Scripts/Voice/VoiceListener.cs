using System.Collections;
using System.Globalization;
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

        private VoiceListenerData _data;
        private bool _isActive;

        protected override void OnEnable()
        {
            base.OnEnable();
            _onStepLoaded.AddAction(StartListeningIfHave);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _onStepLoaded.RemoveAction(StartListeningIfHave);
        }

        protected override void OnHandleResponse(WitResponseNode response)
        {
            if (!_isActive) return;
            Debug.Log("Запрос принял");
            WitResponseNode handleIntent = response.GetFirstIntent();
            var entityName = handleIntent["entities"][0].Value;
            if (!CheckForIntent(handleIntent))
            {
                Debug.Log("Намерение не найдено, пробуем еще раз");
                _voice.Activate();
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
                   && String2Float(handleIntent["confidence"].Value) >= intent.Confidence;
        }

        private void StartListeningIfHave(Step step)
        {
            if (step.ContainsFeature(StepFeature.VoiceListener))
            {
                Debug.Log("Начинаю слушать");
                _isActive = true;
                _data = step.GetFeatureData(StepFeature.VoiceListener) as VoiceListenerData;
                StartCoroutine(StartListening());
            }
            else
                _isActive = false;
        }

        private IEnumerator StartListening()
        {
            yield return new WaitForSeconds(1f);
            _voice.Activate();
        }

        private float String2Float(string line)
        {
            float res;
            try
            {
                string sp = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
                line = line.Replace(".", sp);
                line = line.Replace(",", sp);
                res = float.Parse(line);
            }
            catch
            {
                res = 0f;
            }

            return res;
        }
    }
}