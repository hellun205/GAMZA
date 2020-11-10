namespace GAMJA.Inventory
{
  class Item
  {
    private string pName;
    private string pLore;
    private ItemType type = ItemType.NONE;
    private ArmorType armorType;
    private WeaponType weaponType;

    //private int def;
    private int at;


    public string Name { get => pName; set => pName = value; }
    public string Lore { get => pLore; set => pLore = value; }
    public ItemType Type { get => type; set => type = value; }
    public ArmorType ArmorType { get => armorType; set => armorType = value; }
    public WeaponType WeaponType { get => weaponType; set => weaponType = value; }

    //public int Def { get => def; set => def = value; }
    public Item() { }
    public Item(Material material)
    {
      switch (material)
      {
        case Material.AIR:
          break;
        case Material.TESTARMOR1:
          Name = "Test Armor 1";
          Lore = "Test Armor1";
          Type = ItemType.ARMOR;
          ArmorType = ArmorType.UPPERBODY;
          break;
        case Material.TESTARMOR2:
          Name = "Test Armor 2";
          Lore = "Test Armor2";
          Type = ItemType.ARMOR;
          ArmorType = ArmorType.UPPERBODY;
          break;
        case Material.TESTWEAPON1:
          Name = "Test Weapon 1";
          Lore = "Test Weapon1";
          Type = ItemType.WEAPON;
          break;
        case Material.TESTWEAPON2:
          Name = "Test Weapon 2";
          Lore = "Test Weapon2";
          Type = ItemType.WEAPON;
          break;
        default:
          break;
      }

    }

    public static Material GetAir()
    {
      return Material.AIR;
    }

  }
}
