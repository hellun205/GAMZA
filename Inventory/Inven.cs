using GAMJA.Entity;
using GAMJA.Inventory.Exceptions;
using System;
using System.Collections.Generic;
using static GAMJA.Game.ConsoleFunc;
using static GAMJA.Game.InGame;

namespace GAMJA.Inventory
{
  class Inven
  {
    List<List<Item>> items;
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
      items = new List<List<Item>>();
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

    public void ReplaceItem(int x, int y, Item item)
    {
      if (!IsValidCell(x, y)) throw new InvalidInventoryCellException();
      items[x][y] = item;
    }

    public Item GetItem(int x, int y)
    {
      if (!IsValidCell(x, y)) throw new InvalidInventoryCellException();
      return items[x][y];
    }

    public void ClearInventory() 
    {
      for (int x = 0; x <= width + 1; x++)//Error
      {
        for (int y = 0; y <= height + 1; y++)//Error
        {
          items[x][y] = Item.GetAir();
        }
      }
    }

    private void RenderInventory()
    {
      ConsoleColor fgColor = ConsoleColor.White;
      ConsoleColor bgColor = ConsoleColor.Black;

      WriteLineColor($"「{player.name}」의 인벤토리\n");
      for (int y = 0; y < height; y++)//Error
      {
        for (int x = 0; x < width; x++)//Error
        {
          if (items[x][y] == Item.GetAir())
          {
            WriteColor("□", fgColor, bgColor);
          }
          else
          {
            WriteColor("■", fgColor, bgColor);
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
      for (int y = 0; y < height; y++)//Error
      {
        for (int x = 0; x < width; x++)//Error
        {
          if (items[x][y] == Item.GetAir())
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
    }

    private Item SelectItem()
    {
      int sX = 0;
      int sY = 0;

      while (true)
      {
        CWTitle();
        WriteCurrentLocation();
        RenderInventory(sX, sY);
        WriteLineColor("\n아이템을 선택 하시오.\n", ConsoleColor.Black, ConsoleColor.White);
        WriteLineColor("↑  ↓  ↑  ↓ . 이동\n1 . 선택\n2 . 취소");

        switch (Console.ReadKey().Key)
        {
          case ConsoleKey.UpArrow:
            if (sY == 0)
              sY = height;
            else
              sY--;
            break;

          case ConsoleKey.DownArrow:
            if (sY == height)
              sY = 0;
            else
              sY++;
            break;

          case ConsoleKey.LeftArrow:
            if (sX == 0)
              sX = width;
            else
              sX--;
            break;

          case ConsoleKey.RightArrow:
            if (sX == width)
              sX = 0;
            else
              sX++;
            break;

          case ConsoleKey.D1:
            selectedX = sX;
            selectedY = sY;
            return GetItem(sX, sY);

          case ConsoleKey.D2:
            return null;

          default:
            break;
        }
      }
    }

    public Item Open()
    {
      while (true)
      {
        CWTitle();
        WriteCurrentLocation();
        RenderInventory();

        switch (SelectScreen("무엇을 하시겠습니까?", new string[] { "아이템 선택", "뒤로 가기" }))
        {
          case 1:
            Item selectItem = SelectItem();

            if (selectItem != null)
            {
              return selectItem;
            }
            break;

          case 2:
            return null;
        }
      }
    }

  }
}
