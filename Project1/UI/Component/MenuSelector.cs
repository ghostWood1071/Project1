using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.UI.Component
{
    class MenuSelector
    {
        private string[] ultilities;
        private string title;
        public MenuSelector(string[] ultilities, string title)
        {
            this.ultilities = ultilities;
            this.title = title;
        }

        public int Selector()
        {
            Console.CursorVisible = false;
            int pos = 0;
            PrintMenu(this.ultilities, pos, this.title);
            int thisPad = Console.CursorLeft;
            Console.CursorLeft = Console.WindowWidth / 2 - title.Length / 2;
            Console.WriteLine("Bạn đang chọn: " + (pos + 1));
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (pos < this.ultilities.Length-1)
                        {
                            pos += 1;
                            Console.Clear();
                            PrintMenu(ultilities, pos, this.title);
                            Console.CursorLeft = Console.WindowWidth / 2 - title.Length / 2;
                            Console.WriteLine("Bạn đang chọn: " + (pos + 1));
                        }
                        Console.CursorLeft = thisPad;
                        break;
                    case ConsoleKey.UpArrow:
                        if (pos > 0)
                        {
                            pos -= 1;
                            Console.Clear();
                            PrintMenu(ultilities, pos, this.title);
                            Console.CursorLeft = Console.WindowWidth / 2 - title.Length / 2;
                            Console.WriteLine("Bạn đang chọn: " + (pos + 1));
                        }
                        Console.CursorLeft = thisPad;
                        break;
                    case ConsoleKey.Enter:
                        return pos;
                }
            }

        }

        private void PrintMenu(string[] menu, int pos, string title)
        {
            Console.CursorTop += 10;
            Console.CursorLeft = Console.WindowWidth / 2 - title.Length / 2;
            Console.WriteLine(title);
            for (int i = 0; i < menu.Length; i++)
            {
                if (i == pos)
                {
                    Console.CursorLeft = Console.WindowWidth / 2 - title.Length / 2;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine(menu[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.CursorLeft = Console.WindowWidth / 2 - title.Length / 2;
                    Console.WriteLine(menu[i]);
                }
            }
        }

    }
}
