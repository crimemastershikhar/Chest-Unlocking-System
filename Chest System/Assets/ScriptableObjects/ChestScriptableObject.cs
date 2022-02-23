using UnityEngine;

namespace ChestSO
{
    [CreateAssetMenu(fileName = "ChestScriptableObject", menuName = "Chest/ChestScriptableObject/New Chest")]
    public class ChestScriptableObject : ScriptableObject
    {
        public ChestType chestType;
        public int unlockTime;
        public int minGems;
        public int maxGems;
        public int minCoins;
        public int maxCoins;
    }

    public enum ChestType
    {
        None,
        Common,
        Giant,
        Golden,
        Magical,
        Supermagical
    }
}

