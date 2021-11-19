using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Voice;

namespace Architecture
{
    [InlineEditor()]
    [CreateAssetMenu(
        menuName = "Create Step",
        fileName = "Step",
        order = 120)]
    public class Step : SerializedScriptableObject
    {
        [SerializeField] private Transition[] _transitions;

        [OnValueChanged("DeleteDuplicates")] [SerializeField]
        private StepFeature[] _features;

        [ShowIf("@ContainsFeature(StepFeature.ModularMenu)")]
        [SerializeField] private ModularMenuData _modularMenuData;
        [ShowIf("@ContainsFeature(StepFeature.DialogMenu)")]
        [SerializeField] private DialogMenuData _dialogMenuData;
        [ShowIf("@ContainsFeature(StepFeature.VoiceActing)")]
        [SerializeField] private VoiceActingData _voiceActingData;
        [ShowIf("@ContainsFeature(StepFeature.VoiceListener)")]
        [SerializeField] private VoiceListenerData _voiceListenerData;
        [ShowIf("@ContainsFeature(StepFeature.DeviceAction)")]
        [SerializeField] private DeviceActionData _deviceActionData;
        
        
        private void DeleteDuplicates() => _features = _features.Distinct().ToArray();

        public Data GetFeatureData(StepFeature featureType)
        {
            if (!_features.Contains(featureType)) return null;
            return featureType switch
            {
                StepFeature.ModularMenu => _modularMenuData,
                StepFeature.DialogMenu => _dialogMenuData,
                StepFeature.DeviceAction => _deviceActionData,
                StepFeature.VoiceActing => _voiceActingData,
                StepFeature.VoiceListener => _voiceListenerData,
                _ => null
            };
        }

        public bool ContainsFeature(StepFeature feature) => _features.Contains(feature);

        public Transition[] Transitions => _transitions;
    }
}