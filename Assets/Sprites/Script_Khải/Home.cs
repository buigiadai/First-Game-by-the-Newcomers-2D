using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public GameObject emptyObject;
    public Button ButtonBattle;

    void Start()
    {
        // Kiểm tra nếu nút và đối tượng đã được thiết lập
        if (ButtonBattle != null && emptyObject != null)
        {
            // Đảm bảo đối tượng ban đầu bị ẩn
            emptyObject.SetActive(false);

            // Gắn sự kiện cho nút khi được nhấn
            ButtonBattle.onClick.AddListener(ShowEmptyObject);
        }
    }

    void ShowEmptyObject()
    {
        // Hiển thị đối tượng đã ẩn
        emptyObject.SetActive(true);
    }
}
