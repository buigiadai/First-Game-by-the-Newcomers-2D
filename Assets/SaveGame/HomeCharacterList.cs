//Bùi Gia Đại

//lấy thông tin anh hùng qua id nhập tay
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeCharacterList : MonoBehaviour
{
    private SaveLoadManager saveLoadManager;
    private List<HeroData> heroDataList;
    private HeroData heroData;
    public int idHeros;
    public TextMeshProUGUI heroNameText;
    public TextMeshProUGUI heroLevelText;
    public GameObject shine;
    public GameObject homeCharacter;
    private HomeCharacter homeCharacterScript;

    void Start()
    {
        saveLoadManager = GameObject.Find("SaveLoadManager").GetComponent<SaveLoadManager>();
        homeCharacterScript = homeCharacter.GetComponent<HomeCharacter>();
        //     List<HeroData> testHeroes = new List<HeroData>
        // {
        //     new HeroData
        //     {
        //         ID = 1,
        //         Name = "Hero1",
        //         Level = 1,
        //         Health = 100,
        //         Attack = 50,
        //         Description = "Test Hero 1",
        //         unlockPrice = 100,
        //         SkillName = "Fireball",
        //         SkillDescription = "Casts a fireball",
        //         SkillDamage = 50,
        //         isUnlocked = true,
        //         isSelected = false
        //     }
        // };
        // saveLoadManager.SaveHeroData(testHeroes); // Gọi lưu với dữ liệu mẫu
        heroDataList = saveLoadManager.LoadHeroData();
        heroData = heroDataList[idHeros];
        ShowHeroData();
    }

    public void ShowHeroData()
    {
        heroNameText.text = heroData.Name;
        heroLevelText.text = heroData.Level.ToString();
        if (heroData.isSelected)
            shine.SetActive(true);
        else
            shine.SetActive(false);
    }
    public void openCharacter(int id)
    {
        // // Cập nhật ID và dữ liệu anh hùng
        // idHeros = id;
        // heroData = heroDataList[idHeros];

        // Làm mới thông tin hiển thị6
        ShowHeroData();

        // Bật giao diện nhân vật
        homeCharacterScript.SetIdHero(id);
        homeCharacter.SetActive(true);

    }
}
