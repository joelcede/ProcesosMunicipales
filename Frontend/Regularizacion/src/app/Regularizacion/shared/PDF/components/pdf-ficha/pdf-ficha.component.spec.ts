import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PdfFichaComponent } from './pdf-ficha.component';

describe('PdfFichaComponent', () => {
  let component: PdfFichaComponent;
  let fixture: ComponentFixture<PdfFichaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PdfFichaComponent]
    });
    fixture = TestBed.createComponent(PdfFichaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
