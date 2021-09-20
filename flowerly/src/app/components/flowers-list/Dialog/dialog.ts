import { FormControl, Validators } from '@angular/forms';
import { Flower } from './../../../core/api.service';
import {Component, Inject} from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

export interface DialogData {
  name: FormControl;
  species: FormControl;
  nationality: FormControl;
  imageUrl: FormControl;
  irrigationPerWeek: FormControl;
  waterAmount: FormControl;
}

@Component({
  selector: 'app-create-dialog',
  templateUrl: 'create-dialog.html',
})
export class CreateDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<CreateDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

    name = new FormControl('', [Validators.required]);
    species = new FormControl('', [Validators.required]);
    nationality = new FormControl('', [Validators.required]);
    imageUrl = new FormControl('', [Validators.required]);
    irrigationPerWeek = new FormControl('', [Validators.required,Validators.min(0), Validators.max(10)]);
    waterAmount = new FormControl('', [Validators.required, Validators.min(0), Validators.max(1000)]);
    
  onNoClick(): void {
    this.dialogRef.close();
  }
}
