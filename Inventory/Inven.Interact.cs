using System;
using static GAMJA.Game.ConsoleFunc;
using static GAMJA.Game.InGame;
using static System.Console;
using static System.ConsoleColor;
using static System.ConsoleKey;

namespace GAMJA.Inventory
{
  partial class Inven
  {
    public void Open()
    {
      while (true)
      {
        WriteCurrentLocation();

        switch (SelectScreen("무엇을 보시겠습니까?", new string[] { "인벤토리\n", "장비\n", "뒤로 가기\n" }))
        {
          case D1:
            OpenInventory();
            break;
          case D2:
            OpenEquipment();
            break;
          case D3:
            return;
        }
      }
    }

    private void SelectItem()
    {
      int sX = 0;
      int sY = 0;

      while (true)
      {
        WriteCurrentLocation();
        RenderInventory(sX, sY);
        //WriteLineColor("\n아이템을 선택 하시오.\n", ConsoleColor.Black, ConsoleColor.White);
        //WriteLineColor("↑  ↓  ↑  ↓ . 이동 / 1 . 선택 / 2 . 취소");

        switch (SelectScreen("↑  ↓  ←  → . 이동", new string[] { "선택\n", "취소\n" }, new ConsoleKey[] { Enter, D2 }))
        {
          case UpArrow:
            if (sY == 0)
              sY = height - 1;
            else
              sY--;
            break;

          case DownArrow:
            if (sY == height - 1)
              sY = 0;
            else
              sY++;
            break;

          case LeftArrow:
            if (sX == 0)
              sX = width - 1;
            else
              sX--;
            break;

          case RightArrow:
            if (sX == width - 1)
              sX = 0;
            else
              sX++;
            break;

          case Enter:
            itemSX = sX;
            itemSY = sY;

            if (GetItem(sX, sY) != Item.GetAir())
            {
              ItemInteract(itemSX, itemSY);
            }
            break;
          case D2:
            return;
        }
      }
    }

    private void SelectEquipment()
    {
      int sY = 0;

      while (true)
      {
        int maxY = player.WearedArmors.Length + player.WearedWeapons.Length;
        WriteCurrentLocation();
        RenderEquipment(sY);

        switch (SelectScreen("↑  ↓ . 이동", new string[] { "선택\n", "취소\n" }, new ConsoleKey[] { Enter, D2 }))
        {
          case UpArrow:
            if (sY == 0)
              sY = maxY - 1;
            else
              sY--;
            break;

          case DownArrow:
            if (sY == maxY - 1)
              sY = 0;
            else
              sY++;
            break;

          case Enter:

            Material material = Item.GetAir();
            if (sY < player.WearedArmors.Length)
            {
              material = player.WearedArmors[sY];
              armorSY = sY;
              EquipmentInteract(ItemType.ARMOR, sY);
            }
            else if (sY >= player.WearedArmors.Length)
            {
              material = player.WearedWeapons[sY - player.WearedArmors.Length];
              weaponSY = sY - player.WearedArmors.Length;
              EquipmentInteract(ItemType.WEAPON, sY - player.WearedArmors.Length);
            }

            break;

          case D2:
            return;
        }
      }
    }

    private void OpenInventory()
    {
      while (true)
      {
        WriteCurrentLocation();
        RenderInventory();

        switch (SelectScreen("무엇을 하시겠습니까?", new string[] { "아이템 선택\n", "뒤로 가기\n" }))
        {
          case D1:
            SelectItem();
            break;
          case D2:
            return;
        }
      }
    }

    private void OpenEquipment()
    {
      while (true)
      {
        WriteCurrentLocation();
        RenderEquipment();

        switch (SelectScreen("무엇을 하시겠습니까?", new string[] { "장비 선택\n", "뒤로 가기\n" }))
        {
          case D1:
            SelectEquipment();
            break;
          case D2:
            return;
        }
      }
    }

