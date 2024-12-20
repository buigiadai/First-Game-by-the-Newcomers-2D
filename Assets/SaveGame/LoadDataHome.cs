//Bùi Gia Đại
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadDataHome : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;     // Text để hiển thị tên
    public TextMeshProUGUI playerNameLevel;
    public TextMeshProUGUI playerGold;
    public TextMeshProUGUI playerGem;
    public TMP_InputField nameInputField;  // InputField để nhập tên mới
    private SaveLoadManager saveLoadManager;
    private PlayerData playerData;

    private void Start()
    {
        saveLoadManager = GetComponent<SaveLoadManager>();
        // Kiểm tra và tải dữ liệu
        playerData = saveLoadManager.LoadPlayerData();
        // Hiển thị dữ liệu
        playerNameText.text = playerData.name;
        playerGold.text = playerData.gold.ToString("N0");
        playerGem.text = playerData.gem.ToString("N0");
        playerNameLevel.text = playerData.level.ToString();
    }

    // Hàm thay đổi tên
    public void ChangeName()
    {
        // Debug.Log("Tên mới: " + nameInputField.text);
        if (playerData.gold < 100)
        {
            Debug.Log("Không đủ vàng để đổi tên");
            return;
        }
        playerData.gold -= 100;  // Trừ 100 vàng
        playerData.name = nameInputField.text;
        saveLoadManager.SavePlayerData(playerData);
        playerNameText.text = playerData.name;
        playerGold.text = playerData.gold.ToString("N0");
    }
}
