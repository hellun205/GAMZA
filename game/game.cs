using System;
using static GAMJA.Game.ConsoleFunc;
using static System.Console;

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
        switch (SelectScreen(MainText, new string[] { "게임 시작", "게임 종료" }, true))
        {
          case 1:
            CharacterSetting(MyPlayer);
            CWTitle();
            WriteLineColor(" 마을 필드로 이동합니다.", ConsoleColor.DarkGreen);
            ReadKey();
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

    public static void CharacterSetting(Player player)
    {
      bool whileA = true;
      while (whileA)
      {
        CWTitle();
        WriteLine("지금부터 당신의 캐릭터를 생성합니다.");

        string _name = ReadTextScreen("캐릭터의 이름을 설정하시오.");
        if (_name != "")
        {
          player.Name = _name;
          whileA = false;
        }
      }

      player.Level = 1;

      CWTitle();
      WriteLine("캐릭터 생성을 성공적으로 마쳤습니다. \n\n");
      WriteLineColor(MyPlayer.GetInfo, ConsoleColor.Black, ConsoleColor.White);
      ReadKey();
    }


  }
}
