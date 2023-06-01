import { Component, OnInit, OnDestroy } from '@angular/core';
import {Router} from "@angular/router";
import {DataService} from '../data.service';
import {Chart,registerables} from "chart.js";
import { DatePipe } from '@angular/common';

Chart.register(...registerables);

@Component({
  selector: 'app-graph',
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.css']
})

  export class GraphComponent implements OnInit {
  typeData: any
  dataSet:any
  labels: any
  constructor(private route: Router, public dataService: DataService) {
  }

  ngOnInit() {
    this.typeData = this.dataService.getTypeData()
    if (this.typeData==1){

      this.dataSet = this.dataService.getPressureDataList()
      this.labels = this.dataService.getTime()
      this.RenderChart(this.dataSet,this.labels)
    }
    if (this.typeData==2){
      this.dataSet = this.dataService.getDensityDataList()
      this.labels = this.dataService.getTime()
      this.RenderChart(this.dataSet,this.labels)
    }
    if (this.typeData==3){
      this.dataSet = this.dataService.getConcentrationDataList()
      this.labels = this.dataService.getTime()
      this.RenderChart(this.dataSet,this.labels)
    }

  }
  RenderChart(dataSet: any, labels:any){
    const ctx =document.getElementById('lineChart')!
    // @ts-ignore
    new Chart(ctx, {
      type: 'line',
      data: {
        labels: labels,
        datasets: [{
          borderColor: ["rgb(2,126,251,1)"],
          borderWidth: 2,
          data: dataSet,
          label: 'Данные с датчиков',
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });

  }
  exit() {
    this.route.navigate(['/'])
  }
}


