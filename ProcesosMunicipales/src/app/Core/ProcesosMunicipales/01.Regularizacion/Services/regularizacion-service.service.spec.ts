import { TestBed } from '@angular/core/testing';

import { RegularizacionServiceService } from './regularizacion-service.service';

describe('RegularizacionServiceService', () => {
  let service: RegularizacionServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegularizacionServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
