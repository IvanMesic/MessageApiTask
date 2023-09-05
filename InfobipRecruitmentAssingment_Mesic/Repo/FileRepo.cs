using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfobipRecruitmentAssingment_Mesic
{
    internal class FileRepo : IFileRepo
    {
        internal static readonly string file = "messages.csv";

        public FileRepo() => CreateFileIfNotExist();

        private void CreateFileIfNotExist()
        {
            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }
        }

        public IList<FileMessage> ReadMessages()
        {
            string[] lines = File.ReadAllLines(file);

            IList<FileMessage> messages = lines
                .Skip(1)
                .Select(FileMessage.FormatFromLine)
                .ToList();

            return messages;
        }

        public void SaveMessage(FileMessage message)
        {
            throw new NotImplementedException();
        }

        public void SaveMessages(IList<FileMessage> messages)
        {
            var header = "SenderId|MSISDN|messageId|description";
            var lines = messages.Select(message => message.FormatForFileLine());

            var content = new List<string> { header };
            content.AddRange(lines);

            File.WriteAllLines(file, content);
        }
    }
}
