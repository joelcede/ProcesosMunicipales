import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Observable, Subscription } from 'rxjs';
import { SharedService } from '../../../shared/Servives/shared.service';
import { RegularizacionService } from '../../Services/regularizacion.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { IRegularizacionesCard } from '../../Models/IRegularizacionesCard';
import { CommonModule, DatePipe, NgFor, NgIf } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTabChangeEvent } from '@angular/material/tabs';
import { PdfFichaComponent } from '../../../shared/PDF/components/pdf-ficha/pdf-ficha.component';
import { IVivienda } from '../../../shared/Vivienda/Models/IVivienda';
import { ViviendaService } from '../../../shared/Vivienda/Services/vivienda.service';
import { MatDialog } from '@angular/material/dialog';
import { DeletedComponent } from '../deleted/deleted/deleted.component';
import { EdithComponent } from '../edith/edith.component';
import { NgxLoadingButtonsModule } from 'ngx-loading-buttons';


const today = new Date();
const month = today.getMonth();
const year = today.getFullYear();

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css'],
  providers: [DatePipe],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
  standalone: true,
  imports: [FormsModule, MatFormFieldModule, MatInputModule, MatCardModule, MatButtonModule,
    NgIf, NgFor, MatIconModule, MatInputModule, CommonModule, MatGridListModule,
    MatExpansionModule, MatPaginatorModule, MatTableModule, MatSelectModule, MatDatepickerModule,
    MatNativeDateModule, ReactiveFormsModule, PdfFichaComponent, EdithComponent, NgxLoadingButtonsModule]
})



export class ViewComponent implements OnInit, AfterViewInit {
  selectedTabIndex: number = 0;
  private subscription!: Subscription;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  loadingAll = false;
  loadingAprob = false;
  loadingPend = false;
  loadingNeg = false;
  loadingCorreo = false;
  loadingFicha = false;

  buttonColors: { [key: string]: string } = {
    obtenerRegularizaciones: 'primary',
    obtenerRegAprobadas: 'primary',
    obtenerRegPendientes: 'primary',
    obtenerRegNegadas: 'primary',
    obtenerRegConCorreoErroneo: 'primary'
  };
  resetColors() {
    for (let key in this.buttonColors) {
      this.buttonColors[key] = 'primary';
    }
  }

  cambiarColorFinal() {
    for (let key in this.buttonColors) {
      this.buttonColors[key] = key === 'obtenerRegConCorreoErroneo' ? 'warn' : 'primary';
    }
  }
  selectedValue: string = '';
  selectedValueE: string = '';
  categoriaFiltros = [
    { value: '#Regularizacion', viewValue: 'Num. Regularizacion' },
    { value: 'Cedula', viewValue: 'Cedula' },
    { value: 'Celular', viewValue: 'Celular' },
    { value: 'Codigo Catastral', viewValue: 'Codigo Catastral' }
  ];
  toppings = new FormControl('');
  estados = [
    { value: 'steak-0', viewValue: 'POR HACER' },
    { value: 'pizza-1', viewValue: 'EN ESPERA' },
    { value: 'tacos-2', viewValue: 'SUBSANACION' },
    { value: 'tacos-2', viewValue: 'NEGADA' },
    { value: 'tacos-2', viewValue: 'VUELTA A SUBIR - EN ESPERA' },
    { value: 'tacos-2', viewValue: 'APROBADA' },
    { value: 'tacos-2', viewValue: 'TERMINADA - PAGO INCOMPLETO' },
    { value: 'tacos-2', viewValue: 'TERMINADA' },

  ];
  campaignOne = new FormGroup({
    start: new FormControl(),
    end: new FormControl(),
  });

