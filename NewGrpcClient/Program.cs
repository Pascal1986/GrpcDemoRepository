using GrpcClient;

namespace NewGrpcClient
{
    public static class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Starting");

            try
            {
                var client = new GreeterClient();
                await client.Execute();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            finally
            {
                Console.WriteLine("Finished");
                Console.Read();
            }
        }
    }
}

