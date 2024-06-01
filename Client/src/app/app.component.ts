
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'OnlineDatingApp';
  constructor(private accountService: AccountService) {

  }
  ngOnInit(): void {
    // this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser() {
    //const user: User = JSON.parse(localStorage.getItem('user')!);
    const userString = localStorage.getItem('user');
    if (!userString)
      return;
    const user: User = JSON.parse(userString);
    this.accountService.setcurrentUser(user);

  }

}
