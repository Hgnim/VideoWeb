using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using VideoWeb.Models;
using static VideoWeb.Const.FilePath;
using static VideoWeb.DataCore;
using VideoWeb.Const;

namespace VideoWeb
{
    public class Program
    {
		public static void Main(string[] args) {
			Console.WriteLine(
@$"服务端启动
版本: {About.version}
{About.githubUrl_addHead}"
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
					Console.WriteLine("配置文件已更新，已退出服务端");
					return;
				}
			} catch { Console.WriteLine("处理配置文件时出现错误!"); return; }


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
			{
				var provider = new FileExtensionContentTypeProvider();
				//provider.Mappings[".mkv"] = "video/x-matroska";//支持mkv格式视频文件的MIME类型
				foreach(List<string> map in config.Advanced.Mappings) {
					if (map.Count==2) {
						provider.Mappings[map[0]] = map[1];
					}
				}
				app.UseStaticFiles(new StaticFileOptions {
					ContentTypeProvider = provider
				});
				foreach(string dir in config.DirectoryReadList) {
					if (Directory.Exists(dir)) {
						string _dir=dir;
						if (_dir.Substring(_dir.Length-1) is "/" or "\\") {
							_dir=_dir.Substring(0,_dir.Length-1);//去掉结尾的斜杠
						}
						app.UseStaticFiles(new StaticFileOptions {
							FileProvider = new PhysicalFileProvider(_dir),
							RequestPath = $"/{Path.GetFileName(_dir)}",
							ContentTypeProvider = provider
						});
					}
				}
			}
			app.UseRouting();
			app.UseAuthorization();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=StartPage}/{action=Index}"); // /{id?}");
			app.Run();
		}
	}
}
