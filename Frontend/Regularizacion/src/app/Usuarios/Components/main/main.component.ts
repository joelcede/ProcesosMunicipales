import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { Observable, Observer } from 'rxjs';
import { TipoUsuario } from '../../Enums/TipoUsuario';
import { UsuariosComponent } from '../usuarios/usuarios.component';
export interface ExampleTab {
  label: string;
  content: string;
}
@Component({
  selector: 'app-main-usuario',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
  standalone: true,
  imports: [MatTabsModule, NgIf, MatTabsModule, NgFor, AsyncPipe, UsuariosComponent]
})
export class MainUsuarioComponent {
  tipoCliente: TipoUsuario = TipoUsuario.Cliente;
  tipoPropietario: TipoUsuario = TipoUsuario.Propietario;
  tipoFamiliar: TipoUsuario = TipoUsuario.Familiar;
  constructor() {
   
  }
}
