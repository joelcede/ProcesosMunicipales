import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgFor, NgIf } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MaskitoOptions } from '@maskito/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSelectModule } from '@angular/material/select';
import { IUsuario } from '../../Models/IUsuario';
import { UsuarioService } from '../../Services/usuario-service.service';
import { HttpErrorResponse } from '@angular/common/http';
import { TipoUsuario } from '../../Enums/TipoUsuario';
import { ICuentaMunicipal } from '../../Models/ICuentaMunicipal';
import { Subscription } from 'rxjs';
import { SharedService } from '../../../Servives/shared.service';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css'],
  standalone: true,
  imports: [MatButtonModule, MatDialogModule, MatFormFieldModule, ReactiveFormsModule,
    NgIf, NgFor, MatIconModule, MatInputModule, MatToolbarModule, MatSlideToggleModule, FormsModule, MatSelectModule,
    ],
})
export class UsuarioComponent implements OnInit, OnDestroy {
  @Input() esPropietario: boolean = false;
  @Input() noEsCliente: boolean = false;
  @Input() tipoUsuario: TipoUsuario = TipoUsuario.None;

  constructor(private usuarioService: UsuarioService, private sharedService: SharedService) {
    this.getTop10Clients();
    
  }

  UsuarioForm = new FormGroup({
    id: new FormControl('00000000-0000-0000-0000-000000000000'),
    nombres: new FormControl(''),
    apellidos: new FormControl(''),
    dni: new FormControl('', [Validators.required, Validators.pattern('^[0-9]*$'), Validators.maxLength(13)]),
    telefonoCelular: new FormControl('', [Validators.required, Validators.pattern('^[0-9]*$'), Validators.maxLength(10)]),
    telefonoConvencional: new FormControl('', [Validators.required, Validators.pattern('^[0-9]*$'), Validators.maxLength(15)]),
    esPrincipal: new FormControl(false, [Validators.required]),
    fechaCreacion: new FormControl(new Date()),
    fechaModificacion: new FormControl(new Date()),
  });

  cm_ToggleForm = new FormGroup({
    abrirCM: new FormControl(false)
  });

  readonly digitos10: MaskitoOptions = {
    mask: [/\d/, /\d/, /\d/, /\d/, ' ', /\d/, /\d/, /\d/, /\d/, /\d/, /\d/],
  };

  isChecked = false;

  toppings = new FormControl('');
  disableSelect = new FormControl(false);
  toppingList: string[] = ['Extra cheese', 'Mushroom', 'Onion', 'Pepperoni', 'Sausage', 'Tomato'];
  hide = true;

  //funciones creada por mi
  //top10ClientesList: any[] = [];
  //selected: any;
  private subscription!: Subscription;
  currentStep: number = 0;


  cuentaMunicipalForm = new FormGroup({
    id: new FormControl('00000000-0000-0000-0000-000000000000'),
    idUsuario: new FormControl('00000000-0000-0000-0000-000000000000', Validators.required),
    cuentaMunicipal: new FormControl('', [Validators.required, Validators.maxLength(255)]),
    contrasenaMunicipal: new FormControl('', [Validators.required, Validators.maxLength(255)]),
    esPropietario: new FormControl(false),
    fechaCreacion: new FormControl(new Date()),
    fechaModificacion: new FormControl(new Date()),
  });

  isAgregarCM = false;
  top10Clientes: IUsuario[] = [];
  top10ClientesList: { id: string, nombres: string }[] = [];
  selected = '';

