import { ChangeDetectionStrategy, Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { debounceTime, distinctUntilChanged, take, tap } from 'rxjs';
import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { FormControl, FormGroup } from '@angular/forms';
import { FilterService } from 'src/app/services/filter.service';
import { FiltersPayload, TimeRange } from 'src/app/entities/entities';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FilterComponent implements OnInit {
  parkingAreaName: FormControl = new FormControl('');
  readonly range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  constructor(private _ngZone: NgZone, private filterSvc: FilterService) { }

  @ViewChild('autosize') autosize!: CdkTextareaAutosize;
  ngOnInit(): void {
    this.parkingAreaName.valueChanges.pipe(
      tap(() => this.filterSvc.filterLoading$.next(true)),
      distinctUntilChanged(),
      debounceTime(1000),
      tap((value: string) => {

        let filterPayload: FiltersPayload = {
          parkingAreaName: value,
          dateRange: {
            start: undefined,
            end: undefined
          }
        }
        this.filterSvc.filterSubject.next(filterPayload)
        this.filterSvc.filterLoading$.next(false);
      })
    ).subscribe()

    this.range.valueChanges.pipe(
      tap((value) => {
        if (value.start != null) {
          let startDate: Date = new Date(value.start)
          let endDate: Date | undefined = undefined;
          if (value.end)
            endDate = new Date(value.end)
          // to continue add also the end date
          let timeRange: TimeRange = {
            start: startDate.toISOString(),
            end: endDate?.toISOString()
          }
          let filterPayload: FiltersPayload = {
            parkingAreaName: this.parkingAreaName.value,
            dateRange: timeRange
          }
          this.filterSvc.filterSubject.next(filterPayload)
          this.filterSvc.filterLoading$.next(false);
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
