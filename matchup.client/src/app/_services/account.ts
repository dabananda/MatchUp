import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/User';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class Account {
  private http = inject(HttpClient);
  currentUser = signal<User | null>(null);
  baseurl = environment.apiUrl;
  defaultImage =
    'https://res.cloudinary.com/djz3p8sux/image/upload/v1759038526/MatchUp/user_nzddef.png';
  login(model: any) {
    return this.http.post<User>(this.baseurl + 'account/login', model).pipe(
      map((user) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post<User>(this.baseurl + 'account/register', model).pipe(
      map((user) => {
        if (user) {
          this.setCurrentUser(user);
        }
        return user;
      })
    );
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
  }
  
  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
