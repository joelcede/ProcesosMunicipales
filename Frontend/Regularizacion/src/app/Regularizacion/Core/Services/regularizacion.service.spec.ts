import { TestBed } from '@angular/core/testing';

import { RegularizacionService } from './regularizacion.service';

describe('RegularizacionService', () => {
  let service: RegularizacionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegularizacionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
