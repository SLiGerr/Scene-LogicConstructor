using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scene_LogicConstructor.Runtime
{
    [CreateAssetMenu(menuName = "Create Scene-LogicContainer", fileName = "Scene Template", order = 0)]
    public class LogicContainer : ScriptableObject
    {
        [SerializeField] private List<LogicBlock> blocks;
        [SerializeField] private bool             ignoreParent = false;

        public void Construct(GameObject placer, string placerName)
        {
            var parent = ignoreParent ? null : placer.transform;
            for (var i = 0; i < blocks.Count; i++)
            {
                var block = blocks[i];
                block.Construct(i, parent);
            }

            if (!ignoreParent) placer.name = placerName;
            else Destroy(placer);
        }

        private void OnValidate()
        {
            foreach (var block in blocks) block.Validate();
        }
    }

    [Serializable]
    public class LogicBlockBase
    {
        [HideInInspector] public string name;

        [SerializeField] protected GameObject logicPrefab;
        
        public void Construct(int siblingIndex, Transform parent)
        {
            var logic = Object.Instantiate(logicPrefab, parent);
            logic.name = logicPrefab.name;
            logic.transform.SetSiblingIndex(siblingIndex);
            
            AfterConstruct(logic);

            var callbacks = logic.GetComponents<ISceneConstruction>();
            if (callbacks.Any() )foreach (var callback in callbacks) callback.OnConstruction();
        }

        public virtual void AfterConstruct(GameObject logic) { }
        public virtual void Validate()                       { }
    }

    [Serializable]
    public class LogicBlock : LogicBlockBase
    {
        [SerializeField] private bool                  asDDOL;
        [SerializeField] private List<LogicBlockChild> child = new();
        
        public override void AfterConstruct(GameObject logic)
        {
            if (asDDOL) Object.DontDestroyOnLoad(logic);

            for (var i = 0; i < child.Count; i++)
            {
                var block = child[i];
                block.Construct(i, logic.transform);
            }
        }

        public override void Validate()
        {
            name = logicPrefab
                ? logicPrefab.name + (child.Count > 0 ? $" <color=green>(with {child.Count} child)</color>" : "")
                : "<color=red>Empty slot</color>";

            foreach (var block in child) block.Validate();
        }
    }

    [Serializable]
    public class LogicBlockChild : LogicBlockBase
    {
        public override void Validate() => name = logicPrefab ? logicPrefab.name : "<color=red>Empty slot</color>";
    }

    public interface ISceneConstruction
    {
        void OnConstruction();
    }
}