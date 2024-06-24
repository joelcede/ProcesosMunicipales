import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
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
import { CommonModule, NgFor, NgIf } from '@angular/common';
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


const today = new Date();
const month = today.getMonth();
const year = today.getFullYear();

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css'],
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
    MatNativeDateModule, ReactiveFormsModule, PdfFichaComponent, EdithComponent]
})



export class ViewComponent implements OnInit, AfterViewInit {
  selectedTabIndex: number = 0;
  private subscription!: Subscription;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

 

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
    public dialog: MatDialog,
    ) {
    this.obtenerRegularizaciones();
  }
  openDialogEdith(idReg: string) {
    const dialogRef = this.dialog.open(EdithComponent, {
      data: idReg
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
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
      this.obtenerRegularizaciones();
    });
  }

  CARDS: IRegularizacionesCard[] = [
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'PENDIENTE',
      numRegularizacion: 1,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      numRegularizacion: 2,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      numRegularizacion: 3,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      numRegularizacion: 4,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      numRegularizacion: 5,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      numRegularizacion: 6,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      numRegularizacion: 7,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      numRegularizacion: 8,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      numRegularizacion: 9,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      numRegularizacion: 10,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      numRegularizacion: 11,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      numRegularizacion: 12,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      numRegularizacion: 13,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      numRegularizacion: 14,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      numRegularizacion: 15,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      numRegularizacion: 16,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Regularizacion',
      estadoRegularizacion: 'Pendiente',
      numRegularizacion: 17,
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      dni: '12345667890',
      codigoCatastral: 'ABC123'
    },
    // Agrega más tarjetas aquí
  ];
  regularizaciones: IRegularizacionesCard[] = []
  

  dataSource = new MatTableDataSource<IRegularizacionesCard>(this.regularizaciones);
  columnsToDisplay = [
    'nombrePropietario', 'dni', 'codigoCatastral', 'celular', 'valorRegularizacion',
    'estadoRegularizacion', 'numRegularizacion'
    
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
      console.log('Paginator:', this.paginator);
      this.dataSource.paginator = this.paginator;
    } else {
      console.error('Paginator no está definido');
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

      this.obtenerRegularizaciones();
      //this.items = Array.from({ length: 100 }, (_, i) => i + 1);
      this.loadMoreItems();
      
      console.log("regularizaciones obtenidas desde la instancia: ", this.regularizaciones)
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
    console.log('loadMoreItems:', this.displayedRegularizaciones);
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  obtenerRegularizaciones() {
    this.regularizacionService.getAllRegularizaciones().subscribe({
      next: (response: IRegularizacionesCard[]) => {
        response.forEach((reg: IRegularizacionesCard) => reg.imagenPrincipal = `data:image/png;base64,${reg.imagenPrincipal}`);
        //response.forEach((reg: any) => {
        //  reg.imagenPrincipal = 'data:image/png;base64,' + reg.imagenPrincipal;
        //});
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.paginator = this.paginator;
        this.regularizaciones = response;
        this.toggleRow(response[0]);
        console.log("Regularizaciones obtenidas", response);
      },
      error: (error: HttpErrorResponse) => {
        console.log("Error al agregar la regularizacion", error);
      }
    });
    //console.log("Obteniendo regularizaciones");
  }

  toggleRow(element: IRegularizacionesCard) {
    this.subscription = this.sharedService.tabChange$.subscribe(() => {
      /*this.expandedElement = null;*/
      this.expandedElement = this.expandedElement === element ? null : element;
    });
    //this.expandedElement = this.expandedElement === element ? null : element;
  }
}
