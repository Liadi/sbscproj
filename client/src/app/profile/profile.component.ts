import { Component, OnInit, OnChanges } from '@angular/core';
import { AppService } from '../app.service';
import User, { UserType } from 'src/model/user';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnChanges {
  user: User;
  constructor(private appService: AppService) {
  }

  ngOnChanges() {
    this.appService.userLoggedIn.subscribe(data => {
      if (data == null) {
        return;
      }
      this.user = this.appService.user;
      this.user.UserType = UserType[this.user.UserType] as Object as UserType;
      console.log("AA");
      console.log(this.user);
      console.log("BB");
    });
  }

}
