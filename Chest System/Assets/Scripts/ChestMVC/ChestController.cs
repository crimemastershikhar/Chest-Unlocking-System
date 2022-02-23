using UnityEngine;
using Message;
using ChestSO;
using Playerdir;
using System;

namespace Chests
{
    public class ChestController
    {
        public bool isStartTime;
        public bool addedToUnlockingList;
        public int unlockWithGems;
        public int unlockWithCoins;
        public bool isLocked;
        public bool isEmpty;
        public string chestStatus;
        public int unlockTime;
        public int coins;
        public int gems;
        public ChestType chestType;
        public ChestModel chestModel { get; }
        public ChestView chestView { get; }
        private Sprite sprite;
        public ChestController(ChestModel model, ChestView view, Sprite _sprite)
        {
            chestModel = model;
            chestView = GameObject.Instantiate<ChestView>(view);
            sprite = _sprite;
            chestView.setChestController(this);
        }

        public void addChest(ChestScriptableObject chestSO, Sprite _lockedSprite, Sprite _unlockedSprite)
        {
            chestType = chestSO.chestType;
            coins = UnityEngine.Random.Range(chestSO.minCoins, chestSO.maxCoins + 1);
            gems = UnityEngine.Random.Range(chestSO.minGems, chestSO.maxGems + 1);
            unlockTime = chestSO.unlockTime;
            chestView.lockedChestSprite = _lockedSprite;
            chestView.unlockedChestSprite = _unlockedSprite;
            chestStatus = "Chest is Locked";
            isEmpty = false;
            isLocked = true;
            unlockWithGems = countGemsToUnlock(unlockTime);
            unlockWithCoins = countCoinsToUnlock(unlockTime);
            chestView.showChestData();
        }

        public int countGemsToUnlock(int unlockTime)
        {
            int gems = (int)Mathf.Ceil(unlockTime / 10);
            return gems;
        }

        public int countCoinsToUnlock(int unlockTime)
        {
            int coins = (int)Mathf.Ceil(unlockTime / 100);
            return coins;
        }

        public void instantiateEmptyChest()
        {
            coins = 0;
            gems = 0;
            unlockWithGems = 0;
            unlockWithCoins = 0;
            chestType = ChestType.None;
            chestStatus = "Empty";
            isEmpty = true;
            chestView.lockedChestSprite = sprite;
            addedToUnlockingList = false;
            chestView.showChestData();
        }

        public bool isChestEmpty()
        {
            return isEmpty;
        }

        public void unlockUsingGems()
        {
            bool canUnlock = Player.Instance.removeGems(unlockWithGems);
            if (canUnlock)
            {
                chestUnlocked();
            }
            else
            {
                string msg = "Not Enough Gems||Cant Unlock";
                MsgManager.Instance.displayMessage(msg);
            }
        }

        public void unlockUsingCoins()
        {
            bool canUnlock = Player.Instance.removeCoins(unlockWithCoins);
            if (canUnlock)
            {
                chestUnlocked();
            }
            else
            {
                string msg = "Not Enough Coins||Cant Unlock";
                MsgManager.Instance.displayMessage(msg);
            }
        }

        public void chestBtnClicked()
        {
            string msg;
            if (isEmpty)
            {
                msg = "Current Chest slot is EMPTY :((";
                MsgManager.Instance.displayMessage(msg);
                return;
            }
            if (isLocked)
            {
                msg = "OOps the current Chest is Locked! ::(";
                ChestService.Instance.setChestView(chestView);
                MsgManager.Instance.displayMessageWithBtns(msg, unlockWithGems, unlockWithCoins , addedToUnlockingList);
            }
            else
            {
                Player.Instance.addCoinsandGems(coins, gems);
                msg = coins + "Coins &" + gems + "Gems ";
                MsgManager.Instance.displayMessage(msg);
                instantiateEmptyChest();
            }
        }

        private void chestUnlocked()
        {
            chestStatus = "Unlocked";
            isLocked = false;
            unlockWithGems = 0;
            unlockWithCoins = 0;
            isStartTime = false;
            ChestService.Instance.isChestTimerStarted = false;
            unlockTime = 0;
            chestView.showChestData();
            ChestService.Instance.unlockNextChest(chestView);
        }

        public async void startTime()
        {
            isStartTime = true;
            ChestService.Instance.isChestTimerStarted = true;
            while(unlockTime > 0)
            {
                await new WaitForSeconds(1f);
                unlockTime -= 1;
            }
        }

        public void startUnlocking()
        {
            unlockWithGems = countGemsToUnlock(unlockTime);
            unlockWithCoins = countCoinsToUnlock(unlockTime);
            chestView.showUnlockGems(unlockWithGems);
            chestView.showUnlockCoins(unlockWithCoins);
            if (unlockTime <= 0)
            {
                chestUnlocked();
            }
        }
    }
}

