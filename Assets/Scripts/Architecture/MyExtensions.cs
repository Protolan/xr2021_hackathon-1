using System.Globalization;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using Architecture;
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Voice
{
    public static class MyExtensions
    {
        public static float String2Float(this string line)
        {
            float res;
            try
            {
                string sp = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
                line = line.Replace(".", sp);
                line = line.Replace(",", sp);
                res = float.Parse(line);
            }
            catch
            {
                res = 0f;
            }

            return res;
        }

#if UNITY_EDITOR
        public static void MakeDirty(this Step @object)
        {
            if (@object != null)
                EditorUtility.SetDirty(@object);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
#endif
    }
}