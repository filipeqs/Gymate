import { RoutineExercise } from './routineExercise';

export interface Routine {
    Id: number;
    UserId: number;
    Name: string;
    Exercises: RoutineExercise[];
}
