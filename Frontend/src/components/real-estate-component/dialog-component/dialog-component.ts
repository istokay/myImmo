import { Component, inject, model } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
} from '@angular/material/dialog';
import { RealEstate } from '../api/dtos/realEstate';
import { RealEstateApiService } from '../api/services/real-estate-api-service';

@Component({
  selector: 'app-dialog-component',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatDialogContent,
    MatDialogActions,
  ],
  templateUrl: './dialog-component.html',
  styleUrl: './dialog-component.css',
})
export class RealEstateDialogComponent {
  readonly dialogRef = inject(MatDialogRef<RealEstateDialogComponent>);
  data = inject<RealEstate>(MAT_DIALOG_DATA);
  private readonly realEstateService = inject(RealEstateApiService);

  name = model(this.data?.name);
  private id = this.data?.id;

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSaveClick() {
    if (this.id === undefined) {
      if (this.name() != null) {
        this.realEstateService.createRealEstate({ name: this.name() });
      }
    } else {
      const realEstateName = this.name();
      if (realEstateName != null)
        this.realEstateService.updateRealEstate(this.id, { name: realEstateName });
    }
    this.dialogRef.close();
    return {
      name: this.name(),
    } satisfies RealEstate;
  }
}
