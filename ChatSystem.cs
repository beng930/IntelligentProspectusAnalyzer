using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace OpenAIRequestExample
{
    public class ChatSystem
    {
        private TunningParameters tunningParameters;
        private PromptTemplate promptTemplate;
        private OpenAiService service;
        private ILogger logger;
        private bool _running = false;
        private string inputs = string.Empty;

        public ChatSystem(OpenAiService service, ILogger logger)
        {
            this.tunningParameters = new TunningParameters
            {
                temperature = 0,
            };
            this.promptTemplate = new PromptTemplate
            {
                Template = @"""messages"": [{0}]",
                TunningParameters = tunningParameters
            };

            this.service = service;
            this.logger = logger;
            inputs += Constants.systemContextForBot;
        }

        public async void RunChatSystem()
        {
            var documentsSummary = await this.service.GetDocumentsSummary();
            var documentsContent = documentsSummary.Select(documentSearch => documentSearch.Document["content"].ToString()).Take(2);
            inputs += @"{""role"": ""fileSystem"", ""content"": """ + string.Join(',', documentsContent) + @"""}," + "\n";

            _running = true;
            await GenerateBotResponse(); // generate first greeting message
            while (_running)
            {
                try
                {
                    Console.Write(" > ");

                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        continue;
                    }

                    if (input.ToLower() == "close")
                    {
                        Stop();
                        break;
                    }

                    inputs += (@"{""role"": ""user"", ""content"": """ + input + @"""}," + "\n");
                    await GenerateBotResponse();
                }
                catch(Exception ex)
                {
                    logger.LogError(ex.Message);
                }
            }
        }

        private async Task GenerateBotResponse()
        {
            var response = await this.service.AskWithFormatAsync(this.promptTemplate, inputs);
            JObject joResponse = JObject.Parse(response);
            var responseContent = (string?)joResponse?["content"];
            Console.WriteLine($"{responseContent}");
            inputs += (@"{""role"": ""assistant"", ""content"": """ + responseContent + @"""}," + "\n");
        }

        public void Stop()
        {
            _running = false;
            Console.WriteLine(Constants.endChat);
        }
    }
}
