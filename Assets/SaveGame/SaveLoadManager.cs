// Bùi Gia Đại
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour
{
    private string playerDataPath;
    private string heroDataPath;

    void Awake()
    {
        // Đường dẫn tệp lưu trữ PlayerData và HeroData
        playerDataPath = Application.persistentDataPath + "/playerData.json";
        heroDataPath = Application.persistentDataPath + "/heroData.json";
        Debug.Log("Đường dẫn playerData: " + playerDataPath);
        Debug.Log("Đường dẫn heroData: " + heroDataPath);
    }

    // Lưu dữ liệu PlayerData vào tệp riêng biệt
    public void SavePlayerData(PlayerData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);  // "true" để lưu với định dạng dễ đọc
            File.WriteAllText(playerDataPath, json);
            Debug.Log("Dữ liệu Player đã được lưu tại: " + playerDataPath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Lỗi khi lưu PlayerData: " + e.Message);
        }
    }

    // Tải dữ liệu PlayerData từ tệp
    public PlayerData LoadPlayerData()
    {
        try
        {
            if (File.Exists(playerDataPath))
            {
                string json = File.ReadAllText(playerDataPath);
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);
                return data;
            }
            else
            {
                Debug.LogWarning("Không tìm thấy tệp PlayerData, tạo dữ liệu mặc định.");
                return new PlayerData();  // Trả về dữ liệu mặc định
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Lỗi khi tải PlayerData: " + e.Message);
            return null;
        }
    }

    // Lưu dữ liệu HeroData vào tệp riêng biệt
    public void SaveHeroData(List<HeroData> heroData)
    {
        try
        {
            if (heroData == null || heroData.Count == 0)
            {
                Debug.LogWarning("Không có hero nào để lưu.");
                return;
            }

            // Gói dữ liệu HeroData vào lớp bao bọc
            HeroDataListWrapper wrapper = new HeroDataListWrapper { heroes = heroData };

            // Chuyển đổi thành JSON
            string json = JsonUtility.ToJson(wrapper, true); // "true" để lưu định dạng dễ đọc

            // Ghi dữ liệu JSON vào tệp
            File.WriteAllText(heroDataPath, json);
            Debug.Log("Dữ liệu Hero đã được lưu tại: " + heroDataPath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Lỗi khi lưu HeroData: " + e.Message);
        }
    }

    // Tải dữ liệu HeroData từ tệp
    public List<HeroData> LoadHeroData()
    {
        try
        {
            if (File.Exists(heroDataPath))
            {
                string json = File.ReadAllText(heroDataPath);
                HeroDataListWrapper wrapper = JsonUtility.FromJson<HeroDataListWrapper>(json);
                return wrapper?.heroes ?? new List<HeroData>();
            }
            else
            {
                Debug.LogWarning("Không tìm thấy tệp HeroData, tạo file với dữ liệu mặc định.");

                // Tạo dữ liệu mặc định
                List<HeroData> defaultHeroData = CreateDefaultHeroData();

                // Lưu dữ liệu mặc định vào file
                SaveHeroData(defaultHeroData);

                return defaultHeroData; // Trả về dữ liệu mặc định
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Lỗi khi tải HeroData: " + e.Message);
            return new List<HeroData>();  // Trả về danh sách anh hùng rỗng nếu có lỗi
        }
    }


    // Lớp bao bọc để lưu nhiều anh hùng vào một tệp
    [System.Serializable]
    public class HeroDataListWrapper
    {
        public List<HeroData> heroes; // Phải có thuộc tính này để JsonUtility hoạt động
    }

    // Hàm tạo dữ liệu anh hùng mặc định

    private List<HeroData> CreateDefaultHeroData()
    {
        return new List<HeroData>
    {
        new HeroData
        {
            ID = 1,
            Name = "Default Hero",
            Level = 1,
            Health = 100,
            Attack = 50,
            Description = "Đây là hero mặc định.",
            unlockPrice = 0,
            SkillName = "Basic Attack",
            SkillDescription = "Tấn công cơ bản.",
            SkillDamage = 10,
            isUnlocked = true,
            isSelected = false
        }
    };
    }

}
