import { Component, OnInit } from '@angular/core';
import { Course } from 'src/model/course';
import { Exam } from 'src/model/exam';
import { Question } from 'src/model/question';
import { AppService } from '../app.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  course = new Course();
  exam = new Exam();
  constructor(private appService: AppService) { }

  ngOnInit() {
  }

  AddQuestion() {
    if (this.exam.Questions == undefined) {
      this.exam.Questions = new Array<Question>();
    }
    this.exam.Questions.push(new Question());
  }

  PostExam() {
    this.appService.CreateExam(this.exam).subscribe(
      (res: any) => {
        window.alert('successfuly created');
        this.exam = new Exam();
      },
      err => {
        let log = err;
        if (err.error && err.error.Msg) {
          log = err.error.Msg;
        } else if (err.error) {
          log = err.error;
        }
        window.alert('unsuccessful: ' + log);
      }
    );
  }

  PostCourse() {
    this.appService.CreateCourse(this.course).subscribe(
      (res: any) => {
        window.alert('successfuly created');
        this.course = new Course();
      },
      err => {
        let log = err;
        if (err.error && err.error.Msg) {
          log = err.error.Msg;
        } else if (err.error) {
          log = err.error;
        }
        window.alert('unsuccessful: ' + log);
      }
    );
  }
}
