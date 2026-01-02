import { Component, OnDestroy, OnInit } from '@angular/core';
import Map from 'ol/Map.js';
import View from 'ol/View.js';
import KML from 'ol/format/KML.js';
import HeatmapLayer from 'ol/layer/Heatmap.js';
import TileLayer from 'ol/layer/Tile.js';
import StadiaMaps from 'ol/source/StadiaMaps.js';
import VectorSource from 'ol/source/Vector.js';
import GeoJSON from 'ol/format/GeoJSON.js';
import { FormControl, FormGroup } from '@angular/forms';
import { Utils } from 'src/app/entities/utils';
import Vector from 'ol/source/Vector.js';
import { ParkingCoordinates, PlacesLeftColor } from 'src/app/entities/entities';
import { ParkingEventService } from 'src/app/services/parking-event.service';
import { ReplaySubject, takeUntil, tap } from 'rxjs';
import { fromLonLat } from 'ol/proj';

// const blur: HTMLInputElement | null = document.getElementById('blur') as HTMLInputElement;
// const radius: HTMLInputElement | null = document.getElementById('radius') as HTMLInputElement;






@Component({
  selector: 'app-heat-map',
  templateUrl: './heat-map.component.html',
  styleUrls: ['./heat-map.component.scss']
})
export class HeatMapComponent implements OnInit, OnDestroy {
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  map!: Map;
  blur: FormControl = new FormControl('15');
  radius: FormControl = new FormControl('15');
  parkingEventCoordinates!: ParkingCoordinates;
  LATTTUDE: number = 44.4938134;
  LONGITUDE: number = 11.3394883;


  vector!: HeatmapLayer;

  raster: TileLayer = new TileLayer({
    source: new StadiaMaps({
      layer: 'stamen_toner',
    }),
  });



  constructor(private ParkingEventSvc: ParkingEventService) { }
  ngOnDestroy(): void {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }

  ngOnInit(): void {
    this.map = new Map({
      layers: [this.raster],
      target: 'heatmap',
      view: new View({
        center: fromLonLat([this.LONGITUDE, this.LATTTUDE]),
        zoom: 15,
      })
    });

    this.ParkingEventSvc.getParkinngEventsFeatures().pipe(
      takeUntil(this.destroyed$),
      tap(console.log),
      tap((parkingCoordinates: ParkingCoordinates) => {
        this.parkingEventCoordinates = parkingCoordinates
        this.addHeatMapLayer("parking-events", this.parkingEventCoordinates)

        
        // if (this.blur != null && this.radius != null) {

        //   this.vector.setBlur(parseInt(this.blur?.value, 10));

        //   this.vector.setRadius(parseInt(this.radius?.value, 10));
        // } else {
        //   alert("Blur and radius are not defined!")
        // }
      })
    ).subscribe()

    this.blur.valueChanges.pipe(
       takeUntil(this.destroyed$),
       tap(value => this.vector.setBlur(parseInt(value, 10)))
    ).subscribe()
    this.radius.valueChanges.pipe(
       takeUntil(this.destroyed$),
       tap(value => this.vector.setRadius(parseInt(value, 10)))
    ).subscribe()












  }

  public addHeatMapLayer(name: string, geoJsonData?: any, color?: number[]) {
    // 
    if (geoJsonData == null)
      geoJsonData = Utils.getData(name)

    let featureCollection = {
      type: "FeatureCollection",
      features: geoJsonData
    }
    // debugger
    const vectorSource = new Vector({

      features: new GeoJSON({

        dataProjection: 'EPSG:4326',
        featureProjection: 'EPSG:3857'
      }).readFeatures(featureCollection)
    });

    // Creating a vector Layer      
    // const vectorLayer = new VectorLayer({
    //   properties:
    //   {
    //     "name": name
    //   }
    //   ,
    //   source: vectorSource,
    //   // background: 'white',
    //   // to refactor
    //   style: new Style({
    //     fill: new Fill({
    //       color: color != undefined ?
    //         color :
    //         PlacesLeftColor.UKNOWN,
    //     })
    //   })
    // });

    this.vector = new HeatmapLayer({
      source: vectorSource,
      blur: parseInt(this.blur.value, 10),
      radius: parseInt(this.radius.value, 10),
      weight: function (feature): number {
        let magnitude: number = 1;
        return magnitude;
      },
    });

    this.vector.setVisible(true);
    if (this.vector != undefined && this.map != undefined)
      this.map.addLayer(this.vector);
    // vectorLayer.setVisible(true);
    // if (vectorLayer != undefined && this.map != undefined)
    //   this.map.addLayer(vectorLayer);
  }


}
