using System;
using static System.Console;
using static System.ConsoleKey;

namespace GAMJA.Game
{
  static class ConsoleFunc
  {
    public static string MainText = ConsoleText.GameMainText + "\n감자 MUD RPG 게임 v.0.01\n";
    
    public static int SelectScreen(string question, string[] answers, bool clearOnWrongAnswer = false)
    {
      if (answers.Length == 0 || answers.Length > 9)
        return 0;

      while (true)
      {
        if (clearOnWrongAnswer)
          Clear();
        WriteLine(question);

        for (int i = 0; i < answers.Length; i++)
          WriteLine($"{i + 1} . {answers[i]}");

        ConsoleKey ReadKey = Console.ReadKey().Key;
        ConsoleKey[] SelectKey = { D1 , D2, D3, D4, D5, D6, D7, D8, D9 };

        for (int i = 0; i < SelectKey.Length; i++)
        {
          if (ReadKey == SelectKey[i])
            return i + 1;
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
