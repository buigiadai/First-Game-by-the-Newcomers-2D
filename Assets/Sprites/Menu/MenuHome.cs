//Bùi Gia Đại

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuHome : MonoBehaviour
{
    public GameObject lobby;
    public GameObject popupSetting;
    public GameObject popupChangeName;
    public void OpenCloseSetting()
    {
        popupSetting.SetActive(!popupSetting.activeSelf);
        lobby.SetActive(true);
    }
    public void OpenChangeName()
    {
        popupChangeName.SetActive(!popupSetting.activeSelf);
        lobby.SetActive(true);
    }
    public void CloseChangeName()
    {
        popupChangeName.SetActive(false);
        lobby.SetActive(true);
    }
}
