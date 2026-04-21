import { TestBed } from '@angular/core/testing';

import { RealEstateApiService } from './real-estate-api-service';

describe('RealEstateApiService', () => {
  let service: RealEstateApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RealEstateApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
