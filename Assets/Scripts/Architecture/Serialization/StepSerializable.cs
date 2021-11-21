using System;
using System.Collections.Generic;
using Voice;

namespace Architecture
{
    [Serializable]
    public class StepSerializable
    {
        public StepSerializable(Step step)
        {
            _transitions = step.Transitions;
            _features = step.Features;
            _modularMenuData = step.ModularMenuData;
            _deviceButtonActionData = step.DeviceButtonActionData;
            _dialogDialogMenuData = step.DialogMenuData;
            _voiceActingData = step.ActingData;
            _voiceListenerData = step.ListenerData;
        }
        
        public List<Transition> _transitions;
        public List<StepFeature> _features;
        public ModularMenuData _modularMenuData;
        public DialogMenuData _dialogDialogMenuData;
        public VoiceActingData _voiceActingData;
        public VoiceListenerData _voiceListenerData;
        public DeviceButtonActionData _deviceButtonActionData;
    }
}