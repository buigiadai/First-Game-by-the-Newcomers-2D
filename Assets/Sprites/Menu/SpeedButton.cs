// Bùi Gia Đại
// Quản lý việc thay đổi tốc độ thời gian

// Tham số nhận: danh sách hình ảnh, hình ảnh hiện tại

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedButton : MonoBehaviour
{
    public List<Texture> SpeedButtonList;
    public RawImage rawImage;
    private int count = 1;
    public int max = 3;// tối ưu nếu sau này có thay đổi

    public void ChangeSpeed()
    {
        // Thay đổi tốc độ thời gian
        count++;
        if (count > max)
        {
            count = 1;
        }
        Time.timeScale = count;
        rawImage.texture = SpeedButtonList[count - 1];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeSpeed();
        }
    }
}