    private void ItemInteract(int selectedX, int selectedY)
    {
      Item item = new Item(GetItem(selectedX, selectedY));
      while (true)
      {
        WriteCurrentLocation();
        RenderInventory(selectedX, selectedY);
        switch (item.Type)
        {
          case ItemType.NONE:
            switch (SelectScreen("무엇을 하시겠습니까?", new string[] { "아이템 정보 보기\n", "뒤로 가기\n" }))
            {
              case D1:
                ViewItemInfo(selectedX, selectedY);
                break;
              case D2:
                return;
            }
            break;

          case ItemType.ARMOR:
            switch (SelectScreen("무엇을 하시겠습니까?", new string[] { "아이템 정보 보기\n", "아이템 장착하기\n", "뒤로 가기\n" }))
            {
              case D1:
                ViewItemInfo(selectedX, selectedY);
                break;
              case D2:
                WearItem(selectedX, selectedY);
                break;
              case D3:
                return;
            }
            break;
          case ItemType.WEAPON:
            switch (SelectScreen("무엇을 하시겠습니까?", new string[] { "아이템 정보 보기\n", "아이템 장착하기\n", "뒤로 가기\n" }))
            {
              case D1:
                ViewItemInfo(selectedX, selectedY);
                break;
              case D2:
                WearItem(selectedX, selectedY);
                break;
              case D3:
                return;
            }
            break;
        }
      }
    }

    private void EquipmentInteract(ItemType itemType, int selectedY)
    {
      Item item = new Item();

      while (true)
      {
        WriteCurrentLocation();

        switch (itemType)
        {
          case ItemType.ARMOR:
            item = new Item(player.WearedArmors[selectedY]);
            RenderEquipment(selectedY);

            switch (SelectScreen("무엇을 하시겠습니까?", new string[] { "아이템 정보 보기\n", "장비 벗기\n", "뒤로 가기\n" }))
            {
              case D1:
                ViewEquipmentInfo(itemType, selectedY);
                break;
              case D2:
                UndressItem(itemType, selectedY);
                break;
              case D3:
                return;
            }
            break;
          case ItemType.WEAPON:
            item = new Item(player.WearedWeapons[selectedY]);
            RenderEquipment(selectedY + player.WearedArmors.Length);
            switch (SelectScreen("무엇을 하시겠습니까?", new string[] { "아이템 정보 보기\n", "무기 벗기\n", "뒤로 가기\n" }))
            {
              case D1:
                ViewEquipmentInfo(itemType, selectedY);
                break;
              case D2:
                UndressItem(itemType, selectedY);
                break;
              case D3:
                return;
            }
            break;
          default:
            Write("default");
            ReadKey();
            return;
        }
      }
    }

    private void ViewItemInfo(int selectedX, int selectedY)
    {
      WriteCurrentLocation();
      WriteColor($"위치: 인벤토리({selectedX}, {selectedY})\n\n", Gray);

      ShowItemInfo(GetItem(selectedX, selectedY));
    }

    private void ViewEquipmentInfo(ItemType itemType, int selectedY)
    {
      Material material = Item.GetAir();

      if (itemType == ItemType.ARMOR)
      {
        material = player.WearedArmors[selectedY];
        WriteCurrentLocation();
        WriteColor($"위치: 장비(갑옷, {selectedY})\n\n", Gray);
      }
      else if (itemType == ItemType.WEAPON)
      {
        material = player.WearedWeapons[selectedY];
        WriteCurrentLocation();
        WriteColor($"위치: 장비(무기, {selectedY})\n\n", Gray);
      }
      ShowItemInfo(material);
    }

    private void ShowItemInfo(Material material)
    {
      Item item = new Item(material);

      WriteColor($"  {item.Name} \n", Black, White);
      WriteColor($"    {item.Type.ToString()}", Yellow);
      WriteColor($" {item.Lore} \n", Gray);
      WriteColor($"    ");

      switch (item.Type)
      {
        case ItemType.NONE:
          break;
        case ItemType.ARMOR:
          break;
      }
      Console.ReadKey();
    }

