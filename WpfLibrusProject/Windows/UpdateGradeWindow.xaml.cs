using LibrusProject.Model;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace WpfLibrusProject.Windows
{
    /// <summary>
    /// Interaction logic for UpdateGradeWindow.xaml
    /// </summary>
    public partial class UpdateGradeWindow : Window
    {
        private Grade selectedGrade;
        private ObservableCollection<Student> students;
        private TeacherWindow teacherWindow = new TeacherWindow();
        public UpdateGradeWindow(Grade selectedGrade , ObservableCollection<Student> students)
        {
            InitializeComponent();
            IndexComboBox.ItemsSource = selectedGrade.index.ToString();
            SubjectTextBox.Text = selectedGrade.subject;
            ValueTextBox.Text = selectedGrade.value.ToString();
            DescriptionTextBox.Text = selectedGrade.description;
            this.selectedGrade = selectedGrade;
            this.students = students;

            IndexComboBox.ItemsSource = students.Select(student => student.index); ;

            IndexComboBox.Loaded += (s, e) =>
            {
                if (students.Any(student => student.index == selectedGrade.index))
                {
                    IndexComboBox.Text = selectedGrade.index.ToString();
                }
            };


        }

        private async void UpdateGradeButton_Click(object sender, RoutedEventArgs e)
        {

                
            selectedGrade.index = (int)IndexComboBox.SelectedItem;

            selectedGrade.subject = SubjectTextBox.Text;
            if(Int32.Parse(ValueTextBox.Text) < 1 || Int32.Parse(ValueTextBox.Text) > 6)
            {
                MessageBox.Show("Please enter a valid value.");
                return;
            }
            selectedGrade.value = Int32.Parse(ValueTextBox.Text);
            selectedGrade.description = DescriptionTextBox.Text;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync($"http://localhost:5133/api/grades/{selectedGrade.gradeId}", selectedGrade);
                    response.EnsureSuccessStatusCode();

                    MessageBox.Show("Success.");

                    this.Close();
                    teacherWindow.LoadGradesToDataGrid();
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"An error occurred while updating data: {ex.Message}");
                }
            }
        }
    }
}
