import { Component, Inject, OnInit } from '@angular/core';
import { UsuarioService } from '../../Services/usuario.service';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { DataService } from '../../Services/data.service';
import { TipoUsuario } from '../../Enums/TipoUsuario';
import { MatButtonModule } from '@angular/material/button';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { NgFor, NgIf } from '@angular/common';
import { MaskitoDirective } from '@maskito/angular';
import { MaskitoOptions } from '@maskito/core';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSelectModule } from '@angular/material/select';
import { IUsuario } from '../../../Regularizacion/shared/Usuario/Models/IUsuario';
import { ICuentaMunicipal } from '../../Models/ICuentaMunicipal';

@Component({
  selector: 'app-usuariocmv',
  templateUrl: './usuariocmv.component.html',
  styleUrls: ['./usuariocmv.component.css'],
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, ToastrModule, MatFormFieldModule, ReactiveFormsModule,
    NgIf, NgFor, MaskitoDirective, MatIconModule, MatInputModule, MatToolbarModule, MatSlideToggleModule,
    FormsModule, MatSelectModule,]
})
export class UsuariocmvComponent implements OnInit {
  tipoCliente: TipoUsuario = TipoUsuario.Cliente;
  tipoPropietario: TipoUsuario = TipoUsuario.Propietario;
  tipoFamiliar: TipoUsuario = TipoUsuario.Familiar;
  isChecked = false;
  isAgregarCM = false;
  hide = true;
  loading = false;
  desactivarCuenta = true;
  getUsuarioCM = false;

  constructor(public dialogRef: MatDialogRef<UsuariocmvComponent>,
    private usuarioService: UsuarioService,
    private toastr: ToastrService,
    private dataService: DataService,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.getUsuario();
  }

  ngOnInit() {
    this.UsuarioForm.get('dni')?.valueChanges.subscribe(value => {
      this.cuentaMunicipalForm.get('cuentaMunicipal')?.setValue(value);
    });
  }

  UsuarioForm = new FormGroup({
    id: new FormControl('00000000-0000-0000-0000-000000000000'),
    nombres: new FormControl(''),
    apellidos: new FormControl(''),
    dni: new FormControl('', [Validators.required, Validators.pattern('^[0-9]*$'), Validators.maxLength(13)]),
    telefonoCelular: new FormControl('', [, Validators.pattern('^[0-9]*$'), Validators.maxLength(10)]),
    telefonoConvencional: new FormControl('', [Validators.required, Validators.pattern('^[0-9]*$'), Validators.maxLength(15)]),
    esPrincipal: new FormControl(false, [Validators.required]),
    fechaCreacion: new FormControl(new Date()),
    fechaModificacion: new FormControl(new Date()),
  });
  cuentaMunicipalForm = new FormGroup({
    id: new FormControl('00000000-0000-0000-0000-000000000000'),
    idUsuario: new FormControl('00000000-0000-0000-0000-000000000000', Validators.required),
    cuentaMunicipal: new FormControl('', [Validators.required, Validators.maxLength(255)]),
    contrasenaMunicipal: new FormControl('', [Validators.required, Validators.maxLength(255)]),
    esPropietario: new FormControl(false),
    fechaCreacion: new FormControl(new Date()),
    fechaModificacion: new FormControl(new Date()),
  });
  cm_ToggleForm = new FormGroup({
    abrirCM: new FormControl(false)
  });
  readonly digitos10: MaskitoOptions = {
    mask: [/\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/],
  };

  getUsuarioString(): string {
    let usuarioTipo = '';
    if (this.data.TipoUsuario == this.tipoCliente)
      usuarioTipo = 'Cliente';
    else if (this.data.TipoUsuario == this.tipoPropietario)
      usuarioTipo = 'Propietario';
    else if (this.data.TipoUsuario == this.tipoFamiliar)
      usuarioTipo = 'Familiar';
    return usuarioTipo;
  }
  patchUsuario(data: IUsuario) {
    this.UsuarioForm.patchValue({
      id: data.id,
      nombres: data.nombres,
      apellidos: data.apellidos,
      dni: data.dni,
      telefonoCelular: data.telefonoCelular,
      telefonoConvencional: data.telefonoConvencional,
      esPrincipal: data.esPrincipal,
      fechaCreacion: data.fechaCreacion,
      fechaModificacion: data.fechaModificacion
    });
  }
  getCM_Usuario(idUsuario) {
    this.usuarioService.getByIdCuentaMunicipalUsuario(idUsuario, this.data.TipoUsuario).subscribe({
      next: (data: ICuentaMunicipal) => {
        this.cuentaMunicipalForm.patchValue({
          id: data.id,
          idUsuario: data.idUsuario,
          cuentaMunicipal: data.cuentaMunicipal,
          contrasenaMunicipal: data.contrasenaMunicipal,
          fechaCreacion: data.fechaCreacion,
          fechaModificacion: data.fechaModificacion
        });

        if (this.cuentaMunicipalForm.get('cuentaMunicipal').value !== '' &&
          this.cuentaMunicipalForm.get('cuentaMunicipal').value !== undefined &&
          this.cuentaMunicipalForm.get('cuentaMunicipal').value !== null) {
          this.isAgregarCM = true;
          this.desactivarCuenta = false;
          this.getUsuarioCM = true;
          }
      }
    });
  }
  getUsuario() {
    if (this.data.TipoUsuario == this.tipoCliente)
      this.getCliente();
    else if (this.data.TipoUsuario == this.tipoPropietario)
      this.getPropietario();
    else if (this.data.TipoUsuario == this.tipoFamiliar)
      this.getFamiliar();
  }

