import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  http = inject(HttpClient);
  protected readonly title = signal('MatchUp: The BEST Soulmate Finder');
  users: any;

  ngOnInit(): void {
    this.http.get('http://localhost:5205/api/users').subscribe({
      next: (response) => (this.users = response),
      error: (error) => console.log(error),
      complete: () => console.log('Request completed!'),
    });
  }
}
