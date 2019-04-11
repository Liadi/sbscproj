import { Component, OnInit } from '@angular/core';
import User from 'src/model/user';
import { AppService } from './app.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  showTabs = false;
  userForm: User;
  ngOnInit(): void {
    let tempUserId = localStorage.getItem('userId');
    if (tempUserId != null && tempUserId != "") {
      this.showTabs = true;
    }
  }
  constructor(private appService: AppService, private router: Router) {
    this.appService.userLoggedIn.subscribe(
      (id) => {
        if (id != null) {
          this.showTabs = true;
        } else {
          this.showTabs = false;
        }
      }
    );
  }

  public Logout() {
    this.appService.userLoggedIn.emit(null);
    localStorage.removeItem('userId');
    localStorage.removeItem('token');
    this.router.navigate(['']);
  }


  public submitLoginSignup () {
    this.appService.signup(this.userForm).subscribe(
      res => {
        window.alert("successful signup");
        this.userForm = new User();
      },
      err => {
        window.alert("unsuccessful signup + " + err);
      }
    );
  }
}
