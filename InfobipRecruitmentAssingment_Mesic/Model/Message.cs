    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    namespace InfobipRecruitmentAssingment_Mesic
    {
        public class FileMessage
        {
            public string SenderId { get; set; }
            public string MSISDN { get; set; }
            public string MessageId { get; set; }
            public string MessageDescription { get; set; }

            FileMessage(string senderId, string mSISDN, string messageId, string messageDescription)
            {
                SenderId = senderId;
                MSISDN = mSISDN;
                MessageId = messageId;
                MessageDescription = messageDescription;
            }
            FileMessage(string senderId, string mSISDN)
            {
                SenderId = senderId;
                MSISDN = mSISDN;
            }
            public FileMessage()
            {
            
            }

            public override string ToString()
            {
                return "Sender ID: " + SenderId + "MSISDN: " + MSISDN + "\nMessage ID: " + MessageId + "\nMessage Description: " + MessageDescription;
            }

            public string FormatForFileLine()
            {
                return $"{SenderId},{MSISDN},{MessageId},{MessageDescription}";
            }

            public static FileMessage FormatFromLine(string line)
            {
                string[] parts = line.Split(',');
                return new FileMessage {

                    SenderId = parts.ElementAtOrDefault(0),
                    MSISDN = parts.ElementAtOrDefault(1),
                    MessageId = parts.ElementAtOrDefault(2) ?? "",
                    MessageDescription = parts.ElementAtOrDefault(3) ?? ""
                };
            }

        }
       
    }
