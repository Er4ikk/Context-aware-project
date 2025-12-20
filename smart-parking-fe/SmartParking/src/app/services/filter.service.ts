import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  
  filterSubject:Subject<string>= new Subject();
  filterLoading$:BehaviorSubject<boolean>= new BehaviorSubject(false);
  constructor() { }
}
