using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeCharacter : MonoBehaviour
{
    //Hình ảnh player
    public Image canvasImageHero;  // Tham chiếu đến Image trên canvas
    public Sprite[] imageListHero;
    //Hình ảnh kĩ năng
    public Image canvasImageSkill;  // Tham chiếu đến Image trên canvas
    public Sprite[] imageListSkill;
    /// Thông số
    private SaveLoadManager saveLoadManager;
    private HeroData heroData;
    private PlayerData playerData;
    private int idHeros;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI gem;
    public TextMeshProUGUI nameHero;
    public TextMeshProUGUI levelHero;
    public TextMeshProUGUI healthHero;
    public TextMeshProUGUI attackHero;
    public TextMeshProUGUI descriptionHero;
    public TextMeshProUGUI descriptionSkill;
    public GameObject unlockPriceHero;
    public GameObject BottomMenu;
    public TextMeshProUGUI isSelectHero;
    public TextMeshProUGUI skillNameHero;
    private List<HeroData> listHeroData;
    public GameObject characterList;

    void Start()
    {
        // saveLoadManager = GameObject.Find("SaveLoadManager").GetComponent<SaveLoadManager>();
        // playerData = saveLoadManager.LoadPlayerData();
        // listHeroData = saveLoadManager.LoadHeroData();
        // heroData = listHeroData[idHeros];
        // ShowHeroData();
    }
    public void SetIdHero(int id)
    {
        idHeros = id;
        saveLoadManager = GameObject.Find("SaveLoadManager").GetComponent<SaveLoadManager>();
        playerData = saveLoadManager.LoadPlayerData();
        listHeroData = saveLoadManager.LoadHeroData();
        heroData = listHeroData[idHeros];
        ShowHeroData();
    }

    void ShowHeroData()
    {
        gold.text = playerData.gold.ToString("N0");
        gem.text = playerData.gem.ToString("N0");

        if (!heroData.isSelected)
        {
            isSelectHero.text = "Chọn nhân vật";
        }
        else
        {
            isSelectHero.text = "Đang được chọn";
        }
        nameHero.text = heroData.Name;
        levelHero.text = heroData.Level.ToString();
        healthHero.text = heroData.Health.ToString();
        attackHero.text = heroData.Attack.ToString();
        descriptionHero.text = heroData.Description;
        descriptionSkill.text = heroData.SkillDescription;
        canvasImageHero.sprite = imageListHero[idHeros];
        canvasImageSkill.sprite = imageListSkill[idHeros];
        // Debug.Log(heroData.isSelected);
        if (heroData.isUnlocked)
        {
            unlockPriceHero.gameObject.SetActive(false);
            BottomMenu.gameObject.SetActive(true);
        }
        else
        {
            unlockPriceHero.gameObject.SetActive(true);
            BottomMenu.gameObject.SetActive(false);
        }
        // skillNameHero.text = heroData.SkillName;
    }

    public void UpLevel(int gold)
    {
        if (playerData.gold >= gold)
        {
            playerData.gold -= gold;
            heroData.Level++;
            heroData.Health += 10;
            heroData.Attack += 7;
            saveLoadManager.SavePlayerData(playerData);
            listHeroData[idHeros] = heroData;
            saveLoadManager.SaveHeroData(listHeroData);
            ShowHeroData();
        }
    }
    public void UnlockHero(int gold)
    {
        if (playerData.gold >= gold)
        {
            playerData.gold -= gold;
            heroData.isUnlocked = true;
            saveLoadManager.SavePlayerData(playerData);
            Debug.Log("isUnlocked" + heroData.ID + "  " + heroData.isUnlocked);

            listHeroData[idHeros] = heroData;
            saveLoadManager.SaveHeroData(listHeroData);
            ShowHeroData();
            unlockPriceHero.SetActive(false);
            BottomMenu.SetActive(true);
        }
    }
    public void SelectHero()
    {
        for (int i = 0; i < listHeroData.Count; i++)
        {
            listHeroData[i].isSelected = false;
        }
        heroData.isSelected = true;
        saveLoadManager.SaveHeroData(listHeroData);
        ShowHeroData();
    }
    public void openCharacterList()
    {
        characterList.SetActive(true);
        gameObject.SetActive(false);
    }
}

