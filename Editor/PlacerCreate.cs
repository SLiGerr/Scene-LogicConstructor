using Scene_LogicConstructor.Runtime;
using UnityEngine;

namespace Scene_LogicConstructor.Editor
{
    public static class PlacerCreate
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Tools/Scene-LogicConstructor/Add Logic Placer")]
#endif
        public static void Create()
        {
#if UNITY_EDITOR
            var find = Object.FindObjectOfType<LogicPlacer>();
             
            var placer = find != null ? find : new GameObject("Logic-Constructor").AddComponent<LogicPlacer>();
            placer.transform.SetSiblingIndex(0);

            if (placer.IsNull)
            {
                Debug.LogWarning("Please select any logic constructor template");
                UnityEditor.EditorUtility.OpenPropertyEditor(placer);
            }
            else UnityEditor.Selection.activeGameObject = placer.gameObject;
#endif
        }
    }
}