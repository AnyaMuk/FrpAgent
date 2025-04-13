using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomlyn.Model;
using Tomlyn;

namespace FrpAgent.Config
{

    class localFRPConfig
    {
        public void verifyConfig()
        {
            Console.WriteLine("检测文件是否存在……");
            if (!File.Exists("config.toml"))
            {
                Console.WriteLine("未检测到配置文件！尝试创建文件");
                using (File.Create("config.toml")) ;

            }
        }
        public void GetConfig()
        {
            using (StreamReader sr = new StreamReader("config.toml", Encoding.UTF8))
            {
                string line;
                Console.WriteLine("\n当前配置文件内容为:");
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

        }


        public void ReceiveMainConfig(string server_Addr, int server_Port, string? auth_Method, string? auth_Token)
        {
            if (string.IsNullOrEmpty(auth_Method))
            {
                var MainConfig = new TomlTable
                {
                    ["serverAddr"] = server_Addr,
                    ["serverPort"] = server_Port

                };
                var tomlserialize = Toml.FromModel(MainConfig);
                Console.WriteLine("接收到主配置项数据，尝试写入……");
                File.WriteAllText("config.toml", tomlserialize);
            }
            else
            {
                var MainConfig = new TomlTable
                {
                    ["serverAddr"] = server_Addr,
                    ["serverPort"] = server_Port,
                    ["auth.method"] = auth_Method,
                    ["auth.token"] = auth_Token
                };
                var tomlserialize = Toml.FromModel(MainConfig);
                Console.WriteLine("接收到主配置项数据，尝试写入……");
                File.WriteAllText("config.toml", tomlserialize);
            }


        }
        public void AppendServiceConfig(string _name, string _type, string _localIP, int _localPort, int _remotePort)
        {
            var sc = new TomlTable
            {
                ["proxies"] = new List<TomlTable>
                {
                    new TomlTable
                    {
                        ["name"] = _name,
                        ["type"] = _type,
                        ["localIP"] = _localIP,
                        ["localPort"] = _localPort,
                        ["remotePort"] = _remotePort
                    }

                }
            };
            var tomlserialize = Toml.FromModel(sc);
            Console.WriteLine("接收到业务配置项数据，尝试追加写入……");
            File.AppendAllText("config.toml", tomlserialize);
        }
    }
}
