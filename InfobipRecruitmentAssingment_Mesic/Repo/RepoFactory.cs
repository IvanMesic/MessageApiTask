using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfobipRecruitmentAssingment_Mesic
{
    public static class RepoFactory
    {
        public static IFileRepo fileRepo => new FileRepo();
    }
}
