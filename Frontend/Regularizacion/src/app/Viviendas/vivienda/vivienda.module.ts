import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViviendaComponent } from '../Components/vivienda/vivienda.component';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { DeleteComponent } from '../Components/delete/delete.component';
import { ToastrModule } from 'ngx-toastr';
import { DataService } from '../Services/data.service';
import { ViviendacmvComponent } from '../Components/viviendacmv/viviendacmv.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { UsuariocmvComponent } from '../../Usuarios/Components/usuariocmv/usuariocmv.component';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatDialogModule,
    MatGridListModule,
    ToastrModule.forRoot(),
    ViviendaComponent,
    DeleteComponent,
    ViviendacmvComponent,
    UsuariocmvComponent
  ],
  providers: [DataService],
  exports: [
    ViviendaComponent,
    DeleteComponent,
    ViviendacmvComponent,
    UsuariocmvComponent
  ]
})
export class ViviendaModule { }
