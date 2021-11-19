using System;

namespace Architecture
{
    [Serializable]
    public class DialogMenuData: Data
    {
        public string _text;
        public override StepFeature FeatureType => StepFeature.DialogMenu;
    }
}