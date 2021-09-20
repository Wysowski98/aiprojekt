import { waterAmountTranslate } from './../../../environments/waterAmount';
import { DateDialogComponent } from './Dialog/dialog';
import { Flower, MyFlowers, MyFlowersUpdateCommand } from './../../core/api.service';
import { TrefleService } from './../../core/trefle.service';
import { AuthService, User } from './../../core/auth.service';
import { Component, OnInit } from '@angular/core';
import { ApiClient } from '../../core/api.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

interface DialogData {
  Day: Days;
  FlowerId: number;
}

interface Days {
  value: number;
  viewValue: string;
}

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  selectedDay: DialogData;
  myflowers: MyFlowers[];
  myUid: string;
  durationInSeconds = 5;
  waterAmountTrans = waterAmountTranslate;
  constructor(public auth: AuthService, private api: ApiClient, private snackBar: MatSnackBar, public dialog: MatDialog) {}

  ngOnInit() {
    this.auth.user$.subscribe(x => {
      this.api.myFlowers_GetMyFlowers(x.uid).subscribe( data => {this.myflowers = data;
        this.myflowers.forEach(x => x.irrigationDates.sort((a, b) => a.dayNumber - b.dayNumber)); });
      this.myUid = x.uid; });
  }

  deleteFlower(id: number) {
    this.myflowers = this.myflowers.filter(x => x.id !== id);
    this.api.myFlowers_DeleteFlower(id).subscribe((succ) => {
      if(succ.status === 200)
      {
        this.openSnackBar();
      }
     });
  }

  openSnackBar() {
    this.snackBar.open('Kwiat został usunięty', null, {
      duration: this.durationInSeconds * 1000,
      panelClass: ['error-snack']
    });
  }


  openDialog(flowerId: number): void {
    const dialogRef = this.dialog.open(DateDialogComponent, {
      width: '250px',
      data: {Day: this.selectedDay, FlowerId: flowerId}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.selectedDay = result;
      console.log(this.myflowers.find);
      this.api.myFlowers_UpdateFlower({flowerId: this.selectedDay.FlowerId, dates: { id: undefined, dayNumber: this.selectedDay.Day.value,
        dayName: this.selectedDay.Day.viewValue, isCompleted: false}}).subscribe();
      this.myflowers.find(x => x.id === flowerId).irrigationDates.push({id: undefined ,
          dayNumber: this.selectedDay.Day.value, dayName: this.selectedDay.Day.viewValue, isCompleted: false});
      this.myflowers.find(x => x.id === flowerId).irrigationDates.sort((a, b) => a.dayNumber - b.dayNumber);
    });
  }

}
