using System;
using Architecture;
using UnityEngine;

namespace Voice
{
    [Serializable]
    public class VoiceListenerData
    {
        public float _listenDuration;
        public VoiceIntent[] _intents;
    }
}