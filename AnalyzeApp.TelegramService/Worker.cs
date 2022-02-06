using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AnalyzeApp.TelegramService
{
    public class Worker : BackgroundService
    {
        private const string _fileName = "user.json";

        public Worker()
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                NLogLogger.LogInfo($"Worker running at: {DateTimeOffset.Now}");
                var objUser = new UserModel().LoadJsonFileService(_fileName);
                if (objUser == null)
                {
                    NLogLogger.LogInfo("User is null");
                    continue;
                }
                var lstRemoveFile = new List<string>();
                var lstSend = new List<string>();
                var currentPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                var files = Directory.EnumerateFiles($"{currentPath}\\SendNoti", "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".txt"));
                foreach (var fileName in files)
                {
                    using (var streamReader = File.OpenText(fileName))
                    {
                        var lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (lines.Length > 0)
                        {
                            lstRemoveFile.Add(fileName);
                        }
                        var strTemp = string.Empty;
                        foreach (var line in lines)
                        {
                            if (line.Contains("#*#"))
                            {
                                strTemp += line.Replace("#*#", "");
                                lstSend.Add(strTemp);
                                strTemp = string.Empty;
                            }
                            else
                            {
                                strTemp += $"{line}\r\n";
                            }
                        }
                    }
                    foreach (var item in lstSend)
                    {
                        //send
                        var result = await TeleClient.SendMessage(objUser.Phone, item);
                        Thread.Sleep(1000);
                    }
                    lstSend.Clear();
                }
                foreach (var item in lstRemoveFile)
                {
                    //File.Delete(item);
                }
                lstRemoveFile.Clear();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
