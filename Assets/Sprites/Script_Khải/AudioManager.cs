using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
public AudioSource musicAudioSource;
public AudioSource vfxAudioSource;
//tạo biến lưu trữ Audio Clip
public AudioClip musicClip;
public AudioClip coinClip;
public AudioClip winClip;
    void Start()
    {
        musicAudioSource.clip= musicClip;
	musicAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
