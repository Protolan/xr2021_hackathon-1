using System;
using UnityEngine;

namespace DeviceLogic
{
    public class UserInput
    {
        public Action ONButtonUp;
        public Action ONButtonDown;

        public void ResetInput()
        {
            ONButtonUp = null;
            ONButtonDown = null;
        }

        public void Check()
        {
            CheckForButtonDown();
            CheckForButtonUp();
        }
        
        private void CheckForButtonDown()
        {
            Debug.Log("InvokeButtonDown!");
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) ONButtonDown?.Invoke();
        }

        private void CheckForButtonUp()
        {
            Debug.Log("InvokeButtonUp!");
            if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)) ONButtonUp?.Invoke();
        }
    }
}