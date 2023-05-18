using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace OpenAIRequestExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(); // Add console logging
            });
            var logger = loggerFactory.CreateLogger<OpenAiService>();

            var openAiService = new OpenAiService(logger);
            //var fileParitionService = new FilePartitionService();
            //await fileParitionService.PartitionFileAndSaveToBlob();

            var chat = new ChatSystem(openAiService, logger);
            var chatThread = new Thread(() => chat.RunChatSystem());
            chatThread.Start();

            var exitEvent = new ManualResetEvent(false);

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                chat.Stop();
                exitEvent.Set();
            };

            await Task.Run(() => exitEvent.WaitOne());
        }
    }
}
