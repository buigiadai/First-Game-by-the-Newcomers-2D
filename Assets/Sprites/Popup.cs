//Bùi Gia Đại
//Chưc năng: Xoá sau khoảng thời gian truyền vào

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour
{

    public float lifeTime = 0.25f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

}
