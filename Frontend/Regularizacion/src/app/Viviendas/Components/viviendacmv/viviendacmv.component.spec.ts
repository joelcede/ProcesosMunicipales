import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViviendacmvComponent } from './viviendacmv.component';

describe('ViviendacmvComponent', () => {
  let component: ViviendacmvComponent;
  let fixture: ComponentFixture<ViviendacmvComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViviendacmvComponent]
    });
    fixture = TestBed.createComponent(ViviendacmvComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
