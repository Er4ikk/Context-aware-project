import { Injectable } from '@angular/core';
import { ParkingEvent } from '../entities/entities';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ParkingEventService {
  private baseUrl:string = "ParkingEvent"
  constructor(private httpClient:HttpClient) { }

  public getParkingEventsByParkingAreaId(id:number){
    return this.httpClient.get<ParkingEvent[]>(this.baseUrl+"/api/ParkingEvent/GetParkingEvenstByParkingAreaId/"+id)
  }
}
