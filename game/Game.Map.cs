﻿using System;
using static GAMJA.Game.ConsoleFunc;

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

    private static void WriteCurrentLocation()
    {
      WriteLineColor($"\t 현재 위치 : {CurrentMap}\n=================================\n", ConsoleColor.White);
    }


    private static void TownField()
    {
      WriteCurrentLocation();
      bool whileA = true;
      while (whileA)
      {
        switch (SelectScreen("이 곳에서 무엇을 하시겠습니까?", new string[] { "이동 한다.", "캐릭터 정보를 확인 한다.",
          "인벤토리를 확인한다.", "게임을 종료한다." }))
        {
          case 1:
            break;
          case 2:
            break;
          case 3:
            break;
          case 4:
            GameExit();
            break;
        }
      }
    }

  }
}