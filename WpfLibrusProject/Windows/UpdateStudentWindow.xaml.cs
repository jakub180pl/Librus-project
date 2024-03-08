using LibrusProject.Model;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace WpfLibrusProject.Windows
{
    /// <summary>
    /// Interaction logic for UpdateStudentWindow.xaml
    /// </summary>
    public partial class UpdateStudentWindow : Window
    {
        private Student selectedStudent;
        private TeacherWindow TeacherWindow = new TeacherWindow();
        public UpdateStudentWindow(Student selectedStudent)
        {
            InitializeComponent();
            NameTextBox.Text = selectedStudent.name;
            SurnameTextBox.Text = selectedStudent.surname;
            AddressTextBox.Text = selectedStudent.adress;
            this.selectedStudent = selectedStudent;

        }

        private async void UpdateStudentButton_Click(object sender, RoutedEventArgs e)
        {

            selectedStudent.name = NameTextBox.Text;
            selectedStudent.surname = SurnameTextBox.Text;
            selectedStudent.adress = AddressTextBox.Text;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync($"http://localhost:5133/api/students/{selectedStudent.index}", selectedStudent);
                    response.EnsureSuccessStatusCode();

                    MessageBox.Show("Dane studenta zostały zaktualizowane pomyślnie.");
                    
                    this.Close();
                    TeacherWindow.LoadStudentsToDataGrid();
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas aktualizacji danych: {ex.Message}");
                }
            }

        }
    }
}
