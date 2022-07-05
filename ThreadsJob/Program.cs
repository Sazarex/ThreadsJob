using System.Threading;
using ThreadsJob;

class Program
{
    static void Main(string[] args)
    {
        ThreadExecutor threadExecutor = new ThreadExecutor();

        threadExecutor.Notify += ThreadExecutor_Notify;


        threadExecutor.Add(() =>
        {
            Thread.Sleep(1000);
            Console.WriteLine("First action");
        });
        threadExecutor.Add(() =>
        {
            Thread.Sleep(1000);
            Console.WriteLine("Second action");
        });
        threadExecutor.Add(() =>
        {
            Thread.Sleep(1000);
            Console.WriteLine("Third action");
        });
        threadExecutor.Add(() =>
        {
            Thread.Sleep(1000);
            Console.WriteLine("Four action");
        });
        threadExecutor.Add(() =>
        {
            Thread.Sleep(1000);
            Console.WriteLine("Five action");
        });


        threadExecutor.Start(1);

        Console.WriteLine("Введите Y, чтобы отменить очередь задач.");
        var str = Console.ReadLine();
        if (str == "Y")
        {
            threadExecutor.Stop();
        }
    }


    private static void ThreadExecutor_Notify(object? sender, string e)
    {
        Console.WriteLine(e);
    }
}
