using CommunityToolkit.Maui;
using Gym_Tracker.ViewModels;
using Microsoft.Extensions.Logging;
using Gym_Tracker.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gym_Tracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                // While AuthService uses its own BaseUrl constant, you could alternatively configure it here.
                // client.BaseAddress = new Uri("https://192.168.0.67:7013/");
            });

            builder.Services.AddTransient<ExerciseDetailViewModel>();
            builder.Services.AddTransient<TrainingDetailsViewModel>();
            builder.Services.AddTransient<CreateTrainingViewModel>();
            builder.Services.AddTransient<TrainingViewModel>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddTransient<LoginViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
