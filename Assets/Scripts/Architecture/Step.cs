﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Voice;

namespace Architecture
{
    [CreateAssetMenu(
        menuName = "Create Step",
        fileName = "Step",
        order = 120)]
    public class Step : ScriptableObject
    {
        [SerializeField] private List<Transition> _transitions = new List<Transition>();
        [OnValueChanged("DeleteDuplicates")]  
        [SerializeField] private List<StepFeature> _features = new List<StepFeature>();
        [ShowIf("@ContainsFeature(StepFeature.ModularMenu)")]
        [SerializeField] private ModularMenuData _modularMenuData;
        [ShowIf("@ContainsFeature(StepFeature.DialogMenu)")]
        [SerializeField] private DialogMenuData _dialogDialogMenuData;
        [ShowIf("@ContainsFeature(StepFeature.VoiceActing)")]
        [SerializeField] private VoiceActingData _voiceActingData;
        [ShowIf("@ContainsFeature(StepFeature.VoiceListener)")]
        [SerializeField] private VoiceListenerData _voiceListenerData;
        [ShowIf("@ContainsFeature(StepFeature.DeviceAction)")]
        [SerializeField] private DeviceButtonActionData _deviceButtonActionData;

#if UNITY_EDITOR
        
        private void OnValidate() => this.MakeDirty();
#endif

        private void DeleteDuplicates() => _features = Features.Distinct().ToList();

        public bool ContainsFeature(StepFeature feature) => Features.Contains(feature);

        public List<Transition> Transitions => _transitions;

        public DeviceButtonActionData DeviceButtonActionData => _deviceButtonActionData;

        public VoiceListenerData ListenerData => _voiceListenerData;

        public VoiceActingData ActingData => _voiceActingData;

        public DialogMenuData DialogMenuData => _dialogDialogMenuData;

        public ModularMenuData ModularMenuData => _modularMenuData;

        public List<StepFeature> Features => _features;
    }
}