import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RealEstateDialogComponent } from './dialog-component';

describe('RealEstateDialogComponent', () => {
  let component: RealEstateDialogComponent;
  let fixture: ComponentFixture<RealEstateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RealEstateDialogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RealEstateDialogComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
