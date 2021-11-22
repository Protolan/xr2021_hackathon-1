using System;
using Architecture;
using ScriptableSystem.GameEvent;
using UnityEngine;

namespace DeviceLogic.DeviceDrawer
{
    public class ObjectToDeactive: MonoBehaviour
    {
        [SerializeField] private StepGameEvent _onStepLoaded;
        [SerializeField] private Step _stepToDeactivate;

        private void OnEnable() => _onStepLoaded.AddAction(Deactivate);

        private void OnDestroy() => _onStepLoaded.RemoveAction(Deactivate);

        private void Deactivate(Step step)
        {
            if (step == _stepToDeactivate) gameObject.SetActive(false);
        }
    }
}