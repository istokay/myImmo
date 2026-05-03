import { Component, computed, inject, model, ModelSignal } from '@angular/core';
import { RealEstateApiService } from './api/services/real-estate-api-service';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { RealEstateDialogComponent } from './dialog-component/dialog-component';
import { RealEstate } from './api/dtos/realEstate';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-real-estate-component',
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatTableModule],
  templateUrl: './real-estate-component.html',
  styleUrl: './real-estate-component.css',
  providers: [],
})
export class RealEstateComponent {
  readonly realEstate: ModelSignal<RealEstate | undefined> = model();
  readonly dialog = inject(MatDialog);
  readonly realEstateApiService = inject(RealEstateApiService);

  displayedColumns: string[] = ['name'];
  dataSource = computed(() => this.realEstateApiService.realEstates());
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
