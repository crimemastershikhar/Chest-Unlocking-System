using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Chests;

namespace Message
{
    public class MsgManager : SingletonGeneric<MsgManager>
    {
        [SerializeField] private TextMeshProUGUI messageTxt;
        [SerializeField] private TextMeshProUGUI gemsBtnTxt;
        [SerializeField] private TextMeshProUGUI coinsBtnTxt;
        [SerializeField] private TextMeshProUGUI timerBtnTxt;
        [SerializeField] private Button timerBtn;
        [SerializeField] private Button gemsBtn;
        [SerializeField] private Button coinsBtn;
        [SerializeField] private GameObject popupMsg;
        public void displayMessageWithBtns(string message, int gems, int coins, bool isChestInList)
        {
            popupMsg.SetActive(true);
            string msg;
            if(ChestService.Instance.isChestTimerStarted && ChestService.Instance.noOfChestUnlock > 1)
            {
                msg = "Add a Chest to the Slot";
                timerBtnTxt.text = msg;
            }
            else
            {
                msg = "Start";
                timerBtnTxt.text = msg;
            }
            gemsBtnTxt.text = gems.ToString();
            coinsBtnTxt.text = coins.ToString();
            messageTxt.text = message;
            isChestAdded(isChestInList);
        }

        public void displayMessage(string msg)
        {
            popupMsg.SetActive(true);
            messageTxt.text = msg;
            timerBtn.gameObject.SetActive(false);
            gemsBtn.gameObject.SetActive(false);
            coinsBtn.gameObject.SetActive(false);
            disableMessage();
        }

        private async void disableMessage()
        {
            await new WaitForSeconds(3f);
            popupMsg.gameObject.SetActive(false);
        }

        private void isChestAdded(bool isChestInList)
        {
            if (isChestInList)
            {
                timerBtn.gameObject.SetActive(false);
            }
            else
            {
                timerBtn.gameObject.SetActive(true);
            }
            gemsBtn.gameObject.SetActive(true);
            coinsBtn.gameObject.SetActive(true);
        }

        public void onTimerBtnClicked()
        {
            popupMsg.SetActive(false);
            if (ChestService.Instance.isChestTimerStarted)
            {
                ChestService.Instance.addChestToUnlockList();
            }
            else
            {
                ChestService.Instance.unlockChest();
            }
        }

        public void onGemsBtnClicked()
        {
            popupMsg.SetActive(false);
            ChestService.Instance.unlockUsingGems();
        }

        public void onCoinsBtnClicked()
        {
            popupMsg.SetActive(false);
            ChestService.Instance.unlockUsingCoins();
        }


    }
}

