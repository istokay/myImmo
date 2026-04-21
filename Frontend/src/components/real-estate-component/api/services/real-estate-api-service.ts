import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { RealEstate } from '../dtos/realEstate';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RealEstateApiService {
  private http = inject(HttpClient);

  getAllRealEstates$: Observable<RealEstate[]> = this.http.get<RealEstate[]>('/api/realestate');
}
