using GAMJA.Inventory;
using System;
using System.Collections.Generic;
using static GAMJA.Game.ConsoleFunc;
using static System.ConsoleColor;

namespace GAMJA.Entity
{
  class Player : IEntity, IWearable, IDamageable, ILevelable
  {
    public Inven Inventory;

    private string name;

    public string Name
    {
      get => name;
      set
      {
        name = value;
        if (value.ToLower() == "yeahx4" || value.ToLower() == "khj")
        {
          InitialMaxHp = 750;
          InitialMaxMp = 325;
          InitialAt = 75;
          Level = 30;
        }
        if (value.ToLower() == "mp")
        {
          InitialMaxHp = 1;
          InitialMaxMp = 1;
          InitialAt = 1;
          Level = 1;
        }
      }
    }

    private List<Material> wearedArmors;
    private List<Material> wearedWeapons;
    private int hpPerWearingItem;
    private int mpPerWearingItem;
    private int atPerWearingItem;
    private int defPerWearingItem;

    public List<Material> WearedArmors { get => wearedArmors; set => wearedArmors = value; }
    public List<Material> WearedWeapons { get => wearedWeapons; set => wearedWeapons = value; }
    public int HpPerWearingItem { get => hpPerWearingItem; set => hpPerWearingItem = value; }
    public int MpPerWearingItem { get => mpPerWearingItem; set => mpPerWearingItem = value; }
    public int AtPerWearingItem { get => atPerWearingItem; set => atPerWearingItem = value; }
    public int DefPerWearingItem { get => defPerWearingItem; set => defPerWearingItem = value; }

    private int hp;
    private int mp;

    public int MaxHp { get => InitialMaxHp + (InitialMaxHp / 2) * (Level - 1); }
    public int MaxMp { get => InitialMaxMp + (InitialMaxMp / 2) * (Level - 1); }
    public int Hp
    {
      get => hp;
      set
      {
        if (value >= 0 && value <= MaxHp)
          hp = value;
      }
    }

    public int Mp
    {
      get => mp;
      set
      {
        if (value >= 0 && value <= MaxMp)
          mp = value;
      }
    }

    public int At { get => AtPerWearingItem + AtPerLevel; }
    public int Def { get => DefPerWearingItem + DefPerLevel; }
    public int InitialMaxHp { get; set; }
    public int InitialMaxMp { get; set; }
    public int InitialAt { get; set; }
    public int InitialDef { get; set; }

    public int AtPerLevel { get => InitialAt + (InitialAt / 2) * (Level - 1); }
    public int DefPerLevel { get => InitialDef + (InitialDef / 10) * (Level - 1); }

    private long exp;
    private int level;

    public long Exp { get => exp; set => exp = value; }

    public int Level
    {
      get => level;
      set
      {
        if (value <= 99 && value != 0)
        {
          level = value;
        }
      }
    }

    public Player(string name, int level, int hp, int mp, int at)
    {
      Inventory = new Inven(12, 12, this);
      InitialMaxHp = hp;
      InitialMaxMp = mp;
      InitialAt = at;
      Level = level;
      Name = name;
      Hp = MaxHp;
      Mp = MaxMp;

      ClearWearingItems();
    }

    public void ClearWearingItems()
    {
      int armorTypeLength = Enum.GetValues(typeof(ArmorType)).Length;
      int weaponTypeLength = Enum.GetValues(typeof(WeaponType)).Length;

      wearedArmors = new List<Material>();
      wearedArmors.Clear();
      for (int i = 0; i < armorTypeLength; i++)
        wearedArmors.Add(Item.GetAir());

      wearedWeapons = new List<Material>();
      wearedWeapons.Clear();
      for (int i = 0; i < weaponTypeLength; i++)
        wearedWeapons.Add(Item.GetAir());
    }

    public void GetInfo()
    {
      WriteColor($"\t「 ");
      WriteColor($"{Name}", Cyan);
      WriteColor($" 」\n");

      WriteColor($"\t 레벨 : ");
      WriteColor($"{Level}\n", Green);

      WriteColor($"\t HP : ");
      WriteColor($"{hp}", Green);
      WriteColor($"/");
      WriteColor($"{MaxHp}\n", DarkGreen);

      WriteColor($"\t MP : ");
      WriteColor($"{mp}", Green);
      WriteColor($"/");
      WriteColor($"{MaxMp}\n", DarkGreen);

      WriteColor($"\t 공격력 : ");
      WriteColor($"{At}\n", Green);
    }
  }
}
