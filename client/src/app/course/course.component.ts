import { Component, OnInit } from '@angular/core';
import { Course } from 'src/model/course';
import { AppService } from '../app.service';
import { ActivatedRoute, Router } from '@angular/router';

export enum ViewType {
  All,
  // AllCourseExam,
  SingleCourse
}

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {

  courses: Array<Course>;
  singleCourse: Course;
  viewState = ViewType.All;

  constructor(private appService: AppService, private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    if (this.activatedRoute.snapshot.params['courseId']) {
      this.GetCourse(this.activatedRoute.snapshot.params['courseId']);
    }
    // else if (this.activatedRoute.snapshot.params['examId']) {
    //   this.GetAllUserExam(this.activatedRoute.snapshot.params['userId']);
    // }
    else {
      this.GetAllCourse();
    }
  }

  GetCourse(courseId) {
    this.viewState = ViewType.SingleCourse;
    this.appService.GetCourse(courseId).subscribe(
      (res: Course) => {
        this.singleCourse = res;
      },
      (err) => {
        window.alert(err.error && err.error.Msg ? err.error.Msg : 'an error occured');
      }
    );
  }

  GetAllCourse(): void {
    this.viewState = ViewType.All;
    this.appService.GetAllCourse().subscribe(
      (res) => {
        this.courses = res;
      },
      (err) => {
        window.alert(err.error && err.error.Msg ? err.error.Msg : 'an error occured');
      }
    );
  }

  ViewDetail(courseId): void {
    this.router.navigate(['course', courseId]);
  }

}
