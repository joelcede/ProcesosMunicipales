import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsuariocmvComponent } from './usuariocmv.component';

describe('UsuariocmvComponent', () => {
  let component: UsuariocmvComponent;
  let fixture: ComponentFixture<UsuariocmvComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UsuariocmvComponent]
    });
    fixture = TestBed.createComponent(UsuariocmvComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
