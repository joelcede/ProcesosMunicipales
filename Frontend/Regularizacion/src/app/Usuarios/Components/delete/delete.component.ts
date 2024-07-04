import { Component, Inject, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { TipoUsuario } from '../../Enums/TipoUsuario';
import { UsuarioService } from '../../Services/usuario.service';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { DataService } from '../../Services/data.service';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css'],
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, ToastrModule]
})
export class DeleteComponent {
  tipoCliente: TipoUsuario = TipoUsuario.Cliente;
  tipoPropietario: TipoUsuario = TipoUsuario.Propietario;
  tipoFamiliar: TipoUsuario = TipoUsuario.Familiar;
  constructor(public dialogRef: MatDialogRef<DeleteComponent>,
    private usuarioService: UsuarioService,
    private toastr: ToastrService,
    private dataService: DataService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

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
  deleteUsuario() {
    this.usuarioService.deleteUsuario(this.data.Usuario.id, this.data.TipoUsuario).subscribe({
      next: () => {
        this.toastr.success(`${this.getUsuarioString()} borrado correctamente`, 'Borrado');
      },
      error: () => {
        this.toastr.error(`Ocurrio un error al borrar al ${this.getUsuarioString()}`, 'Error');
      },
      complete: () => { this.dataService.refreshTable() }
    });
  }
  //deleteCliente
}
