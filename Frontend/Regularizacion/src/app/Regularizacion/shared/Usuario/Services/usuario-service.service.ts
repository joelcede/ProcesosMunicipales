import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { getEnvironment } from '../../../../../environments/GetEnvironment';
import { environment } from '../environmentUsuario';
import { Observable } from 'rxjs';
import { IUsuario } from '../Models/IUsuario';
import { ICuentaMunicipal } from '../Models/ICuentaMunicipal';


@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  //constructor() { }
  private urlCliente: string = `${getEnvironment(environment)}Clientes/`;
  private urlFamiliar: string = `${getEnvironment(environment)}Familiares/`;
  private urlPropietario: string = `${getEnvironment(environment)}Propietarios/`;
  private urlCuentaMunicipal: string = `${getEnvironment(environment)}CuentaMunicipal/`;
  constructor(private http: HttpClient) { }

  /*  ############################## Clientes ##############################*/
  getAllClientes(): Observable<IUsuario[]> {
    const url = `${this.urlCliente}GetClientes`;
    const response = this.http.get<IUsuario[]>(url);
    return response;
  }

  addCliente(cliente: IUsuario): Observable<IUsuario> {
    const url = `${this.urlCliente}AddCliente`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<IUsuario>(url, cliente, { headers });
  }

  getByIdCliente(id: string): Observable<IUsuario> {
    const url = `${this.urlCliente}GetCliente/${id}`;
    return this.http.get<IUsuario>(url);
  }

  deleteCliente(id: string): Observable<any> {
    const url = `${this.urlCliente}DeleteCliente/${id}`;
    return this.http.delete(url);
  }

  updateCliente(id: string, cliente: IUsuario): Observable<IUsuario> {
    const url = `${this.urlCliente}UpdateCliente/${id}`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<IUsuario>(url, cliente, { headers });
  }
  /*  ############################## Fin Clientes ##############################*/
  /*  ############################## INICIO Familiares ##############################*/

  getAllFamiliares(): Observable<IUsuario[]> {
    const url = `${this.urlFamiliar}GetFamiliares`;
    return this.http.get<IUsuario[]>(url);
  }
  addFamiliar(familiar: IUsuario): Observable<IUsuario> {
    const url = `${this.urlFamiliar}AddFamiliar`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<IUsuario>(url, familiar, { headers });
  }
  getByIdFamiliar(id: string): Observable<IUsuario> {
    const url = `${this.urlFamiliar}GetFamiliar/${id}`;
    return this.http.get<IUsuario>(url);
  }
  deleteFamiliar(id: string): Observable<any> {
    const url = `${this.urlFamiliar}DeleteFamiliar/${id}`;
    return this.http.delete(url);
  }
  updateFamiliar(id: string, familiar: IUsuario): Observable<IUsuario> {
    const url = `${this.urlFamiliar}UpdateFamiliar/${id}`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<IUsuario>(url, familiar, { headers });
  }
  /*  ############################## INICIO CUENTA MUNICIPAL FAMILIARES ##############################*/
  //addFamiliarCM(cm: ICuentaMunicipal): Observable<ICuentaMunicipal> {
  //  const url = `${this.urlFamiliar}AddCMFamiliar`;
  //  const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  //  return this.http.post<ICuentaMunicipal>(url, cm, { headers });
  //}
  //getbyIdFamiliarCM(id: string): Observable<ICuentaMunicipal> {
  //  const url = `${this.urlFamiliar}GetCMFamiliar/${id}`;
  //  return this.http.get<ICuentaMunicipal>(url);
  //}
  //updateFamiliarCM(id: string, cm: ICuentaMunicipal): Observable<ICuentaMunicipal> {
  //  const url = `${this.urlFamiliar}UpdateCMFamiliar/${id}`;
  //  const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  //  return this.http.put<ICuentaMunicipal>(url, cm, { headers });
  //}
  //deleteFamiliarCM(id: string): Observable<any> {
  //  const url = `${this.urlFamiliar}DeleteCMFamiliar/${id}`;
  //  return this.http.delete(url);
  //}
  /*  ############################## FIN CUENTA MUNICIPAL FAMILIARES ##############################*/
  /*  ############################## Fin Familiares ##############################*/


  /*  ############################## INICIO Propietarios ##############################*/
  getAllPropietarios(): Observable<IUsuario[]> {
    const url = `${this.urlPropietario}GetAllPropietarios`;
    return this.http.get<IUsuario[]>(url);
  }
  getByIdPropietario(id: string): Observable<IUsuario> {
    const url = `${this.urlPropietario}GetPropietario/${id}`;
    return this.http.get<IUsuario>(url);
  }
  addPropietario(propietario: IUsuario): Observable<IUsuario> {
    const url = `${this.urlPropietario}AddPropietario`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<IUsuario>(url, propietario, { headers });
  }
  updatePropietario(id: string, propietario: IUsuario): Observable<IUsuario> {
    const url = `${this.urlPropietario}UpdatePropietario/${id}`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<IUsuario>(url, propietario, { headers });
  }
  deletePropietario(id: string): Observable<any> {
    const url = `${this.urlPropietario}DeletePropietario/${id}`;
    return this.http.delete(url);
  }
  /*  ############################## INICIO CUENTA MUNICIPAL Propietarios ##############################*/
  //addPropietarioCM(cm: ICuentaMunicipal): Observable<ICuentaMunicipal> {
  //  const url = `${this.urlPropietario}AddCMPropietario`;
  //  const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  //  return this.http.post<ICuentaMunicipal>(url, cm, { headers });
  //}
  //getByIdPropietarioCM(id: string): Observable<ICuentaMunicipal> {
  //  const url = `${this.urlPropietario}GetCMPropietario/${id}`;
  //  return this.http.get<ICuentaMunicipal>(url);
  //}
  //updatePropietarioCM(id: string, cm: ICuentaMunicipal): Observable<ICuentaMunicipal> {
  //  const url = `${this.urlPropietario}UpdateCMPropietario/${id}`;
  //  const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  //  return this.http.put<ICuentaMunicipal>(url, cm, { headers });
  //}
  //deletePropietarioCM(id: string): Observable<any> {
  //  const url = `${this.urlPropietario}DeleteCMPropietario/${id}`;
  //  return this.http.delete(url);
  //}
  /*  ############################## FIN CUENTA MUNICIPAL Propietarios ##############################*/
  /*  ############################## Fin Propietarios ##############################*/



  /*  ############################## INICIO CUENTA MUNICIPAL USUARIO ##############################*/
  addCuentaMunicipal(cm: ICuentaMunicipal): Observable<ICuentaMunicipal> {
    const url = `${this.urlCuentaMunicipal}AddCuentaMunicipal`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<ICuentaMunicipal>(url, cm, { headers });
  }
  getByIdCuentaMunicipalUsuario(id: string): Observable<ICuentaMunicipal> {
    const url = `${this.urlCuentaMunicipal}GetCuentaMunicipal/${id}`;
    return this.http.get<ICuentaMunicipal>(url);
  }
  updateCuentaMunicipalUsuario(id: string, cm: ICuentaMunicipal): Observable<ICuentaMunicipal> {
    const url = `${this.urlCuentaMunicipal}UpdateCuentaMunicipal/${id}`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<ICuentaMunicipal>(url, cm, { headers });
  }
  deleteCuentaMunicipalUsuario(id: string): Observable<any> {
    const url = `${this.urlCuentaMunicipal}DeleteCuentaMunicipal/${id}`;
    return this.http.delete(url);
  }

  /*  ############################## FIN CUENTA MUNICIPAL USUARIO ##############################*/
}
