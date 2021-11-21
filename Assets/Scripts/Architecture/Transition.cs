using System;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace Architecture
{
    [Serializable]
    public class Transition
    {
        public Transition(Step nextStep, GameEvent condition)
        {
            _nextStep = nextStep;
            _condition = condition;
        }
        [SerializeField] private Step _nextStep;
        [SerializeField] private GameEvent _condition;
        public Action<Step> OnConditionComplete;

        public void WaitForCondition() => _condition.AddAction(CommitTransition);
        public void CommitTransition() => OnConditionComplete.Invoke(_nextStep);
        public void StopWaitingForCondition() => _condition.RemoveAction(CommitTransition);
    }
}