import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusIcon'
})
export class StatusIconPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): string {
    if (value === 'Prospect'){
      return 'online';
    }
    if (value === 'Purchased') {
      return 'money';
    }
    return 'users';
  }
}
