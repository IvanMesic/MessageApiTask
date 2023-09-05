using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfobipRecruitmentAssingment_Mesic
{
    public interface IFileRepo
    {
        void SaveMessage(FileMessage message);
        void SaveMessages(IList<FileMessage> messages);
        IList<FileMessage> ReadMessages();
    }
}
