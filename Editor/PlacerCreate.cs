using Scene_LogicConstructor.Runtime;
using UnityEditor;
using UnityEngine;

namespace Scene_LogicConstructor.Editor
{
    public static class PlacerCreate
    {
        [MenuItem("Tools/Scene-LogicConstructor/Add Logic Placer")]
        public static void Create()
        {
            var find = Object.FindObjectOfType<LogicPlacer>();
             
            var placer = find != null ? find : new GameObject("Logic-Constructor").AddComponent<LogicPlacer>();
            placer.transform.SetSiblingIndex(0);

            if (placer.IsNull)
            {
                Debug.LogWarning("Please select any logic constructor template");
                EditorUtility.OpenPropertyEditor(placer);
            }
            else Selection.activeGameObject = placer.gameObject;
        }
    }
}