using Architecture;
using ScriptableSystem.GameEvent;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace ScriptableSystem.GameEventParameterized
{
    [CreateAssetMenu(
        menuName = "Create EventLocator",
        fileName = "EventLocator",
        order = 120)]
    public class EventLocator : SerializedScriptableObject
    {
#if UNITY_EDITOR
        private const string EventFolderPath = "Assets/Main/Events/Test";

        [Button]
        private static void UpdateGameEvents()
        {
            OnStepLoaded = RegisterEvent<Step>("OnStepLoaded");
            AssetDatabase.SaveAssets();
        }

        
        private static GameEventBase<T> RegisterEvent<T>(string eventName)
        {
            var gameEvent = CreateInstance<GameEventBase<T>>();
            AssetDatabase.CreateAsset(gameEvent, $"{EventFolderPath}/{typeof(T).Name}/{eventName}");
            return gameEvent;
        }

#endif

        [ShowInInspector]
        public static GameEventBase<Step> OnStepLoaded;
    }
}