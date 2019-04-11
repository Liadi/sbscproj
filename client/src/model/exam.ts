import { Question } from './question';
import { UserExam } from './userexam';

export class Exam
{
    Id: string;

    Description: string;


    PassScorePercentage: number;

    Questions: Array <Question>;

    UserExam: Array <UserExam>;

    CourseId: string;
}