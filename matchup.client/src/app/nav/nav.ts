import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Account } from '../_services/account';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav {
  accountService = inject(Account);
  private router = inject(Router);
  private toastr = inject(ToastrService);
  model: any = {};
  login() {
    this.accountService.login(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl('/members');
        this.toastr.success("Login successfull")
      },
      error: (error) => this.toastr.error(error.error),
    });
  }
  logout() {
    this.accountService.logout();
    this.toastr.info('Logout successfull');
    this.router.navigateByUrl('/');
  }
}
