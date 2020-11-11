namespace GAMJA.Entity
{
  interface IDamageable
  {
    int MaxHp { get; }
    int MaxMp { get; }
    int Hp { get; set; }
    int Mp { get; set; }
    int At { get; }
    int Def { get; }

    int InitialMaxHp { get; set; }
    int InitialMaxMp { get; set; }
    int InitialAt { get; set; }
    int InitialDef { get; set; }

    int AtPerLevel { get; }
    int DefPerLevel { get; }
  }
}
