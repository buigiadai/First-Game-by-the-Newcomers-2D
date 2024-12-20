//Bùi Gia Đại

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuMonster : MonoBehaviour
{
    public TextMeshProUGUI monsterCountDieText;
    private int monsterCountDie = 0;
    public LevelSlider levelSlider;

    public void MonsterDie()
    {
        monsterCountDie++;
        monsterCountDieText.text = monsterCountDie.ToString();
        // Debug.Log("Số lượng quái chết: " + monsterCountDie);

        levelSlider.monsterDie();
    }
}
