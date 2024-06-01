import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../_models/User';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'http://localhost:9090/api/';
  private cuurentSourceUser = new BehaviorSubject<User | null>(null);  //user or null
  currentUser$ = this.cuurentSourceUser.asObservable();  // $ conven to signigy this is observable

  constructor(private http: HttpClient) { }
  login(model: User) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(map((response: any) => {
      const user = response;
      if (user) {
        localStorage.setItem('user', JSON.stringify(user))
        this.cuurentSourceUser.next(user);
      }
    })) //passed body as its post(model)
  }
  setcurrentUser(user: User) {
    this.cuurentSourceUser.next(user);
  }
  logout() {
    localStorage.removeItem('user');
    this.cuurentSourceUser.next(null);
  }
}
