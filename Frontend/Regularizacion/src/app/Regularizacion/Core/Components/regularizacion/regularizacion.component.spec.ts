import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegularizacionComponent } from './regularizacion.component';

describe('RegularizacionComponent', () => {
  let component: RegularizacionComponent;
  let fixture: ComponentFixture<RegularizacionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegularizacionComponent]
    });
    fixture = TestBed.createComponent(RegularizacionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
