using GAMJA.Inventory;
using System.Collections.Generic;

namespace GAMJA.Entity
{
  interface IWearable
  {
    List<Material> WearedArmors { get; set; }
    List<Material> WearedWeapons { get; set; }

    int AtPerWearingItem { get; set; }
    int HpPerWearingItem { get; set; }
    int MpPerWearingItem { get; set; }
    int DefPerWearingItem { get; set; }

    void ClearWearingItems();

  }
}
