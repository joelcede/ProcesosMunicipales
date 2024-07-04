import { CommonModule, NgIf } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MainComponent } from '../../../Regularizacion/Core/Components/main/main.component';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ViviendaService } from '../../Services/vivienda.service';
import { HttpErrorResponse } from '@angular/common/http';
import { IVivienda } from '../../Models/IVivienda';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { DeleteComponent } from '../delete/delete.component';
import { Subscription } from 'rxjs';
import { DataService } from '../../Services/data.service';
import { ViviendacmvComponent } from '../viviendacmv/viviendacmv.component';
import { MatGridListModule } from '@angular/material/grid-list';

@Component({
  selector: 'app-vivienda',
  templateUrl: './vivienda.component.html',
  styleUrls: ['./vivienda.component.css'],
  standalone: true,
  imports: [CommonModule, MatSidenavModule, NgIf, MatButtonModule,
    MatToolbarModule, MatListModule, MatIconModule, MatTableModule,
    MatFormFieldModule, MatInputModule, MatSortModule, MatPaginatorModule, MatDialogModule,
    DeleteComponent, ViviendacmvComponent]
})
export class ViviendaComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = ['direccion', 'codigoCatastral',
    'telefono', 'coordenadas', 'borrar', 'editar'];
  
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  viviendas: IVivienda[] = [];
  dataSource: MatTableDataSource<IVivienda>;
  private refreshTableSubscription: Subscription;
  constructor(private viviendaService: ViviendaService, public dialog: MatDialog,
    private dataService: DataService) {

  }
  ngOnInit() {
    this.refreshTableSubscription = this.dataService.refreshTable$.subscribe(() => {
      this.getViviendas();
    });
  }
  openDialog(enterAnimationDuration: string, exitAnimationDuration: string, data: IVivienda): void {
    this.dialog.open(DeleteComponent, {
      width: '250px',
      enterAnimationDuration,
      exitAnimationDuration,
      data: data
    });
  }

  openDialogCMV(enterAnimationDuration: string, exitAnimationDuration: string, data: IVivienda): void {
    this.dialog.open(ViviendacmvComponent, {
      width: '75%',
      enterAnimationDuration,
      exitAnimationDuration,
      data: data
    });
  }

  ngOnDestroy() {
    // Asegúrate de desuscribirte para evitar fugas de memoria
    if (this.refreshTableSubscription) {
      this.refreshTableSubscription.unsubscribe();
    }
  }
  ngAfterViewInit() {
    this.getViviendas();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getViviendas() {
    this.viviendaService.getAllViviendas().subscribe({
      next: (response: IVivienda[]) => {
        //this.viviendas = response;
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error: (error: HttpErrorResponse) => {
      },
      complete: () => {

      }
    });
  }
}
