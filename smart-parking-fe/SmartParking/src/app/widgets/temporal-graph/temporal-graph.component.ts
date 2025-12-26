import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Inject, Input, LOCALE_ID, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { CanvasJSAngularChartsModule } from '@canvasjs/angular-charts';
import { Observable, tap } from 'rxjs';
import {formatDate} from '@angular/common';
import { EventType, ParkingArea, ParkingEvent } from 'src/app/entities/entities';


@Component({
	selector: 'app-temporal-graph',
	templateUrl: './temporal-graph.component.html',
	styleUrls: ['./temporal-graph.component.scss'],
	// changeDetection:ChangeDetectionStrategy.OnPush
})


export class TemporalGraphComponent implements OnInit, OnChanges {

	// @Input()parkingEvents$:Observable<ParkingEvent[]> | null | undefined;
	@Input() parkingArea: ParkingArea | undefined | null = null;

	@Input() parkingEvents: ParkingEvent[] | undefined;

	chartOptions = {}
	chart: any;
	constructor(
		private cdr: ChangeDetectorRef,
		@Inject(LOCALE_ID) private locale: string
	) {

	}

	//also this very important forcing re-rendering
	getChartInstance(chart: object) {
		this.chart = chart;
	}

	ngOnChanges(changes: SimpleChanges): void {
		if (this.parkingArea && this.parkingEvents) {

			this.updateChart();
			// very important!
			this.cdr.detectChanges();

		}

	}
	ngOnInit(): void {
		// this.parkingEvents$?.pipe(
		// 	tap((events:ParkingEvent[]) => this.parkingEvents = events)
		// ).subscribe()
	}

	updateChart(): void {
		this.chartOptions = {
			title: { text: "Grafico di occupazione parcheggio" },
			animationEnabled: true,
			axisY: { includeZero: true },
			data: [{
				type: "column",
				indexLabelFontColor: "#5A5757",
				dataPoints: this.getDataPoints()
			}]
		};
		
		// https://stackoverflow.com/questions/76298430/canvasjs-chart-in-angular-2way-binding
		if (this.chart) {
			this.chart.options = this.chartOptions;
			this.chart.render();
		}
	}


	getDataPoints(): Coordinate[] {

		if (!this.parkingArea || !this.parkingEvents || this.parkingEvents.length === 0) {
			return [];
		}

		let coordinates: Coordinate[] = [];
		let coordinate: Coordinate
		let placeLeft: number = 0;
		// 
		if (this.parkingArea && this.parkingEvents) {

			placeLeft = this.parkingArea.placesLeft;
			this.parkingEvents.forEach(
				(parkingEvent: ParkingEvent, index: number) => {

					if (parkingEvent.eventType == EventType.PARKING && placeLeft > 0) {
						placeLeft--
					} else {
						placeLeft++
					}

					coordinate = {
						x: index,
						y: placeLeft,
						indexLabel: formatDate(parkingEvent.timeStamp,"dd/MM/yyyy",this.locale)
					}

					coordinates.push(coordinate)
				}
			)
		}

		return coordinates.reverse().map((coord, idx) => ({ ...coord, x: idx }));
	}

}

export interface Coordinate {
	x: number,
	y: number,
	indexLabel: string
}
