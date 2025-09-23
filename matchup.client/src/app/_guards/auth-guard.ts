import { CanActivateFn } from '@angular/router';
import { Account } from '../_services/account';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(Account);
  const toastr = inject(ToastrService);
  if (accountService.currentUser()) return true;
  else {
    toastr.warning('Premision Denied!');
    return false;
  }
};
