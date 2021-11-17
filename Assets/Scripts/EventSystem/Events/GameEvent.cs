using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ScriptableSystem.GameEvent
{
    [CreateAssetMenu(
        menuName = "Create GameEvent",
        fileName = "GameEvent", 
        order = 120)]
    public class GameEvent : SerializedScriptableObject
    {
        [SerializeField] private List<Action> _actions = new List<Action>();
        
        [Button]
        public void Invoke()
        {
            for (var i = _actions.Count - 1; i >= 0; i--)
            {
                _actions[i]?.Invoke();
            }
        }
        public void AddAction(Action action)
        {
            if(!_actions.Contains(action)) _actions.Add(action);
        }

        public void RemoveAction(Action action)
        {
            if(_actions.Contains(action)) _actions.Remove(action);
        }
    }
}