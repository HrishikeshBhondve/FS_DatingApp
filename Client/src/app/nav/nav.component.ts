import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/User';
import { Router } from '@angular/router';
import { ToastrModule, ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit {
  model: any = {}
  // currentUser$: Observable<User | null> = of(null);

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) {

  }
  ngOnInit(): void {
    //this.currentUser$ = this.accountService.currentUser$;
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: response => this.router.navigateByUrl('/members')
      //console.log(response);
      //error: error => this.toastr.error(error.error), // handle by interceptor
    }   //return observable from serviceb need to sub
    )
  }
  logout() {
    this.accountService.logout();  //removeItem  fromlocalstorage
    // this.loggedIn = false;
    this.router.navigateByUrl('/');

  }
}
