using System;
using UnityEngine;

namespace Scene_LogicConstructor.Runtime
{
    [DefaultExecutionOrder(-100)]
    public class LogicPlacer : MonoBehaviour
    {
        [SerializeField] private LogicContainer logic;
        [SerializeField] private string         logicName = "--------------------- Logic ---------------------";

        public bool IsNotNull => logic != null;

        private void Awake()
        {
            if (IsNotNull) logic.Construct(gameObject, logicName);
            else throw new NullReferenceException($"Placer contains no logic, {gameObject.name}");
        }
    }
}