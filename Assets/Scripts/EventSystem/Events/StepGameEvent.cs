using Architecture;
using ScriptableSystem.GameEventParameterized;
using UnityEngine;

namespace ScriptableSystem.GameEvent
{
    [CreateAssetMenu(
        menuName = "Create StepEvent",
        fileName = "StepEvent", 
        order = 120)]
    public class StepGameEvent: GameEventBase<Step>
    {
        
    }
}