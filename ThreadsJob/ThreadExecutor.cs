using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsJob
{
    internal class ThreadExecutor : IJobExecutor
    {
        public event EventHandler<string> Notify;


        public int Amount { get; }

        private List<Action> tasks;
        private ParallelOptions parallelOptions;
        private CancellationTokenSource CancelSource;
        private CancellationToken Token;



        private void CallNotify(string message)
        {
            Notify?.Invoke(this, message);
        }

        public ThreadExecutor()
        {
            tasks = new List<Action>();
            parallelOptions = new ParallelOptions();
            CancelSource = new CancellationTokenSource();
            Token = CancelSource.Token;
            parallelOptions.CancellationToken = Token;
        }

        public void Add(Action action)
        {
            if (action== null)
            {
                throw new ArgumentNullException(action.ToString());
            }

            tasks.Add(action);
            CallNotify("Добавлен элемент в очередь задач.");
        }

        public void Clear()
        {
            tasks = new List<Action>();
            CallNotify("Очередь задач была очищена.");
        }

        public async void Start(int maxConcurrent)
        {
            Action[] actionsFromTasks = tasks.ToArray<Action>();
            parallelOptions.MaxDegreeOfParallelism = maxConcurrent;

            await Task.Run( async() =>
            {
                await Task.Run(() => CallNotify($"Очередь задач была запущена. Максимум выполняемых задач параллельно: {maxConcurrent}"));
                Parallel.Invoke(parallelOptions, actionsFromTasks);
                });
            
            
        }

        public void Stop()
        {
            CancelSource.Cancel();
            CallNotify("Очередь задач была остановлена.");
        }
    }
}
