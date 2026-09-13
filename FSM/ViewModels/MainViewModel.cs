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

        [ObservableProperty]
        private ObservableCollection<Assignment> assignments = new();

        [ObservableProperty]
        private Course? selectedCourseForAssignment;

        [ObservableProperty]
        private string newAssignmentTitle = string.Empty;

        [ObservableProperty]
        private string dueDateText = string.Empty;

        public MainViewModel()
        {
            db = new AppDbContext();
            db.Database.Migrate();
            LoadCourses();
            LoadScheduleItems();
            LoadAssignments();
        }

        private void LoadScheduleItems()
        {
            ScheduleItems = new ObservableCollection<ScheduleItem>(db.ScheduleItems.ToList());
        }

        private void LoadCourses()
        {
            Courses = new ObservableCollection<Course>(db.Courses.ToList());
        }
        private void LoadAssignments()
        {
            Assignments = new ObservableCollection<Assignment>(db.Assignments.ToList());
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
        private void AddAssignment()
        {
            if (SelectedCourseForAssignment == null)
                return;

            if (!DateTime.TryParse(DueDateText, out DateTime dueDate))
                return;

            var assignment = new Assignment
            {
                CourseID = SelectedCourseForAssignment.ID,
                Name = NewAssignmentTitle,
                DueDate = dueDate,
                IsCompleted = false
            };

            db.Assignments.Add(assignment);
            db.SaveChanges();

            NewAssignmentTitle = string.Empty;
            DueDateText = string.Empty;

            LoadAssignments();
        }

        [RelayCommand]
        private void DeleteAssignment(Assignment assignment)
        {
            if (assignment == null)
                return;

            db.Assignments.Remove(assignment);
            db.SaveChanges();

            LoadAssignments();
        }

        [RelayCommand]
        private void ToggleAssignmentCompleted(Assignment assignment)
        {
            if (assignment == null)
                return;

            assignment.IsCompleted = !assignment.IsCompleted;
            db.SaveChanges();
        }

        [RelayCommand]
        private void AddCourse()
        {
            if (string.IsNullOrWhiteSpace(NewCourseName))
                return;
            var course = new Course
            {
                Name = NewCourseName,
                Professor = NewCourseProfessor,
                Room = NewCourseRoom
            };
            db.Courses.Add(course);
            db.SaveChanges();

            NewCourseName = string.Empty;
            NewCourseProfessor = string.Empty;
            NewCourseRoom = string.Empty;

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
