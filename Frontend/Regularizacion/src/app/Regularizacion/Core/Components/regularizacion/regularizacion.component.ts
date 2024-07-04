import { Component, OnDestroy, OnInit } from '@angular/core';
import { TipoUsuario } from '../../../shared/Usuario/Enums/TipoUsuario';
import { AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { DecimalPipe, NgFor, NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatDialogModule } from '@angular/material/dialog';
import { ViviendaComponent } from '../../../shared/Vivienda/Components/vivienda/vivienda.component';
import { MatStepperModule } from '@angular/material/stepper';
import { SharedModule } from '../../../shared/shared.module';
import { ViviendaService } from '../../../shared/Vivienda/Services/vivienda.service';
import { IVivienda } from '../../../shared/Vivienda/Models/IVivienda';
import { HttpErrorResponse } from '@angular/common/http';
import { IRegularizacion } from '../../Models/IRegularizacion';
import { RegularizacionService } from '../../Services/regularizacion.service';
import { EstadosType } from '../../Enums/EstadosType';
import { SharedService } from '../../../shared/Servives/shared.service';
import { Subscription } from 'rxjs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { ISecReg } from '../../Models/ISecReg';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { MaskitoOptions } from '@maskito/core';
import { MaskitoDirective } from '@maskito/angular';

@Component({
  selector: 'app-regularizacion',
  templateUrl: './regularizacion.component.html',
  styleUrls: ['./regularizacion.component.css'],
  standalone: true,
  imports: [
    FormsModule, MatFormFieldModule, MatInputModule,
    MatCardModule, MatSelectModule, ReactiveFormsModule,
    NgIf, NgFor, MatButtonModule, MatIconModule, MatDatepickerModule,
    MatNativeDateModule, MatDividerModule, MatDialogModule,
    MatStepperModule, SharedModule, MatToolbarModule, MatSlideToggleModule,
    ToastrModule, MaskitoDirective
  ]
})
export class RegularizacionComponent implements OnInit, OnDestroy {

  readonly digitos10: MaskitoOptions = {
    mask: [/\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/],
  };
  constructor(private viviendaService: ViviendaService,
    private regularizacion: RegularizacionService,
    private sharedService: SharedService,
    private toastr: ToastrService) {
    this.getUltimasViviendas();
  }

  hide = true;
  tipoCliente = TipoUsuario.Cliente;
  tipoPropietario = TipoUsuario.Propietario;
  tipoFamiliar = TipoUsuario.Familiar;

  loading = false;
  email = new FormControl('', [Validators.required, Validators.email]);
  toppings = new FormControl('');
  disableSelect = new FormControl(false);
  toppingList: string[] = ['Extra cheese', 'Mushroom', 'Onion', 'Pepperoni', 'Sausage', 'Tomato'];
  getErrorMessage() {
    if (this.email.hasError('required')) {
      return 'Ingresa un correo';
    }

    return this.email.hasError('email') ? 'Correo no valido' : '';
  }

  //metodos mios

  private subscription!: Subscription;
  currentStep: number = 0;

  getSecuenciaReg_ToggleForm = new FormGroup({
    obtenerSecuencia: new FormControl(false)
  });

  isObtenerSecuencia = false;

  onNumberInput(event: Event, control: string): void {
    const input = event.target as HTMLInputElement;
    let cleanedValue = input.value.replace(/\D/g, '');
    if (cleanedValue.length > 10) {
      cleanedValue = cleanedValue.substring(0, 10);
    }
    this.regularizacionForm.get(control)?.setValue(cleanedValue, { emitEvent: false });
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
  ngOnInit(): void {

    this.regularizacionForm.get('Anticipo')?.valueChanges.subscribe(() => {
      this.calcularValorPendiente();
      this.nonNegativeNumberValidator();
    });

    this.regularizacionForm.get('valorRegularizacion')?.valueChanges.subscribe(() => {
      this.calcularValorPendiente();
      this.nonNegativeNumberValidator();
    });

    this.subscription = this.sharedService.currentStep$.subscribe((step) => {
      this.currentStep = step;
      if (this.currentStep == 4) {
        this.getUltimasViviendas();
      }
    });

  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  ultimas10Viviendas: { id: string, value: string }[] = [];

  getUltimasViviendas() {
    this.viviendaService.getAllViviendas().subscribe({
      next: (response: any) => {
        this.ultimas10Viviendas = response.slice(0, 10).map((vivienda: { id: any; codigoCatastral: any; }) => ({
          id: vivienda.id,
          value: vivienda.codigoCatastral
          //nombres: familiar.nombres + ' ' + familiar.apellidos
        }));
      },
      error: (error: HttpErrorResponse) => {
      }
    })
  }

  getSecuenciaReg() {
    let cantidadReg = 0;
    this.regularizacion.getSecuenciaRegularizacion().subscribe({
      next: (response: ISecReg) => {
        cantidadReg = response.nuM_REGULARIZACION
      },
      error: (error: HttpErrorResponse) => {
        console.log("Error al obtener los clientes", error);
      },
      complete: () =>{
        return cantidadReg.toString();
      }
    });
    
  }

  regularizacionForm = new FormGroup({
    id: new FormControl('00000000-0000-0000-0000-000000000000'),
    idVivienda: new FormControl('', [Validators.required]),
    NumeroExpediente: new FormControl(''),
    valorRegularizacion: new FormControl('', [Validators.required]),
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
  nonNegativeNumberValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value;
      if (value !== null && value !== undefined && value < 0) {
        return { nonNegative: true };
      }
      return null;
    };
  }

  get regularizacionF(): IRegularizacion {
    return this.regularizacionForm.value as unknown as IRegularizacion;
  }

  addRegularizacion(): void {
    this.loading = true;
    const viviendaForm = this.regularizacionF;
    this.regularizacion.addRegularizacion(viviendaForm).subscribe({
      next: (response: any) => {
        this.resetForm();
        this.toastr.success('Regularizacion Guardada', 'Exito');
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
      },
      complete: () => {
        this.loading = false;
      }
    });
  }
  valorPositivo = true;
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
