import { Injectable } from '@angular/core';
import { getEnvironment } from '../../../../../environments/GetEnvironment';
import { environment } from '../enviromentRegularizacion';
import { HttpClient } from '@angular/common/http';
import { IRegularizacion } from '../models/IRegularizacion';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegularizacionServiceService {
  private urlRegularizacion: string = `${getEnvironment(environment)}Regularizacion/`;
  constructor(private http: HttpClient) { }

  /*  ############################## Regularizacion ##############################*/

  getAllRegularizaciones() {
    const url = `${this.urlRegularizacion}GetAllRegularizaciones`;
    return this.http.get(`${this.urlRegularizacion}GetAllRegularizaciones`);
  }
  addRegularizacion(regularizacion: IRegularizacion): Observable<IRegularizacion> {
    const url = `${this.urlRegularizacion}AddRegularizacion`;
    const headers = { 'Content-Type': 'application/json' };
    return this.http.post<IRegularizacion>(url, regularizacion, { headers });
  }
  getByIdRegularizacion(id: string): Observable<IRegularizacion> {
    const url = `${this.urlRegularizacion}GetRegularizacion/${id}`;
    return this.http.get<IRegularizacion>(url);
  }
  deleteRegularizacion(id: string): Observable<any> {
    const url = `${this.urlRegularizacion}DeleteRegularizacion/${id}`;
    return this.http.delete<any>(url);
  }
  updateRegularizacion(id: string, regularizacion: IRegularizacion): Observable<any> {
    const url = `${this.urlRegularizacion}UpdateRegularizacion/${id}`;
    const headers = { 'Content-Type': 'application/json' };
    return this.http.put<any>(url, regularizacion, { headers });
  }
  /*  ############################## FIN Regularizacion ##############################*/
}
