import { CommonModule, NgFor, NgIf } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { EstadosType } from '../../Enums/EstadosType';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { HttpErrorResponse } from '@angular/common/http';
import { RegularizacionService } from '../../Services/regularizacion.service';
import { ISecReg } from '../../Models/ISecReg';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { ToastrService } from 'ngx-toastr';
import { IRegularizacion } from '../../Models/IRegularizacion';
import { MaskitoOptions } from '@maskito/core';
import { MaskitoDirective } from '@maskito/angular';

@Component({
  selector: 'app-edith',
  templateUrl: './edith.component.html',
  styleUrls: ['./edith.component.css'],
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatDialogModule, FormsModule, MatFormFieldModule, MatInputModule,
    MatSelectModule, ReactiveFormsModule,
    NgIf, NgFor, MatButtonModule, MatIconModule, MatDatepickerModule,
    MatNativeDateModule, MatSlideToggleModule, MaskitoDirective
  ],
})
export class EdithComponent implements OnInit {

  valorPositivo = true;
  hide = true;
  email = new FormControl('', [Validators.required, Validators.email]);

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: string,
    private regularizacion: RegularizacionService, private toastr: ToastrService) {
    this.getRegularizacion();
  }
  ngOnInit() {
    this.regularizacionForm.get('Anticipo')?.valueChanges.subscribe(() => {
      this.calcularValorPendiente();
      this.nonNegativeNumberValidator();
    });

    this.regularizacionForm.get('valorRegularizacion')?.valueChanges.subscribe(() => {
      this.calcularValorPendiente();
      this.nonNegativeNumberValidator();
    });
  }
  readonly digitos10: MaskitoOptions = {
    mask: [/\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/],
  };
  regularizacionForm = new FormGroup({
    id: new FormControl('00000000-0000-0000-0000-000000000000'),
    idVivienda: new FormControl('', [Validators.required]),
    NumeroExpediente: new FormControl(''),
    valorRegularizacion: new FormControl(''),
    Anticipo: new FormControl('', [Validators.required]),
    ValorPendiente: new FormControl('', [this.nonNegativeNumberValidator()]),
    Estado: new FormControl(EstadosType.None),
    FechaRegistro: new FormControl(new Date()),
    FechaInsercion: new FormControl(new Date()),
    FechaActualizacion: new FormControl(new Date()),
    Correo: new FormControl('', [Validators.email, Validators.required]),
    Contrasena: new FormControl('', [Validators.required]),
    intentosSubidas: new FormControl(''),
    intentosSubsanacion: new FormControl(''),
    cantidadNegada: new FormControl(''),
    numRegularizacion: new FormControl('', [Validators.required]),
  });
  getSecuenciaReg_ToggleForm = new FormGroup({
    obtenerSecuencia: new FormControl(false)
  });
  nonNegativeNumberValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value;
      if (value !== null && value !== undefined && value < 0) {
        return { nonNegative: true };
      }
      return null;
    };
  }
  onNumberInput(event: Event, control: string): void {
    const input = event.target as HTMLInputElement;
    let cleanedValue = input.value.replace(/\D/g, '');
    if (cleanedValue.length > 10) {
      cleanedValue = cleanedValue.substring(0, 10);
    }
    this.regularizacionForm.get(control)?.setValue(cleanedValue, { emitEvent: false });
  }
  getErrorMessage() {
    if (this.email.hasError('required')) {
      return 'Ingresa un correo';
    }

    return this.email.hasError('email') ? 'Correo no valido' : '';
  }
  onToggleChange(isChecked: boolean): void {
    if (isChecked) {
      //const secuencia: string = this.getSecuenciaReg();
      this.regularizacion.getSecuenciaRegularizacion().subscribe({
        next: (response: ISecReg) => {
          const secuencia: number = response.nuM_REGULARIZACION;
          this.regularizacionForm.get('numRegularizacion')?.setValue(secuencia?.toString(), { emitEvent: false });
        },
        error: (error: HttpErrorResponse) => {
          console.log("Error al obtener los clientes", error);
        }
      });

    } else {
      this.regularizacionForm.get('numRegularizacion')?.setValue('');
    }
  }
  get regularizacionF(): IRegularizacion {
    return this.regularizacionForm.value as unknown as IRegularizacion;
  }

  getRegularizacion(): void {
    this.regularizacion.getByIdRegularizacion(this.data).subscribe({
      next: (response: IRegularizacion) => {
        this.regularizacionForm.patchValue({
          id: response.id,
          idVivienda: response.idVivienda,
          NumeroExpediente: response.numeroExpediente,
          valorRegularizacion: response.valorRegularizacion?.toString(),
          Anticipo: response.anticipo?.toString(),
          ValorPendiente: response.valorPendiente?.toString(),
          Estado: response.estado,
          FechaRegistro: new Date(response.fechaRegistro),
          Correo: response.correo,
          Contrasena: response.contrasena,
          numRegularizacion: response.numRegularizacion?.toString(),
        });
      },
      error: (error: HttpErrorResponse) => {
        if (error.error.errors) {
          const errorMessages = this.extractErrorMessages(error.error.errors);
          errorMessages.forEach(errMsg => {
            this.toastr.error(errMsg, 'ERROR');
            //this.alert.open(`Error al guardar el Cliente:</strong><br> ${errMsg}`).subscribe();
          });
        } else {
          //this.alert.open(`Error al guardar el Cliente: ${error.message}`).subscribe();
        }
        console.log("Error al agregar la regularizacion", error);
      }
    });
  }

  edithRegularizacion(): void {
    const viviendaForm = this.regularizacionF;
    this.regularizacion.updateRegularizacion(this.data, viviendaForm).subscribe({
      next: (response: any) => {
        this.resetForm();
        this.toastr.success('Regularizacion Editada', 'Exito');
      },
      error: (error: HttpErrorResponse) => {
        if (error.error.errors) {
          const errorMessages = this.extractErrorMessages(error.error.errors);
          errorMessages.forEach(errMsg => {
            this.toastr.error(errMsg, 'ERROR');
          });
        } else {
        }
        console.log("Error al agregar la regularizacion", error);
      }
    });
  }
  calcularValorPendiente() {
    const valorRegularizacion = parseFloat(this.regularizacionForm.get('valorRegularizacion')?.value || '0');
    const anticipo = parseFloat(this.regularizacionForm.get('Anticipo')?.value || '0');
    const valorPendiente = valorRegularizacion - anticipo;
    this.regularizacionForm.get('ValorPendiente')?.setValue((valorPendiente).toString());

    if (valorPendiente < 0) this.valorPositivo = false;
    else this.valorPositivo = true;
  }


  resetForm() {
    this.regularizacionForm.reset({
      id: '00000000-0000-0000-0000-000000000000',
      idVivienda: '00000000-0000-0000-0000-000000000000',
      NumeroExpediente: '',
      valorRegularizacion: '',
      Anticipo: '',
      ValorPendiente: '',
      Estado: EstadosType.None,
      FechaRegistro: new Date(),
      FechaInsercion: new Date(),
      FechaActualizacion: new Date(),
      Correo: '',
      Contrasena: '',
      intentosSubidas: '',
      intentosSubsanacion: '',
      cantidadNegada: ''
    });
  }
  private extractErrorMessages(errors: any): string[] {
    const errorMessages: string[] = [];
    for (const key in errors) {
      if (errors.hasOwnProperty(key)) {
        if (Array.isArray(errors[key])) {
          errors[key].forEach((errorMsg: string) => {
            errorMessages.push(errorMsg);
          });
        } else {
          errorMessages.push(errors[key]);
        }
      }
    }
    return errorMessages;
  }
}
