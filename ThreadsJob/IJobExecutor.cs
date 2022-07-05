using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsJob
{
    internal interface IJobExecutor
    {
        int Amount { get; }
        void Start(int maxConcurrent);
        void Stop();
        void Add(Action action);
        void Clear();
    }
}
    
