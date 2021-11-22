using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Architecture
{
    [Serializable]
    public class ModularMenuData
    {
        public string _mainText;

        [ShowIf("@ContainsFeature(ModularMenuFeature.Image)")]
        public string _imageText;
        
        [ShowIf("@ContainsFeature(ModularMenuFeature.Image)")]
        public AnimationType _animation;
        

        public ModularMenuFeature[] _features = Array.Empty<ModularMenuFeature>();

        private bool ContainsFeature(ModularMenuFeature feature) => _features.Contains(feature);
    }
}