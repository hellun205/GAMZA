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
        
        private static void MainText()
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
            
_GameMain:  switch (GameMain())
            {
                case SelectMain.GAMESTART:
                    InGame();
                    break;
                case SelectMain.GAMEEXIT:
                    
                    switch (GameExit())
                    {
                        case SelectIfExit.GAMEEXIT:
                            System.Environment.Exit(0);
                            return;
                        case SelectIfExit.GOBACK:
                            goto _GameMain;                          
                    }

                    break;
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

        private static SelectIfExit GameExit()
        {
            while (true)
            {
                MainText();
                Console.WriteLine("게임을 종료하시겠습니까?");
                Console.WriteLine("1 . 게임 종료");
                Console.WriteLine("2 . 뒤로 가기");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        return SelectIfExit.GAMEEXIT;
                    case ConsoleKey.D2:
                        return SelectIfExit.GOBACK;
                    default:
                        MainText();
                        Console.WriteLine("잘못된 선택지 입니다.");
                        Console.ReadKey();
                        break;
                }

            }
        }

        private static void InGame()
        {



        }

}
}
