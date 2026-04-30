import { Component, inject, model, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
  MatDialogContainer,
} from '@angular/material/dialog';
import { RealEstate } from '../api/dtos/realEstate';

@Component({
  selector: 'app-dialog-component',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
  ],
  templateUrl: './dialog-component.html',
  styleUrl: './dialog-component.css',
})
export class RealEstateDialogComponent {
  readonly dialogRef = inject(MatDialogRef<RealEstateDialogComponent>);
  data = inject<RealEstate>(MAT_DIALOG_DATA);

  name = model(this.data?.name);
  private id = this.data?.id;

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSaveClick() {
    if (this.id === undefined) {
      //post
    } else {
      //put
    }
    return {
      name: this.name(),
      incomes: [],
    } satisfies RealEstate;
  }
}
