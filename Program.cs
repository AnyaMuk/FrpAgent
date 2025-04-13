namespace FrpAgent
{
    using FrpAgent.Config;
    using FrpAgent.Client;
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("程序开始运行……");
            Console.WriteLine("开始初始化Frpc配置文件……");
            localFRPConfig _serialize = new localFRPConfig();
            _serialize.verifyConfig();
            _serialize.ReceiveMainConfig("1.1.1.1", 7000,null,null);
            _serialize.AppendServiceConfig("TEST", "tcp", "127.0.0.1", 25565, 25565);
            _serialize.GetConfig();

            Console.WriteLine("配置文件已就绪！尝试启动FRPC进程……");
            ClientController myclient = new ClientController();
            var cts = new CancellationTokenSource();
            await myclient.OpenFRProcess(cts.Token);


            Console.WriteLine("程序终止");
            Console.ReadKey();
        }




    }
}
