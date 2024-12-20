//Bùi Gia Đại
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMap : MonoBehaviour
{
    public GameObject play_Pause;
    public GameObject play_Lose;
    public GameObject play_Win;
    public GameObject player;
    public PlayerHealth playerHealth;
    public GameObject level;
    public GameObject loading;
    // private int timeScale = 1;
    public MapSaveData mapSaveData;

    void Start()
    {
        mapSaveData = GameObject.Find("SaveLoadManager").GetComponent<MapSaveData>();

        player = GameObject.FindGameObjectWithTag("Player");
        level = GameObject.Find("Level");
        Time.timeScale = 1; // Khởi tạo thời gian chạy game
        play_Win.SetActive(false);
        play_Lose.SetActive(false);
        play_Pause.SetActive(false);
    }

    public void Pause() // tạm dừng
    {
        // Debug.Log("Pause");
        play_Pause.SetActive(true);
        player.SetActive(false);
        level.SetActive(false);
        Time.timeScale = 0;
    }
    public void Lose()  // thua
    {
        // Debug.Log("Lose");
        play_Lose.SetActive(true);
        level.SetActive(false);
    }

    public void Win() // thắng
    {
        // Debug.Log("Win");
        Time.timeScale = 0;
        play_Win.SetActive(true);
        // SceneManager.LoadScene(1);// chuyển cảnh
    }
    public void continueGame() // tiếp tục 
    {
        play_Pause.SetActive(false);
        player.SetActive(true);
        level.SetActive(true);
        Time.timeScale = 1;
    }

    public void revive_HoiSinh(int gold) // hồi sinh
    {
        if (!mapSaveData.minusGold(gold))
        {
            Debug.Log("Không đủ vàng để hồi sinh");
            return;
        }
        // Debug.Log("Hồi sinh thành công");
        play_Lose.SetActive(false);
        player.SetActive(true);
        level.SetActive(true);
        playerHealth.currentHealth = playerHealth.maxHealth;
        playerHealth.healBar.UpdateBar(playerHealth.currentHealth, playerHealth.maxHealth);
        Time.timeScale = 1;
    }
    public void claimGold(int gold)
    {
        mapSaveData.addGold(gold);
        mapSaveData.addGem(3);
        goHome();
    }
    public void goHome()// thoát
    {
        loading?.SetActive(true);//chờ trong lúc load
        // Debug.Log("Home");
        SceneManager.LoadScene(1);
    }
    public void Replay()// Chơi lại
    {
        // Tải lại cảnh hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

}
