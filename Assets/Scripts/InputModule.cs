using System;

namespace DeviceLogic
{
    public class InputModule
    {
        public Action ONButtonUp;
        public Action ONButtonDown;


        public void Check()
        {
            CheckForButtonDown();
            CheckForButtonUp();
        }
        
        private void CheckForButtonDown()
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) ONButtonDown?.Invoke();
        }

        private void CheckForButtonUp()
        {
            if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)) ONButtonUp?.Invoke();
        }
    }
}