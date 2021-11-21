using System;
using Voice;

namespace Architecture
{
    [Serializable]
    public class StepSerializable
    {
        public Transition[] _transitions;
        public StepFeature[] _features;
        public ModularMenuData _modularMenuData;
        public DialogMenuData _dialogDialogMenuData;
        public VoiceActingData _voiceActingData;
        public VoiceListenerData _voiceListenerData;
        public DeviceButtonActionData _deviceButtonActionData;
    }
}