﻿using GAMJA.Inventory;
using System;
using static GAMJA.Game.ConsoleFunc;
using static System.Console;
using static System.ConsoleKey;

namespace GAMJA.Game
{
  static partial class InGame
  {
    private static MapList cMap;
    private static MapList CurrentMap
    {
      get => cMap;
      set
      {
        cMap = value;
        switch (cMap)
        {
          case MapList.None:
            break;
          case MapList.TownField:
            TownField();
            break;
          default:
            break;
        }
      }
    }

    public static void WriteCurrentLocation()
    {
      CWTitle();
      WriteLineColor($"\t 현재 위치 : {CurrentMap}\n=================================\n", ConsoleColor.White);
    }


    private static void TownField()
    {
      myInventory.ReplaceItem(5, 5, Material.TESTARMOR);
      bool whileA = true;
      while (whileA)
      {

        WriteCurrentLocation();
        switch (SelectScreen("이 곳에서 무엇을 하시겠습니까?", new string[] { "이동 한다.\n", "캐릭터 정보를 확인 한다.\n",
          "인벤토리를 확인한다.\n", "게임을 종료한다.\n" }))
        {
          case D1:
            break;
          case D2:
            OpenPlayerStatus();
            break;
          case D3:
            OpenInventory();
            break;
          case Escape:
            GameExit();
            break;
          case LeftWindows:
            Console.Clear();
            WriteColor("당신은 죽었습니다.", ConsoleColor.DarkRed);
            Console.Beep(1000, 20000);
            ReadKey();
            Environment.Exit(0);
            return;
        }
      }
    }

  }
}
