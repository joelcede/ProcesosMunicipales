import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private top10ClientesSubject = new BehaviorSubject<any[]>([]);
  top10Clientes$ = this.top10ClientesSubject.asObservable();

  private currentStepSubject = new BehaviorSubject<number>(0);
  currentStep$ = this.currentStepSubject.asObservable();

  private selectedTabIndexSubject = new BehaviorSubject<number>(0);
  selectedTabIndex$ = this.selectedTabIndexSubject.asObservable();

  private tabChangeSubject = new BehaviorSubject<void>(undefined);
  tabChange$ = this.tabChangeSubject.asObservable();

  updateTop10ClientesList(newList: any[]) {
    this.top10ClientesSubject.next(newList);
  }

  getTop10ClientesList() {
    return this.top10ClientesSubject.getValue();
  }

  updateCurrentStep(step: number) {
    this.currentStepSubject.next(step);
  }

  getCurrentStep() {
    return this.currentStepSubject.getValue();
  }
  //kkc


  updateSelectedTabIndex(index: number) {
    this.selectedTabIndexSubject.next(index);
  }

  getSelectedTabIndex() {
    return this.selectedTabIndexSubject.getValue();
  }
  notifyTabChange() {
    this.tabChangeSubject.next();
  }
}
