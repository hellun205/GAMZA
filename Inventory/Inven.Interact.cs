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
            equipmentSY = sY;

            Material material = Item.GetAir();
            if (sY < player.WearedArmors.Length)
              material = player.WearedArmors[sY];
            else if (sY >= player.WearedArmors.Length)
              material = player.WearedWeapons[sY - player.WearedArmors.Length];
            if (player.WearedArmors[sY - player.WearedArmors.Length] != Item.GetAir())
            {
              EquipmentInteract(equipmentSY);
            }
            break;
          case D2:
            return;
        }
      }
    }

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

    private void EquipmentInteract(int selectedY)
    {
      Item item = new Item();
      if (selectedY < player.WearedArmors.Length)
        item = new Item(player.WearedArmors[selectedY]);
      else if (selectedY >= player.WearedArmors.Length)
        item = new Item(player.WearedWeapons[selectedY - player.WearedArmors.Length]);

      while (true)
      {
        WriteCurrentLocation();
        RenderEquipment(selectedY);
        switch (item.Type)
        {
          case ItemType.ARMOR:
            switch (SelectScreen("무엇을 하시겠습니까?", new string[] { "아이템 정보 보기\n", "장비 벗기\n", "뒤로 가기\n" }))
            {
              case D1:
                ViewEquipmentInfo(selectedY);
                break;
              case D2:
                //UndressItem(selectedY);
                break;
              case D3:
                return;
            }
            break;
          case ItemType.WEAPON:
            switch (SelectScreen("무엇을 하시겠습니까?", new string[] { "아이템 정보 보기\n", "무기 벗기\n", "뒤로 가기\n" }))
            {
              case D1:
                ViewEquipmentInfo(selectedY);
                break;
              case D2:
                //UndressItem(selectedY);
                break;
              case D3:
                return;
            }
            break;
        }
      }
    }

    private void ViewItemInfo(int selectedX, int selectedY)
    {
      WriteCurrentLocation();
      WriteColor($"위치: 인벤토리({selectedX}, {selectedY})\n\n", Gray);

      ShowItemInfo(GetItem(selectedX, selectedY));
    }

    private void ViewEquipmentInfo(int selectedY)
    {
      Material material = Item.GetAir();
      if (selectedY < player.WearedArmors.Length)
      {
        material = player.WearedArmors[selectedY];
        WriteCurrentLocation();
        WriteColor($"위치: 장비(갑옷, {selectedY})\n\n", Gray);
      }
      else if (selectedY >= player.WearedArmors.Length)
      {
        material = player.WearedWeapons[selectedY - player.WearedArmors.Length];
        WriteCurrentLocation();
        WriteColor($"위치: 장비(무기, {selectedY - player.WearedArmors.Length})\n\n", Gray);
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
            while (true)
            {
              WriteCurrentLocation();
              WriteColor("현재 착용 하고 있는 ");
              WriteColor(wearedArmorItem.Name, Cyan);
              WriteColor("(이)랑 변경하시겠습니까?");
              switch (SelectScreen("", new string[] { "변경\n", "취소\n" }, new ConsoleKey[] { Enter, D2 }))
              {
                case Enter:
                  player.WearedArmors[itemArmorType] = material;
                  ReplaceItem(x, y, wearedArmor);

                  WriteCurrentLocation();
                  WriteColor("현재 착용 중이던 ");
                  WriteColor(wearedArmorItem.Name, Cyan);
                  WriteColor("에서 ");
                  WriteColor(new Item(player.WearedArmors[itemArmorType]).Name, Cyan);
                  WriteColor("(으)로 변경했습니다.");

                  ReadKey();
                  return;
                case D2:
                  return;
              }
            }
          }
          else
          {
            while (true)
            {
              WriteCurrentLocation();
              WriteColor(item.Name, Cyan);
              WriteColor("을(를) 착용하시겠습니까?");
              switch (SelectScreen("", new string[] { "변경\n", "취소\n" }, new ConsoleKey[] { Enter, D2 }))
              {
                case Enter:
                  player.WearedArmors[itemArmorType] = material;
                  ReplaceItem(x, y, wearedArmor);

                  WriteCurrentLocation();
                  WriteColor(new Item(player.WearedArmors[itemArmorType]).Name, Cyan);
                  WriteColor("(을)를 착용했습니다.");

                  ReadKey();
                  return;
                case D2:
                  return;
              }
            }
          }


        case ItemType.WEAPON:
          int itemWeaponType = (int)item.WeaponType;
          Material wearedWeapon = player.WearedArmors[itemWeaponType];
          Item wearedWeaponItem = new Item(wearedWeapon);

          if (wearedWeapon != Item.GetAir())
          {
            while (true)
            {
              WriteCurrentLocation();
              WriteColor("현재 착용 하고 있는 ");
              WriteColor(wearedWeaponItem.Name, Cyan);
              WriteColor("(이)랑 변경하시겠습니까?");
              switch (SelectScreen("", new string[] { "변경\n", "취소\n" }, new ConsoleKey[] { Enter, D2 }))
              {
                case Enter:
                  player.WearedArmors[itemWeaponType] = material;
                  ReplaceItem(x, y, wearedWeapon);

                  WriteCurrentLocation();
                  WriteColor("현재 착용 중이던 ");
                  WriteColor(wearedWeaponItem.Name, Cyan);
                  WriteColor("에서 ");
                  WriteColor(new Item(player.WearedArmors[itemWeaponType]).Name, Cyan);
                  WriteColor("(으)로 변경했습니다.");

                  ReadKey();
                  return;
                case D2:
                  return;
              }
            }
          }
          else
          {
            while (true)
            {
              WriteCurrentLocation();
              WriteColor(item.Name, Cyan);
              WriteColor("을(를) 착용하시겠습니까?");
              switch (SelectScreen("", new string[] { "변경\n", "취소\n" }, new ConsoleKey[] { Enter, D2 }))
              {
                case Enter:
                  player.WearedArmors[itemWeaponType] = material;
                  ReplaceItem(x, y, wearedWeapon);

                  WriteCurrentLocation();
                  WriteColor(new Item(player.WearedArmors[itemWeaponType]).Name, Cyan);
                  WriteColor("(을)를 착용했습니다.");

                  ReadKey();
                  return;
                case D2:
                  return;
              }
            }
          }
      }
    }

    //private void UndressItem(int y)
    //{
    //  Material material = GetItem(x, y);
    //  Item item = new Item(material);

    //  switch (item.Type)
    //  {
    //    case ItemType.ARMOR:
    //      int itemArmorType = (int)item.ArmorType;
    //      Material wearedArmor = player.WearedArmors[itemArmorType];
    //      Item wearedArmorItem = new Item(wearedArmor);

    //      if (wearedArmor != Item.GetAir())
    //      {
    //        while (true)
    //        {
    //          WriteCurrentLocation();
    //          WriteColor("현재 착용 하고 있는 ");
    //          WriteColor(wearedArmorItem.Name, Cyan);
    //          WriteColor("(이)랑 변경하시겠습니까?");
    //          switch (SelectScreen("", new string[] { "변경\n", "취소\n" }, new ConsoleKey[] { Enter, D2 }))
    //          {
    //            case Enter:
    //              player.WearedArmors[itemArmorType] = material;
    //              ReplaceItem(x, y, wearedArmor);

    //              WriteCurrentLocation();
    //              WriteColor("현재 착용 중이던 ");
    //              WriteColor(wearedArmorItem.Name, Cyan);
    //              WriteColor("에서 ");
    //              WriteColor(new Item(player.WearedArmors[itemArmorType]).Name, Cyan);
    //              WriteColor("(으)로 변경했습니다.");

    //              ReadKey();
    //              return;
    //            case D2:
    //              return;
    //          }
    //        }
    //      }
    //      player.WearedArmors[itemArmorType] = material;
    //      break;
    //    case ItemType.WEAPON:
    //      break;
    //  }
    //}
  }
}
