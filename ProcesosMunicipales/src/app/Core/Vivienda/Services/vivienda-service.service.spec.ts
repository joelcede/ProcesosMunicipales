import { TestBed } from '@angular/core/testing';

import { ViviendaServiceService } from './vivienda-service.service';

describe('ViviendaServiceService', () => {
  let service: ViviendaServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ViviendaServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
