using GAMJA.Entity;
using GAMJA.Inventory.Exceptions;
using System;
using System.Collections.Generic;
using static GAMJA.Game.ConsoleFunc;
using static GAMJA.Game.InGame;
using static System.ConsoleColor;
using static System.ConsoleKey;

namespace GAMJA.Inventory
{
  class Inven
  {
    List<Material> items;
    private int width;
    private int height;
    private Player player;

    public int selectedX = 1;
    public int selectedY = 1;

    public Inven(int x, int y, Player player)
    {
      if (x <= 0 || y <= 0)
      {
        throw new InvalidInventorySizeException();
      }

      width = x + 1;
      height = y + 1;
      items = new List<Material>(); // ?????
      ClearInventory(); // okay.... 실행해 보시죠 how? shit ??  이거 실행어케시켜야됨 라이브쉐어에섴ㅋ ???

      this.player = player;
    }

    private bool IsValidCell(int x, int y) => (x > 0 && x <= width) && (y > 0 && y <= height);
    private int GetCellId(int x, int y) => width * y + x;

    public void ReplaceItem(int x, int y, Material item)
    {
      if (!IsValidCell(x + 1, y + 1)) throw new InvalidInventoryCellException();
      items[GetCellId(x, y)] = item;
    }

    public Material GetItem(int x, int y)
    {
      if (!IsValidCell(x + 1, y + 1)) throw new InvalidInventoryCellException();
      return items[GetCellId(x, y)];
    }

    public void ClearInventory()
    {
      items = new List<Material>();
      for (int x = 0; x <= width + 1; x++)
      {
        for (int y = 0; y <= height + 1; y++)
        {
          items.Add(Item.GetAir());
        }
      }
    }

    private void RenderInventory()
    {
      ConsoleColor fgColor = White;
      ConsoleColor bgColor = Black;
      
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (GetItem(x, y) == Item.GetAir())
          {
            WriteColor("□", fgColor, bgColor); // 님아 아이템 여기다가 뭐 넣은거 잇음? ㅇ ㅇㄷㅇmyInventory.ReplaceItem(1, 1, Material.TESTARMOR); 1, 1
          }
          else
          {
            WriteColor("■", bgColor, fgColor);
          }
        }
        WriteLineColor("", fgColor, bgColor);
      }
    }

    private void RenderInventory(int selectedX, int selectedY)
    {
      ConsoleColor fgColor = White;
      ConsoleColor bgColor = Black;

      WriteLineColor($"「{player.name}」의 인벤토리\n");
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (GetItem(x, y) == Item.GetAir())
          {
            if (x == selectedX && y == selectedY)
              WriteColor("□", fgColor, bgColor);
            else
              WriteColor("□", bgColor, fgColor);
          }
          else
          {
            if (x == selectedX && y == selectedY)
              WriteColor("■", fgColor, bgColor);
            else
              WriteColor("■", bgColor, fgColor);
          }
        }
        WriteLineColor("", fgColor, bgColor);
      }

      this.selectedX = selectedX;
      this.selectedY = selectedY;
      if (GetItem(selectedX, selectedY) == Item.GetAir())
      {
        WriteColor($"비어 있음\n");
        WriteColor($"  아이템이 없습니다.\n", DarkGray);
      }
      else
      {
        Item sItem = new Item(GetItem(selectedX, selectedY));
        WriteColor($"{sItem.Name}\n");
        WriteColor($"  {sItem.Lore}\n", DarkGray);
      }

    }

    private Material SelectItem()
    {
      int sX = 0;
      int sY = 0;

      while (true)
      {
        WriteCurrentLocation();
        RenderInventory(sX, sY);
        //WriteLineColor("\n아이템을 선택 하시오.\n", ConsoleColor.Black, ConsoleColor.White);
        //WriteLineColor("↑  ↓  ↑  ↓ . 이동 / 1 . 선택 / 2 . 취소");

        switch (SelectScreen("↑  ↓  ↑  ↓ . 이동", new string[] { "선택\n", "취소\n" }, new ConsoleKey[] { Enter, D2 }))
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
            selectedX = sX;
            selectedY = sY;
            return GetItem(sX, sY);

          case D2:
            return Item.GetAir();

          default:
            break;
        }
      }
    }

    public void Open()
    {
      while (true)
      {
        WriteCurrentLocation();
        RenderInventory();

        switch (SelectScreen("무엇을 하시겠습니까?", new string[] { "아이템 선택\n", "뒤로 가기\n" }))
        {
          case D1:
            Material selectItem = SelectItem();
            if (selectItem != Item.GetAir())
            {
              ItemInterAct(selectedX, selectedY);
            }
            break;
          case D2:
            return;
        }
      }
    }

    private void ItemInterAct(int selectedX, int selectedY)
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
      Item item = new Item(GetItem(selectedX, selectedY));
      while (true)
      {
        WriteCurrentLocation();
        WriteColor($"위치: ({selectedX}, {selectedY})\n\n", Gray);
        WriteColor($"  {item.Name} \n");
        WriteColor($"    {item.Type.ToString()}", Yellow);
        WriteColor($" {item.Lore} \n", Gray);
        //WriteColor($"    {}")

        switch (item.Type)
        {
          case ItemType.NONE:
            break;
        }
        Console.ReadKey();
      }
    }

  }
}
