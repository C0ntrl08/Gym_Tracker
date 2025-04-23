using Gym_Tracker.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Gym_Tracker.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        // Resolve the LoginViewModel from the DI container.
        BindingContext = Application.Current?.Handler?.MauiContext?.Services
                 .GetRequiredService<LoginViewModel>();
    }
}