    private void WearItem(int x, int y)
    {
      Material material = GetItem(x, y);
      Item item = new Item(material);

      switch (item.Type)
      {
        case ItemType.ARMOR:
          int itemArmorType = (int)item.ArmorType;
          Material wearedArmor = player.WearedArmors[itemArmorType];
          Item wearedArmorItem = new Item(wearedArmor);

          if (wearedArmor != Item.GetAir())
          {
            if (WearItem(material, player.WearedArmors[itemArmorType]))
            {
              player.WearedArmors[itemArmorType] = material;
              ReplaceItem(x, y, wearedArmor);
            }
          }
          else
          {
            if (WearItem(material))
            {
              player.WearedArmors[itemArmorType] = material;
              ReplaceItem(x, y, wearedArmor);
            }
          }
          break;

        case ItemType.WEAPON:
          int itemWeaponType = (int)item.WeaponType;
          Material wearedWeapon = player.WearedWeapons[itemWeaponType];
          Item wearedWeaponItem = new Item(wearedWeapon);

          if (wearedWeapon != Item.GetAir())
          {
            if (WearItem(material, player.WearedWeapons[itemWeaponType]))
            {
              player.WearedWeapons[itemWeaponType] = material;
              ReplaceItem(x, y, wearedWeapon);
            }
          }
          else
          {
            if (WearItem(material))
            {
              player.WearedWeapons[itemWeaponType] = material;
              ReplaceItem(x, y, wearedWeapon);
            }
          }
          break;
      }
    }

    private bool WearItem(Material itemToWearMaterial, Material wearedItemMaterial)
    {
      Item wearedItem = new Item(wearedItemMaterial);
      Item itemToWear = new Item(itemToWearMaterial);
      while (true)
      {
        WriteCurrentLocation();
        WriteColor("현재 착용 하고 있는 ");
        WriteColor(wearedItem.Name, Cyan);
        WriteColor("(이)랑 변경하시겠습니까?");
        switch (SelectScreen("", new string[] { "변경\n", "취소\n" }, new ConsoleKey[] { Enter, D2 }))
        {
          case Enter:
            WriteCurrentLocation();
            WriteColor("현재 착용 중이던 ");
            WriteColor(wearedItem.Name, Cyan);
            WriteColor("에서 ");
            WriteColor(itemToWear.Name, Cyan);
            WriteColor("(으)로 변경했습니다.");
            ReadKey();
            return true;
          case D2:
            return false;
        }
      }
    }

    private bool WearItem(Material itemToWearMaterial)
    {
      Item itemToWear = new Item(itemToWearMaterial);
      while (true)
      {
        WriteCurrentLocation();
        WriteColor(itemToWear.Name, Cyan);
        WriteColor("을(를) 착용하시겠습니까?");
        switch (SelectScreen("", new string[] { "착용\n", "취소\n" }, new ConsoleKey[] { Enter, D2 }))
        {
          case Enter:
            WriteCurrentLocation();
            WriteColor(itemToWear.Name, Cyan);
            WriteColor("(을)를 착용했습니다.");
            ReadKey();
            return true;
          case D2:
            return false;
        }
      }
    }

    private void UndressItem(ItemType itemType, int selectedY)
    {
      int itemAirCell = GetAirCell();
      if (itemAirCell == -1)
      {
        WriteColor("인벤토리가 부족하여 아이템을 벗을 수 없습니다.");
        ReadKey();
      }

      switch (itemType)
      {
        case ItemType.ARMOR:
          Material wearedArmor = player.WearedArmors[selectedY];

          if (wearedArmor != Item.GetAir())
          {
            if (UndressItem(itemType, selectedY, wearedArmor))
            {
              player.WearedArmors[selectedY] = Item.GetAir();
              ReplaceItem(itemAirCell, wearedArmor);
            }
          }
          break;

        case ItemType.WEAPON:
          Material wearedWeapon = player.WearedWeapons[selectedY];

          if (wearedWeapon != Item.GetAir())
          {
            if (UndressItem(itemType, selectedY, wearedWeapon))
            {
              player.WearedArmors[selectedY] = Item.GetAir() ;
              ReplaceItem(itemAirCell, wearedWeapon);
            }
          }
          break;
      }
    }

    private bool UndressItem(ItemType itemType, int selectY, Material materialToUndress)
    {
      Item itemToUndress = new Item(materialToUndress);
      while (true)
      {
        WriteCurrentLocation();
        WriteColor(itemToUndress.Name, Cyan);
        WriteColor("을(를) 벗으시겠습니까?");
        switch (SelectScreen("", new string[] { "벗기\n", "취소\n" }, new ConsoleKey[] { Enter, D2 }))
        {
          case Enter:
            WriteCurrentLocation();
            WriteColor(itemToUndress.Name, Cyan);
            WriteColor("(을)를 벗었습니다.");
            ReadKey();
            return true;
          case D2:
            return false;
        }
      }
    }

  }
}
