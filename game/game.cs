using GAMJA.Entity;
using System;
using static GAMJA.Game.ConsoleFunc;
using static GAMJA.Game.Profile.Profile;
using static System.Console;
using static System.ConsoleKey;

namespace GAMJA.Game
{
  static partial class InGame
  {
    public static Player myPlayer;

    public static void GameEx()
    {
      Title = "GAMJA MUD RPG";
      GameMain();
    }

    private static void GameMain()
    {
      while (true)
      {
        Player user;
        CWTitle();
        switch (SelectScreen("\n감자 MUD RPG 게임 v.0.01\n", new string[] { "새로 시작\n", "이어서 시작\n", "게임 종료\n" }))
        {
          case D1:
            CWTitle();
            user = CreateUser();
            if (user != null)
            {
              myPlayer = user;
              myPlayer.GetInfo();
              CWTitle();
              CurrentMap = MapList.TownField;
              return;
            }
            else break;

          case D2:
            CWTitle();
            user = GetPlayer();
            if (user != null)
            {
              myPlayer = user;
              myPlayer.GetInfo();
              CWTitle();
              CurrentMap = MapList.TownField;
              return;
            }
            else break;
          case D3:
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
    //public static void KillMe()
    //{
    //  Clear();
    //  WriteColor(ConsoleText.KillMeText, DarkRed);
    //  Beep(1000, 2000);
    //  ReadKey();
    //  Environment.Exit(0);
    //}
  }
}
