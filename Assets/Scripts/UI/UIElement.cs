using Architecture;
using ScriptableSystem.GameEvent;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public abstract class UIElement : MonoBehaviour
    {
        [SerializeField] private StepGameEvent _onStepLoaded;
        
        
        
        protected void OnEnable() => _onStepLoaded.AddAction(LoadData);

        protected void OnDisable() => _onStepLoaded.RemoveAction(LoadData);

        [Button]
        protected abstract void LoadData(Step step);
    }
}