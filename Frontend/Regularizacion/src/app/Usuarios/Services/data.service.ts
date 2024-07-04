import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private refreshTableSubject = new Subject<void>();

  // Observable para que otros componentes puedan suscribirse
  refreshTable$ = this.refreshTableSubject.asObservable();

  // Método para emitir el evento de refresco
  refreshTable() {
    this.refreshTableSubject.next();
  }
}
