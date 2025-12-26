import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { FiltersPayload, TimeRange } from '../entities/entities';

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  timeRange: TimeRange = {
    start: '',
    end: ''
  }
  filterPayload: FiltersPayload = {
    parkingAreaName: '',
    dateRange: this.timeRange
  }
  filterSubject: BehaviorSubject<FiltersPayload> = new BehaviorSubject(this.filterPayload);
  filterLoading$: BehaviorSubject<boolean> = new BehaviorSubject(false);
  constructor() { }
}
