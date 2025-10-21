using VideoWeb.Models;
using static VideoWeb.DataCore;
using static VideoWeb.Const.FilePath;

namespace VideoWeb
{
    public class Program
    {
		public static void Main(string[] args) {
			Console.WriteLine(
@$"���������"
				);
			try {
				static void saveData() => ConfigModel.SaveData(configFile, config);

				if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);

				if (!File.Exists(configFile))
					saveData();
				else
					config = ConfigModel.ReadData(configFile);

				if (config.UpdateConfig == true) {
					config.UpdateConfig = false;
					saveData();
					Console.WriteLine("�����ļ��Ѹ��£����˳������");
					return;
				}
			} catch { Console.WriteLine("���������ļ�ʱ���ִ���!"); return; }


			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllersWithViews();

			builder.WebHost.UseUrls(config.Website.Url.Get());
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddSession();

			var app = builder.Build();

			app.UsePathBase(config.Website.Url.UrlRoot);
			app.UseSession();

			/*// Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }*/
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthorization();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=StartPage}/{action=Index}"); // /{id?}");
			app.Run();
		}
	}
}
