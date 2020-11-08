using static GAMJA.Game.ConsoleFunc;
using static System.Console;
using System;

namespace GAMJA.Game
{
  class Player
  {
    public string Name { get; set; }
    int maxHP;
    int HP;
    int maxMP;
    int MP;
    int AT;

    public void CharacterSetting()
    {
      bool whileA = true;
      while (whileA)
      {
        Clear();
        InGame.CWTitle();
        WriteLine("지금부터 당신의 캐릭터를 생성합니다.");
        
        string _name = ReadTextScreen("캐릭터의 이름을 설정하시오.");
        if (_name != "")
        {
          Name = _name;
          whileA = false;
        }
      }

      maxHP = 50;
      HP = maxHP;
      maxMP = 50;
      MP = maxMP;
      AT = 10;

      Clear();
      InGame.CWTitle();
      WriteLine("캐릭터 생성을 성공적으로 마쳤습니다. \n\n");
      WriteLineColor($"\t당신의 캐릭터의 능력치 ", ConsoleColor.Black, ConsoleColor.White);
      WriteLineColor($"\t이름: {Name} ", ConsoleColor.Black, ConsoleColor.White);
      WriteLineColor($"\tHP: {HP} ", ConsoleColor.Black, ConsoleColor.White);
      WriteLineColor($"\tMP: {MP} ", ConsoleColor.Black, ConsoleColor.White);
      WriteLineColor($"\t공격력: {AT} ", ConsoleColor.Black, ConsoleColor.White);
      ReadKey();
    }
  }
}
