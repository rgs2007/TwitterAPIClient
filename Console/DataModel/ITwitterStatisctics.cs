using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterStatisticsClient
{
    public interface ITwitterStatisctics
    {
        public void CalculateStatistcs(string line);
        public string Write();

    }
}
