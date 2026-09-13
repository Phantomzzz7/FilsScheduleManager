using CommunityToolkit.Mvvm.ComponentModel;
using FSM.Data;
using FSM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using System;

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

        [ObservableProperty]
        private ObservableCollection<ScheduleItem> scheduleItems = new();

        [ObservableProperty]
        private Course? selectedCourse;

        [ObservableProperty]
        private DayOfWeek selectedDay = DayOfWeek.Monday;

        [ObservableProperty]
        private string newActivityType = string.Empty;

        [ObservableProperty]
        private string startTimeText = string.Empty;

        [ObservableProperty]
        private string endTimeText = string.Empty;

        public MainViewModel()
        {
            db = new AppDbContext();
            db.Database.Migrate();
            LoadCourses();
            LoadScheduleItems();
        }

        private void LoadScheduleItems()
        {
            ScheduleItems = new ObservableCollection<ScheduleItem>(db.ScheduleItems.ToList());
        }

        private void LoadCourses()
        {
            Courses = new ObservableCollection<Course>(db.Courses.ToList());
        }

        [RelayCommand]
        private void AddScheduleItem()
        {
            if (SelectedCourse == null)
                return;

            if (!TimeSpan.TryParse(StartTimeText, out TimeSpan startTime))
                return;

            if (!TimeSpan.TryParse(EndTimeText, out TimeSpan endTime))
                return;

            var item = new ScheduleItem
            {
                CourseID = SelectedCourse.ID,
                ActivityType = NewActivityType,
                Day = SelectedDay,
                StartTime = startTime,
                EndTime = endTime
            };

            db.ScheduleItems.Add(item);
            db.SaveChanges();

            NewActivityType = string.Empty;
            StartTimeText = string.Empty;
            EndTimeText = string.Empty;

            LoadScheduleItems();
        }

        [RelayCommand]
        private void DeleteScheduleItem(ScheduleItem item)
        {
            if (item == null)
                return;

            db.ScheduleItems.Remove(item);
            db.SaveChanges();

            LoadScheduleItems();
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
        public Array DaysOfWeek => Enum.GetValues(typeof(DayOfWeek));
    }
}
