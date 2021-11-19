using System;
using UnityEngine;

namespace Architecture
{
    [Serializable]
    public class VoiceActingData: Data
    {
        public AudioClip _clip;
        public override StepFeature FeatureType => StepFeature.VoiceActing;
    }
}