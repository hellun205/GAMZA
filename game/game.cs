using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GAMJA.game;

namespace GAMJA.game
{
    class ingame
    {
        
        static void MainText()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(ConsoleText.GameMainText);
            Console.ResetColor();
            Console.WriteLine("감자 MUD RPG 게임 v.0.01");
            Console.WriteLine("");            
        }

        public static void Gameex()
        {
            SelectMain SM = GameMain();

            if (SM == SelectMain.GAMESTART)
            {
                  

            }

        }

        private static SelectMain GameMain()
        {
            while (true)
            {
                MainText();                
                Console.WriteLine("다음 선택지 중 선택 하시오.");
                Console.WriteLine("1 . 게임 시작");
                Console.WriteLine("2 . 게임 종료");
                
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        return SelectMain.GAMESTART;
                    case ConsoleKey.D2:
                        return SelectMain.GAMEEXIT;
                    default:
                        MainText();
                        Console.WriteLine("잘못된 선택지 입니다.");
                        Console.ReadKey();
                        break;

                }

            }
        }

        private static void Storys()
        {



        }

    }
}
