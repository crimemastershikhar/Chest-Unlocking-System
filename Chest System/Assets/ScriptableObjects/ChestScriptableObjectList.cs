using UnityEngine;

namespace ChestSO
{
    [CreateAssetMenu(fileName = "ChestScriptableObjectList", menuName = "Chest/ChestScriptableObject/ChestList")]
    public class ChestScriptableObjectList : ScriptableObject
    {
        public ChestScriptableObject[] chestSOL;
    }
}

