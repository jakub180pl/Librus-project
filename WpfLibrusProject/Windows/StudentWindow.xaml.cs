using System.Net.Http;
using System.Windows;
using System.Net.Http.Json;
using LibrusProject.Model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace WpfLibrusProject.Windows
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        private readonly HttpClient _httpClient;
        public StudentWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5133/api/grades");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void ShowGradesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index;
                if (!int.TryParse(IndexTextBox.Text, out index))
                {
                    MessageBox.Show("Wprowadź poprawny indeks.");
                    return;
                }

                HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5133/api/grades/{index}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    ObservableCollection<Grade> grades = JsonConvert.DeserializeObject<ObservableCollection<Grade>>(responseBody);

                    if (grades.Any())
                    {
                        gradeDataGrid.ItemsSource = grades;
                    }
                    else
                    {
                        MessageBox.Show("Brak ocen dla podanego indeksu.");
                    }
                }
                else
                {
                    MessageBox.Show($"Wystąpił błąd podczas pobierania danych. Kod błędu: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas pobierania danych: {ex.Message}");
            }
        }
    }
}
