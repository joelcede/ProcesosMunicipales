import { ChangeDetectionStrategy, Component, Inject, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TuiDialogService } from '@taiga-ui/core';
import { TuiDialogFormService } from '@taiga-ui/kit';
import { PolymorpheusContent } from '@tinkoff/ng-polymorpheus';
import { MaskitoOptions } from '@maskito/core';
import { UsuarioService } from '../../../../Core/Usuario/Services/usuario.service';
import { Usuario } from '../../../../Core/Usuario/models/usuario.interface';
import { TypeUsuario } from '../../../../Core/Usuario/enum/TypeUsuario';
import { TypeCrud } from '../../../enum/typeCrud';

@Component({
  selector: 'app-add-edit-button-user',
  templateUrl: './add-edit-button-user.component.html',
  styleUrls: ['./add-edit-button-user.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [TuiDialogFormService],
})
export class AddEditButtonUserComponent {
  value = '';
  @Input() tipoUsuario: TypeUsuario = TypeUsuario.None;
  @Input() crud: TypeCrud = TypeCrud.None;
  constructor(
    @Inject(TuiDialogFormService) private readonly dialogForm: TuiDialogFormService,
    @Inject(TuiDialogService) private readonly dialogs: TuiDialogService,
    private usuarioService: UsuarioService,
  ) { }

  onModelChange(value: string): void {
    this.value = value;
    this.dialogForm.markAsDirty();
  }

  onClick(content: PolymorpheusContent): void {
    const closeable = this.dialogForm.withPrompt({
      label: 'Estas seguro que deseas salir?',
      data: {
        content: 'No se guardaran tus datos.',
        no: 'Cancelar',
        yes: 'Salir',
      },
    });

    this.dialogs.open(content, { closeable, dismissible: closeable }).subscribe({
      next: () => {
        this.dialogForm.markAsPristine();
      },
      complete: () => {
        this.value = '';
        this.proceso();
        this.dialogForm.markAsPristine();
      },
    });
  }
  readonly UsuarioForm = new FormGroup({
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
  readonly digitos10: MaskitoOptions = {
    mask: [/\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/],
  };
  readonly digitos15: MaskitoOptions = {
    mask: [/\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/],
  };
  get usuarioF(): Usuario {
    return this.UsuarioForm.value as unknown as Usuario;
  }
  addCliente(): void {
    const usuarioForm = this.usuarioF;
    this.usuarioService.addCliente(usuarioForm).subscribe({
      next: (response: any) => {
        console.log("Nuevo Cliente creado", response);
      },
      error: (error: any) => {
        console.log("Error al crear un cliente", error);
      }
    });
  }
  addFamiliar(): void {
    const usuarioForm = this.usuarioF;
    this.usuarioService.addFamiliar(usuarioForm).subscribe({
      next: (response: any) => {
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
      next: (response: any) => {
        console.log("Nuevo propietario creado", response);
        //this.emitirEventoActualizado();
      },
      error: (error: any) => {
        console.log("Error al crear un propietario", error);
      }
    });
  }

  editCliente(): void {
    const usuarioForm = this.usuarioF;
    this.usuarioService.updateCliente(usuarioForm.id, usuarioForm).subscribe({
      next: (response: any) => {
        console.log("Cliente actualizado", response);
        //this.emitirEventoActualizado();
      },
      error: (error: any) => {
        console.log("Error al actualizar un cliente", error);
      }
    });
  }

  editFamiliar(): void {
    const usuarioForm = this.usuarioF;
    this.usuarioService.updateFamiliar(usuarioForm.id, usuarioForm).subscribe({
      next: (response: any) => {
        console.log("Familiar actualizado", response);
        //this.emitirEventoActualizado();
      },
      error: (error: any) => {
        console.log("Error al actualizar un familiar", error);
      }
    });
  }
  editPropietario(): void {
    const usuarioForm = this.usuarioF;
    this.usuarioService.updatePropietario(usuarioForm.id, usuarioForm).subscribe({
      next: (response: any) => {
        console.log("Propietario actualizado", response);
        //this.emitirEventoActualizado();
      },
      error: (error: any) => {
        console.log("Error al actualizar un propietario", error);
      }
    });
  }

  addUsuario(): void {
    const usuarioForm = this.usuarioF;
    if (this.UsuarioForm.valid) {
      const validador = TypeUsuario;
      //this.accionAgregar();
    } else {
      this.UsuarioForm.markAllAsTouched();
    }
  }
  accionAgregar(usuario: TypeUsuario): void {
    switch (usuario) {
      case TypeUsuario.Cliente:
        this.addCliente();
        break;
      case TypeUsuario.Familiar:
        this.addFamiliar();
        break
      case TypeUsuario.Propietario:
        this.addPropietario();
        break;
    }
  }
  accionEditar(usuario: TypeUsuario): void {
    switch (usuario) {
      case TypeUsuario.Cliente:
        this.editCliente();
        break;
      case TypeUsuario.Familiar:
        this.editFamiliar();
        break
      case TypeUsuario.Propietario:
        this.editPropietario();
        break;
    }
  }

  proceso(): void {
    const crear = TypeCrud.Create;
    const editar = TypeCrud.Update;

    if (this.crud === crear) {
      this.accionAgregar(this.tipoUsuario);
    } else if (this.crud === editar) {
      this.accionEditar(this.tipoUsuario);
    }
  }

}
