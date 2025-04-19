using System.Text;

namespace FrpAgent
{

    class localFRPConfig
    {
        public async Task ConfigChecker()
        {
            verifyConfig();
            GetConfig();
        }
        public void verifyConfig()
        {
            Console.WriteLine("检测文件是否存在……");
            if (!File.Exists("config.json"))
            {
                Console.WriteLine("未检测到配置文件！尝试创建文件");
                using (File.Create("config.json")) ;

            }
            else
            {
                Console.WriteLine("文件存在");
            }
        }
        public void GetConfig()
        {
            using (StreamReader sr = new StreamReader("config.json", Encoding.UTF8))
            {
                string line;
                Console.WriteLine("\n当前配置文件内容为:");
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

        }
    }
}
