using UnityEngine;
using ChestSO;

namespace Chests
{
    public class ChestModel
    {
        private ChestController chestController;
        public int gems { get; private set; }
        public int coins { get; private set; }
        public int unlockTime { get; private set; }
        public ChestType ChestType{get;private set;}
        public ChestModel(ChestScriptableObject chestSO)
        {
            ChestType = chestSO.chestType;
            unlockTime = chestSO.unlockTime;
            coins = Random.Range(chestSO.minCoins, chestSO.maxGems + 1);
            gems = Random.Range(chestSO.minGems, chestSO.maxGems + 1);
        }

        public void setChestController(ChestController _controller)
        {
            chestController = _controller;
        }
    }
}

