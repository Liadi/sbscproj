import { Component, OnInit } from '@angular/core';
import User from 'src/model/user';
import { AppService } from '../app.service';
import { ActivatedRoute, Router } from '@angular/router';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent {
  userForm: User;

  loginSignupToggle: boolean;

  constructor(private appService: AppService, private activatedRoute: ActivatedRoute, private router: Router) {
    this.loginSignupToggle = true;
    this.userForm = new User();
  }

  public toggleForm(): void {
    this.loginSignupToggle = !this.loginSignupToggle;
  }

  public submitLoginSignup () {
    if (this.loginSignupToggle) {
      this.login();
    } else {
      this.signup();
    }
  }

  private login () {
    this.appService.login(this.userForm).subscribe(
      (res: any) => {
        this.appService.user = res;
        this.userForm = new User();
        localStorage.setItem('token', res.Token);
        localStorage.setItem('userId', res.Username);
        const decoded = jwt_decode(res.Token);
        this.appService.userLoggedIn.emit(decoded.sub);
        this.router.navigate(['dashboard']);
      },
      err => {
        window.alert(err.error.Msg);
      }
    );
  }

  private signup () {
    this.appService.signup(this.userForm).subscribe(
      (res: any) => {
        window.alert("successful signup");
        this.userForm = new User();
      },
      err => {
        window.alert("unsuccessful signup " + err.error.Msg);
      }
    );
  }

}
