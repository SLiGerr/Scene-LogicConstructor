using System;
using System.Linq;
using UnityEngine;

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
            //Debug.Log($"{GetInstanceID()} is min {IsFirstCheck()}");
            if (!IsFirstCheck()) return; //This makes sure that callbacks for all ISceneConstruction will fire only once, even if there is multiple Placers

            var callbacks = FindObjectsOfType<MonoBehaviour>().OfType<ISceneConstruction>();
            if (!callbacks.Any()) return;
            
            foreach (var callback in callbacks) callback.OnConstruction();
        }

        private bool IsFirstCheck() => FindObjectsOfType<LogicPlacer>().Select(o => o.GetInstanceID()).Min() == GetInstanceID();
    }
}