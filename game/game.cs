using System;
using GAMJA.Entity;
using GAMJA.Inventory;
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
        CWTitle();
        switch (SelectScreen("\n감자 MUD RPG 게임 v.0.01\n", new string[] { "게임 시작", "게임 종료" }))
        {
          case 1:
            CharacterSetting();
            CWTitle();
            WriteLineColor(" 마을 필드로 이동합니다.", ConsoleColor.DarkGreen);
            ReadKey();
            CurrentMap = MapList.TownField;
            return;
          case 2:
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
        switch (SelectScreen("\n게임을 종료하시겠습니까?", new string[] { "게임 종료", "뒤로 가기" }))
        {
          case 1:
            Environment.Exit(0);
            return;
          case 2:
            return;
        }
      }
    }


    static Player myPlayer;
    static Inven myInventory;

    public static void CharacterSetting()
    {
      bool whileA = true;
      while (whileA)
      {
        CWTitle();
        WriteLine("지금부터 당신의 캐릭터를 생성합니다.");

        string readName = ReadTextScreen("캐릭터의 이름을 설정하시오.");
        if (readName != "")
        {
          myPlayer = new Player(readName, 1, 50, 30, 10);
          myInventory = new Inven(24, 24, myPlayer);
          whileA = false;
        }
      }

      CWTitle();
      WriteLine("캐릭터 생성을 성공적으로 마쳤습니다. \n\n");
      WriteLineColor(myPlayer.GetInfo, ConsoleColor.Black, ConsoleColor.White);
      ReadKey();
    }

    public static void OpenInventory()
    {
      myInventory.Open();
    }

    public static void OpenPlayerStatus()
    {

    }
  }
}
