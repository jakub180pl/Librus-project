using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace WpfLibrusProject.Windows
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        HttpClient client;
        public AddStudent()
        {
            InitializeComponent();
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5133");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newStudent = new
                {
                    name = NameTextBox.Text,
                    surname = SurnameTextBox.Text,
                    adress = AddressTextBox.Text
                   
                };

                var response = await client.PostAsJsonAsync("/api/students", newStudent);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Student has been added.");

                }
                else
                {
                    MessageBox.Show("Failed to add student.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    
    }
}
