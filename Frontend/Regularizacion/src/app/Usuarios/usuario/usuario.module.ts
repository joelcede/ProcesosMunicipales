import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsuariosComponent } from '../Components/usuarios/usuarios.component';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTabsModule } from '@angular/material/tabs';
import { MainUsuarioComponent } from '../Components/main/main.component';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MatStepperModule,
    MatTabsModule,
    MatTableModule,
    MatDialogModule,
    ToastrModule.forRoot(),
    UsuariosComponent,
    MainUsuarioComponent
  ],
  exports: [
    UsuariosComponent,
    MainUsuarioComponent
  ]
})
export class UsuarioModule { }
