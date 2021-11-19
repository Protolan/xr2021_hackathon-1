using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Architecture
{
    [InlineEditor()]
    [CreateAssetMenu(
        menuName = "Create Step",
        fileName = "Step",
        order = 120)]
    public class Step : SerializedScriptableObject
    {
        [SerializeField] private Transition[] _transitions;

        
        [OnValueChanged("UpdateFeatures")]
        [DictionaryDrawerSettings(KeyLabel = "Feature Type", ValueLabel = "Feature Data")]
        [SerializeField] private Dictionary<StepFeature, Data> _features = new Dictionary<StepFeature, Data>();

        private void UpdateFeatures()
        {
            var list = _features.Where(feature => feature.Key != feature.Value.FeatureType);
            foreach (var pair in list)
            {
                _features.Remove(pair.Key);
            }
        }

        public Data GetFeatureData(StepFeature featureType) => _features[featureType];

        public bool ContainsFeature(StepFeature feature) => _features.ContainsKey(feature);

        public Transition[] Transitions => _transitions;
    }
}