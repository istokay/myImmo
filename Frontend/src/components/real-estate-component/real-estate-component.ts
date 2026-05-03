import { Component, computed, inject, model, ModelSignal } from '@angular/core';
import { RealEstateApiService } from './api/services/real-estate-api-service';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { RealEstateDialogComponent } from './dialog-component/dialog-component';
import { RealEstate } from './api/dtos/realEstate';
import { MatTableModule } from '@angular/material/table';
import { ConfirmationDialog } from '../confirmation-dialog/confirmation-dialog';

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

  displayedColumns: string[] = ['name', 'edit', 'delete'];
  dataSource = computed(() => this.realEstateApiService.realEstates());
  openDialog(realEstate?: RealEstate): void {
    const dialogRef = this.dialog.open(RealEstateDialogComponent, {
      data: { id: realEstate?.id, name: realEstate?.name },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result !== undefined) {
        this.realEstate.set(result);
      }
    });
  }
  api = inject(RealEstateApiService);

  updateRealEstate(realEstate: RealEstate) {
    this.openDialog(realEstate);
  }

  deleteRealEstate(id: number) {
    const dialogRef = this.dialog.open(ConfirmationDialog);

    dialogRef.afterClosed().subscribe((confirmed) => {
      if (confirmed === true) {
        this.realEstateApiService.deleteRealEstate(id);
      }
    });
  }
}
