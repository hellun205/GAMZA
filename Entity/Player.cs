namespace GAMJA.Entity
{
  class Player
  {
    private int InitialMaxHp { get; set; }
    private int InitialMaxMp { get; set; }
    private int InitialAt { get; set; }
    private int pmaxHP;
    private int pHP;
    private int pmaxMP;
    private int pMP;
    private int plevel;
    private int atPerLevel;
    private int itemAT;
    private int at { get => itemAT + atPerLevel; }

    private int ATPerLevel { get => atPerLevel; set => atPerLevel = value; }

    public int ItemAt { get => itemAT; set => itemAT = value; }

    public Player(string name, int level, int hp, int mp, int at)
    {
      this.name = name;
      InitialMaxHp = hp;
      InitialMaxMp = mp;
      InitialAt = at;
      Level = level;
    }

    public int MaxHP
    {
      get => pmaxHP;
      set
      {
        pmaxHP = value;
        pHP = value;
      }
    }

    public int MaxMP
    {
      get => pmaxMP;
      set
      {
        pmaxMP = value;
        pMP = value;
      }
    }



    public int Level
    {
      get => plevel;
      set
      {
        if (value <= 20 && value != 0)
        {
          MaxHP = InitialMaxHp + (InitialMaxHp / 2) * (value - 1);
          MaxMP = InitialMaxMp + (InitialMaxMp / 2) * (value - 1);
          ATPerLevel = InitialAt + (InitialAt / 2) * (value - 1);
          plevel = value;
        }
      }
    }

    public string name { get; set; }

    public string GetInfo
    {
      get
      {
        return $"「{name}」의 정보 \n  레벨 : {Level} \n  HP : {pHP}/{MaxHP} \n  MP : {pMP}/{MaxMP} \n  공격력 : {at}";
      }
    }

  }
}
