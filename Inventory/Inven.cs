using GAMJA.Entity;
using GAMJA.Inventory.Exceptions;
using System;
using System.Collections.Generic;
using static GAMJA.Game.ConsoleFunc;
using static GAMJA.Game.InGame;
using static System.ConsoleKey;

namespace GAMJA.Inventory
{
  class Inven
  {
    List<Material> items;
    private int width;
    private int height;
    private Player player;

    public int selectedX;
    public int selectedY;

    public Inven(int x, int y, Player player)
    {
      if (x <= 0 || y <= 0)
      {
        throw new InvalidInventorySizeException();
      }

      width = x;
      height = y;
      items = new List<Material>();
      ClearInventory();

      this.player = player;
    }

    private enum Dir
    {
      LEFT, RIGHT, UP, DOWN
    }

    private bool IsValidCell(int x, int y)
    {
      return (x > 0 && x <= width) && (y > 0 && y <= height);
    }

    public void ReplaceItem(int x, int y, Material item)
    {
      if (!IsValidCell(x, y)) throw new InvalidInventoryCellException();
      items[x * y] = item;
    }

    public Material GetItem(int x, int y)
    {
      if (!IsValidCell(x, y)) throw new InvalidInventoryCellException();
      return items[x * y];
    }

    public void ClearInventory()
    {
      items = new List<Material>();
      for (int x = 0; x <= width + 1; x++)
      {
        for (int y = 0; y <= height + 1; y++)
        {
          items.Add(Material.AIR);
        }
      }
    }

    private void RenderInventory()
    {
      ConsoleColor fgColor = ConsoleColor.White;
      ConsoleColor bgColor = ConsoleColor.Black;

      WriteLineColor($"「{player.name}」의 인벤토리\n");
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (items[x * y] == new Material())
          {
            WriteColor("□", fgColor, bgColor);
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
      ConsoleColor fgColor = ConsoleColor.White;
      ConsoleColor bgColor = ConsoleColor.Black;

      WriteLineColor($"「{player.name}」의 인벤토리\n");
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (items[x * y] == Item.GetAir())
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
      if (items[selectedX * selectedY] == Item.GetAir())
      {
        WriteColor($"비어 있음\n");
        WriteColor($"  아이템이 없습니다.\n", ConsoleColor.DarkGray);
      }
      else
      {
        Item sItem = new Item(items[selectedX * selectedY]);
        WriteColor($"{sItem.Name}\n");
        WriteColor($"  {sItem.Lore}\n", ConsoleColor.DarkGray);
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
        WriteLineColor("↑  ↓  ↑  ↓ . 이동 / 1 . 선택 / 2 . 취소");

        switch (Console.ReadKey().Key)
        {
          case ConsoleKey.UpArrow:
            if (sY == 0)
              sY = height - 1;
            else
              sY--;
            break;

          case ConsoleKey.DownArrow:
            if (sY == height - 1)
              sY = 0;
            else
              sY++;
            break;

          case ConsoleKey.LeftArrow:
            if (sX == 0)
              sX = width - 1;
            else
              sX--;
            break;

          case ConsoleKey.RightArrow:
            if (sX == width - 1)
              sX = 0;
            else
              sX++;
            break;

          case ConsoleKey.D1:
            selectedX = sX;
            selectedY = sY;
            return GetItem(sX, sY);

          case ConsoleKey.D2:
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
              ItemInterAct(selectItem, selectedX, selectedY);
            }
            break;
          case D2:
            return;
        }
      }
    }

    private void ItemInterAct(Material material, int selectedX, int selectedY)
    {
      Item item = new Item(material);
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
                ViewItemInfo(material, selectedX, selectedY);
                break;
              case D2:
                return;

            }

            break;
        }
      }
    }

    private void ViewItemInfo(Material material, int selectedX, int selectedY)
    {
      while (true)
      {

      }
    }

  }
}
