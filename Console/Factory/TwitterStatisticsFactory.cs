using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterStatisticsClient
{
    public class TwitterStatisticsFactory
    {
        public static ITwitterStatisctics CreateStatistics(string statistcs)
        {
            switch (statistcs)
            {
                case "1":
                    return new TweetsPerMinuteStatistics();
                case "2":
                    return new OnlyTotalTweets();
                default:
                    return new TweetsPerMinuteStatistics();
            }
        }
    }
}
