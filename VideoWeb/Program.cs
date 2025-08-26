namespace VideoWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.WebHost.UseUrls("http://*:80");            
            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");
            app.UsePathBase("/video");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
