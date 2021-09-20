import { waterAmount, waterAmountTranslate } from './../../../environments/waterAmount';
import { FormControl, Validators } from '@angular/forms';
import { Flower, MyFlowersDto, WaterAmountEnum } from './../../core/api.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from './../../core/auth.service';
import { Component, OnInit } from '@angular/core';
import { ApiClient } from '../../core/api.service';
import { CreateDialogComponent } from './Dialog/dialog';
import { MatDialog } from '@angular/material/dialog';

export interface DialogData {
  name: FormControl;
  species: FormControl;
  nationality: FormControl;
  imageUrl: FormControl;
  irrigationPerWeek: FormControl;
  waterAmount: FormControl;
}

@Component({
  selector: 'app-flowers-list',
  templateUrl: './flowers-list.component.html',
  styleUrls: ['./flowers-list.component.scss']
})
export class FlowersListComponent implements OnInit {

  myflowers: Flower[];
  myUid: string;
  durationInSeconds = 5;
  waterAmountTrans = waterAmountTranslate;
  constructor(public auth: AuthService, private api: ApiClient,private snackBar: MatSnackBar, public dialog: MatDialog) {

  }

  ngOnInit() {
    this.auth.user$.subscribe(x => this.myUid = x.uid);
    this.api.flower_GetAll().subscribe((x) => {
      this.myflowers = x;
    });
  }

  addFlowerToAccount(id: number){
    this.api.myFlowers_AddNewFlower({flowerId: id, userId: this.myUid}).subscribe( (succ) => {
      if(succ.status === 200)
    {
      this.openSnackBar();
    }
  });
  }

  openSnackBar() {
    this.snackBar.open('Nowy kwiat został dodany do konta', null, {
      duration: this.durationInSeconds * 1000,
      panelClass: ['success-snack']
    });
  }

  createNewType()
  {
    const dialogRef = this.dialog.open(CreateDialogComponent, {
      width: '450px',
      data: {name: new FormControl('', [Validators.required]), species: new FormControl('', [Validators.required]),
      nationality: new FormControl('', [Validators.required]), imageUrl: new FormControl(''),
      irrigationPerWeek: new FormControl('', [Validators.required,Validators.min(0), Validators.max(10)]),
      waterAmount: new FormControl('', [Validators.required, Validators.min(0), Validators.max(1000)])}
    });

    dialogRef.afterClosed().subscribe(result => {
      let dialogData = result as DialogData;
      let amountEnum;
      if(dialogData.waterAmount.value < waterAmount[0])
      {
        amountEnum = 0;
      }
      else if (dialogData.waterAmount.value > waterAmount[0] && dialogData.waterAmount.value < waterAmount[1])
      {
        amountEnum = 1;
      }
      else{
        amountEnum = 3;
      }

      this.api.flower_AddNewFlowerType({name: dialogData.name.value, nationality: dialogData.nationality.value,
      species: dialogData.species.value,
      imageUrl: dialogData.imageUrl.value !== "" ? dialogData.imageUrl.value : 'assets/placeholder.png',
      irrigationPerWeek: 0, waterAmount: amountEnum}).subscribe( result => {
        this.api.flower_GetAll().subscribe((x) => {
          this.myflowers = x;
        });
      });
    });
  }

}
