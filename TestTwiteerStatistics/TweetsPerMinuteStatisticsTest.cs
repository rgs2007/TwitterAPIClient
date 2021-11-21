using Xunit;
using TwitterStatisticsClient;

namespace TestTwiteerStatistics
{
    public class TweetsPerMinuteStatisticsTest
    {
        [Fact]
        public void CalculateStatistcs()
        {
            TweetsPerMinuteStatistics statisticsClient = new TweetsPerMinuteStatistics();
            string line = "test";

            statisticsClient.CalculateStatistcs(line);
            statisticsClient.CalculateStatistcs(line);
            statisticsClient.CalculateStatistcs(line);

            Assert.Equal(statisticsClient.Write(),
                "Total Tweets Received: 3\r\nAverage Tweets Per Mminute: UNKNOWN");


        }

        //TODO: more test

    }
}