using System;
using static System.Console;
using static System.ConsoleKey;

namespace GAMJA.Game
{
  static class ConsoleFunc
  {

    public static void CWTitle()
    {
      Clear();
      WriteLineColor(ConsoleText.GameTitleText, ConsoleColor.DarkBlue);
    }

    public static int SelectScreen(string question, string[] answers, bool clearOnWrongAnswer = false)
    {
      if (answers.Length == 0 || answers.Length > 9)
        return 0;

      while (true)
      {
        if (clearOnWrongAnswer)
          Clear();
        WriteLineColor(question);

        for (int i = 0; i < answers.Length; i++)
          WriteLineColor($"{i + 1} . {answers[i]}");

        ConsoleKey ReadKey = Console.ReadKey().Key;
        ConsoleKey[] SelectKey = { D1, D2, D3, D4, D5, D6, D7, D8, D9 };

        for (int i = 0; i < SelectKey.Length; i++)
        {
          if (ReadKey == SelectKey[i])
            return i + 1;
        }
        return -1;
      }
    }

    public static string ReadTextScreen(string question)
    {
      WriteLineColor("\n" + question + "\n");
      WriteColor("여기에 입력: ");
      string CRead = ReadLine();
      WriteLine();

      return CRead;
    }

    public static void WriteLineColor<T>(T text, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
    {
      BackgroundColor = backgroundColor;
      ForegroundColor = foregroundColor;
      WriteLine(text);
      ResetColor();
    }
    public static void WriteColor<T>(T text, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
    {
      BackgroundColor = backgroundColor;
      ForegroundColor = foregroundColor;
      Write(text);
      ResetColor();
    }
  }
}
