using LibrusProject.Model;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace WpfLibrusProject.Windows
{
    /// <summary>
    /// Interaction logic for AddGrade.xaml
    /// </summary>
    public partial class AddGrade : Window
    {
        HttpClient client;

        public AddGrade(ObservableCollection<Student> students)
        {
            InitializeComponent();
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5133");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            indexComboBox.ItemsSource = students.Select(student => student.index); ;



        }

        private async void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {



            if (Int32.TryParse(indexComboBox.Text, out int selectedIndex) && Convert.ToDouble(valueTextBox.Text) >= 1 && Convert.ToDouble(valueTextBox.Text) <= 6)
            {
                var newGrade = new
                {
                    index = selectedIndex,
                    subject = subjectTextBox.Text,
                    value = Convert.ToDouble(valueTextBox.Text),
                    description = descriptionTextBox.Text
                };

                var response = await client.PostAsJsonAsync("/api/grades", newGrade);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Grade has been added.");
                }
                else
                {
                    MessageBox.Show("Failed to add grade.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid data.");
            }
            
        }
    }
}
