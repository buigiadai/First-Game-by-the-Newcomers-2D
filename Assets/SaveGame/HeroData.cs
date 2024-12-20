[System.Serializable]
public class HeroData
{
    public int ID;
    public string Name;
    public int Level;
    public int Health;
    public int Attack;
    public string Description;
    public int unlockPrice;//giá mở khóa
    public string SkillName;
    public string SkillDescription;
    public int SkillDamage;
    public bool isUnlocked;//đã mở khóa chưa
    public bool isSelected;//đã chọn chưa
}
