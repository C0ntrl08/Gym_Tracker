﻿namespace GymTrackerApi.DataTransferObjects
{
    public class SaveTrainingRequest2
    {
        public List<TrainingExerciseDto> Exercises { get; set; } = new List<TrainingExerciseDto>();
    }
}
