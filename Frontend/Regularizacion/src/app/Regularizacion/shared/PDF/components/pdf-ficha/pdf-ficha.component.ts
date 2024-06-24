import { Component, Input } from '@angular/core';
declare module 'pdfmake/build/pdfmake';
import * as pdfMake from 'pdfmake/build/pdfmake';
import * as pdfFonts from 'pdfmake/build/vfs_fonts';
import { IRegularizacion } from '../../../../Core/Models/IRegularizacion';
import { IVivienda } from '../../../Vivienda/Models/IVivienda';
import { IUsuario } from '../../../Usuario/Models/IUsuario';
import { ICuentaMunicipal } from '../../../Usuario/Models/ICuentaMunicipal';
import { IUsuarioCM } from '../../../Usuario/Models/UsuarioCM';
import { ViviendaService } from '../../../Vivienda/Services/vivienda.service';
import { HttpErrorResponse } from '@angular/common/http';
import { IViviendaUsuario } from '../../../Vivienda/Models/IViviendaUsuario';
import { UsuarioService } from '../../../Usuario/Services/usuario-service.service';
import { TipoUsuario } from '../../../Usuario/Enums/TipoUsuario';
import { IViviendaFamProp } from '../../../Vivienda/Models/IViviendaFamProp';
import { Observable, catchError, forkJoin, map, of, switchMap } from 'rxjs';
import { RegularizacionService } from '../../../../Core/Services/regularizacion.service';
import { IRegularizacionesCard } from '../../../../Core/Models/IRegularizacionesCard';
import { DatePipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { NgxLoadingButtonsModule } from 'ngx-loading-buttons';


pdfMake.vfs = pdfFonts.pdfMake.vfs;

@Component({
  selector: 'app-pdf-ficha',
  templateUrl: './pdf-ficha.component.html',
  styleUrls: ['./pdf-ficha.component.css'],
  standalone: true,
  imports: [MatButtonModule, NgxLoadingButtonsModule],
  providers: [DatePipe]

})

export class PdfFichaComponent {

  contentPdf: any[] = [];
  currentDate: string;
  @Input() regularizacion: IRegularizacionesCard;
  @Input() vivienda: IVivienda;
  @Input() propietarios: IUsuarioCM[];
  @Input() familiares: IUsuarioCM[];

  viviendas: IVivienda;
  viviendaFamiliar: IViviendaFamProp[];
  CMFamiliar: ICuentaMunicipal;
  viviendaPropietario: IViviendaFamProp[];
  CMPropietario: ICuentaMunicipal;
  regularizacionLocal: IRegularizacion;

  loading = false;
  usrFamiliar: TipoUsuario = TipoUsuario.Familiar;
  usrPropietario: TipoUsuario = TipoUsuario.Propietario;
  userPropietario: IUsuarioCM[] = [];
  //const styles = this.estilos();
  constructor(private viviendaService: ViviendaService, private usuarioService: UsuarioService,
    private regularizacionService: RegularizacionService, private datePipe: DatePipe) {
  }

  createPdf() {
    this.createTablesPDF(this.regularizacion).then(() => {
      const pdfDefinition: any = {
        content: [...this.contentPdf],
        styles: this.estilos()
      };
      const pdf = pdfMake.createPdf(pdfDefinition);
      pdf.open();
    }).catch(error => {
    });
    //this.createTablesPDF(this.regularizacion)
    //const pdfDefinition: any = {
    //  content: [...this.contentPdf]
    //}
    //console.log('pdfDefinition', pdfDefinition);
    //const pdf = pdfMake.createPdf(pdfDefinition);
    //pdf.open();
  }

  createCabezera(reg: IRegularizacion) {
    const today = new Date();
    const month = today.getMonth();
    const year = today.getFullYear();
    const day = today.getDay();
    const fechaActual = this.datePipe.transform(today, 'dd/MM/yyyy');
    const fechaInicio = this.datePipe.transform(reg.fechaRegistro, 'dd/MM/yyyy');
    this.contentPdf.push(
      { text: 'Arq. Jhonny Cedeno Mendoza', style: 'cabezera' },
      { text: `${fechaActual}`, style: 'cabezera' },
      { text: `FICHA DE DATOS - REGULARIZACION #${reg.numRegularizacion}`, style: ['headerTextFicha'] },
      { text: `FECHA DE INICIO REG.: ${fechaInicio}`, style: ['fechaI', 'anotherStyle'] },
    );
  }


  createTableRegularizacion(reg: IRegularizacion) {
    this.contentPdf.push(
      {
        table: {
          headerRows: 1,
          widths: ['*'],
          body: [
            [{ text: 'Datos de la Regularización', bold: true, style: 'header' }]
          ]
        },
      },
      {
        table: {
          headerRows: 1,
          widths: [70, '*', 70, '*'],

          body: [

            [{ text: 'Expediente', bold: true }, `${reg.numeroExpediente}`,
              { text: 'Valor', bold: true }, `${reg.valorRegularizacion}`],
            [{ text: 'Correo', bold: true }, `${reg.correo}`,
              { text: 'Contrasena', bold: true }, `${reg.contrasena}`]
          ]
        },
      },
    );
  }

  createTableVivienda(viv: IVivienda) {
    this.contentPdf.push(
      {
        table: {
          headerRows: 1,
          widths: ['*'],
          body: [
            [{ text: 'Informacion de la Vivienda', bold: true, style: 'header' }]
          ]
        },
      },
      {
        table: {
          headerRows: 1,
          widths: [150, '*'],
          body: [

            [{ text: 'Ubicación', bold: true }, `${this.viviendas.direccion}`],
            [{ text: 'Codigo Catastral', bold: true }, `${this.viviendas.codigoCatastral}`],
            [{ text: 'Teléfono', bold: true }, `${this.viviendas.telefono}`],
            [{ text: 'Foto', bold: true }, { image: `data:image/png;base64,${this.viviendas.imagen}`, width: 150, height: 150, alignment: 'center' }]
          ]
        },
      },
    );
  }

  createTablePropietarios() {
    
    let body: any[] = [];
    let prop: string = '';
    const vp = this.viviendaPropietario;
    
    if (vp.length > 1)
      prop = 'Informacion de los Propietarios';
    else
      prop = 'Informacion del propietario';
    
    vp.forEach((usr) => {
      this.obtenerCuentaMunicipalPropietario(usr.idUsuario);

      if (usr.propietarioPrincipal) {
        body.push([
          { text: 'Nombres', bold: true, fillColor: 'lightblue' }, { text: `${usr.nombres}`, fillColor: 'lightblue' },
          { text: 'Apellidos', bold: true, fillColor: 'lightblue' }, { text: `${usr.apellidos}`, fillColor: 'lightblue' }]
        );
        body.push([
          { text: 'Cedula', bold: true, fillColor: 'lightblue' }, { text: `${usr.dni}`, fillColor: 'lightblue' },
          { text: 'Celular', bold: true, fillColor: 'lightblue' }, { text: `${usr.telefonoCelular}`, fillColor: 'lightblue' }]
        );
        if (usr.cuentaMunicipal != null && usr.cuentaMunicipal != undefined) {
          body.push([
            { text: 'Cuenta Municipal', bold: true, fillColor: 'lightblue' }, { text: `${usr.cuentaMunicipal.cuentaMunicipal}`, fillColor: 'lightblue' },
            { text: 'Clave Municipal', bold: true, fillColor: 'lightblue' }, { text: `${usr.cuentaMunicipal.contrasenaMunicipal}`, fillColor: 'lightblue' }]
          );
        }
      } else {
        body.push([{ text: 'Nombres', bold: true }, `${usr.nombres}`, { text: 'Apellidos', bold: true }, `${usr.apellidos}`]);
        body.push([{ text: 'Cedula', bold: true }, `${usr.dni}`, { text: 'Celular', bold: true }, `${usr.telefonoCelular}`]);
        if (usr.cuentaMunicipal != null && usr.cuentaMunicipal != undefined) {
          body.push([{ text: 'Cuenta Municipal', bold: true }, `${usr.cuentaMunicipal.cuentaMunicipal}`, { text: 'Clave Municipal', bold: true }, `${usr.cuentaMunicipal.contrasenaMunicipal}`]);
        }
      }
      //body.push([{ text: 'Nombres', bold: true }, `${usr.nombres}`, { text: 'Apellidos', bold: true }, `${usr.apellidos}`]);
      //body.push([{ text: 'Cedula', bold: true }, `${usr.dni}`, { text: 'Celular', bold: true }, `${usr.telefonoCelular}`]);
      //if (usr.cuentaMunicipal != null && usr.cuentaMunicipal != undefined) {
      //  if (usr.propietarioPrincipal) {
      //    body.push([{ text: 'Cuenta Municipal', bold: true, style: 'header' }, `${usr.cuentaMunicipal.cuentaMunicipal}`, { text: 'Clave Municipal', bold: true, style: 'header' }, `${usr.cuentaMunicipal.contrasenaMunicipal}`]);
      //  } else {
      //    body.push([{ text: 'Cuenta Municipal', bold: true }, `${usr.cuentaMunicipal.cuentaMunicipal}`, { text: 'Clave Municipal', bold: true }, `${usr.cuentaMunicipal.contrasenaMunicipal}`]);
      //  }
        
      //}
    });
    if (vp.length > 0) {
      this.contentPdf.push(
        {
          table: {
            headerRows: 1,
            widths: ['*'],
            body: [
              [{ text: `${prop}`, bold: true, style: 'header' }]
            ]
          },
        },
        {
          table: {
            // headers are automatically repeated if the table spans over multiple pages
            // you can declare how many rows should be treated as headers
            headerRows: 1,
            widths: [100, 150, 100, 128.2],

            body: body,
          }
        },

      );
    }
    
  }
  createTableFamiliares() {
    let body: any[] = [];
    let prop: string = '';
    const vp = this.viviendaFamiliar;
    if (vp.length > 1)
      prop = 'Datos de los Familiares';
    else
      prop = 'Dato del familiar';
    vp.forEach((usr) => {
      this.obtenerCuentaMunicipalPropietario(usr.idUsuario);
      
      body.push([{ text: 'Nombres', bold: true }, `${usr.nombres}`, { text: 'Apellidos', bold: true }, `${usr.apellidos}`]);
      body.push([{ text: 'Cedula', bold: true }, `${usr.dni}`, { text: 'Celular', bold: true }, `${usr.telefonoCelular}`]);
      if (usr.cuentaMunicipal != null && usr.cuentaMunicipal != undefined) {
        body.push([{ text: 'Cuenta Municipal', bold: true }, `${usr.cuentaMunicipal.cuentaMunicipal}`, { text: 'Clave Municipal', bold: true }, `${usr.cuentaMunicipal.contrasenaMunicipal}`]);
      }
    });
    if (vp.length > 0) {
      this.contentPdf.push(
        {
          table: {
            headerRows: 1,
            widths: ['*'],
            body: [
              [{
                text: `${prop}`, bold: true, style: 'header'
              }]
            ]
          },
        },
        {
          table: {
            // headers are automatically repeated if the table spans over multiple pages
            // you can declare how many rows should be treated as headers
            headerRows: 1,
            widths: [100, 150, 100, 128.2],

            body: body,
          }
        },

      );
    }
    
  }

  createTablesPDF(reg: IRegularizacionesCard): Promise<void> {
    return new Promise((resolve, reject) => {
      if (!reg || !reg.idRegularizacion || !reg.idVivienda) {
        reject(new Error('Invalid regularization data'));
        return;
      }
      forkJoin({
        regularizacion: this.obtenerRegularizacion(reg.idRegularizacion),
        vivienda: this.obtenerVivienda(reg.idVivienda),
        viviendaFamiliar: this.obtenerVivivendaFamiliar(reg.idVivienda),
        viviendaPropietario: this.obtenerViviendaPropietario(reg.idVivienda)
      }).subscribe({
        next: ({ vivienda, regularizacion }) => {
          this.createCabezera(regularizacion);
          this.createTableRegularizacion(regularizacion);
          this.createTableVivienda(vivienda);
          this.createTablePropietarios();
          if (this.viviendaFamiliar != null && this.viviendaFamiliar != undefined)
            this.createTableFamiliares();
          resolve(); // Resuelve la promesa después de completar las tareas
        },
        error: (error: HttpErrorResponse) => {
          reject(error); // Rechaza la promesa en caso de error
        }
      });
    });
    //this.obtenerVivienda(reg.idVivienda);
    //this.obtenerVivivendaFamiliar(reg.idVivienda);
    //this.obtenerViviendaPropietario(reg.idVivienda);

    //this.createCabezera(reg);
    //this.createTableRegularizacion(reg);
    //this.createTableVivienda(this.vivienda);
    //this.createTablePropietarios();
    //this.createTableFamiliares();
  }

  obtenerRegularizacion(idRegularizacion: string): Observable<IRegularizacion> {
    return this.regularizacionService.getByIdRegularizacion(idRegularizacion).pipe(
      map((response: IRegularizacion) => {
        this.regularizacionLocal = response;
        return response;
      })
    );
  }
  obtenerVivienda(idVivienda: string): Observable<IVivienda> {
    return this.viviendaService.getByIdVivienda(idVivienda).pipe(
      map((response: IVivienda) => {
        this.viviendas = response;
        return response;
      })
    );
  }

  obtenerVivivendaFamiliar(idVivienda: string): Observable<IViviendaFamProp[]> {
    return this.viviendaService.getByIdViviendaFamiliar(idVivienda).pipe(
      switchMap((response: any[]) => {
        if (response != null && response != undefined && response.length > 0) {
          let observables: Observable<IViviendaFamProp>[] = response.map((usr) =>
            this.usuarioService.getByIdCuentaMunicipalUsuario(usr.idUsuario, this.usrFamiliar).pipe(
              map((cuentaMunicipal: ICuentaMunicipal) => {
                usr.cuentaMunicipal = cuentaMunicipal;
                return usr;
              }),
              catchError(error => {
                // Handle the error by returning the user object without cuentaMunicipal
                return of(usr);
              })
            )
          );
          return forkJoin(observables).pipe(
            map((usuariosConCuentaMunicipal) => {
              this.viviendaFamiliar = usuariosConCuentaMunicipal;
              return usuariosConCuentaMunicipal;
            })
          );
        } else {
          // Return an observable of an empty array if response is null or undefined
          return of([]);
        }
      }),
      catchError(error => {
        // Return an observable of an empty array in case of error
        return of([]);
      })
    );
  }
  
  obtenerCuentaMunicipalFamiliar(idUsuario: string) {
    this.usuarioService.getByIdCuentaMunicipalUsuario(idUsuario, this.usrFamiliar).subscribe({
      next: (response: ICuentaMunicipal) => {
        this.CMFamiliar = response;
      },
      error: (error: HttpErrorResponse) => {
      }
    });
  }

  obtenerViviendaPropietario(idVivienda: string): Observable<IViviendaFamProp[]> {
    return this.viviendaService.getByIdViviendaPropietario(idVivienda).pipe(
      switchMap((response: any[]) => {
        if (response != null && response != undefined && response.length > 0) {
          let observables: Observable<IViviendaFamProp>[] = response.map((usr) =>
            this.usuarioService.getByIdCuentaMunicipalUsuario(usr.idUsuario, this.usrPropietario).pipe(
              map((cuentaMunicipal: ICuentaMunicipal) => {
                usr.cuentaMunicipal = cuentaMunicipal;
                return usr;
              }),
              catchError(error => {
                // Handle the error by returning the user object without cuentaMunicipal
                return of(usr);
              })
            )
          );
          return forkJoin(observables).pipe(
            map((usuariosConCuentaMunicipal) => {
              this.viviendaPropietario = usuariosConCuentaMunicipal;
              return usuariosConCuentaMunicipal;
            })
          );
        } else {
          // Return an observable of an empty array if response is null or undefined
          return of([]);
        }
      }),
      catchError(error => {
        // Return an observable of an empty array in case of error
        return of([]);
      })
    );
  }
  obtenerCuentaMunicipalPropietario(idUsuario: string) {
    const propietario = TipoUsuario.Propietario;
    this.usuarioService.getByIdCuentaMunicipalUsuario(idUsuario, this.usrPropietario).subscribe({
      next: (response: ICuentaMunicipal) => {
        this.CMPropietario = response;
      },
      error: (error: HttpErrorResponse) => {
      }
    });
  }
  estilos() {
    return {
      header: {
        fontSize: 16,
        bold: true,
        alignment: 'center',
        Arial: true
      },
      cabezera: {
        Arial: true,
        fontSize: 9,
        alignment: 'right'
      },
      headerTextFicha: {
        Arial: true,
        fontSize: 23,
        bold: true,
        alignment: 'right'
      },
      fechaI: {
        Arial: true,
        fontSize: 11,
      },
      foto: {
        heigth: 500
      },
      tableExample: {
        margin: [0, 5, 0, 15]
      },
      anotherStyle: {
        italics: true,
        alignment: 'right'
      },
      principal: {
        background: 'lightblue'
      }
    }
  }

  obtenerContrato() {
    this.loading = true;
    this.regularizacionService.getPdfContrato(this.regularizacion.idRegularizacion).subscribe({
      next: (response: string) => {
        let xd = response;
        const pdfBlob = this.base64ToBlob(response, 'application/pdf')
        const pdfUrl = URL.createObjectURL(pdfBlob);
        // Abrir el PDF en una nueva ventana
        window.open(pdfUrl);
      },
      error: (error: HttpErrorResponse) => {
      },
      complete: () => {
        this.loading = false;
      }
    });
  }
  base64ToBlob(base64: string, contentType: string) {
    const byteCharacters = atob(base64);
    const byteNumbers = new Array(byteCharacters.length);

    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }

    const byteArray = new Uint8Array(byteNumbers);
    return new Blob([byteArray], { type: contentType });
  }
}
