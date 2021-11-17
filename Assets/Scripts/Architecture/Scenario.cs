using UnityEngine;

namespace Architecture
{
    [CreateAssetMenu(
        menuName = "Create Scenario", 
        fileName = "Scenario", 
        order = 120)]
    public class Scenario : ScriptableObject
    {
        [SerializeField] private Step[] _steps;

        public void Start()
        {
        
        }

        public void Break()
        {
        
        }
    }
}