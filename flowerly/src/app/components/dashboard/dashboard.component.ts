import { waterAmountTranslate } from './../../../environments/waterAmount';
import { ApiClient, IrrigationDates, IrrigationHistory, Flower, MyFlowers } from './../../core/api.service';
import { Component, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartPointOptions, ChartType } from 'chart.js';
import { Label } from 'ng2-charts';
import { AuthService } from 'src/app/core/auth.service';
import {DatePipe} from '@angular/common';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  myflowersList: MyFlowers[] = [];
  selectedflower: MyFlowers;
  flowerHistory: IrrigationDates[];
  nextIrrigations: Date[] = [];
  cahrtData: any[];
  myUid: any = '';
  daysToNextIrr: number;
  waterAmountTrans = waterAmountTranslate;

  Months: string[] = ['Styczeń', 'Luty', 'Marzec','Kwiecień','Maj',
  'Czerwiec','Lipec','Sierpien','Wrzesien','Październik','Listopad','Grudzien'];

  public barChartOptions: ChartOptions = {
    responsive: true,
    // We use these empty structures as placeholders for dynamic theming.
    scales: { xAxes: [{}], yAxes: [{display: true,
      ticks: {
          beginAtZero: true   // minimum value will be 0.
      }}] },
    plugins: {
      datalabels: {
        anchor: 'end',
        align: 'end',
      }
    }
  };

  public barChartOptionsForFlowers: ChartOptions = {
    responsive: true,
    // We use these empty structures as placeholders for dynamic theming.
    scales: { xAxes: [{}], yAxes: [{display: true,
      ticks: {
        min: 0,  // minimum value will be 0.
      }}] },
    plugins: {
      datalabels: {
        anchor: 'end',
        align: 'end',
      }
    },
    showLines: false,
    elements: {
      point: {
        radius: 5
      },
      line: {
        fill: false
      }
    },
    tooltips: {
      callbacks: {
          label(tooltipItem, data) {
              var label = data.datasets[tooltipItem.datasetIndex].label || '';
              if (tooltipItem.yLabel === 0) {
                  label += 'Niepodlane';
              } else {
                  label += 'Podlane';
              }
              return label;
          },
          labelColor(tooltipItem, chart) {
            var value = chart.data.datasets[tooltipItem.datasetIndex];
            if (tooltipItem.yLabel === 0 ) {
                  return {
                    borderColor: 'rgb(0, 0, 255)',
                    backgroundColor: 'rgb(0, 0, 255)'
                };
              } else {
                  return {
                    borderColor: 'rgb(255, 0, 0)',
                    backgroundColor: 'rgb(255, 0, 0)'
                };
              }
        },
      }
  }
  };



  public barChartLabels: string[] = [];
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;

  public barChartData: ChartDataSets[] = [
    { data: [], label: 'Podlane'},
    { data: [], label: 'Niepodlane'},
  ];

  public barChartLabelsForFlower: string[] = [];
  public barChartTypeForFlower: ChartType = 'line';
  public barChartLegendForFlower = true;

  public barChartDataForFlower: ChartDataSets[] = [
    { data: []},
  ];

  constructor(public auth: AuthService, private api: ApiClient, private datePipe: DatePipe) { }

  ngOnInit() {
      this.auth.user$.subscribe(x => {
        this.myUid = x.uid;
        this.api.statisctics_GetMyIrrigationHistory(x.uid).subscribe( x => {
          this.cahrtData = x;
          this.setLabels(x);
        });

        this.api.myFlowers_GetMyFlowers(this.myUid).subscribe(x=> {
          this.myflowersList = x;
          this.selectedflower = this.myflowersList[0];
          this.api.myFlowers_GetMyFlowersIrrigationHistory(this.selectedflower.id).subscribe( x => {
            x.forEach( el => {
              el.history.forEach( y => {
                if(y.isCompleted) {
                  this.barChartDataForFlower[0].data.push(1);
                } else {
                  this.barChartDataForFlower[0].data.push(0);
                }

                this.barChartLabelsForFlower.push(this.datePipe.transform(y.irrigationDate,"yyyy-MM-dd hh:mm:ss"));
              });
            });
        });
          this.api.statisctics_GetDataForProgress(this.selectedflower.id).subscribe(result => {
            this.nextIrrigations = result;
          });
        });
      });
  }

  onChange(date: MyFlowers) {

    this.barChartDataForFlower[0].data = [];
    this.barChartLabelsForFlower = [];

    this.api.statisctics_GetDataForProgress(date.id).subscribe(result => {
      this.nextIrrigations = result;
      this.api.myFlowers_GetMyFlowersIrrigationHistory(date.id).subscribe( x => {
        x.forEach( el => {
          el.history.forEach( y => {
            if(y.isCompleted) {
              this.barChartDataForFlower[0].data.push(1);
            } else {
              this.barChartDataForFlower[0].data.push(0);
            }

            this.barChartLabelsForFlower.push(this.datePipe.transform(y.irrigationDate,"yyyy-MM-dd hh:mm:ss"));
          });
        });
    });
    });

  }

  calculateProgress(): Number {
    var next = this.nextIrrigations[0];
    this.nextIrrigations.forEach(el => {
      if(el < next) {
        next = el;
      }
    });

    var date2 = new Date(next);
    var date1 = new Date();
    var Difference_In_Time = date2.getTime() - date1.getTime();
    var Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);

    var result = Difference_In_Days;

    if(result > ~~Difference_In_Days) {
      result = ~~Difference_In_Days +1;
    }

    this.daysToNextIrr = result;
    return ((7- result)/7)*100;
  }

  setLabels(data: IrrigationHistory[]) {
    var irr = 0;
    var notIrr = 0;
    data.forEach(element => {
      {
        if(this.barChartLabels.find(x =>  x === this.Months[new Date(element.irrigationDate).getMonth()]) === undefined) {
          const date = new Date(element.irrigationDate).getMonth();
          this.barChartLabels.push(this.Months[date]);
        }
      }
    });

    this.barChartLabels.sort((a, b) => Number(a) - Number(b));

    this.barChartLabels.forEach(x => {
      data.forEach(y => {
        if(this.Months.indexOf(x) === new Date(y.irrigationDate).getMonth()) {
          if(y.isCompleted) {
            irr++;
          } else {
            notIrr++;
          }
        }
      });
      this.barChartData[0].data.push(irr);
      this.barChartData[1].data.push(notIrr);
      irr = notIrr = 0;
    });
  }
}
