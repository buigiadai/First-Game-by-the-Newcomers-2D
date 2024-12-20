using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class Menu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Map01()
    {
        SceneManager.LoadScene(2);
    }

    public void Map02()
    {
        SceneManager.LoadScene(3);
    }

    public void Replay1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Replay2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Replay3()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ThoatRaMenu()
    {
        SceneManager.LoadScene(0);
    }
}
