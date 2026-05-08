import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RealEstateDetails } from './real-estate-details';

describe('RealEstateDetails', () => {
  let component: RealEstateDetails;
  let fixture: ComponentFixture<RealEstateDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RealEstateDetails],
    }).compileComponents();

    fixture = TestBed.createComponent(RealEstateDetails);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
