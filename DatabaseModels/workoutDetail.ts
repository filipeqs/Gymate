import { Exercise } from './exercise';
import { WorkoutExerciseDetail } from './workoutExerciseDetail';

export interface WorkoutDetail {
    Id: number;
    WorkoutId: number;
    Exercise: Exercise;
    ExerciseDeails: WorkoutExerciseDetail[];
}
