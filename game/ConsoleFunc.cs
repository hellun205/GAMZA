using System;
using static System.Console;

namespace GAMJA.Game
{
  static class ConsoleFunc
  {
    public static string MainText = ConsoleText.GameMainText + "\n감자 MUD RPG 게임 v.0.01\n";



    public static int SelectScreen(string _Question, string[] _Answer, bool _ClearConsole = false)
    {
      if (_Answer.Length == 0 || _Answer.Length > 9)
      {
        return 0;
      }

      if (_ClearConsole) Clear();

      while (true)
      {
        WriteLine(_Question);

        for (int i = 0; i < _Answer.Length; i++)
        {
          WriteLine((i + 1) + " . " + _Answer[i]);
        }

        ConsoleKey ReadKey = Console.ReadKey().Key;
        ConsoleKey[] SelectKey = {ConsoleKey.D1 , ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4
            , ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9};

        for (int i = 0; i < _Answer.Length; i++)
        {
          if (ReadKey == SelectKey[i])
          {
            return i + 1;
          }
        }

      }

    }

    public static string ReadTextScreen(string _Question)
    {
      WriteLine("\n" + _Question + "\n");
      Write("여기에 입력: ");
      string CRead = ReadLine();
      WriteLine();

      return CRead;
    }

    public static void WriteLineColor<T>(T _Text, ConsoleColor _ForegroundColor, ConsoleColor _BackgroundColor = ConsoleColor.Black)
    {
      BackgroundColor = _BackgroundColor;
      ForegroundColor = _ForegroundColor;
      WriteLine(_Text);
      ResetColor();
    }

  }
}
