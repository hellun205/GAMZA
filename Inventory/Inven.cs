using GAMJA.Entity;
using GAMJA.Inventory.Exceptions;
using System;
using System.Collections.Generic;
using static GAMJA.Game.ConsoleFunc;
using static System.ConsoleColor;

namespace GAMJA.Inventory
{
  partial class Inven
  {
    List<Material> items;
    private int width;
    private int height;
    private Player player;

    public int itemSX = 0;
    public int itemSY = 0;

    public int equipmentSY = 0;



    public Inven(int x, int y, Player player)
    {
      if (x <= 0 || y <= 0)
        throw new InvalidInventorySizeException();

      width = x;
      height = y;
      items = new List<Material>();
      ClearInventory();



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

      WriteLineColor($"「{player.name}」의 인벤토리\n");

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (GetItem(x, y) == Item.GetAir())
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

      this.itemSX = selectedX;
      this.itemSY = selectedY;
      if (GetItem(selectedX, selectedY) == Item.GetAir())
      {
        WriteColor($"비어 있음\n");
        WriteColor($"  아이템이 없습니다.\n", DarkGray);
      }
      else
      {
        Item sItem = new Item(GetItem(selectedX, selectedY));
        WriteColor($"{sItem.Name}\n");
        WriteColor($"  {sItem.Type.ToString() }", Yellow);
        WriteColor($" {sItem.Lore}\n", DarkGray);
      }

    }

    private void RenderEquipment()
    {
      ConsoleColor fgColor = White;
      ConsoleColor bgColor = Black;

      WriteLineColor($"「{player.name}」의 장비\n");

      for (int i = 0; i < player.WearedArmors.Length; i++)
      {
        WriteColor("\t");
        if (player.WearedArmors[i] == Item.GetAir())
          WriteColor("□\n", fgColor, bgColor);
        else
          WriteColor("■\n", fgColor, bgColor);
      }
      WriteColor("\n");
      for (int i = 0; i < player.WearedWeapons.Length; i++)
      {
        //WriteColor("\t");
        if (player.WearedWeapons[i] == Item.GetAir())
          WriteColor("\t□\n", fgColor, bgColor);
        else
          WriteColor("\t■\n", fgColor, bgColor);
      }
    }

    private void RenderEquipment(int selectedY)
    {
      ConsoleColor fgColor = White;
      ConsoleColor bgColor = Black;

      WriteLineColor($"「{player.name}」의 장비\n");

      for (int y = 0; y < player.WearedArmors.Length; y++)
      {
        WriteColor("\t");
        if (player.WearedArmors[y] == Item.GetAir())
        {
          if (y == selectedY)
            WriteColor("□\n", fgColor, bgColor);
          else
            WriteColor("□\n", bgColor, fgColor);
        }
        else
        {
          if (y == selectedY)
            WriteColor("■\n", fgColor, bgColor);
          else
            WriteColor("■\n", bgColor, fgColor);
        }
      }
      WriteColor("\n");

      for (int y = player.WearedArmors.Length; y < player.WearedWeapons.Length + player.WearedArmors.Length; y++)
      {
        WriteColor("\t");
        if (player.WearedWeapons[y - player.WearedArmors.Length] == Item.GetAir())
        {
          if (y == selectedY)
            WriteColor("□\n", fgColor, bgColor);
          else
            WriteColor("□\n", bgColor, fgColor);
        }
        else
        {
          if (y == selectedY)
            WriteColor("■\n", bgColor, fgColor);
          else
            WriteColor("■\n", fgColor, bgColor);
        }
      }
      equipmentSY = selectedY;

      if (selectedY < player.WearedArmors.Length)
      {
        if (player.WearedArmors[selectedY] == Item.GetAir())
        {
          WriteColor($"비어 있음\n");
          WriteColor($"  아이템이 없습니다.\n", DarkGray);
        }
        else
        {
          Item sItem = new Item(player.WearedArmors[selectedY]);
          WriteColor($"{sItem.Name}\n");
          WriteColor($"  {sItem.Type.ToString() }", Yellow);
          WriteColor($" {sItem.Lore}\n", DarkGray);
        }
      }else if (selectedY >= player.WearedArmors.Length)
      {
        if (player.WearedWeapons[selectedY - player.WearedArmors.Length] == Item.GetAir())
        {
          WriteColor($"비어 있음\n");
          WriteColor($"  아이템이 없습니다.\n", DarkGray);
        }
        else
        {
          Item sItem = new Item(player.WearedWeapons[selectedY - player.WearedArmors.Length]);
          WriteColor($"{sItem.Name}\n");
          WriteColor($"  {sItem.Type.ToString() }", Yellow);
          WriteColor($" {sItem.Lore}\n", DarkGray);
        }
      }

      
    }

  }
}
