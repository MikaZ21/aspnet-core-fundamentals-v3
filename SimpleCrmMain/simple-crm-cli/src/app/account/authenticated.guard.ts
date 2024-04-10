import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { UserSummaryViewModel } from './account.model';
import { AccountService } from './account.service';

export const AuthenticatedGuard = () => {
  const router = inject(Router);
  const accountService = inject(AccountService);

  return accountService.user.pipe(
    map((user: UserSummaryViewModel) => {
      if (user.name === 'Anonymous') {
        return router.createUrlTree(['./login']);
      }
      return true;
    })
  )
  // console.log("User is accessing a guarded route.", route, state);
  // return router.createUrlTree(['not-authorized']);
};
