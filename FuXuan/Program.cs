namespace FuXuan
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            while (true)
            {
                bool exitFlag = menu.Process();
                if (exitFlag) break;
            }
        }
    }
}