using System;
using Architecture;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace UI
{
    public class ChoosingMenu: MonoBehaviour
    {
        [SerializeField] private StepGameEvent _onStepLoaded;

        private void OnEnable() => _onStepLoaded.AddAction(ShowIfHave);

        private void OnDestroy() => _onStepLoaded.RemoveAction(ShowIfHave);

        private void ShowIfHave(Step step)
        {
            gameObject.SetActive(step.ContainsFeature(StepFeature.ChoosingMenu));
        }
    }
}