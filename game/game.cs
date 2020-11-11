﻿using System;
using GAMJA.Entity;
using GAMJA.Inventory;
using static GAMJA.Game.ConsoleFunc;
using static System.Console;
using static System.ConsoleKey;
using static System.ConsoleColor;

namespace GAMJA.Game
{
  static partial class InGame
  {

    public static void GameEx()
    {
      Title = "GAMJA MUD RPG";
      GameMain();
    }

    private static void GameMain()
    {
      while (true)
      {
        CWTitle();
        switch (SelectScreen("\n감자 MUD RPG 게임 v.0.01\n", new string[] { "게임 시작\n", "게임 종료\n" }))
        {
          case D1:
            CharacterSetting();
            CWTitle();
            WriteLineColor(" 마을 필드로 이동합니다.", ConsoleColor.DarkGreen);
            ReadKey();
            CurrentMap = MapList.TownField;
            return;
          case D2:
            GameExit();
            break;
        }
      }

    }

    private static void GameExit()
    {
      while (true)
      {
        CWTitle();
        switch (SelectScreen("\n게임을 종료하시겠습니까?\n", new string[] { "게임 종료\n", "뒤로 가기\n" }))
        {
          case D1:
            Environment.Exit(0);
            return;
          case D2:
            return;
        }
      }
    }


    static Player myPlayer;

    public static void CharacterSetting()
    {
      bool whileA = true;
      while (whileA)
      {
        CWTitle();
        WriteLineColor("지금부터 당신의 캐릭터를 생성합니다.");

        string readName = ReadTextScreen("캐릭터의 이름을 설정하시오.");
        if (readName != "")
        {
          myPlayer = new Player(readName, 1, 50, 30, 10);
          whileA = false;
        }
      }

      CWTitle();
      WriteLineColor("캐릭터 생성을 성공적으로 마쳤습니다. \n\n");
      myPlayer.GetInfo();
      ReadKey();
    }

    public static void OpenInventory()
    {
      myPlayer.Inventory.Open();
    }

    public static void OpenPlayerStatus()
    {
      WriteCurrentLocation();
      myPlayer.GetInfo();
      ReadKey();
    }
    public static void KillMe()
    {
      Clear();
      WriteColor(ConsoleText.KillMeText, DarkRed);
      Beep(1000, 2000);
      ReadKey();
      Environment.Exit(0);
    }
  }
}
