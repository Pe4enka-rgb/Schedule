using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schedule.Data;
using Schedule.Infrastracture.DI;
using Schedule.Services;
using System.Windows;

namespace Schedule {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {

		public bool IsDesignTime;

		public static Window CurrentWindow => FocusedWindow ?? ActiveWindow;

		public static Window ActiveWindow => Application.Current.Windows
			.OfType<Window>()
			.FirstOrDefault(w => w.IsActive)
		;

		public static Window FocusedWindow => Application.Current.Windows
			.OfType<Window>()
			.FirstOrDefault(w => w.IsFocused)
		;
		private static IHost _Host;
		public static IHost Host => _Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
		public static IServiceProvider Services => _Host.Services;
		internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
			.AddDataBase(host.Configuration.GetSection("Database"))
			.AddServices()
			.AddViewModels()
		;

		protected override async void OnStartup(StartupEventArgs args) {
			var host = Host;

			using (var scope = Services.CreateScope()) {
				await scope.ServiceProvider.GetRequiredService<DbInitializer>().InitializeAsync();
			}

			base.OnStartup(args);
			await host.StartAsync();
		}

		protected override async void OnExit(ExitEventArgs args) {
			using var host = Host;
			base.OnExit(args);
			await host.StopAsync();
		}
	}


}
