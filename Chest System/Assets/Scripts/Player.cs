using UnityEngine;
using TMPro;

namespace Playerdir
{
 public class Player : SingletonGeneric<Player>
    {
      private bool isenoughGems;
        private bool isenoughCoins;
      [SerializeField] private int gems;
      [SerializeField] private int coins;
      [SerializeField] private TextMeshProUGUI gemsTxt;
      [SerializeField] private TextMeshProUGUI coinsTxt;

        private void Start()
        {
            showPlayerData();
        }

        private void showPlayerData()
        {
            coinsTxt.text = "Coins: " + coins.ToString();
            gemsTxt.text = "Gems: " + gems.ToString();
        }

        public void addCoinsandGems(int _coins, int _gems)
        {
            coins += _coins;
            gems += _gems;
            showPlayerData();
        }

        public bool removeGems(int _gems)
        {
            if(_gems <= gems)
            {
                gems -= _gems;
                isenoughGems = true;
            }
            else
            {
                isenoughGems = false;
            }
            showPlayerData();
            return isenoughGems;
        }

        public bool removeCoins(int _coins)
        {
            if (_coins <= coins)
            {
                coins -= _coins;
                isenoughCoins = true;
            }
            else
            {
                isenoughCoins = false;
            }
            showPlayerData();
            return isenoughCoins;
        }
    }
}
