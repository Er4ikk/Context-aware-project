import { Component, OnInit } from '@angular/core';
import { combineLatest, filter, Observable, switchMap, tap } from 'rxjs';
import { ParkingArea, ParkingEvent } from 'src/app/entities/entities';
import { ParkingAreaService } from 'src/app/services/parking-area.service';
import { ParkingEventService } from 'src/app/services/parking-event.service';

@Component({
  selector: 'app-parking-area-detail',
  templateUrl: './parking-area-detail.component.html',
  styleUrls: ['./parking-area-detail.component.scss']
})
export class ParkingAreaDetailComponent implements OnInit {

  parkingArea$:Observable<ParkingArea> | undefined;
  parkingEvents$: Observable<ParkingEvent[]> | undefined; 
  parkingAreaName:string="";
  isShown=false;
  data$: Observable<{ area: ParkingArea, events: ParkingEvent[] }> | undefined;
  constructor(
    private parkingAreaSvc:ParkingAreaService,
    private parkingEventsSvc:ParkingEventService
  ) { }

  ngOnInit(): void {
  //   this.parkingArea$ = this.parkingAreaSvc.parkingAreaSelected$.pipe(
  //   switchMap(value => {
  //     const id = +value.charAt(value.length - 1);
  //     return this.parkingAreaSvc.getParkingAreaById(id);
  //   })
  // );

  // this.parkingEvents$ = this.parkingAreaSvc.parkingAreaSelected$.pipe(
  //   switchMap(value => {
  //     const id = +value.charAt(value.length - 1);
  //     return this.parkingEventsSvc.getParkingEventsByParkingAreaId(id);
  //   })
  // );


  this.data$ = this.parkingAreaSvc.parkingAreaSelected$.pipe(
    filter(name => !!name), 
    switchMap(name => {
      const id = +name.charAt(name.length - 1);
      this.parkingAreaName = name;
      
      return combineLatest({
        area: this.parkingAreaSvc.getParkingAreaById(id),
        events: this.parkingEventsSvc.getParkingEventsByParkingAreaId(id)
      });
    })
  );

  }

}
