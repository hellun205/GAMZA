namespace GAMJA.Inventory
{
  class Item
  {
    private string pName;
    private string pLore;
    private ItemType type = ItemType.NONE;
    private ArmorType armorType = ArmorType.NONE;

    public string Name { get => pName; set => pName = value; }
    public string Lore { get => pLore; set => pLore = value; }
    public ItemType Type { get => type; set => type = value; }
    public ArmorType ArmorType { get => armorType; set => armorType = value; }

    public Item(Material material)
    {
      switch (material)
      {
        case Material.AIR:
          break;
        case Material.TESTARMOR:
          Name = "Test Armor 1";
          Lore = "Test Armor";
          Type = ItemType.ARMOR;
          ArmorType = ArmorType.UPPERBODY;
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
