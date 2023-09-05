using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfobipRecruitmentAssingment_Mesic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IFileRepo fileRepo = RepoFactory.fileRepo;
            File.WriteAllText("messages.csv", string.Empty);
            string desc = "SenderId|MSISDN|messageId|description";
            string defValue = "InfoSMS,385993283330";

            string[] lines = new string[] { desc, defValue, defValue, defValue };
            
            File.WriteAllLines("messages.csv", lines);
            MessageService messageService = new MessageService(ConfigService.GetApiUrl(), ConfigService.GetApiKey());
           
            MessageManager messageManager = new MessageManager(fileRepo, messageService);

            await messageManager.SendMessagesToApi();

        }
    }
}
