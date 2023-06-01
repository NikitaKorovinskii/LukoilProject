import {List} from "echarts";
import {concatWith} from "rxjs";

export class DataService {
  private typeData  = 0;
  private pressureData: any[] = []; //Давление список
  private densityData: any[] = [];
  private concentrationData: any[] = [];
  private time: any[] = [];

  getConcentrationDataList() {
    return this.concentrationData;
  }
  setConcentrationDataList(concentrationData: any){
    this.concentrationData = concentrationData;
  }
  getTime() {
    return this.time;
  }
  setTime(time: any){
    this.time = time;
  }
  getDensityDataList() {
    return this.densityData;
  }
  setDensityDataList(densityData: any){
    this.densityData = densityData;
  }
  getPressureDataList() {
    return this.pressureData;
  }
  setPressureDataList(pressureData: any){
    this.pressureData = pressureData
  }

  getTypeData() {
    return this.typeData;
  }
  setTypeAdd(typeData: number){
    this.typeData = typeData;
  }

}
