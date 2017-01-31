
using Microsoft.AspNetCore.Builder;

namespace KestrelHttpsPerf
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UsePlainText();
        }
    }
}