using System;
using static GAMJA.Game.ConsoleFunc;
using static System.Console;

namespace GAMJA.Game
{
  static class InGame
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
        switch (SelectScreen(MainText, new string[] { "게임 시작", "게임 종료" }, true))
        {
          case 1:
            GameStart();
            return;
          case 2:
            if (SelectScreen(MainText + "게임을 종료하시겠습니까?", new string[] { "게임 종료", "뒤로 가기" }, true) == 1)
            {
              Environment.Exit(0);
            }
            break;
        }
      }

    }

    static Player MyPlayer = new Player();

    private static void GameStart()
    {
      MyPlayer.CharacterSetting();

    }



    public static void CWTitle()
    {
      Clear();
      ForegroundColor = ConsoleColor.DarkBlue;
      WriteLine(ConsoleText.GameTitleText);
      ResetColor();
    }
  }
}
