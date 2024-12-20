//Bùi Gia Đại
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSlider : MonoBehaviour
{
    public Slider slider; // Tham chiếu tới Slider UI
    public TextMeshProUGUI levelText; // Tham chiếu tới Text UI

    private int maxCount = 0;
    private int currentValue = 0;
    private LevelManager levelManager;

    private void Start()
    {
        // LevelManager nẳm trong empty object LevelManager
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        if (levelManager == null)
        {
            Debug.LogError("LevelManager không được tìm thấy! LevelSlider.cs");
        }
        levelText.text = "Level 1";
        currentValue = 0;
        slider.value = 0;
    }

    public void UpdateMaxCount(int maxValue)
    {
        maxCount = maxValue;
        currentValue = 0;
    }

    public void monsterDie()
    {
        currentValue++;
        slider.value = (float)currentValue / (float)maxCount * 100;//Tính phần trăm giá trị slider
        // Debug.Log("currentValue: " + currentValue + " maxCount: " + maxCount);

        if (currentValue == maxCount)
        {
            NextLevel();
        }

    }

    public void NextLevel()
    {
        slider.value = 0;
        levelText.text = "Level " + (int.Parse(levelText.text.Substring(6)) + 1);
        levelManager.NextLevel();
    }


}
