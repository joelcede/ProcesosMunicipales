import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
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
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css'],
  standalone: true,
  imports: [FormsModule, MatFormFieldModule, MatInputModule, MatCardModule, MatButtonModule,
    NgIf, NgFor, MatIconModule, MatInputModule, CommonModule, MatGridListModule,
    MatExpansionModule, MatPaginatorModule, ]
})
export class ViewComponent implements OnInit {
  selectedTabIndex: number = 0;
  private subscription!: Subscription;

  constructor(private sharedService: SharedService, private regularizacionService: RegularizacionService) {
    this.obtenerRegularizaciones();
  }
  cards: IRegularizacionesCard[] = [
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'PENDIENTE',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'REGULARIZACION',
      estadoRegularizacion: 'TERMINADA - PAGO INCOMPLETO',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Tramite 1',
      estadoRegularizacion: 'Pendiente',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    {
      idRegularizacion: '1',
      idVivienda: '1',
      nombreTramite: 'Regularizacion',
      estadoRegularizacion: 'Pendiente',
      valorRegularizacion: 1000,
      imagenPrincipal: 'https://material.angular.io/assets/img/examples/shiba2.jpg', // Example base64 string
      nombrePropietario: 'John Doe',
      celular: '1234567890',
      codigoCatastral: 'ABC123'
    },
    // Agrega más tarjetas aquí
  ];
  regularizaciones: IRegularizacionesCard[] = []

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
        response.forEach((reg: any) => {
          reg.imagenPrincipal = 'data:image/png;base64,' + reg.imagenPrincipal;
        });
        //response.forEach((element: any) => {
          //<img mat - card - image[src]="/png;base64,{{tarjeta.imagenPrincipal}}" alt = "Image of {{tarjeta.nombreTramite}}" >
          //element.
        this.regularizaciones = response;
        console.log("Regularizaciones obtenidas", response);
      },
      error: (error: HttpErrorResponse) => {
        console.log("Error al agregar la regularizacion", error);
      }
    });
    //console.log("Obteniendo regularizaciones");
  }
}
