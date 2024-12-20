//Youtube: https://www.youtube.com/watch?v=_lREXfAMUcE

//Bùi Gia Đại
//Tham số nhận: Slider thanh máu của quái vật
//Mô tả: Thanh máu quái vật sẽ hiển thị tạm thời sau mỗi lần SetHealth

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MonsterHealth : MonoBehaviour
{
    [SerializeField] public Slider slider;
    private Coroutine hideCoroutine;

    void Start()
    {
        if (slider == null)
        {
            Debug.LogError("Slider is not assigned in Inspector!", this);
        }
        else
        {
            slider.gameObject.SetActive(false); // Ẩn thanh máu lúc đầu
        }
    }

    // Phương thức xóa thanh máu
    public void RemoveHealthBar()
    {
        if (slider != null)
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Slider is not assigned in MonsterHealth.");
        }
    }

    // Phương thức thiết lập sức khỏe và hiển thị thanh máu tạm thời
    public void SetHealth(float currentHealth, float maxHealth)
    {
        if (slider != null)
        {
            slider.value = currentHealth / maxHealth * 100;
            ShowHealthBarTemporarily();
        }
        else
        {
            Debug.LogWarning("Slider is not assigned in MonsterHealth.");
        }
    }

    // Phương thức hiển thị thanh máu tạm thời
    private void ShowHealthBarTemporarily()
    {
        slider.gameObject.SetActive(true); // Hiển thị thanh máu

        // Nếu có Coroutine ẩn thanh máu đang chạy, hủy nó
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        // Bắt đầu Coroutine mới để ẩn thanh máu sau 3 giây
        hideCoroutine = StartCoroutine(HideHealthBarAfterDelay(3f));
    }

    // Coroutine để ẩn thanh máu sau thời gian trì hoãn
    private IEnumerator HideHealthBarAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (slider != null)
        {
            slider.gameObject.SetActive(false); // Ẩn thanh máu sau thời gian trì hoãn
        }
    }
}