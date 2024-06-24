import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MatDialogModule, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { HttpErrorResponse } from '@angular/common/http';
import { RegularizacionService } from '../../../Services/regularizacion.service';
import { ToastrModule, ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-deleted',
  templateUrl: './deleted.component.html',
  styleUrls: ['./deleted.component.css'],
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, ToastrModule]
})
export class DeletedComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: string,
    public dialogRef: MatDialogRef<DeletedComponent>,
    private regularizacionService: RegularizacionService,
    private toastr: ToastrService) { }


  borrarRegularizacion() {
    this.regularizacionService.deleteRegularizacion(this.data).subscribe({
      next: (response: any) => {
        //console.log("Regularizaciones obtenidas", response);
        this.toastr.success('Regularizacion borrada.', 'ERROR');
      },
      error: (error: HttpErrorResponse) => {
        this.toastr.error(error.error, 'ERROR');
        //console.log("Error al agregar la regularizacion", error);
      }
    });
  }
}
