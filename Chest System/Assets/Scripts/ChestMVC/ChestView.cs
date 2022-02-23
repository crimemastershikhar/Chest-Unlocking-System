using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Chests
{
    public class ChestView : MonoBehaviour
    {
        public bool isChestSprite = false;
        [SerializeField] private TextMeshProUGUI coinsTxt;
        [SerializeField] private TextMeshProUGUI gemsTxt;
        [SerializeField] private TextMeshProUGUI chestTimerTxt;
        [SerializeField] private TextMeshProUGUI chestStatusTxt;
        [SerializeField] private TextMeshProUGUI chestTypeTxt;
        [SerializeField] private Image chestSlotSprite;
        [SerializeField] private TextMeshProUGUI unlockCoinsTxt;
        [SerializeField] private TextMeshProUGUI unlockGemsTxt;
        public Sprite lockedChestSprite;
        public Sprite unlockedChestSprite;
        public ChestController chestController;

        public void setChestController(ChestController controller)
        {
            chestController = controller;
        }

        private void Start()
        {
            makeParent();
            chestController.instantiateEmptyChest();
        }

        private void Update()
        {
            if (chestController.isStartTime)
            {
                chestController.startUnlocking();
            }
        }

        private void makeParent()
        {
            this.transform.SetParent(ChestService.Instance.chestSlotGroup.transform);
            this.transform.localScale = ChestService.Instance.chestSlotGroup.transform.localScale;
        }

        public void showChestData()
        {
            coinsTxt.text = chestController.coins.ToString();
            gemsTxt.text = chestController.gems.ToString();
            if(chestController.unlockTime <= 0)
            {
                chestController.unlockTime = 0;
            }
            chestTimerTxt.text = "Unlock Time: " + chestController.unlockTime.ToString();
            unlockGemsTxt.text = "Unlock" + chestController.unlockWithGems.ToString();
            unlockCoinsTxt.text = "Unlock " + chestController.unlockWithCoins.ToString();
            chestStatusTxt.text = chestController.chestStatus;
            chestTypeTxt.text = chestController.chestType.ToString();
            if (chestController.isLocked || chestController.isEmpty)
            {
                chestSlotSprite.sprite = lockedChestSprite;
            }
            else
            {
                isChestSprite = true;
                chestSlotSprite.sprite = unlockedChestSprite;
            }
        }

        public void showUnlockGems(int gems)
        {
            unlockGemsTxt.text = "Unlock Gems: " + gems.ToString();
        }

        public void showUnlockCoins(int coins)
        {
            unlockCoinsTxt.text = "Unlock Coins: " + coins.ToString();
        }

        public void onChestBtnClicked()
        {
            chestController.chestBtnClicked();
        }

        public void showUnlockTime(int time)
        {
            chestTimerTxt.text = "Unlocking Time for this is: " + time.ToString();
        }
    }
}

