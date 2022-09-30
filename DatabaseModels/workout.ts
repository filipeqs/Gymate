import { WorkoutDetail } from './workoutDetail';

export interface Workout {
    Id: number;
    UserId: number;
    Date: Date;
    WorkoutDetails: WorkoutDetail[];
}
