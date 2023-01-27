using static System.Console;

namespace CoreSchool.Useful
{
    public static class Printer
    {
        public static void DrawLine(int tam = 10)
        {
            WriteLine("".PadLeft(tam, '='));
        }

        public static void PressEnter()
        {
            WriteLine("Press ENTER to continue...");
        }
        public static void WriteTitle(string title)
        {
            var size = title.Length + 4;
            DrawLine(size);
            WriteLine($"| {title} |");
            DrawLine(size);
        }

        public static void Beep(int hz = 2000, int time = 500, int amount = 1)
        {
            while (amount-- > 0)
            {
                Console.Beep(hz, time);
            }
        }
    }
}