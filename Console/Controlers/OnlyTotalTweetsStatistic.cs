using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterStatisticsClient
{
    public class OnlyTotalTweets : ITwitterStatisctics
    {

        private long totalTweets = 0;

        public void CalculateStatistcs(string line)
        {
            totalTweets++;
        }

        public string Write()
        {
            string restul = string.Format("Total Tweets Received: {0}"
               , totalTweets.ToString());

            return restul;
        }
    }
}
