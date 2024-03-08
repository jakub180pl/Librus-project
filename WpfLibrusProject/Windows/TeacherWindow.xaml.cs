using LibrusProject.Model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace WpfLibrusProject.Windows
{
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {

        HttpClient _httpClient;
        ObservableCollection<Student> students = new();
        public TeacherWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5133/api");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            LoadGradesToDataGrid();
            LoadStudentsToDataGrid();
        }

        public async void LoadStudentsToDataGrid()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5133/api/students");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                 students = JsonConvert.DeserializeObject<ObservableCollection<Student>>(responseBody);

                studentDataGrid.ItemsSource = students;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas pobierania danych: {ex.Message}");
            }
        }

        public async void LoadGradesToDataGrid()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5133/api/grades"); 
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                ObservableCollection<Grade> grades = JsonConvert.DeserializeObject<ObservableCollection<Grade>>(responseBody);

                gradeDataGrid.ItemsSource = grades;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas pobierania danych: {ex.Message}");
            }
        }


        private  void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            AddStudent addStudent = new AddStudent();
            addStudent.ShowDialog();
            LoadStudentsToDataGrid();
        }

        

        private  void UpdateStudentDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                if (dataGrid.SelectedItem is Student selectedStudent)
                {

                    UpdateStudentWindow updateStudentWindow = new UpdateStudentWindow(selectedStudent);
                    updateStudentWindow.ShowDialog();
                    LoadStudentsToDataGrid();

                }
            }
        }

        private void UpdateGradeDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                if (dataGrid.SelectedItem is Grade selectedGrade)
                {

                    UpdateGradeWindow updatGradeWindow = new UpdateGradeWindow(selectedGrade, students);
                    updatGradeWindow.ShowDialog();
                    LoadGradesToDataGrid();

                }
            }
        }
        private async void btnDeleteStudent_Click(object sender, RoutedEventArgs e)
        {

            
                if (studentDataGrid.SelectedItem is Student selectedStudent)
                {

                    try
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            HttpResponseMessage response = await client.DeleteAsync($"http://localhost:5133/api/students/{selectedStudent.index}");
                            response.EnsureSuccessStatusCode();
                            MessageBox.Show("Student został usunięty.");
                            LoadStudentsToDataGrid();
                        }
                    }     
                    catch (HttpRequestException ex)
                    {
                        MessageBox.Show($"Wystąpił błąd podczas usuwania studenta: {ex.Message}");
                    }

                }
            
        }

        private void btnAddGrade_Click(object sender, RoutedEventArgs e)
        {
            AddGrade addGrade = new AddGrade(students);
            addGrade.ShowDialog();
            LoadGradesToDataGrid();
        }

        private async void btnDeleteGrade_Click(object sender, RoutedEventArgs e)
        {
            if (gradeDataGrid.SelectedItem is Grade selectedGrade)
            {

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.DeleteAsync($"http://localhost:5133/api/grades/{selectedGrade.gradeId}");
                        response.EnsureSuccessStatusCode();
                        MessageBox.Show("Grade has been removed.");
                        LoadGradesToDataGrid();
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }


    }
}
