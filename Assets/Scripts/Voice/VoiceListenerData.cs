using System;
using Architecture;
using UnityEngine;

namespace Voice
{
    [Serializable]
    public class VoiceListenerData
    {
        public AudioClip _notUnderStandClip;
        public VoiceIntent[] _intents;
    }
}