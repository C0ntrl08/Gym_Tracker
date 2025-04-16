﻿using System.Collections.ObjectModel;

namespace Gym_Tracker.Models
{
    partial class ExerciseGroup : ObservableCollection<Exercise>
    {
        public string GroupName { get; set; } = string.Empty;
        public ExerciseGroup(string groupName, IEnumerable<Exercise> exercises) : base(exercises)
        {
            GroupName = groupName;
        }
    }
}
