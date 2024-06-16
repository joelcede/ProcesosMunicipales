import { Directive, HostListener } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
  selector: '[appOnlyDigits]'
})
export class OnlyDigitsDirective {

  constructor(private ngControl: NgControl) { }

  @HostListener('input', ['$event']) onInputChange(event: Event) {
    const input = event.target as HTMLInputElement;
    const digits = input.value.replace(/\D/g, '');
    //this.ngControl.control?.setValue(digits);

    if (digits !== input.value) {
      input.value = digits;
      this.ngControl.control?.setValue(digits, { emitEvent: false });
    }
  }
}
