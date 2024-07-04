import { CommonModule } from '@angular/common';
import { Component, Inject, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { ViviendaService } from '../../Services/vivienda.service';
import { IVivienda } from '../../Models/IVivienda';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { DataService } from '../../Services/data.service';
import { TipoUsuario } from '../../../Usuarios/Enums/TipoUsuario';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css'],
  standalone: true,
  imports: [MatButtonModule, MatDialogModule, ToastrModule],
})
export class DeleteComponent {

  constructor(@Inject(MAT_DIALOG_DATA) public data: IVivienda,
    public dialogRef: MatDialogRef<DeleteComponent>,
    private viviendaService: ViviendaService,
    private toastr: ToastrService,
    private dataService: DataService) { }

  deleteVivienda() {
    this.viviendaService.deleteViviendaIT(this.data.id).subscribe({
      next: (resonse: any) => {
        this.toastr.success('Regularizacion borrada.', 'Borrado');
        //console.log('borrado', this.data.id)
      },
      error: () => { this.toastr.warning('Ocurrio un error.', 'Error'); },
      complete: () => { this.dataService.refreshTable() }
    });
  }
}
