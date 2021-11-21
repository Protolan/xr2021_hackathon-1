using System;
using ScriptableSystem.GameEvent;
using UnityEngine;
using Voice;

namespace Architecture
{
    public class PlayerChooseController: MonoBehaviour
    {
        [SerializeField] private VoiceIntent _onWoolIntent;
        [SerializeField] private VoiceIntent _onCottonIntent;
        [SerializeField] private VoiceIntent _onSneakersIntent;
        [SerializeField] private VoiceIntent _onJeansIntent;
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private VoiceIntent _onConfirm;
        [SerializeField] private GameEvent _onWoolChoose;
        [SerializeField] private GameEvent _onCottonChoose;
        [SerializeField] private GameEvent _onSneakersChoose;
        [SerializeField] private GameEvent _onJeansChoose;

        private TypeOfClothing _chooseClothing = TypeOfClothing.Cotton;
        
        private void OnEnable()
        {
            _onWoolIntent.AddAction(ChooseWool);
            _onCottonIntent.AddAction(ChooseCotton);
            _onSneakersIntent.AddAction(ChooseSneakers);
            _onJeansIntent.AddAction(ChooseJeans);
            _onStepLoaded.AddAction(InvokeIfConfirmButtonPressed);
        }

        private void InvokeIfConfirmButtonPressed(Step step)
        {
            if (step.ContainsFeature(StepFeature.PlayerChoose)) 
                _onConfirm.AddAction(InvokeChoose);
        }
        
        private void OnDisable()
        {
            _onWoolIntent.RemoveAction(ChooseWool);
            _onCottonIntent.RemoveAction(ChooseCotton);
            _onSneakersIntent.RemoveAction(ChooseSneakers);
            _onJeansIntent.RemoveAction(ChooseJeans);
            _onStepLoaded.RemoveAction(InvokeIfConfirmButtonPressed);
            _onConfirm.RemoveAction(InvokeChoose);
        }

        private void InvokeChoose()
        {
            _onConfirm.RemoveAction(InvokeChoose);
            switch (_chooseClothing)
            {
                case TypeOfClothing.Wool:
                    _onWoolChoose.Invoke();
                    break;
                case TypeOfClothing.Cotton:
                    _onCottonChoose.Invoke();
                    break;
                case TypeOfClothing.Jeans:
                    _onJeansChoose.Invoke();
                    break;
                case TypeOfClothing.SportSneakers:
                    _onSneakersChoose.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChooseWool() => _chooseClothing = TypeOfClothing.Wool;

        private void ChooseCotton() => _chooseClothing = TypeOfClothing.Cotton;

        private void ChooseSneakers() => _chooseClothing = TypeOfClothing.SportSneakers;

        private void ChooseJeans() => _chooseClothing = TypeOfClothing.Jeans;
    }
}