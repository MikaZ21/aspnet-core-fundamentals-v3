import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authenticatedGuard: CanActivateFn = () => {
  const router = inject(Router);
  // const accountService = inject(AccountService);
  console.log("Auth guards works!");
  return true;
};
