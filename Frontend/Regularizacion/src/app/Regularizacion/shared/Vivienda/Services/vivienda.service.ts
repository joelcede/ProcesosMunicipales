import { Injectable } from '@angular/core';
import { getEnvironment } from '../../../../../environments/GetEnvironment';
import { environment } from '../enviromentVivienda';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IVivienda } from '../Models/IVivienda';
import { IViviendaUsuario } from '../Models/IViviendaUsuario';
import { IViviendaFamProp } from '../Models/IViviendaFamProp';

@Injectable({
  providedIn: 'root'
})
export class ViviendaService {
  private urlCliente: string = `${getEnvironment(environment)}Vivienda/`;
  private urlFamiliar: string = `${getEnvironment(environment)}ViviendaFamiliar/`;
  private urlPropietario: string = `${getEnvironment(environment)}ViviendaPropietario/`;
  private urlViviendaImagen: string = `${getEnvironment(environment)}ViviendaImagen/`;
  private urlViviendaTI: string = `${getEnvironment(environment)}ViviendaIT/`;
  constructor(private http: HttpClient) { }

  /*  ############################## Vivienda ##############################*/
  getAllViviendas(): Observable<IVivienda> {
    const url = `${this.urlCliente}GetAllViviendas`;
    return this.http.get<IVivienda>(url);
  }
  getByIdVivienda(id: string): Observable<IVivienda> {
    const url = `${this.urlCliente}GetVivienda/${id}`;
    return this.http.get<IVivienda>(url);
  }
  addVivienda(vivienda: IVivienda): Observable<IVivienda> {
    const url = `${this.urlCliente}AddVivienda`;
    const headers = { 'Content-Type': 'application/json' };
    return this.http.post<IVivienda>(url, vivienda, { headers });
  }
  deleteVivienda(id: string): Observable<any> {
    const url = `${this.urlCliente}DeleteVivienda/${id}`;
    return this.http.delete(url);
  }
  updateVivienda(id: string, vivienda: IVivienda): Observable<IVivienda> {
    const url = `${this.urlCliente}UpdateVivienda/${id}`;
    const headers = { 'Content-Type': 'application/json' };
    return this.http.put<IVivienda>(url, vivienda, { headers });
  }
  /*  ############################## Fin Vivienda ##############################*/
  /*  ############################## Vivienda Familiar ##############################*/

  addViviendaFamiliar(vivienda: IViviendaUsuario): Observable<IViviendaUsuario> {
    const url = `${this.urlFamiliar}AddViviendaFamiliar`;
    const headers = { 'Content-Type': 'application/json' };
    return this.http.post<IViviendaUsuario>(url, vivienda, { headers });
  }
  getByIdViviendaFamiliar(id: string): Observable<IViviendaFamProp[]> {
    const url = `${this.urlFamiliar}GetViviendaFamiliar/${id}`;
    return this.http.get<IViviendaFamProp[]>(url);
  }
  /*  ############################## Fin Vivienda Familiar ##############################*/

  /*  ############################## Inicio Vivienda Imagen ##############################*/
  /*  ############################## Fin Vivienda Familiar ##############################*/


  /*  ############################## INICIO Vivienda Propietario ##############################*/
  addViviendaPropietario(vivienda: IViviendaUsuario): Observable<IViviendaUsuario> {
    const url = `${this.urlPropietario}AddViviendaPropietario`;
    const headers = { 'Content-Type': 'application/json' };
    return this.http.post<IViviendaUsuario>(url, vivienda, { headers });
  }
  getByIdViviendaPropietario(id: string): Observable<IViviendaFamProp[]> {
    const url = `${this.urlPropietario}GetViviendaPropietario/${id}`;
    return this.http.get<IViviendaFamProp[]>(url);
  }
  /*  ############################## FIN Vivienda Propietario ##############################*/

  /*  ############################## Inicio Vivienda TABLAS INTERMEDIAS ##############################*/
  getByIdViviendaTI(id: string): Observable<IVivienda> {
    const url = `${this.urlViviendaTI}GetViviendaUsuario/${id}`;
    return this.http.get<IVivienda>(url);
  }
  /*  ############################## FIN Vivienda TABLAS INTERMEDIA ##############################*/

}
