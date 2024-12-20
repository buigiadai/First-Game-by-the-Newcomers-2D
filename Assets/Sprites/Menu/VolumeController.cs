//Bùi Gia Đại
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // Kéo thả Slider vào đây
    [SerializeField] private bool controlGlobalVolume = true; // Điều khiển toàn game hay từng AudioSource

    private void Start()
    {
        // Gán giá trị mặc định cho Slider từ giá trị hiện tại
        if (controlGlobalVolume)
            volumeSlider.value = AudioListener.volume;
        else
            volumeSlider.value = GetComponent<AudioSource>().volume;

        // Lắng nghe sự kiện thay đổi giá trị của Slider
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float value)
    {
        if (controlGlobalVolume)
        {
            AudioListener.volume = value; // Điều chỉnh âm lượng toàn game
        }
        else
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = value; // Điều chỉnh âm lượng của AudioSource
            }
        }
    }
}
