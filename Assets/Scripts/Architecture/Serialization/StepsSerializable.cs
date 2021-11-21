using System;

namespace Architecture
{
    [Serializable]
    public class StepsSerializable
    {
        public StepSerializable[] _steps;

        public StepsSerializable(StepSerializable[] steps) => _steps = steps;
    }
}