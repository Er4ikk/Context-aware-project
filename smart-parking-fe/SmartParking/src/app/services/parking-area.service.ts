import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ParkingArea } from '../entities/entities';

@Injectable({
  providedIn: 'root'
})
export class ParkingAreaService {
  private baseUrl:string = "ParkingArea"

  constructor(private httpClient:HttpClient) { }

  public getParkingAreas(){
    return this.httpClient.get<ParkingArea[]>(this.baseUrl+"/api/ParkingArea/GetParkingAreas")
  }
}
