import { Injectable } from '@angular/core';
import { environment } from './environmentUsuario';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { getEnvironment } from '../../../../environments/GetEnvironment';
import { Observable } from 'rxjs';
import { Usuario } from '../models/usuario.interface';
import { CuentaMunicipal } from '../models/cuenta-municipal.interface';

@Injectable({
  providedIn: 'root'
})

export class UsuarioService {

  private urlCliente: string = `${getEnvironment(environment)}Clientes/`;
  private urlFamiliar: string = `${getEnvironment(environment)}Familiares/`;
  private urlPropietario: string = `${getEnvironment(environment)}Propietarios/`;
  constructor(private http: HttpClient) { }

  /*  ############################## Clientes ##############################*/
  getAllClientes(): Observable<Usuario[]> {
    const url = `${this.urlCliente}GetClientes`;
    const response = this.http.get<Usuario[]>(url);
    return response;
  }

  addCliente(cliente: Usuario): Observable<Usuario> {
    const url = `${this.urlCliente}AddCliente`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<Usuario>(url, cliente, { headers });
  }

  getByIdCliente(id: string): Observable<Usuario> {
    const url = `${this.urlCliente}GetCliente/${id}`;
    return this.http.get<Usuario>(url);
  }

  deleteCliente(id: string): Observable<any> {
    const url = `${this.urlCliente}DeleteCliente/${id}`;
    return this.http.delete(url);
  }

  updateCliente(id: string, cliente: Usuario): Observable<Usuario> {
    const url = `${this.urlCliente}UpdateCliente/${id}`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<Usuario>(url, cliente, { headers });
  }
  /*  ############################## Fin Clientes ##############################*/
  /*  ############################## INICIO Familiares ##############################*/

  getAllFamiliares(): Observable<Usuario[]> {
    const url = `${this.urlFamiliar}GetFamiliares`;
    return this.http.get<Usuario[]>(url);
  }
  addFamiliar(familiar: Usuario): Observable<Usuario> {
    const url = `${this.urlFamiliar}AddFamiliar`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<Usuario>(url, familiar, { headers });
  }
  getByIdFamiliar(id: string): Observable<Usuario> {
    const url = `${this.urlFamiliar}GetFamiliar/${id}`;
    return this.http.get<Usuario>(url);
  }
  deleteFamiliar(id: string): Observable<any> {
    const url = `${this.urlFamiliar}DeleteFamiliar/${id}`;
    return this.http.delete(url);
  }
  updateFamiliar(id: string, familiar: Usuario): Observable<Usuario> {
    const url = `${this.urlFamiliar}UpdateFamiliar/${id}`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<Usuario>(url, familiar, { headers });
  }
  /*  ############################## INICIO CUENTA MUNICIPAL FAMILIARES ##############################*/
  addFamiliarCM(cm: CuentaMunicipal): Observable<CuentaMunicipal> {
    const url = `${this.urlFamiliar}AddCMFamiliar`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<CuentaMunicipal>(url, cm, { headers });
  }
  getbyIdFamiliarCM(id: string): Observable<CuentaMunicipal> {
    const url = `${this.urlFamiliar}GetCMFamiliar/${id}`;
    return this.http.get<CuentaMunicipal>(url);
  }
  updateFamiliarCM(id: string, cm: CuentaMunicipal): Observable<CuentaMunicipal> {
    const url = `${this.urlFamiliar}UpdateCMFamiliar/${id}`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<CuentaMunicipal>(url, cm, { headers });
  }
  deleteFamiliarCM(id: string): Observable<any> {
    const url = `${this.urlFamiliar}DeleteCMFamiliar/${id}`;
    return this.http.delete(url);
  }
  /*  ############################## FIN CUENTA MUNICIPAL FAMILIARES ##############################*/
  /*  ############################## Fin Familiares ##############################*/


  /*  ############################## INICIO Propietarios ##############################*/
  getAllPropietarios(): Observable<Usuario[]> {
    const url = `${this.urlPropietario}GetPropietarios`;
    return this.http.get<Usuario[]>(url);
  }
  getByIdPropietario(id: string): Observable<Usuario> {
    const url = `${this.urlPropietario}GetPropietario/${id}`;
    return this.http.get<Usuario>(url);
  }
  addPropietario(propietario: Usuario): Observable<Usuario> {
    const url = `${this.urlPropietario}AddPropietario`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<Usuario>(url, propietario, { headers });
  }
  updatePropietario(id: string, propietario: Usuario): Observable<Usuario> {
    const url = `${this.urlPropietario}UpdatePropietario/${id}`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<Usuario>(url, propietario, { headers });
  }
  deletePropietario(id: string): Observable<any> {
    const url = `${this.urlPropietario}DeletePropietario/${id}`;
    return this.http.delete(url);
  }
  /*  ############################## INICIO CUENTA MUNICIPAL Propietarios ##############################*/
  addPropietarioCM(cm: CuentaMunicipal): Observable<CuentaMunicipal> {
    const url = `${this.urlPropietario}AddCMPropietario`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<CuentaMunicipal>(url, cm, { headers });
  }
  getByIdPropietarioCM(id: string): Observable<CuentaMunicipal> {
    const url = `${this.urlPropietario}GetCMPropietario/${id}`;
    return this.http.get<CuentaMunicipal>(url);
  }
  updatePropietarioCM(id: string, cm: CuentaMunicipal): Observable<CuentaMunicipal> {
    const url = `${this.urlPropietario}UpdateCMPropietario/${id}`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<CuentaMunicipal>(url, cm, { headers });
  }
  deletePropietarioCM(id: string): Observable<any> {
    const url = `${this.urlPropietario}DeleteCMPropietario/${id}`;
    return this.http.delete(url);
  }
  /*  ############################## FIN CUENTA MUNICIPAL Propietarios ##############################*/
  /*  ############################## Fin Propietarios ##############################*/
}
