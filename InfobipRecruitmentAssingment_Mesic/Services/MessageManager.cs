using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfobipRecruitmentAssingment_Mesic
{
    internal class MessageManager
    {
        private readonly IFileRepo _fileRepo;
        private readonly MessageService _messageService;

        public MessageManager(IFileRepo fileRepo, MessageService messageService)
        {
            _fileRepo = fileRepo;
            _messageService = messageService;
        }

        internal int GenerateRandomId()
        {
            Random random = new Random();
            return random.Next(10000, 999999);
        }

        public async Task SendMessagesToApi()
        {
            IList<FileMessage> fileMessages = _fileRepo.ReadMessages();
            List<ResponseMessage> newMessages = new List<ResponseMessage>();

            Console.WriteLine($"Total messages read from file: {fileMessages.Count}");

            foreach (var smsMessage in fileMessages)
            {
                try
                {
                  
                    smsMessage.MessageId = GenerateRandomId().ToString();
                    var messageToSend = CreateApiMessage(smsMessage, smsMessage.MessageId);

                    var response = await SendApiMessageAsync(messageToSend);

                    newMessages.Add(DeserialiseResponse(response));

                    Console.WriteLine($"Message sent: {smsMessage}");
                    Console.WriteLine($"API Response: {response}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send message: {smsMessage}\nError: {ex.Message}");
                }
            }

            MatchResponseToFileMessages(fileMessages, newMessages);

            _fileRepo.SaveMessages(fileMessages);
        }

        

        private void MatchResponseToFileMessages(IList<FileMessage> fileMessages, List<ResponseMessage> newMessages)
        {
            Dictionary<string, string> descriptions = new Dictionary<string, string>(); 
            newMessages.SelectMany(x => x.messages).ToList().ForEach(y=> descriptions.Add(y.messageId, y.status.description));

            descriptions.ToList().ForEach(x => fileMessages.Where(y => y.MessageId == x.Key).ToList().ForEach(z => z.MessageDescription = x.Value));
        }


        private RequestMessage CreateApiMessage(FileMessage smsMessage, string messageId)
        {

            return new RequestMessage
            {
                destinations = new List<Destination>
        {
            new Destination
            {
                messageId = messageId,
                to = smsMessage.MSISDN
            }
        },
                from = smsMessage.SenderId,
                text = smsMessage.MessageDescription
            };
        }

        private async Task<string> SendApiMessageAsync(RequestMessage message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            return await _messageService.SendAsync(message);
        }

        private void HandleApiResponse(FileMessage smsMessage, Dictionary<string, string> messageIdToDescription, string response)
        {
            if (messageIdToDescription.TryGetValue(smsMessage.MessageId, out string description))
            {
                Console.WriteLine($"Message sent: {smsMessage}\n");

                Console.WriteLine($"API Response: {response}\n");

                smsMessage.MessageDescription = description;
            }
        }

        static ResponseMessage DeserialiseResponse(string response)
        {
            return JsonSerializer.Deserialize<ResponseMessage>(response);
        }

    }
}