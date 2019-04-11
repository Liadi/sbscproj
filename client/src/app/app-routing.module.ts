import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LandingComponent } from './landing/landing.component';
import { ProfileComponent } from './profile/profile.component';
import { ExamComponent } from './exam/exam.component';
import { CourseComponent } from './course/course.component';
import { RouteGuardService } from './route-guard.service';

export const routes: Routes = [
  { path: '', component: LandingComponent, canActivate: [RouteGuardService] },
  { path: 'dashboard', component: DashboardComponent, canActivate: [RouteGuardService] },
  { path: 'profile', component: ProfileComponent, canActivate: [RouteGuardService] },
  { path: 'exam', component: ExamComponent, canActivate: [RouteGuardService] },
  { path: 'exam/:examId', component: ExamComponent, canActivate: [RouteGuardService] },
  { path: 'exam/user/:userId', component: ExamComponent, canActivate: [RouteGuardService] },
  { path: 'course', component: CourseComponent, canActivate: [RouteGuardService] },
  { path: 'course/:courseId', component: CourseComponent, canActivate: [RouteGuardService] }
];

// @NgModule({
//   imports: [RouterModule.forRoot(routes)],
//   exports: [RouterModule]
// })
// export class AppRoutingModule { }
