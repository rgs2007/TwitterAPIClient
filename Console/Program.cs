using log4net;
using log4net.Config;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;

namespace TwitterStatisticsClient
{
    class Program
    {

        static HttpClient client = new HttpClient();
        static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        static async Task ReadTweetsAsync(ITwitterStatisctics statisctics)
        {

            var url = "https://api.twitter.com/2/tweets/sample/stream";
            var bearerToken = "AAAAAAAAAAAAAAAAAAAAAM7xVwEAAAAAYzdqup7%2BDz4Hcs5HvBu7eH%2FEBW4%3DCBu9bhwQCTUI428pE5gA2Db0PxFBKY8k2w2D5rUhI7HiLsGup4";
            string? line;

            client.Timeout = TimeSpan.FromMilliseconds(30000);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var request = new HttpRequestMessage(HttpMethod.Get, url);
                
            using (var response = await client.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    using (var body = await response.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(body))
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            statisctics.CalculateStatistcs(line);
                        }  
                        else
                        {
                            log.Info(string.Format("Keep alive"));
                        }
                    }
                }
                else
                {
                    log.Error(string.Format("Error while connecting: {0} {1}", response.StatusCode.ToString(), response.ReasonPhrase));
                }
                            
            }

        }

        static void Main()
        {

            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

                Console.WriteLine("Choose the type of statistics you want to calculate:");
                Console.WriteLine("1 - Average Tweets Per Minute and Total Tweets");
                Console.WriteLine("2 - Total Tweets Only");

                string statisctsType = Console.ReadLine();

                ITwitterStatisctics twitterStatisctics = TwitterStatisticsFactory.CreateStatistics(statisctsType);

                try
                {
                    ReadTweetsAsync(twitterStatisctics);

                }
                catch (Exception ex)
                {
                    log.Error(string.Format("Failed while reading tweets: {0}", ex.Message));
                    throw;
                }

                while (true)
                {
                    Console.WriteLine("Type \"check\" to see the results");
                    string checkResponse = Console.ReadLine();

                    if (checkResponse == "check")
                    {
                        Console.WriteLine(twitterStatisctics.Write());
                    }

                }
            }
            catch(Exception ex)
            {
                log.Error(string.Format("Failed. Error: {0}", ex.Message));
            }

        }

    }
}
