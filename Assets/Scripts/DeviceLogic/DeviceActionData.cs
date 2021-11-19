using System;
using Sirenix.OdinInspector;

namespace Architecture
{
    [Serializable]
    public class DeviceActionData: Data
    {
        public override StepFeature FeatureType => StepFeature.DeviceAction;
    }
}