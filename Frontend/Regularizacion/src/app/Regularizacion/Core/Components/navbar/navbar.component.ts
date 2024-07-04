import { CommonModule, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MainComponent } from '../main/main.component';
import { ViviendaModule } from '../../../../Viviendas/vivienda/vivienda.module';
import { ViviendaComponent } from '../../../../Viviendas/Components/vivienda/vivienda.component';
import { UsuarioModule } from '../../../../Usuarios/usuario/usuario.module';
import { MainUsuarioComponent } from '../../../../Usuarios/Components/main/main.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  standalone: true,
  imports: [CommonModule, MatSidenavModule, NgIf, MatButtonModule, MatToolbarModule, MatListModule, MatIconModule,
    MainComponent, ViviendaModule, ViviendaComponent, UsuarioModule, MainUsuarioComponent]
})
export class NavbarComponent {
  showFiller = false;
  isRegularizacion = false;
  isUsuario = false;
  isVivienda = false;

  getRegularizacion() {
    this.isRegularizacion = true;
    this.isUsuario = false;
    this.isVivienda = false;
  }
  getUsuario() {
    this.isUsuario = true;
    this.isRegularizacion = false;
    this.isVivienda = false;
  }
  getVivienda() {
    this.isVivienda = true;
    this.isUsuario = false;
    this.isRegularizacion = false;  }
}
