import { Injectable } from '@angular/core';
import { environment } from '../enviromentRegularizacion';
import { getEnvironment } from '../../../../environments/GetEnvironment';
import { HttpClient } from '@angular/common/http';
import { IRegularizacion } from '../Models/IRegularizacion';
import { Observable } from 'rxjs';
import { IRegularizacionesCard } from '../Models/IRegularizacionesCard';
import { ISecReg } from '../Models/ISecReg';

@Injectable({
  providedIn: 'root'
})
export class RegularizacionService {
  private urlRegularizacion: string = `${getEnvironment(environment)}Regularizacion/`;
  private urlGraficos: string = `${getEnvironment(environment)}Graficos/`
    
  constructor(private http: HttpClient) { }

  /*  ############################## Regularizacion ##############################*/

  getAllRegularizaciones(): Observable<IRegularizacionesCard[]> {
    const url = `${this.urlRegularizacion}GetAllRegularizaciones`;
    return this.http.get<IRegularizacionesCard[]>(url);
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
  getSecuenciaRegularizacion(): Observable<ISecReg> {
    const url = `${this.urlRegularizacion}GetSecuenciaReg`;
    return this.http.get<ISecReg>(url);
  }
  getPdfContrato(id: string): Observable<string> {
    const url = `${this.urlRegularizacion}GetContrato/${id}`;
    return this.http.get<string>(url);
  }
  getGraficoMesesReg(): Observable<any> {
    const url = `${this.urlGraficos}GetGraficaCantMes`;
    return this.http.get(url);
      
  }
  GetGananciaRegMes(): Observable<any> {
    const url = `${this.urlGraficos}GetGananciaRegMes`;
    return this.http.get(url);

  }
  
  /*  ############################## FIN Regularizacion ##############################*/
}
