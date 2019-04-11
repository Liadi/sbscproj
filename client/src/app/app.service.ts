import { Observable } from 'rxjs';
import User from 'src/model/user';
import { HttpClient, HttpHeaders, } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable, EventEmitter } from '@angular/core';
import { Exam } from 'src/model/exam';
import { Question } from 'src/model/question';
import { Course } from 'src/model/course';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  headers: HttpHeaders;
  userLoggedIn = new EventEmitter();
  user: User;
  

  constructor(private httpClient: HttpClient) {
    const token = localStorage.getItem('token') == null ? '' : localStorage.getItem('token');
    this.headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Headers': 'X-Requested-With',
      Authorization: 'Bearer ' + token
    });

    this.userLoggedIn.subscribe((data) => {
      console.log('a');
      console.log(this.headers.keys());
      console.log('b');
      const newToken = localStorage.getItem('token') == null ? '' : localStorage.getItem('token');
      if (data != null) {
        this.headers.set('Authorization', 'Bearer ' + newToken);
      }
    });
  }

  public signup(user: User): Observable<any>  {
    return this.httpClient.post<string>(
      environment.apiUrl + '/api/user/signup',
      user,
      {
        headers: this.headers,
      }
    );
  }

  public login(user: User): Observable<User>  {
    return this.httpClient.post<User>(
      environment.apiUrl + '/api/user',
      user,
      {
        headers: this.headers
      }
    );
  }

  public GetAllExam(): Observable<Array<Exam>>  {
    return this.httpClient.get<Array<Exam>>(
      environment.apiUrl + '/api/exam/getall',
      {
        headers: this.headers
      }
    );
  }

  public GetExam(examId: string): Observable<Exam>  {
    return this.httpClient.get<Exam>(
      environment.apiUrl + `/api/exam/${examId}`,
      {
        headers: this.headers
      }
    );
  }

  public AllUserExam(userId: string): Observable<Array<Exam>>  {
    return this.httpClient.get<Array<Exam>>(
      environment.apiUrl + `/api/exam/GetAllUser/${userId}`,
      {
        headers: this.headers
      }
    );
  }

  public GetAllCoureExams(courseId: string): Observable<Array<Exam>>  {
    return this.httpClient.get<Array<Exam>>(
      environment.apiUrl + `/api/exam/GetAllCourse/${courseId}`,
      {
        headers: this.headers
      }
    );
  }

  public UpdateExamQuestion(question: Question, examId: string): Observable<any> {
    return this.httpClient.put<any>(
      environment.apiUrl + `/api/exam/UpdateQuestion/${examId}`,
      question,
      {
        headers: this.headers
      }
    );
  }

  public CreateExam(exam:Exam): Observable<any> {
    return this.httpClient.post<any>(
      environment.apiUrl + `/api/exam`,
      exam,
      {
        headers: this.headers
      }
    );
  }

  public SubmitExam(exam: Exam): Observable<any> {
    return this.httpClient.post<any>(
      environment.apiUrl + `/api/exam/UserSubmitExam`,
      exam,
      {
        headers: this.headers
      }
    );
  }

  public GetAllCourse(): Observable<Array<Course>> {
    return this.httpClient.get<Array<Course>>(
      environment.apiUrl + `/api/course/getall`,
      {
        headers: this.headers
      }
    );
  }

  public GetCourse(courseId: string): Observable<Course> {
    return this.httpClient.get<Course>(
      environment.apiUrl + `/api/course/${courseId}`,
      {
        headers: this.headers
      }
    );
  }

  public CreateCourse(course: Course): Observable<any> {
    return this.httpClient.post<any>(
      environment.apiUrl + `/api/course`,
      course,
      {
        headers: this.headers
      }
    );
  }

  public UpdateCourse(course: Course): Observable<any> {
    return this.httpClient.put<any>(
      environment.apiUrl + `/api/course`,
      course,
      {
        headers: this.headers
      }
    );
  }

}
