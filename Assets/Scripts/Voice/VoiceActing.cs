using System.Collections;
using Architecture;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace Voice
{
    public class VoiceActing: MonoBehaviour
    {
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private GameEvent _onStepEnded;
        [SerializeField] private GameEvent _onClipFinished;
        [SerializeField] private AudioSource _source;

        private Coroutine _currentPlaying;
        
        private void OnEnable()
        {
            _onStepLoaded.AddAction(StartActingIfHave);
            _onStepEnded.AddAction(StopClip);
        }

        private void OnDisable()
        {
            _onStepLoaded.RemoveAction(StartActingIfHave);
            _onStepEnded.RemoveAction(StopClip);
        }

        private void StopClip()
        { 
            if(_currentPlaying != null) StopCoroutine(_currentPlaying); 
            if(_source.isPlaying) _source.Stop();
        }

        private void StartActingIfHave(Step step)
        {
            if (step.ContainsFeature(StepFeature.VoiceActing)) 
                StartActing(step.ActingData);
        }

        private void StartActing(VoiceActingData data)
        {
            _source.clip = data._clip;
            _source.Play();
            _currentPlaying =  StartCoroutine(WaitForEnding(data._clip.length));
        }

        private IEnumerator WaitForEnding(float clipDuration)
        {
            yield return clipDuration;
            _onClipFinished.Invoke();
        }
    }
}