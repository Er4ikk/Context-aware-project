import { ChangeDetectionStrategy, Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { debounceTime, distinctUntilChanged, take, tap } from 'rxjs';
import {CdkTextareaAutosize} from '@angular/cdk/text-field';
import { FormControl, FormGroup } from '@angular/forms';
import { FilterService } from 'src/app/services/filter.service';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FilterComponent implements OnInit {
  parkingAreaName:FormControl=new FormControl('');
  readonly range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  constructor(private _ngZone: NgZone, private filterSvc: FilterService) { }

  @ViewChild('autosize') autosize!: CdkTextareaAutosize;
  ngOnInit(): void {
    this.parkingAreaName.valueChanges.pipe(
      tap(()=>this.filterSvc.filterLoading$.next(true)),
      distinctUntilChanged(),
      debounceTime(1000),
      tap((value:string) => {
        this.filterSvc.filterSubject.next(value)
        this.filterSvc.filterLoading$.next(false);
      })
    ).subscribe()

    this.range.valueChanges.pipe(
      tap((value)=>{
        if(value.start != null){
            let startDate :Date= new Date(value.start)
            // to continue add also the end date
            console.log(startDate.toISOString())
        }
        // console.log(this.formatDateToPostgres())
      })
    ).subscribe()
  }


  triggerResize() {
    // Wait for changes to be applied, then trigger textarea resize.
    this._ngZone.onStable.pipe(take(1)).subscribe(() => this.autosize.resizeToFitContent(true));
  }

 


}
