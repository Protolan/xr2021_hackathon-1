using Architecture;
using UnityEngine;

namespace UI
{
    public abstract class UIElement: MonoBehaviour
    {
        public abstract void LoadData(Step step);
    }
}