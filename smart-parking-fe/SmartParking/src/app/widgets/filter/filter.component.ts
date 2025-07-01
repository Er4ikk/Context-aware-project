import { Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { take } from 'rxjs';
import {CdkTextareaAutosize} from '@angular/cdk/text-field';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit {

  constructor(private _ngZone: NgZone) { }

  @ViewChild('autosize') autosize!: CdkTextareaAutosize;
  ngOnInit(): void {
  }


  triggerResize() {
    // Wait for changes to be applied, then trigger textarea resize.
    this._ngZone.onStable.pipe(take(1)).subscribe(() => this.autosize.resizeToFitContent(true));
  }

}
