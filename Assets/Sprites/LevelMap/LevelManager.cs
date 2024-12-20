//Bùi Gia Đại

using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public CreateMonster createMonster; // Tham chiếu tới script CreateMonster
    public int[] monstersPerLevel; // Mảng số lượng quái vật theo level

    public float spawnDelay = 1.0f; // Thời gian giữa các lần spawn
    private int currentLevel = 0; // Level hiện tại
    private bool isLevelRunning = false; // Kiểm tra trạng thái level
    private int monsterCount = 0; // Số lượng quái vật hiện tại 
    public LevelSlider levelSlider; // Tham chiếu tới script LevelSlider
    public GameObject boss; // Tham chiếu tới boss

    void Start()
    {
        if (monstersPerLevel.Length > 0)
        {
            StartLevel(currentLevel);
        }
    }

    public void StartLevel(int level)
    {
        if (isLevelRunning)
        {
            Debug.LogWarning("Level hiện tại đang chạy, không thể bắt đầu level mới!");
            return;
        }

        if (level < monstersPerLevel.Length)
        {
            currentLevel = level;
            levelSlider.UpdateMaxCount(monstersPerLevel[level]);
            monsterCount = monstersPerLevel[level]; // Lấy số lượng quái vật của level hiện tại
            isLevelRunning = true;
            createMonster.OnSpawnComplete += OnSpawnComplete; // Đăng ký sự kiện hoàn tất
            createMonster.StartSpawning(monsterCount, spawnDelay);
        }
        else
        {
            // Debug.Log("Hết level để chơi!");
        }
    }

    public int GetMonsterCount()
    {
        return monsterCount;
    }

    public void NextLevel()
    {
        // Kết thúc level hiện tại trước khi chuyển
        EndCurrentLevel();

        // Chuyển sang level tiếp theo
        currentLevel++;
        if (currentLevel < monstersPerLevel.Length)
        {
            StartLevel(currentLevel);
        }
        else
        {
            // Debug.Log("Bạn đã hoàn thành tất cả các level!");
            boss.SetActive(true);
            levelSlider.slider.gameObject.SetActive(false);
        }
    }

    private void EndCurrentLevel()
    {
        isLevelRunning = false;
        createMonster.OnSpawnComplete -= OnSpawnComplete; // Hủy đăng ký sự kiện
    }

    private void OnSpawnComplete()
    {
        Debug.Log("Level: Đã tạo xong quái của level " + currentLevel + 1);

        // Kết thúc level hiện tại
        EndCurrentLevel();
    }
}
