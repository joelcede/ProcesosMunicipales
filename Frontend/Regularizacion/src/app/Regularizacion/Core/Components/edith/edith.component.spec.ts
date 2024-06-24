import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EdithComponent } from './edith.component';

describe('EdithComponent', () => {
  let component: EdithComponent;
  let fixture: ComponentFixture<EdithComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EdithComponent]
    });
    fixture = TestBed.createComponent(EdithComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
