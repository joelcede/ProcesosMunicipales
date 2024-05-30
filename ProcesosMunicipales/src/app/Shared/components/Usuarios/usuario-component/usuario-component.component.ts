import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MaskitoOptions } from '@maskito/core';
import { UsuarioService } from '../../../../Core/Usuario/Services/usuario.service';
import { Usuario } from '../../../../Core/Usuario/models/usuario.interface';
import { TuiDialogService } from '@taiga-ui/core';
import { TypeUsuario } from '../../../../Core/Usuario/enum/TypeUsuario';
import { TypeCrud } from '../../../enum/typeCrud';

interface User {
  readonly email: string;
  readonly name: string;
  readonly status: 'alive' | 'deceased';
  readonly tags: readonly string[];
}
@Component({
  selector: 'app-usuario-component',
  templateUrl: './usuario-component.component.html',
  styleUrls: ['./usuario-component.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UsuarioComponentComponent implements OnInit {
  //readonly UsuarioForm = new FormGroup({
  //  Nombres: new FormControl(''),
  //  Apellidos: new FormControl(''),
  //  DNI: new FormControl(null, [Validators.required, Validators.pattern('^[0-9]*$'), Validators.maxLength(13)]),
  //  TelefonoCelular: new FormControl(null, [Validators.required, Validators.pattern('^[0-9]*$'), Validators.maxLength(10)]),
  //  TelefonoConvencional: new FormControl(null, [Validators.required, Validators.pattern('^[0-9]*$'), Validators.maxLength(15)]),
  //  esPrincipal: new FormControl(false, [Validators.required]),
  //});
  //readonly testForm = new FormGroup({
  //  testValue1: new FormControl(''),
  //  testValue2: new FormControl(''),
  //});
  tipo = TypeUsuario.Cliente;
  crud = TypeCrud.Create;
  readonly digitos10: MaskitoOptions = {
    mask: [/\d/, /\d/, /\d/, /\d/, ' ', /\d/, /\d/, /\d/, /\d/, /\d/, /\d/],
  };
  //readonly digitos15: MaskitoOptions = {
  //  mask: [/\d/, /\d/, /\d/, /\d/, ' ', /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/],
  //};

  readonly formfilter = new FormGroup({
    names: new FormControl(''),
    dni: new FormControl(''),
  });
  //readonly filter = (names: string, dni: string): boolean => names >= dni;
  onToggle(enabled: boolean): void {
    if (enabled) {
      this.formfilter.enable();
    } else {
      this.formfilter.disable();
    }
  }


  //readonly filterNombres = (item: string, value: string): boolean => item.includes(value);
  readonly filterNombres = (item: string, value: string): boolean => item.includes(value);

  readonly filterDni = (item: string, value: string): boolean => item.includes(value);

  showDialogAlertDelete(nombre: string, dni: string, id: string): void {
    this.dialogs
      .open(`Seguro que quieres eliminar al cliente ${nombre} con el dni: ${dni}`, {
        label: 'Eliminar',
        size: 's',
        data: { button: [this.remove(id), 'Eliminar'] },
        closeable: true,
      })
      .subscribe();
  }

  users: Usuario[] = [];
  constructor(@Inject(TuiDialogService) private readonly dialogs: TuiDialogService,
    private usuarioService: UsuarioService) { }

  ngOnInit(): void {
    this.getDataClientes();
  }

  getDataClientes(): void {
    this.usuarioService.getAllClientes().subscribe(
      (response) => {
        this.users = response;
      },
      (error) => {
        console.error('Error al obtener los datos:', error);
      }
    );
  }

  getDataFamiliares(): void {
    this.usuarioService.getAllFamiliares().subscribe(
      (response) => {
        this.users = response;
      },
      (error) => {
        console.error('Error al obtener los datos:', error);
      }
    );
  }
  getDataPropietarios(): void {
    this.usuarioService.getAllPropietarios().subscribe(
      (response) => {
        this.users = response;
      },
      (error) => {
        console.error('Error al obtener los datos:', error);
      }
    );
  }


  readonly columns = ['nombres', 'apellidos', 'dni', 'telefonoCelular', 'telefonoConvencional', 'esPrincipal', 'actionsDelete', 'actionsEdith'];


  remove(item: string): void {
    this.usuarioService.deleteCliente(item).subscribe(
      (response) => {
        this.getDataClientes();
      },
      (error) => {
        console.error('Error al eliminar el registro:', error);
      }
    );
  }
}
