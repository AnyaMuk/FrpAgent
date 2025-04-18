﻿using System.Diagnostics;

namespace FrpAgent
{
    class ProcessController
    {
        public async Task OpenFRProcess(CancellationToken cts)
        {
            Process frpclient = new Process
            {
               StartInfo = new ProcessStartInfo
               {
                   FileName = "frpc.exe",
                   UseShellExecute = false,
                   CreateNoWindow = true,
                   Arguments = "-c config.json"
               }
            };
            frpclient.Start();

            Console.WriteLine("进程已启动！Id：{0}", frpclient.Id);
            await Task.Run(async () =>
            {
                Console.WriteLine("开始监控程序运行状态……");
                while(!frpclient.HasExited)
                {
                    Console.WriteLine("Frp客户端运行中……");
                    await Task.Delay(3000);
                    frpclient.Refresh();
                    if (cts.IsCancellationRequested)
                    {
                        break;
                    }
                }
                frpclient.Kill();
                Console.WriteLine("进程已退出！{0}",frpclient.ExitCode);
            });
        }
    }
}
