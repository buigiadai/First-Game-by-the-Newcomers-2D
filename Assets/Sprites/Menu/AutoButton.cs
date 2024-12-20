// Bùi Gia Đại
// Quản lý thay đổi hình ảnh auto
//+Kích hoạt
//+Tắt

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoButton : MonoBehaviour
{
    public RawImage TuOffImage, TuOnImage;

    // private Weapon weapon;

    void Start()
    {
        // // Lấy component Weapon của EMPTY Weapon, mà EMPTY Weapon nằm trong EMPTY Player
        // // weapon = GameObject.Find("Player").transform.Find("Weapon").GetComponent<Weapon>();
        // weapon = GameObject.Find("Player").GetComponentInChildren<Weapon>();

    }
    public void ChangeImage()
    {
        if (TuOnImage.gameObject.activeSelf)
        {
            TuOnImage.gameObject.SetActive(false);
            TuOffImage.gameObject.SetActive(true);
        }
        else
        {
            TuOnImage.gameObject.SetActive(true);
            TuOffImage.gameObject.SetActive(false);
        }
    }
    // public void TurnOn()
    // {
    //     weapon.isAutoShoot = !weapon.isAutoShoot;
    // }
}