  getCliente() {
    this.usuarioService.getByIdCliente(this.data.Usuario.id).subscribe({
      next: (data: IUsuario) => {
        this.patchUsuario(data);
      }
    });
  }

  getPropietario() {
    this.usuarioService.getByIdPropietario(this.data.Usuario.id).subscribe({
      next: (data: IUsuario) => {
        this.patchUsuario(data);
        this.getCM_Usuario(data.id);
      }
    });
  }
  getFamiliar() {
    this.usuarioService.getByIdFamiliar(this.data.Usuario.id).subscribe({
      next: (data: IUsuario) => {
        this.patchUsuario(data);
        this.getCM_Usuario(data.id);
      }
    });
  }

  updateUsuario() {
    if (this.data.TipoUsuario == this.tipoCliente)
      this.updateCliente();
    else if (this.data.TipoUsuario == this.tipoPropietario)
      this.updatePropietario();
    else if (this.data.TipoUsuario == this.tipoFamiliar)
      this.updateFamiliar();
  }
  get usuarioF(): IUsuario {
    return this.UsuarioForm.value as unknown as IUsuario;
  }
  get cuentaMunicipalF(): ICuentaMunicipal {
    return this.cuentaMunicipalForm.value as unknown as ICuentaMunicipal;
  }
  updateCM() {
    this.usuarioService.updateCuentaMunicipalUsuario(this.data.Usuario.id, this.cuentaMunicipalF, this.data.TipoUsuario).subscribe({
      next: (data: any) => {
        this.toastr.success(`Cuenta Municipal editada correctamente`, 'Guardado')
      },
      error: () => { this.toastr.error(`Ocurrio un error al actualizar la Cuenta Municipal`, 'ERROR'); },
      complete: () => { this.dataService.refreshTable(); }
    });
  }
  updateFomCM(idUsuario: string, esPropietarioP: boolean) {
    const tieneCM = this.cm_ToggleForm.get('abrirCM')?.value;
    if (tieneCM) {
      this.cuentaMunicipalForm.patchValue({
        idUsuario: idUsuario,
        esPropietario: esPropietarioP
      });
    }
  }
  createCM() {
    this.usuarioService.addCuentaMunicipal(this.cuentaMunicipalF, this.data.TipoUsuario).subscribe({
      next: (data: ICuentaMunicipal) => {
        this.toastr.success(`Cuenta Municipal creada correctamente`, 'Guardado')
      },
      error: () => { this.toastr.error(`Ocurrio un error al crear la Cuenta Municipal`, 'ERROR'); },
      complete: () => { this.dataService.refreshTable(); }
    });
  }
  updateCliente() {
    this.usuarioService.updateCliente(this.data.Usuario.id, this.usuarioF).subscribe({
      next: (data: IUsuario) => {
        this.toastr.success(`${this.getUsuarioString()} editado correctamente`, 'Guardado')
      },
      error: () => { this.toastr.error(`Ocurrio un error al actualizar el ${this.getUsuarioString()}`, 'ERROR'); },
      complete: () => { this.dataService.refreshTable(); }
    });
  }
  updatePropietario() {
    this.usuarioService.updatePropietario(this.data.Usuario.id, this.usuarioF).subscribe({
      next: (data: IUsuario) => {
        this.toastr.success(`${this.getUsuarioString()} editado correctamente`, 'Guardado')
        if (this.getUsuarioCM)
          this.updateCM();
        else {
          this.updateFomCM(this.data.Usuario.id, true);
          this.createCM();
        }
          
      },
      error: () => { this.toastr.error(`Ocurrio un error al actualizar el ${this.getUsuarioString()}`, 'ERROR'); },
      complete: () => { this.dataService.refreshTable(); }
    });
  }
  updateFamiliar() {
    this.usuarioService.updateFamiliar(this.data.Usuario.id, this.usuarioF).subscribe({
      next: (data: IUsuario) => {
        this.toastr.success(`${this.getUsuarioString()} editado correctamente`, 'Guardado')
        if (this.getUsuarioCM)
          this.updateCM();
        else {
          this.updateFomCM(this.data.Usuario.id, false);
          this.createCM();
        }
          
      },
      error: () => { this.toastr.error(`Ocurrio un error al actualizar el ${this.getUsuarioString()}`, 'ERROR'); },
      complete: () => { this.dataService.refreshTable(); }
    });
  }

}
