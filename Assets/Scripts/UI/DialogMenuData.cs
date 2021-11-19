using System;

namespace Architecture
{
    [Serializable]
    public class DialogMenuData: Data
    {
        public string _text;
        public bool _hasButton = true;
        public override StepFeature FeatureType => StepFeature.DialogMenu;
    }
}