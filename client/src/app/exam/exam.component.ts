import { Component, OnInit } from '@angular/core';
import { AppService } from '../app.service';
import { Exam } from 'src/model/exam';
import { ActivatedRoute, Router } from '@angular/router';
import { ViewState } from '@angular/core/src/view';

export enum ViewType {
  All,
  AllUserExam,
  SingleExam
}


@Component({
  selector: 'app-exam',
  templateUrl: './exam.component.html',
  styleUrls: ['./exam.component.css']
})
export class ExamComponent implements OnInit {

  exams: Array<Exam>;
  singleExam: Exam;
  viewState = ViewType.All;
  constructor(private appService: AppService, private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    if (this.activatedRoute.snapshot.params['examId']) {
      this.GetExam(this.activatedRoute.snapshot.params['examId']);
    } else if (this.activatedRoute.snapshot.params['userId']) {
      this.GetAllUserExam(this.activatedRoute.snapshot.params['userId']);
    } else {
      this.GetAllExam();
    }
  }

  GetExam(examId) {
    this.viewState = ViewType.SingleExam;
    this.appService.GetExam(examId).subscribe(
      (res: Exam) => {
        this.singleExam = res;
      },
      (err) => {
        window.alert(err.error && err.error.Msg ? err.error.Msg : 'an error occured');
      }
    );
  }

  GetAllUserExam(userId): void {
    this.viewState = ViewType.AllUserExam;
    this.appService.AllUserExam(userId).subscribe(
      (res) => {
        this.exams = res;
      },
      (err) => {
        window.alert(err.error && err.error.Msg ? err.error.Msg : 'an error occured');
      }
    );
  }

  GetAllExam(): void {
    this.viewState = ViewType.All;
    this.appService.GetAllExam().subscribe(
      (res) => {
        this.exams = res;
      },
      (err) => {
        window.alert(err.error && err.error.Msg ? err.error.Msg : 'an error occured');
      }
    );
  }

  SubmitExam(): void {
    this.appService.SubmitExam(this.singleExam).subscribe (
      (res) => {
        window.alert(res.Msg ? res.Msg : 'submitted');
      },
      (err) => {
        window.alert(err.error && err.error.Msg ? err.error.Msg : 'an error occured');
      }
    );
  }

  ViewDetail(examId): void {
    this.router.navigate(['exam', examId]);
  }

}
