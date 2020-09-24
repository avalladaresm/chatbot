using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Web;

namespace chatbotapi
{
    public class Program
    {
        public static void Main(string[] args)
        {

					// YOUR-APP-ID: The App ID GUID found on the www.luis.ai Application Settings page.
            var appId = "3b0c5522-c455-456d-bf96-218f4f368fd9";

            // YOUR-PREDICTION-KEY: 32 character key.
            var predictionKey = "ce0692c934ff4097b6be348fade5705b";

            // YOUR-PREDICTION-ENDPOINT: Example is "https://westus.api.cognitive.microsoft.com/"
            var predictionEndpoint = "https://luisresource-alejo.cognitiveservices.azure.com/";

            // An utterance to test the pizza app.
            var utterance = "agendar reunion el viernes a las 2 pm";
            //////////

            MakeRequest(predictionKey, predictionEndpoint, appId, utterance);
						
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

				static async void MakeRequest(string predictionKey, string predictionEndpoint, string appId, string utterance)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // The request header contains your subscription key
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", predictionKey);

            // The "q" parameter contains the utterance to send to LUIS
            queryString["query"] = utterance;

            // These optional request parameters are set to their default values
            queryString["verbose"] = "true";
            queryString["show-all-intents"] = "true";
            queryString["staging"] = "false";
            queryString["timezoneOffset"] = "0";

            var predictionEndpointUri = String.Format("{0}luis/prediction/v3.0/apps/{1}/slots/production/predict?{2}", predictionEndpoint, appId, queryString);

            // Remove these before updating the article.
            Console.WriteLine("endpoint: " + predictionEndpoint);
            Console.WriteLine("appId: " + appId);
            Console.WriteLine("queryString: " + queryString);
            Console.WriteLine("endpointUri: " + predictionEndpointUri);

            var response = await client.GetAsync(predictionEndpointUri);

            var strResponseContent = await response.Content.ReadAsStringAsync();

            // Display the JSON result from LUIS.
            Console.WriteLine(strResponseContent.ToString());
        }
    }
}
