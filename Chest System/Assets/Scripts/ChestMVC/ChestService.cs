using UnityEngine;
using ChestSO;
using Message;
using System.Collections.Generic;

namespace Chests
{
    public class ChestService : SingletonGeneric<ChestService>
    {
        [SerializeField] private ChestScriptableObjectList chestScriptableObjectList;
        [SerializeField] private Sprite[] chestLockedSprites;
        [SerializeField] private Sprite[] chestUnlockedSprites;
        [SerializeField] private ChestView chestView;
        public GameObject chestSlotGroup;
        private ChestView chestToUnlock;
        private ChestController[] chestSlots;
        [SerializeField] private int noOfSlots;
        private int chestSlotFull;
        [SerializeField] private List<ChestView> chestUnlockingList;
        [HideInInspector] public bool isChestTimerStarted;
        public int noOfChestUnlock;

        private void Start()
        {
            chestSlots = new ChestController[noOfSlots];
            for (int i = 0; i < noOfSlots; i++)
            {
                chestSlots[i] = createChestController(chestScriptableObjectList.chestSOL[chestScriptableObjectList.chestSOL.Length - 1],
                    chestView, chestLockedSprites[chestLockedSprites.Length - 1]);
            }
        }

        private ChestController createChestController(ChestScriptableObject chestSO, ChestView view, Sprite _sprite)
        {
            ChestModel model = new ChestModel(chestSO);
            ChestController controller = new ChestController(model, view, _sprite);
            return controller;
        }

        public void unlockChest()
        {
            chestUnlockingList.Add(chestToUnlock);
            chestToUnlock.chestController.addedToUnlockingList = true;
            chestToUnlock.chestController.startTime();
        }

        public void unlockNextChest(ChestView view)
        {
            chestUnlockingList.Remove(view);
            if(chestUnlockingList.Count > 0)
            {
                chestUnlockingList[0].chestController.startTime();
            }
        }

        public void createAndAddChest()
        {
            int random = Random.Range(0, chestScriptableObjectList.chestSOL.Length);
            chestSlotFull = 0;
            for(int i=0; i < chestSlots.Length; i++)
            {
                if (chestSlots[i].isChestEmpty())
                {
                    chestSlots[i].addChest(chestScriptableObjectList.chestSOL[random], chestLockedSprites[random], 
                        chestUnlockedSprites[random]);
                    i = chestSlots.Length + 1;
                }
                else
                {
                    chestSlotFull++;
                }
            }
            if(chestSlotFull == chestSlots.Length)
            {
                string msg = "You have to Wait. No SLOT Available";
                MsgManager.Instance.displayMessage(msg);
            }
        }

        public void addChestToUnlockList()
        {
            string msg;
            if(isChestTimerStarted && noOfChestUnlock == chestUnlockingList.Count)
            {
                msg = "Unlocking Chest Limit Exceeded. Please WAIT";
                MsgManager.Instance.displayMessage(msg);
            }
            else
            {
                msg = "Chest Successfully Added!!!";
                MsgManager.Instance.displayMessage(msg);
                chestUnlockingList.Add(chestToUnlock);
                chestToUnlock.chestController.addedToUnlockingList = true;
            }
        }

        public void unlockUsingGems()
        {
            chestToUnlock.chestController.unlockUsingGems();
        }

        public void unlockUsingCoins()
        {
            chestToUnlock.chestController.unlockUsingCoins();
        }

        public void setChestView(ChestView _view)
        {
            chestToUnlock = _view;
        }
    }
}

