using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ScriptableSystem.GameEventParameterized
{
    public class GameEventBase<TData> : SerializedScriptableObject
    {
        [SerializeField] private List<Action<TData>> _actions = new List<Action<TData>>();
        
        public void AddAction(Action<TData> action)
        {
            if(!_actions.Contains(action)) _actions.Add(action);
        }

        public void RemoveAction(Action<TData> action)
        {
            if(_actions.Contains(action)) _actions.Remove(action);
        }
        
        public void Invoke(TData value)
        {
            Debug.Log(_actions.Count);
            foreach (var action in _actions)
            {
                Debug.Log(action.Method.Name);
                action?.Invoke(value);
            }
        }
    }
}