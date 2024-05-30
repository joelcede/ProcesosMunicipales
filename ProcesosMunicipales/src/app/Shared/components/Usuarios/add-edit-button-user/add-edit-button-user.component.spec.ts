import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditButtonUserComponent } from './add-edit-button-user.component';

describe('AddEditButtonUserComponent', () => {
  let component: AddEditButtonUserComponent;
  let fixture: ComponentFixture<AddEditButtonUserComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddEditButtonUserComponent]
    });
    fixture = TestBed.createComponent(AddEditButtonUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
