import { Injectable } from '@angular/core';
import { ParkingArea, ParkingCoordinates, ParkingEvent, PolygonCoordinates } from '../entities/entities';
import { HttpClient } from '@angular/common/http';
import { Feature } from 'ol';

@Injectable({
  providedIn: 'root'
})
export class ParkingEventService {
  private baseUrl:string = "ParkingEvent"
  constructor(private httpClient:HttpClient) { }

  public getParkingEventsOfParkingAreaByTimeRange(start:string, end :string,  id:number){
    return this.httpClient.get<ParkingEvent[]>(this.baseUrl+"/api/ParkingEvent/GetParkingEvenstByTimeRange/"+start+"/"+end+"/"+id)
  }

  public getParkingEventsByAreaId( id:number){
    return this.httpClient.get<ParkingEvent[]>(this.baseUrl+"/api/ParkingEvent/GetParkingEvenstByParkingAreaId/"+id)
  }

  public getParkingAreaSnapshotByTimeRange(start:string,end:string){
    return this.httpClient.get<ParkingArea[]>(this.baseUrl+"/api/ParkingEvent/GetParkingAreasSnapshotByTimeRange/"+start+"/"+end)
  }

  public getParkinngEventsFeatures(){
    return this.httpClient.get<Feature[]>(this.baseUrl+"/api/ParkingEvent/GetParkingEventsFeatures")
  }
}
