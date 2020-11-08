namespace GAMJA.Game
{
  class Player
  {
    private int maxHP;
    private int HP;
    private int maxMP;
    private int MP;
    private int level;
    private int ATPerLevel;
    private int ItemAT;
    private int AT;

    public int MaxHP
    {
      get => maxHP;
      set
      {
        maxHP = value;
        HP = value;
      }
    }

    public int MaxMP
    {
      get => maxMP;
      set
      {
        maxMP = value;
        MP = value;
      }
    }

    private int pATPerLevel
    {
      get => ATPerLevel;
      set
      {
        ATPerLevel = value;
        AT = pATPerLevel + pItemAT;
      }
    }

    public int pItemAT
    {
      get => ItemAT;
      set
      {
        ItemAT = value;
        AT = pATPerLevel + pItemAT;
      }
    }

    public int Level
    {
      get => level;
      set
      {
        if (value <= 20 && value != 0)
        {
          MaxHP = 50 + 30 * (value - 1);
          MaxMP = 30 + 15 * (value - 1);
          pATPerLevel = 10 + 5 * (value - 1);
          level = value;
        }
      }
    }

    public string Name { get; set; }

    public string GetInfo
    {
      get
      {
        return $"「{Name}」의 정보 \n  레벨 : {Level} \n  HP : {HP}/{MaxHP} \n  MP : {MP}/{MaxMP} \n  공격력 : {AT}";
      }
    }

  }
}
