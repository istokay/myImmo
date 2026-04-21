import { Component, inject } from '@angular/core';
import { RealEstateService } from './services/real-estate-service';
import { RealEstateApiService } from './api/services/real-estate-api-service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-real-estate-component',
  imports: [AsyncPipe],
  templateUrl: './real-estate-component.html',
  styleUrl: './real-estate-component.css',
  providers: [RealEstateService, RealEstateApiService],
})
export class RealEstateComponent {
  api = inject(RealEstateApiService);
}
