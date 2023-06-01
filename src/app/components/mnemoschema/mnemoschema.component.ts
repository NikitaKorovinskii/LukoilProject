import {Input,Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import axios from "axios";
import {DataService} from '../../data.service';
@Component({
  selector: 'app-mnemoschema',
  templateUrl: './mnemoschema.component.html',
  styleUrls: ['./mnemoschema.component.css']
})
export class MnemoschemaComponent implements OnInit{
  pressure: any;
  density: any;
  concentration: any;
  pressureList: any[] = []
  densityList: any[] = []
  concentrationList: any[] = []
  time: any[] = []

  constructor(private route: Router,private dataService: DataService) {}
  ngOnInit(): void {
    axios.get('https://localhost:44326/api').then(res => {
        for (let i = 0; i < res.data.length; i++) {
          this.pressure = res.data[res.data.length-1].pressure
          this.density = res.data[res.data.length-1].density
          this.concentration = res.data[res.data.length-1].concentration
          this.pressureList.push(res.data[i].pressure)
          this.densityList.push(res.data[i].density)
          this.concentrationList.push(res.data[i].concentration)
          this.time.push(res.data[i].time)
        }
        this.dataService.setPressureDataList(this.pressureList)
        this.dataService.setDensityDataList(this.densityList)
        this.dataService.setConcentrationDataList(this.concentrationList)
        this.dataService.setTime(this.time)
      })
  }
  graph(type: number) {
    this.route.navigate(['/graph'])
    this.dataService.setTypeAdd(type)
  }
}



