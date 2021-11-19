using System;
using Architecture;
using UnityEngine;

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