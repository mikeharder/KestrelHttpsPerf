
using Microsoft.AspNetCore.Hosting;
using System;

namespace KestrelHttpsPerf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>()
                .UseUrls("http://*:8080;https://*:8081")
                .UseKestrel(options =>
                {
                    options.UseHttps("testCert.pfx", "testPassword");
                });

            var host = builder.Build();

            Console.WriteLine("Listening on https://*:8080 and https://*:8081 ...");

            host.Run();
        }
    }
}
