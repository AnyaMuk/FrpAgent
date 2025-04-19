namespace FrpAgent
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine("程序开始运行……");

            launcher run = new launcher();
            await run.RunProgram();

            Console.WriteLine("程序终止运行");

            Console.ReadKey();
        }

    }
}
