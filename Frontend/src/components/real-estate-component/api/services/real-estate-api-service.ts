import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal, WritableSignal } from '@angular/core';
import { RealEstate } from '../dtos/realEstate';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RealEstateApiService {
  private http = inject(HttpClient);

  getAllRealEstates$: Observable<RealEstate[]> = this.http.get<RealEstate[]>('/api/realestate');

  realEstates: WritableSignal<RealEstate[]> = signal([]);

  constructor() {
    this.getAllRealEstates$.subscribe((re: RealEstate[]) => this.realEstates.set(re));
  }

  updateRealEstate(realEstateId: number, realEstate: RealEstate) {
    this.http.put<RealEstate>(`/api/realEstate/${realEstateId}`, realEstate).subscribe((re) =>
      this.realEstates.update((realEstates) => {
        return [...realEstates.filter((filterRe) => filterRe.id != re.id), re];
      }),
    );
  }

  createRealEstate(realEstate: RealEstate) {
    this.http.post<RealEstate>(`/api/realEstate`, realEstate).subscribe((re) => {
      this.realEstates.update((realEstates) => [...realEstates, re]);
    });
  }

  deleteRealEstate(realEstateId: number) {
    this.http.delete<RealEstate>(`/api/realEstate/${realEstateId}`).subscribe(() => {
      this.realEstates.update((realEstates) => [
        ...realEstates.filter((re) => re.id !== realEstateId),
      ]);
    });
  }
}
