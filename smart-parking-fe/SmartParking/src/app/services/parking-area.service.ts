import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ParkingArea, ParkingAreaCentroid } from '../entities/entities';
import { ReplaySubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ParkingAreaService {
  private baseUrl:string = "ParkingArea"
  
  public parkingAreaSelected$ : ReplaySubject<string> = new ReplaySubject(1);

  constructor(private httpClient:HttpClient) { }

  public getParkingAreas(){
    return this.httpClient.get<ParkingArea[]>(this.baseUrl+"/api/ParkingArea/GetParkingAreas")
  }

  public getParkingAreasCentroids(){
    return this.httpClient.get<ParkingAreaCentroid[]>(this.baseUrl+"/api/ParkingArea/GetParkingAreasCentroids")
  }

  public getParkingAreaById(id:number){
    return this.httpClient.get<ParkingArea>(this.baseUrl+"/api/ParkingArea/GetParkingAreaById/"+id)
  }
}
