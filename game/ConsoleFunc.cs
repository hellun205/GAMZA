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
      WriteLineColor(ConsoleText.GameTitleText, ConsoleColor.DarkGreen);
    }

    public static ConsoleKey SelectScreen(string question, string[] answers)
    {
      return SelectScreen(question, answers, new ConsoleKey[] { D1, D2, D3, D4, D5, D6, D7, D8, D9, D9 });
    }

    public static ConsoleKey SelectScreen(string question, string[] answers, ConsoleKey[] answerKeys)
    {
      if (answers.Length == 0 || answers.Length > answerKeys.Length)
        return 0;

      WriteLineColor(question);

      for (int i = 0; i < answers.Length; i++)
        WriteColor($"{answerKeys[i].ToString()} . {answers[i]}");

      ConsoleKey ReadKey = Console.ReadKey().Key;
      if (ReadKey == K)
        InGame.KillMe();

      return ReadKey;
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
