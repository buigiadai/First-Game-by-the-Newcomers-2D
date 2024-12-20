//Bùi Gia Đại
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HomeShop : MonoBehaviour
{
    public TextMeshProUGUI playerGold;
    public TextMeshProUGUI playerGem;
    private SaveLoadManager saveLoadManager;
    private PlayerData playerData;

    private void Start()
    {
        saveLoadManager = GameObject.Find("SaveLoadManager").GetComponent<SaveLoadManager>();
        // Kiểm tra và tải dữ liệu
        playerData = saveLoadManager.LoadPlayerData();
        // Hiển thị dữ liệu
        Debug.Log("Gold: " + playerData.gold);
        if (playerGold != null && playerGem != null)
        {
            playerGold.text = playerData.gold.ToString("N0");
            playerGem.text = playerData.gem.ToString("N0");
        }
    }

    // Mua vàng
    public void BuyGold(String gemGold)
    {
        //gemGold = gem + gold
        //ví dụ gemGold=""10+100""
        string[] arr = gemGold.Split('+');
        int gem = int.Parse(arr[0]);
        int gold = int.Parse(arr[1]);

        if (playerData.gem < gem)
        {
            Debug.Log("Không đủ gem để mua vàng");
            return;
        }
        playerData.gem -= gem;  // Trừ gem
        playerData.gold += gold;  // Cộng vàng
        saveLoadManager.SavePlayerData(playerData);
        playerGold.text = playerData.gold.ToString("N0");
        playerGem.text = playerData.gem.ToString("N0");
    }

    //Mua Gem
    public void OpenHTMLPage(String url)
    {
        Application.OpenURL(url);
    }


}
