using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using Gym_Tracker.Models;

namespace Gym_Tracker.ViewModels
{
    public partial class TrainingViewModel : INotifyPropertyChanged
    {
        // Cache a single instance of JsonSerializerOptions for reuse.
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        // The list of trainings (returned from the backend as TrainingDto objects)
        public ObservableCollection<Training> Trainings { get; } = new ObservableCollection<Training>();
        public TrainingViewModel()
        {
            // Start the data loading process.
            Task.Run(async () => await LoadTrainingsAsync());
        }

        public async Task LoadTrainingsAsync()
        {
            try
            {
                // Create custom HTTP handler to bypass SSL validation
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                // Create HttpClient with custom handler
                using HttpClient client = new(handler);

                // Set your backend base URL
                client.BaseAddress = new Uri("https://192.168.0.67:7013/");

                // Call the backend endpoint
                HttpResponseMessage response = await client.GetAsync("api/Exercises");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    // Reuse the cached JsonOptions instance for deserialization.
                    var trainings = JsonSerializer.Deserialize<List<Training>>(json, JsonOptions);

                    // Update the observable collection
                    Trainings.Clear();
                    if (trainings != null)
                    {
                        foreach (var training in trainings)
                        {
                            Trainings.Add(training);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Improved error logging
                Debug.WriteLine($"Error fetching trainings: {ex}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
