namespace GAMJA.Inventory
{
  class Item
  {
    private string pName;
    private string pLore;

    public string Name { get => pName; set => pName = value; }
    public string Lore { get => pLore; set => pLore = value; }

    //public Item() { }

    public static Item GetAir()
    {
      return GetItem(Material.AIR);
    }

    public static Item GetItem(Material material)
    {
      Item item = new Item();
      switch (material)
      {
        case Material.AIR:
          item.Name = "";
          item.Lore = "";
          break;

        case Material.TEST:
          break;

        default:
          return new Item();
      }
      return item;
    }

  }
}
