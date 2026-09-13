using CommunityToolkit.Mvvm.ComponentModel;
using FSM.Data;
using FSM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace FSM.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly AppDbContext db;
        
        [ObservableProperty]
        private ObservableCollection<Course> courses=new();

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
    }
}
