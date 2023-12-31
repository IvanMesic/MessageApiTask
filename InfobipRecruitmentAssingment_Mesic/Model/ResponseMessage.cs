﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfobipRecruitmentAssingment_Mesic
{
    public class ResponseMessage
    {
        public List<MessageInfo> messages { get; set; }
    }

    public class MessageInfo
    {
        public string to { get; set; }
        public Status status { get; set; }
        public string messageId { get; set; }
        public int smsCount { get; set; }
    }

    public class Status
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
