import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusIcon'
})
export class StatusIconPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): string {
    value = value || '';
    if(value.search(/prospect/i) === 0) {
      return 'online';
    }
      if (value.match(/Purchased/i)) {
      return 'money';
      }
    return 'users';
  }
}