  ngOnInit(): void {
    this.UsuarioForm.get('dni')?.valueChanges.subscribe(value => {
      this.cuentaMunicipalForm.get('cuentaMunicipal')?.setValue(value);
    });

    this.subscription = this.sharedService.currentStep$.subscribe((step) => {
      this.currentStep = step;
      if (this.currentStep == 1 || this.currentStep == 2) {
        this.getTop10Clients();
      }
     
    });
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  onDniInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    let cleanedValue = input.value.replace(/\D/g, '');
    if (cleanedValue.length > 10) {
      cleanedValue = cleanedValue.substring(0, 10);
    }
    this.UsuarioForm.get('dni')?.setValue(cleanedValue, { emitEvent: false });
  }
  onCelularInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    let cleanedValue = input.value.replace(/\D/g, '');
    if (cleanedValue.length > 10) {
      cleanedValue = cleanedValue.substring(0, 10);
    }
    this.UsuarioForm.get('telefonoCelular')?.setValue(cleanedValue, { emitEvent: false });
  }
  onConvencionalInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    const cleanedValue = input.value.replace(/\D/g, '');
    this.UsuarioForm.get('telefonoConvencional')?.setValue(cleanedValue, { emitEvent: false });
  }
  onUsuarioInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    let cleanedValue = input.value.replace(/\D/g, '');
    if (cleanedValue.length > 10) {
      cleanedValue = cleanedValue.substring(0, 10);
    }
    this.cuentaMunicipalForm.get('cuentaMunicipal')?.setValue(cleanedValue, { emitEvent: false });
  }

  onClienteChange(event: any): void {
    const selectedId = event.value;
    const selectedCliente = this.top10Clientes.find(cliente => cliente.id === selectedId);
    if (selectedCliente) {
      this.UsuarioForm.patchValue({
        nombres: selectedCliente.nombres,
        apellidos: selectedCliente.apellidos,
        dni: selectedCliente.dni,
        telefonoCelular: selectedCliente.telefonoCelular,
        telefonoConvencional: selectedCliente.telefonoConvencional
      });
    }
    //this.selectedCliente = this.top10ClientesList.find(cliente => cliente.id === selectedId);
    //console.log('Cliente seleccionado:', this.selectedCliente);
    // Aquí puedes agregar lógica adicional para llenar otros campos
  }

  procesarGuardado(): void {
    this.accionAgregar();
    this.getTop10Clients();
    //this.sharedService.triggerReload();
  }

  accionAgregar(): void {
    switch (this.tipoUsuario) {
      case TipoUsuario.Cliente:
        this.addCliente();
        break;
      case TipoUsuario.Familiar:
        this.addFamiliar();
        break
      case TipoUsuario.Propietario:
        this.addPropietario();
        break;
    }
  }

  resetForm(): void {
    this.UsuarioForm.reset({
      id: '00000000-0000-0000-0000-000000000000',
      nombres: '',
      apellidos: '',
      dni: '',
      telefonoCelular: '',
      telefonoConvencional: '',
      esPrincipal: false,
      fechaCreacion: new Date(),
      fechaModificacion: new Date(),
    });
    this.cm_ToggleForm.reset({
      abrirCM: false
    })
    this.cuentaMunicipalForm.reset({
      id: '00000000-0000-0000-0000-000000000000',
      idUsuario: '00000000-0000-0000-0000-000000000000',
      cuentaMunicipal: '',
      contrasenaMunicipal: '',
      esPropietario: false,
      fechaCreacion: new Date(),
      fechaModificacion: new Date()
    })
  }

  getTop10Clients(): void {
    this.usuarioService.getAllClientes().subscribe({
      next: (response: IUsuario[]) => {
        this.top10ClientesList = response.slice(0, 10).map(cliente => ({
          id: cliente.id,
          nombres: cliente.nombres + ' ' + cliente.apellidos + ' ' + cliente.dni
        }));
        this.top10Clientes = response.slice(0, 10);
        console.log("Clientes", response.slice(0, 10));
      },
      error: (error: HttpErrorResponse) => {
        console.log("Error al obtener los clientes", error);
      }
    });
  }
  get usuarioF(): IUsuario {
    return this.UsuarioForm.value as unknown as IUsuario;
  }

  addCliente(): void {
    const usuarioForm = this.usuarioF;
    this.usuarioService.addCliente(usuarioForm).subscribe({
      next: (response: IUsuario) => {
        this.resetForm();
        //this.sharedService.updateTop10ClientesList(this.top10ClientesList);
        console.log("Nuevo Cliente creado", response);
      },
      error: (error: HttpErrorResponse) => {
        if (error.error.errors) {
          const errorMessages = this.extractErrorMessages(error.error.errors);
          errorMessages.forEach(errMsg => {
            //this.alert.open(`Error al guardar el Cliente:</strong><br> ${errMsg}`).subscribe();
          });
        } else {
          //this.alert.open(`Error al guardar el Cliente: ${error.message}`).subscribe();
        }
        //this.alert.open(`Error al guardar el Cliente: ${error.error.errors.forear}`).subscribe();
        console.log("Error al crear un cliente", error);
      }
    });
  }
  addFamiliar(): void {
    const usuarioForm = this.usuarioF;
    this.usuarioService.addFamiliar(usuarioForm).subscribe({
      next: (response: IUsuario) => {
        this.procesoCuentaMunicipal(response.id, response.esPrincipal);
        console.log("Nuevo familiar creado", response);
        //this.emitirEventoActualizado();
      },
      error: (error: any) => {
        console.log("Error al crear un familiar", error);
      }
    });
  }




  addPropietario(): void {
    const usuarioForm = this.usuarioF;
    this.usuarioService.addPropietario(usuarioForm).subscribe({
      next: (response: IUsuario) => {
          this.procesoCuentaMunicipal(response.id, response.esPrincipal);
          
        console.log("Nuevo propietario creado", response);
        //this.emitirEventoActualizado();
      },
      error: (error: any) => {
        console.log("Error al crear un propietario", error);
      }
    });
  }

  procesoCuentaMunicipal(idUsuario: string, esPropietario: boolean): void {
    const tieneCM = this.cm_ToggleForm.get('abrirCM')?.value;
    if (tieneCM) {
      this.cuentaMunicipalForm.patchValue({
        idUsuario: idUsuario,
        esPropietario: esPropietario
      });
      this.addCuentaMunicipal();
    }
    else {
      this.resetForm();
    }

  }
  get cuentaMunicipalF(): ICuentaMunicipal {
    return this.cuentaMunicipalForm.value as unknown as ICuentaMunicipal;
  }
  addCuentaMunicipal(): void {
    const cuentaMunicipalForm = this.cuentaMunicipalF;
    this.usuarioService.addCuentaMunicipal(cuentaMunicipalForm, this.tipoUsuario).subscribe({
      next: (response: any) => {
        console.log("Nueva cuenta municipal creada", response);
        this.resetForm();
      },
      error: (error: any) => {
        if (this.tipoUsuario == TipoUsuario.Familiar)
          this.deletedFamiliar(cuentaMunicipalForm.idUsuario);
        else if (this.tipoUsuario == TipoUsuario.Propietario)
          this.deletedPropietario(cuentaMunicipalForm.idUsuario); 
        console.log("Error al crear una cuenta municipal", error);
      }
    });
  }
  deletedFamiliar(id: string): void {
    this.usuarioService.deleteFamiliar(id).subscribe({
      next: (response: any) => {
        console.log("Familiar eliminado", response);
      },
      error: (error: any) => {
        console.log("Error al eliminar un familiar", error);
      }
    });
  }
  deletedPropietario(id: string): void {
    this.usuarioService.deletePropietario(id).subscribe({
      next: (response: any) => {
        console.log("Propietario eliminado", response);
      },
      error: (error: any) => {
        console.log("Error al eliminar un propietario", error);
      }
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
