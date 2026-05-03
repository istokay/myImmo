import { Component, inject, model, ModelSignal } from '@angular/core';
import { RealEstateService } from './services/real-estate-service';
import { RealEstateApiService } from './api/services/real-estate-api-service';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { RealEstateDialogComponent } from './dialog-component/dialog-component';
import { RealEstate } from './api/dtos/realEstate';

@Component({
  selector: 'app-real-estate-component',
  imports: [MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './real-estate-component.html',
  styleUrl: './real-estate-component.css',
  providers: [],
})
export class RealEstateComponent {
  readonly realEstate: ModelSignal<RealEstate | undefined> = model();
  readonly dialog = inject(MatDialog);
  readonly realEstateApiService = inject(RealEstateApiService);
  openDialog(): void {
    const dialogRef = this.dialog.open(RealEstateDialogComponent, {
      data: this.realEstateApiService,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result !== undefined) {
        this.realEstate.set(result);
      }
    });
  }
  api = inject(RealEstateApiService);
}
