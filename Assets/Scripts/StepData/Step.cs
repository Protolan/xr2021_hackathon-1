using System;
using System.Linq;
using ScriptableSystem.GameEvent;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Architecture
{
    [InlineEditor()]
    [CreateAssetMenu(
        menuName = "Create Step",
        fileName = "Step",
        order = 120)]
    public class Step : ScriptableObject
    {
        [SerializeField] private StepFeature[] _features;
        [SerializeField] private Transition[] _transitions;
        
        
        
        
        public bool ContainsFeature(StepFeature feature) => _features.Contains(feature);

        [ShowIf("@ContainsFeature(StepFeature.ModularMenu)")] [SerializeField]
        private ModularMenuData _modularMenuData;
        
        [ShowIf("@ContainsFeature(StepFeature.DialogMenu)")] [SerializeField]
        private DialogMenuData _dialogMenuData;
        [ShowIf("@ContainsFeature(StepFeature.VoiceActing)")] [SerializeField]
        private VoiceActingData _voiceActingData;
        [ShowIf("@ContainsFeature(StepFeature.DeviceAction)")] [SerializeField]
        private DeviceActionData _deviceActionData;
     

        public ModularMenuData MenuData => _modularMenuData;

        public DialogMenuData DialogMenuData => _dialogMenuData;

        public VoiceActingData ActingData => _voiceActingData;

        public Transition[] Transitions => _transitions;
    }

    [Serializable]
    public class Transition
    {
        [SerializeField] private Step _nextStep;
        [SerializeField] private GameEvent _condition;
        public Action<Step> OnConditionComplete;

        public void WaitForCondition() => _condition.AddAction(CommitTransition);
        public void CommitTransition() => OnConditionComplete.Invoke(_nextStep);
        public void StopWaitingForCondition() => _condition.RemoveAction(CommitTransition);
    }
}