import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatSelectChange, MatSelectModule } from '@angular/material/select';
import { NgFor, NgIf } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { TextFieldModule } from '@angular/cdk/text-field';
import { MatDividerModule } from '@angular/material/divider';
import { UsuarioService } from '../../../Usuario/Services/usuario-service.service';
import { IUsuario } from '../../../Usuario/Models/IUsuario';
import { HttpErrorResponse } from '@angular/common/http';
import { ViviendaService } from '../../Services/vivienda.service';
import { IVivienda } from '../../Models/IVivienda';
import { IViviendaUsuario } from '../../Models/IViviendaUsuario';
import { Subscription } from 'rxjs';
import { SharedService } from '../../../Servives/shared.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ToastrModule, ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-vivienda',
  templateUrl: './vivienda.component.html',
  styleUrls: ['./vivienda.component.css'],
  standalone: true,
  imports: [MatButtonModule, MatDialogModule, MatFormFieldModule, MatSelectModule, ReactiveFormsModule,
    NgIf, NgFor, MatIconModule, MatInputModule, MatToolbarModule, TextFieldModule, MatDividerModule, FormsModule,
    MatSnackBarModule, ToastrModule],
})
export class ViviendaComponent implements OnInit, OnDestroy {
  //private reloadSubscription!: Subscription;
  constructor(private usuarioService: UsuarioService,
    private viviendaService: ViviendaService,
    private sharedService: SharedService,
    private snackBar: MatSnackBar,
    private toastr: ToastrService) {
    this.obtenerUltimos10Propietarios();
    this.obtenerUltimos10Familiares();
  }
  textoImagen = 'Subir Imagen';

