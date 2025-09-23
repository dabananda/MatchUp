import { Component, inject } from '@angular/core';
import { Register } from '../register/register';
import { HttpClient } from '@angular/common/http';
import { Account } from '../_services/account';

@Component({
  selector: 'app-home',
  imports: [Register],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  registerMode = false;
  http = inject(HttpClient);
  private accountService = inject(Account);
  users: any;

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  getUsers() {
    this.http.get(this.accountService.baseurl + 'users').subscribe({
      next: (response) => (this.users = response),
      error: (error) => console.log(error),
      complete: () => console.log('Request completed!'),
    });
  }
}
