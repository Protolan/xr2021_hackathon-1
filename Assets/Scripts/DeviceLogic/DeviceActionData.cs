using System;
using DeviceLogic;
using OVR;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Architecture
{
    [Serializable]
    public class DeviceActionData: Data
    {
        [SerializeField] private int _buttonNumber;
        public override StepFeature FeatureType => StepFeature.DeviceAction;
    }
}