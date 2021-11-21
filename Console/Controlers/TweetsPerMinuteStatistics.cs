using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterStatisticsClient
{
    public class TweetsPerMinuteStatistics : ITwitterStatisctics
    {

        private DateTime lastCheck = DateTime.UtcNow;
        private TimeSpan breakDuration = TimeSpan.FromMinutes(1);

        private long totalTweets = 0;
        private long totalTweetsOnLastCheck = 0;
        private int tweetsPerMinute = 0;
        private int minutesPassed = 0;
        private int avegarageTweetsPerMinute = 0;

        public void CalculateStatistcs(string line)
        {
            totalTweets++;

            DateTime timeCheck = DateTime.UtcNow;

            if (timeCheck - lastCheck >= breakDuration)
            {
                tweetsPerMinute = (int)(totalTweets - totalTweetsOnLastCheck);
                lastCheck = timeCheck;
                totalTweetsOnLastCheck = totalTweets;
                minutesPassed++;
                avegarageTweetsPerMinute = (int)(totalTweetsOnLastCheck / minutesPassed);
            }

            //SAVE - if needed
        }

        public void SaveStatistcs(ITwitertStatisticsRecord record)
        {
            //TODO :  Persist data if needed
            throw new NotImplementedException();
        }

        public string Write()
        {
            string restul = string.Format("Total Tweets Received: {0}\r\nAverage Tweets Per Mminute: {1}"
                           , totalTweets.ToString()
                           , (avegarageTweetsPerMinute == 0) ? "UNKNOWN" : avegarageTweetsPerMinute);

            return restul;

        }
    }
}
