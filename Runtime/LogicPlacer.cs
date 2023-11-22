using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene_LogicConstructor.Runtime
{
    [DefaultExecutionOrder(-100)]
    public class LogicPlacer : MonoBehaviour
    {
        [SerializeField] private LogicContainer logic;
        [SerializeField] private string         logicName = "---- Gameplay Logic ----";

        public bool IsNull => logic == null;

        private void Awake()
        {
            if (IsNull) Debug.LogError($"Placer contains no logic", gameObject);
            
            if (!IsNull) logic.Construct(gameObject);

            RunCallbacks();
            
            if (IsNull || !logic.IgnoreParent) gameObject.name = logicName + $"{(IsNull ? "(NO LOGIC)" : "")}";
            else Destroy(gameObject);
        }

        private void RunCallbacks()
        {
            if (!IsFirstCheck()) return; //This makes sure that callbacks for all ISceneConstruction will fire only once and on the one scene, even if there is multiple Placers

            var callbacks = FindInterfaces.Find<ISceneConstruction>(gameObject.scene);
            if (!callbacks.Any()) return;
            
            foreach (var callback in callbacks) callback.OnConstruction();
        }
        

        private bool IsFirstCheck() => FindObjectsOfType<LogicPlacer>()
            .Where(o => o.gameObject.scene.name.Equals(gameObject.scene.name))
            .Select(o => o.GetInstanceID()).Min() == GetInstanceID();
    }
    
    //https://discussions.unity.com/t/how-can-i-find-all-objects-that-have-a-script-that-implements-a-certain-interface/126233/5
    public static class FindInterfaces
    {
        public static List<T> Find<T>(Scene unityScene)
        {
            var interfaces      = new List<T>();
            var rootGameObjects = unityScene.GetRootGameObjects();

            foreach (var rootGameObject in rootGameObjects)
            {
                var childrenInterfaces = rootGameObject.GetComponentsInChildren<T>();
                interfaces.AddRange(childrenInterfaces);
            }
        
            return interfaces;
        }
    }
}