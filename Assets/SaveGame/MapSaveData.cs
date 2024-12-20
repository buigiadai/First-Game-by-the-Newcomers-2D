using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSaveData : MonoBehaviour
{
    public SaveLoadManager saveLoadManager;
    private PlayerData playerData;
    private HeroData heroData;
    private PlayerHealth playerHealth;

    void Start()
    {
        saveLoadManager = GameObject.Find("SaveLoadManager").GetComponent<SaveLoadManager>();
        playerData = saveLoadManager.LoadPlayerData();
        heroData = saveLoadManager.LoadHeroData().Find(x => x.isSelected);
        // Debug.Log(playerData.gold);

        //Gán vào thông tin player: đam máu
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.maxHealth = heroData.Health;
        playerHealth.currentHealth = heroData.Health;
        playerHealth.healBar.UpdateBar(playerHealth.currentHealth, playerHealth.maxHealth);
    }
    public int getDame()
    {
        return heroData.Attack;
    }

    public bool minusGold(int gold)
    {
        if (playerData.gold < gold)
        {
            return false;
        }
        playerData.gold -= gold;
        saveLoadManager.SavePlayerData(playerData);
        return true;
    }

    public void addGold(int gold)
    {
        playerData.gold += gold;
        saveLoadManager.SavePlayerData(playerData);
    }
    public void addGem(int gem)
    {
        playerData.gem += gem;
        saveLoadManager.SavePlayerData(playerData);
    }


}
