using CommunityToolkit.Mvvm.ComponentModel;
using FSM.Data;
using FSM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.Input;

namespace FSM.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly AppDbContext db;
        
        [ObservableProperty]
        private ObservableCollection<Course> courses=new();

        [ObservableProperty]
        private string newCourseName = string.Empty;

        [ObservableProperty]
        private string newCourseProfessor = string.Empty;

        [ObservableProperty]
        private string newCourseRoom = string.Empty;

        public MainViewModel()
        {
            db = new AppDbContext();
            db.Database.Migrate();
            LoadCourses();
        }

        private void LoadCourses()
        {
            Courses = new ObservableCollection<Course>(db.Courses.ToList());
        }

        [RelayCommand]
        private void AddCourse()
        {
            if (string.IsNullOrWhiteSpace(newCourseName))
                return;
            var course = new Course
            {
                Name = newCourseName,
                Professor = newCourseProfessor,
                Room = newCourseRoom
            };
            db.Courses.Add(course);
            db.SaveChanges();

            newCourseName = string.Empty;
            newCourseProfessor = string.Empty;
            newCourseRoom = string.Empty;

            LoadCourses();
        }
        [RelayCommand]
        private void DeleteCourse(Course course)
        {
            if (course == null)
                return;

            db.Courses.Remove(course);
            db.SaveChanges();

            LoadCourses();
        }
    }
}
