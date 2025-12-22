import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { FiltersPayload } from '../entities/entities';

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  
  filterSubject:Subject<FiltersPayload>= new Subject();
  filterLoading$:BehaviorSubject<boolean>= new BehaviorSubject(false);
  constructor() { }
}
