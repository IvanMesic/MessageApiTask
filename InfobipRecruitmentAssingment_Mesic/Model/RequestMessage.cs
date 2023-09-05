using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfobipRecruitmentAssingment_Mesic
{
    public class Destination
    {
        public string messageId { get; set; }
        public string to { get; set; }
    }

    public class RequestMessage
    {
        public List<Destination> destinations { get; set; }
        public string from { get; set; }
        public string text { get; set; }
        public Status status { get; set; }
    }


    public class Root
    {
        public List<RequestMessage> messages { get; set; }
    }
}
