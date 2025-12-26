import { Component, OnInit } from '@angular/core';
import { combineLatest, filter, Observable, repeatWhen, switchMap, tap } from 'rxjs';
import { FiltersPayload, ParkingArea, ParkingEvent } from 'src/app/entities/entities';
import { FilterService } from 'src/app/services/filter.service';
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

  filtersValue:FiltersPayload ={
    parkingAreaName: '',
    dateRange: {
      start: '',
      end: ''
    }
  };
  data$: Observable<{ area: ParkingArea, events: ParkingEvent[] }> | undefined;
  constructor(
    private parkingAreaSvc:ParkingAreaService,
    private parkingEventsSvc:ParkingEventService,
    private filterSvc:FilterService
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

  // this.filterSvc.filterSubject.pipe(
  //   tap((filtersPayload) => this.filtersValue = filtersPayload)
  // ).subscribe()


  this.data$ = (combineLatest([this.parkingAreaSvc.parkingAreaSelected$.pipe(filter(name => !!name)),this.filterSvc.filterSubject])).pipe(
  switchMap(([parkingAreaName,filtersPayload]:[string,FiltersPayload]) => {
    this.parkingAreaName = parkingAreaName;
    const id:number = +this.parkingAreaName.charAt(this.parkingAreaName.length-1) 
    
    return combineLatest({
      area: this.parkingAreaSvc.getParkingAreaById(id),
      events: filtersPayload.dateRange.start !== '' ?
        this.parkingEventsSvc.getParkingEventsOfParkingAreaByTimeRange(
          filtersPayload.dateRange.start, 
         filtersPayload.dateRange.end, 
          id
        ) :
        this.parkingEventsSvc.getParkingEventsByAreaId(id)
    });
  })
  );

  }

}
