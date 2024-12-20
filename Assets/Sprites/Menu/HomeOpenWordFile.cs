using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class HomeOpenWordFile : MonoBehaviour
{
    // Đường dẫn đến file Word
    private string relativePath = "/Documents/ThongTin.docx";

    public void OpenWordDocument()
    {
        // Lấy đường dẫn đầy đủ đến file Word
        string wordFilePath = Application.dataPath + relativePath;

        // Kiểm tra file có tồn tại không
        if (!System.IO.File.Exists(wordFilePath))
        {
            UnityEngine.Debug.LogError("File không tồn tại tại đường dẫn: " + wordFilePath);
            return;
        }

        try
        {
            // Mở file Word bằng chương trình mặc định
            Process.Start(new ProcessStartInfo(wordFilePath)
            {
                UseShellExecute = true // Sử dụng chương trình mặc định
            });
            UnityEngine.Debug.Log("Đã mở file Word: " + wordFilePath);
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("Lỗi khi mở file Word: " + ex.Message);
        }
    }
    public void OpenHTMLPage()
    {
        string url = "https://www.facebook.com/profile.php?id=100056024986927";
        Application.OpenURL(url);
    }
}
