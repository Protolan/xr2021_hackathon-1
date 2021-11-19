using System;
using Architecture;

namespace Voice
{
    [Serializable]
    public class VoiceListenerData: Data
    {
        public float _listenDuration;
        public VoiceIntent[] _intents;
        public override StepFeature FeatureType => StepFeature.VoiceListener;
    }
}