import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { UsuarioService } from '../../Services/usuario.service';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { IUsuario } from '../../Models/IUsuario';
import { TipoUsuario } from '../../Enums/TipoUsuario';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { NgClass } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { DeleteComponent } from '../delete/delete.component';
import { DataService } from '../../Services/data.service';
import { Subscription } from 'rxjs';
import { UsuariocmvComponent } from '../usuariocmv/usuariocmv.component';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css'],
  standalone: true,
  imports: [MatTableModule, MatFormFieldModule, MatInputModule, MatSortModule, MatPaginatorModule,
    MatIconModule, NgClass, MatButtonModule, UsuariocmvComponent]
})
export class UsuariosComponent implements OnInit, AfterViewInit {
  @Input() TipoUsuarioI: TipoUsuario;
  tipoCliente: TipoUsuario = TipoUsuario.Cliente;
  tipoPropietario: TipoUsuario = TipoUsuario.Propietario;
  tipoFamiliar: TipoUsuario = TipoUsuario.Familiar;

  displayedColumns: string[] = ['nombres', 'apellidos', 'dni', 'telefonoCelular'  ];
  dataSource: MatTableDataSource<IUsuario>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  private refreshTableSubscription: Subscription;

  constructor(private usuarioService: UsuarioService,
    private dataService: DataService,
    public dialog: MatDialog) {

  }
  openDialog(enterAnimationDuration: string, exitAnimationDuration: string, usuario: IUsuario): void {
    this.dialog.open(DeleteComponent, {
      width: '500px',
      enterAnimationDuration,
      exitAnimationDuration,
      data: {
        TipoUsuario: this.TipoUsuarioI,
        Usuario: usuario
      }
    });
  }
  openDialogcmv(enterAnimationDuration: string, exitAnimationDuration: string, usuario: IUsuario) {
    this.dialog.open(UsuariocmvComponent, {
      width: '75%',
      enterAnimationDuration,
      exitAnimationDuration,
      data: {
        TipoUsuario: this.TipoUsuarioI,
        Usuario: usuario
      }
    });
  }
  ngOnInit() {
    this.refreshTableSubscription = this.dataService.refreshTable$.subscribe(() => {
      this.getUsuarios();
    });
    if (this.TipoUsuarioI == this.tipoPropietario) {
      this.displayedColumns.push('esPrincipal');
    }
    this.displayedColumns.push('update');
    this.displayedColumns.push('delete');
  }
  ngAfterViewInit() {
    this.getUsuarios();
    //this.dataSource.paginator = this.paginator;
    //this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  getUsuarios() {
    if (this.TipoUsuarioI == this.tipoCliente)
      this.getClientes();
    else if (this.TipoUsuarioI == this.tipoPropietario)
      this.getPropietarios();
    else if (this.TipoUsuarioI == this.tipoFamiliar)
      this.getFamiliares();
  }

  getClientes() {
    this.usuarioService.getAllClientes().subscribe({
      next: (data: IUsuario[]) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error: () => { },
      complete: () => { }
    });
  }
  getFamiliares() {
    this.usuarioService.getAllFamiliares().subscribe({
      next: (data: IUsuario[]) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error: () => { },
      complete: () => { }
    });
  }
  getPropietarios() {
    this.usuarioService.getAllPropietarios().subscribe({
      next: (data: IUsuario[]) => {
        this.dataSource = new MatTableDataSource(data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error: () => { },
      complete: () => { }
    })
  }
}
