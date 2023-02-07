using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("test");
            var task = Task.Run(MainAsync);
            task.Wait();
        }


        private static async Task MainAsync()
        {
            try
            {
                var client = new GreeterClient();
                
                await client.Execute();
                Console.WriteLine("Finished succesfully");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            finally
            {
                Console.Read();
            }
        }
    }
}