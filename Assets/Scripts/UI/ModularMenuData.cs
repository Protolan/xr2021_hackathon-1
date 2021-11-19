using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Architecture
{
    [Serializable]
    public class ModularMenuData: Data
    {
        public override StepFeature FeatureType => StepFeature.ModularMenu;
        public string _mainText;

        [ShowIf("@ContainsFeature(ModularMenuFeature.Image)")]
        public Sprite _sprite;

        [ShowIf("@ContainsFeature(ModularMenuFeature.Image)")]
        public string _imageText;

        [ShowIf("@ContainsFeature(ModularMenuFeature.ChoosingMenu)")] [SerializeField]
        private DeviceChoosingData _deviceActionData;

        public ModularMenuFeature[] _features = Array.Empty<ModularMenuFeature>();

        private bool ContainsFeature(ModularMenuFeature feature) => _features.Contains(feature);
    }
}