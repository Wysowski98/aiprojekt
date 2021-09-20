import { Flower } from './../../../core/api.service';
import {Component, Inject} from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

export interface DialogData {
  Day: Days;
  FlowerId: number;
}

interface Days {
  value: number;
  viewValue: string;
}

@Component({
  selector: 'app-date-dialog',
  templateUrl: 'dialog.html',
})
export class DateDialogComponent {

  selectedValue: string;
  daysOptions: Days[] = [
    { value: 1, viewValue: 'Poniedziałek'},
    { value: 2, viewValue: 'Wtorek'},
    { value: 3, viewValue: 'Środa'},
    { value: 4, viewValue: 'Czwartek'},
    { value: 5, viewValue: 'Piątek'},
    { value: 6, viewValue: 'Sobota'},
    { value: 7, viewValue: 'Niedziela'},
  ];
  constructor(
    public dialogRef: MatDialogRef<DateDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

}