  constructor(
    private sharedService: SharedService,
    private regularizacionService: RegularizacionService,
    public dialog: MatDialog, private cdr: ChangeDetectorRef
    ) {
    //this.obtenerRegularizaciones();
  }
  openDialogEdith(idReg: string) {
    const dialogRef = this.dialog.open(EdithComponent, {
      data: idReg
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  openDialog(enterAnimationDuration: string, exitAnimationDuration: string, data: string): void {
    const dialogRef = this.dialog.open(DeletedComponent, {
      width: '250px',
      enterAnimationDuration,
      exitAnimationDuration,
      data: data
    });
    dialogRef.afterClosed().subscribe(result => {
      //this.obtenerRegularizaciones();
    });
  }
  regularizaciones: IRegularizacionesCard[] = []
  

  dataSource = new MatTableDataSource<IRegularizacionesCard>(this.regularizaciones);
  columnsToDisplay = [
    'nombrePropietario', 'dni', 'codigoCatastral', 'celular', 'estadoRegularizacion', 'numRegularizacion'
  ];
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];
  expandedElement: IRegularizacionesCard | null = null;

  columnNames: { [key: string]: string } = {
    nombreTramite: 'Nombre del Trámite',
    numRegularizacion: 'Registro',
    estadoRegularizacion: 'Estado',
    valorRegularizacion: 'Valor',
    nombrePropietario: 'Nombres y Apellidos',
    dni: 'Cedula',
    celular: 'Celular',
    codigoCatastral: 'Cód. Catastral',
    expand: 'Expandir'
  };
  ngAfterViewInit() {
    if (this.paginator) {
      this.dataSource.paginator = this.paginator;
    } else {
    }
  }
  onTabChange(event: MatTabChangeEvent) {
    this.sharedService.notifyTabChange();
  }
  //onTabChange(event: MatTabChangeEvent) {
  //  this.expandedElement = null; // Restablecer la fila expandida
  //}
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  //items: number[] = [];
  displayedRegularizaciones: IRegularizacionesCard[] = [];
  itemsPerPage: number = 10;
  currentPage: number = 1;

  ngOnInit(): void {
    
    this.subscription = this.sharedService.selectedTabIndex$.subscribe(index => {
      this.selectedTabIndex = index;

      //this.obtenerRegularizaciones();
      //this.items = Array.from({ length: 100 }, (_, i) => i + 1);
      this.loadMoreItems();
      
      //console.log("Estoy visualizando las regfularizaciones: ", this.selectedTabIndex);
    });
    this.subscription = this.sharedService.tabChange$.subscribe(() => {
      this.expandedElement = null;
    });
  }
  loadMoreItems() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = this.currentPage * this.itemsPerPage;
    this.displayedRegularizaciones = this.regularizaciones.slice(0, endIndex);
    this.currentPage++;
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  obtenerRegularizaciones() {
    this.resetColors();
    this.buttonColors['obtenerRegularizaciones'] = 'accent';
    this.loadingAll = true;
    this.regularizacionService.getAllRegularizaciones().subscribe({
      next: (response: IRegularizacionesCard[]) => {
        response.forEach((reg: IRegularizacionesCard) => reg.imagenPrincipal = `data:image/png;base64,${reg.imagenPrincipal}`);
        //response.forEach((reg: any) => {
        //  reg.imagenPrincipal = 'data:image/png;base64,' + reg.imagenPrincipal;
        //});
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.paginator = this.paginator;
        this.regularizaciones = response;
        //this.toggleRow(response[0]);
      },
      error: (error: HttpErrorResponse) => {
      },
      complete: () => {
        this.loadingAll = false;
      }
    });
    //console.log("Obteniendo regularizaciones");
  }
  obtenerRegAprobadas() {
    this.resetColors();
    this.buttonColors['obtenerRegAprobadas'] = 'accent';
    this.loadingAprob = true;
    this.regularizacionService.getAllAprobadas().subscribe({
      next: (response: IRegularizacionesCard[]) => {
        response.forEach((reg: IRegularizacionesCard) => reg.imagenPrincipal = `data:image/png;base64,${reg.imagenPrincipal}`);

        this.dataSource = new MatTableDataSource(response);
        this.dataSource.paginator = this.paginator;
        this.regularizaciones = response;
        //this.toggleRow(response[0]);
      },
      error: (error: HttpErrorResponse) => {
      },
      complete: () => {
        this.loadingAprob = false;
      }
    });
  }
  obtenerRegPendientes() {
    this.resetColors();
    this.buttonColors['obtenerRegPendientes'] = 'accent';
    this.loadingPend = true;
    this.regularizacionService.getAllPendientes().subscribe({
      next: (response: IRegularizacionesCard[]) => {
        response.forEach((reg: IRegularizacionesCard) => reg.imagenPrincipal = `data:image/png;base64,${reg.imagenPrincipal}`);

        this.dataSource = new MatTableDataSource(response);
        this.dataSource.paginator = this.paginator;
        this.regularizaciones = response;
        //this.toggleRow(response[0]);
      },
      error: (error: HttpErrorResponse) => {
      },
      complete: () => {
        this.loadingPend = false;
      }
    });
  }
  obtenerRegNegadas() {
    this.resetColors();
    this.buttonColors['obtenerRegNegadas'] = 'accent';
    this.loadingNeg = true;
    this.regularizacionService.getAllNegadas().subscribe({
      next: (response: IRegularizacionesCard[]) => {
        response.forEach((reg: IRegularizacionesCard) => reg.imagenPrincipal = `data:image/png;base64,${reg.imagenPrincipal}`);

        this.dataSource = new MatTableDataSource(response);
        this.dataSource.paginator = this.paginator;
        this.regularizaciones = response;
        //this.toggleRow(response[0]);
      },
      error: (error: HttpErrorResponse) => {
      },
      complete: () => {
        this.loadingNeg = false;
      }
    });
  }
  obtenerRegConCorreoErroneo() {
    this.resetColors();
    this.buttonColors['obtenerRegConCorreoErroneo'] = 'accent';
    this.loadingCorreo = true;
    this.regularizacionService.getAllCorreosIncorrectos().subscribe({
      next: (response: IRegularizacionesCard[]) => {
        response.forEach((reg: IRegularizacionesCard) => reg.imagenPrincipal = `data:image/png;base64,${reg.imagenPrincipal}`);

        this.dataSource = new MatTableDataSource(response);
        this.dataSource.paginator = this.paginator;
        this.regularizaciones = response;
        //this.toggleRow(response[0]);
      },
      error: (error: HttpErrorResponse) => {
      },
      complete: () => {
        this.loadingCorreo = false;
      }
    });
  }
  toggleRow(element: IRegularizacionesCard) {
    this.subscription = this.sharedService.tabChange$.subscribe(() => {
      /*this.expandedElement = null;*/
      this.expandedElement = this.expandedElement === element ? null : element;
    });
    //this.expandedElement = this.expandedElement === element ? null : element;
  }
  obtenerFichaExcel() {
    this.loadingFicha = true;
    this.regularizacionService.getFichaExcel().subscribe({
      next: (response: string) => {
        const nombreDoc = 'Ficha de Regularizaciones.xlsx';
        const tipoDoc = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
        this.regularizacionService.downloadFile(response, nombreDoc, tipoDoc);
      },
      error: (error: HttpErrorResponse) => {
      },
      complete: () => {
        this.loadingFicha = false;
        this.cdr.detectChanges();
      }
    });
  }
}