  ngOnInit(): void {
    this.subscription = this.sharedService.currentStep$.subscribe((step) => {
      this.currentStep = step;
      if (this.currentStep == 3) {
        this.obtenerUltimos10Propietarios();
        this.obtenerUltimos10Familiares();
      }
    });
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  codigoCastastral = new FormControl('', [Validators.required]);
  getErrorMessage() {
    if (this.codigoCastastral.hasError('required')) {
      return 'Ingresa un codigo Catastral';
    }
    return  '';
  }
  //getErrorMessageImage() {
  //  if (this.codigoCastastral.hasError('required')) {
  //    return 'Ingresa un codigo Catastral';
  //  }
  //  return '';
  //}
  disableSelect = new FormControl(false);
  toppingList: string[] = ['Extra cheese', 'Mushroom', 'Onion', 'Pepperoni', 'Sausage', 'Tomato'];
  hide = true;

  selectedFiles?: FileList;

  onFileSelected(event: any) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      let fileName = file.name;
      reader.readAsDataURL(file);

      reader.onload = (e: any) => {
        const base64String = (reader.result as string).split(',')[1]; // Extraer solo la parte base64
        this.viviendaForm.patchValue({
          imagen: base64String
        });
        this.snackBar.open('Imagen subida correctamente', 'Cerrar', {
          duration: 3000,
        });
        if (fileName.length > 15) {
          fileName = fileName.substring(0, 15) + '...';
          this.textoImagen = fileName;
        }
        
      };
    
    }
  }
  csvInputChange(fileInputEvent: any) {

  }
  selectFiles(event: any): void { }
  uploadFiles(): void {

  }
  toppings = new FormControl('');
  isPrincipalSelected: boolean = false;
  selectedUserId: string | null = null;

  onSelectionChange(event: MatSelectChange): void {
    let selectPrincipal = true;
    event.value.forEach((value: string) => {
      const selectedUser = this.top10Propietarios.find(usuario => usuario.id === value);
      if (selectedUser) {
        if (selectedUser.principal && !this.isPrincipalSelected) {
          this.isPrincipalSelected = true;
          this.selectedUserId = selectedUser.id;
        }
        else {
          selectPrincipal = false;
          
        }

      } 
    });
    if (!selectPrincipal && !this.isPrincipalSelected) {
      this.isPrincipalSelected = false;
      //this.selectedUserId = '';
    }
    else {
      this.isPrincipalSelected = true;
      //this.selectedUserId = 'df';
    }
    //if ((event.value == null || event.value.length == 0) && this.isPrincipalSelected) {
    //  this.isPrincipalSelected = false;
    //  this.selectedUserId = '';
    //}

  }
  //metodos propios

  private subscription!: Subscription;
  currentStep: number = 0;

  top10Propietarios: { id: string, nombres: string, principal: boolean }[] = [];
  top10Familiares: { id: string, nombres: string }[] = [];

  PropietarioControl = new FormControl([], [Validators.required]);
  FamiliarControl = new FormControl([], [Validators.required]);

  TI_ViviendaUsuarioForm = new FormGroup({
    id: new FormControl('00000000-0000-0000-0000-000000000000'),
    idUsuario: new FormControl('00000000-0000-0000-0000-000000000000'),
    idVivienda: new FormControl('00000000-0000-0000-0000-000000000000')
  });

  viviendaForm = new FormGroup({
    id: new FormControl('00000000-0000-0000-0000-000000000000'),
    direccion: new FormControl('', [Validators.required]),
    codigoCatastral: new FormControl(''),
    telefono: new FormControl(''),
    coordenadas: new FormControl(''),
    fechaCreacion: new FormControl(new Date()),
    fechaActualizacion: new FormControl(new Date()),
    imagen: new FormControl('', [Validators.required])
  });
  allFormsValid(): boolean {
    return this.viviendaForm.valid && this.PropietarioControl.valid && this.FamiliarControl.valid;
  }
  onNumberInput(event: Event, control: string, cant: number): void {
    const input = event.target as HTMLInputElement;
    let cleanedValue = input.value.replace(/\D/g, '');
    if (cleanedValue.length > 10) {
      cleanedValue = cleanedValue.substring(0, cant);
    }
    this.viviendaForm.get(control)?.setValue(cleanedValue, { emitEvent: false });
  }
  get viviendaF(): IVivienda {
    return this.viviendaForm.value as unknown as IVivienda;
  }


  addVivienda(): void {
    const viviendaForm = this.viviendaF;
    this.viviendaService.addVivienda(viviendaForm).subscribe({
      next: (response: any) => {
        const listPropietarios = this.listPropietarios(response.id);
        this.addPropietarioVivienda(listPropietarios);
        const listFamiliares = this.listFamiliares(response.id);
        this.addFamiliarVivienda(listFamiliares);
        this.resetForm();
        this.toastr.success('Vivienda Guardado', 'Exito');
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
        //console.log("Error al agregar la vivienda", error);
      }
    })
    console.log(this.viviendaForm.value);
  }

  listPropietarios(idVivienda: string): IViviendaUsuario[] {
    const propietarios = this.PropietarioControl.value;
    const listaPropietarios: IViviendaUsuario[] = [];

    propietarios?.forEach((propietario: string) => {

      const viviendaPropietario: IViviendaUsuario = {
          id: '00000000-0000-0000-0000-000000000000',
          idUsuario: propietario,
          idVivienda: idVivienda
      };
      listaPropietarios.push(viviendaPropietario);

    });
    return listaPropietarios;
  }

  addPropietarioVivienda(propiedades: IViviendaUsuario[]): void {

    propiedades.forEach(propiedad => {

      this.viviendaService.addViviendaPropietario(propiedad).subscribe({
        next: (response: any) => {
          //console.log("Vivienda Usuario agregado", response);
        },
        error: (error: HttpErrorResponse) => {
          console.log("Error al agregar la vivienda usuario", error);
        }
      })

    });

  }

  listFamiliares(idVivienda: string): IViviendaUsuario[] {
    const familiares = this.FamiliarControl.value;
    const listaFamiliares: IViviendaUsuario[] = [];

    familiares?.forEach((propietario: string) => {

      const viviendaPropietario: IViviendaUsuario = {
        id: '00000000-0000-0000-0000-000000000000',
        idUsuario: propietario,
        idVivienda: idVivienda
      };
      listaFamiliares.push(viviendaPropietario);

    });
    return listaFamiliares;
  }

  addFamiliarVivienda(familiares: IViviendaUsuario[]): void {

    familiares.forEach(familiar => {

      this.viviendaService.addViviendaFamiliar(familiar).subscribe({
        next: (response: any) => {
          //console.log("Vivienda Usuario agregado", response);
        },
        error: (error: HttpErrorResponse) => {

          //console.log("Error al agregar la vivienda usuario", error);
        }
      })

    });

  }

  obtenerUltimos10Propietarios() {
    this.usuarioService.getAllPropietarios().subscribe({
      next: (response: IUsuario[]) => {
        this.top10Propietarios = response.slice(0, 10).map(propietario => ({
          id: propietario.id,
          nombres: propietario.nombres + ' ' + propietario.apellidos,
          principal: propietario.esPrincipal
        }));
        //this.top10Clientes = response.slice(0, 10);
        console.log("Propietarios", response.slice(0, 10));
      },
      error: (error: HttpErrorResponse) => {
        console.log("Error al obtener los clientes", error);
      }
    })
  }

  obtenerUltimos10Familiares() {
    this.usuarioService.getAllFamiliares().subscribe({
      next: (response: IUsuario[]) => {
        this.top10Familiares = response.slice(0, 10).map(familiar => ({
          id: familiar.id,
          nombres: familiar.nombres + ' ' + familiar.apellidos
        }));
        //this.top10Clientes = response.slice(0, 10);
        console.log("Familiares", response.slice(0, 10));
      },
      error: (error: HttpErrorResponse) => {
        console.log("Error al obtener los clientes", error);
      }
    })
  }

  resetForm(): void {
    this.viviendaForm.reset({
      id: '00000000-0000-0000-0000-000000000000',
      direccion: '',
      codigoCatastral: '',
      telefono: '',
      coordenadas: '',
      fechaCreacion: new Date(),
      fechaActualizacion: new Date(),
      imagen: ''
    });
    this.FamiliarControl.reset();
    this.PropietarioControl.reset();
  }

  getPropietariosDisplay(): string {
    const selected = this.PropietarioControl.value || [];
    //if (selected.length === 0) {
    //  return '';
    //}
    const firstSelected = this.top10Propietarios.find(propietario => propietario.id === selected[0]);
    if (selected.length === 1) {
      return firstSelected?.nombres || '';
    }
    return `${firstSelected?.nombres} (+${selected.length - 1} ${selected.length === 2 ? 'otro' : 'otros'})`;
  }

  getSelectedFamiliaresDisplay(): string {
    const selected = this.FamiliarControl.value || [];
    //if (selected.length === 0) {
    //  return '';
    //}
    const firstSelected = this.top10Familiares.find(familiar => familiar.id === selected[0]);
    if (selected.length === 1) {
      return firstSelected?.nombres || '';
    }
    return `${firstSelected?.nombres} (+${selected.length - 1} ${selected.length === 2 ? 'otro' : 'otros'})`;

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
