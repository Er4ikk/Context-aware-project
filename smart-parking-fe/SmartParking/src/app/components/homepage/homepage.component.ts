import { Component, OnInit } from '@angular/core';
import Map from 'ol/Map';
import View from 'ol/View';
import TileLayer from 'ol/layer/Tile';
import OSM from 'ol/source/OSM';
import { fromLonLat } from 'ol/proj';
import VectorLayer from 'ol/layer/Vector';

import GeoJSON from 'ol/format/GeoJSON.js';
import { Vector } from 'ol/source';
import Fill from 'ol/style/Fill';
import Style from 'ol/style/Style';
import Stroke from 'ol/style/Stroke';
import Select, { SelectEvent } from 'ol/interaction/Select';

import { Utils } from 'src/app/entities/utils';
import { ParkingAreaService } from 'src/app/services/parking-area.service';
import {  Subject, take, tap } from 'rxjs';
import { FiltersPayload, ParkingArea, PlacesLeftColor, PolygonCoordinates } from 'src/app/entities/entities';
import { FilterService } from 'src/app/services/filter.service';
import BaseLayer from 'ol/layer/Base';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent implements OnInit {


  LATTTUDE: number = 44.4938134;
  LONGITUDE: number = 11.3394883;

  selected: Style = new Style({
    fill: new Fill({
      color: '#eeeeee',
    }),
    stroke: new Stroke({
      color: 'rgba(10, 1, 1, 0.7)',
      width: 2,
    }),
  });

  select: Select = new Select({
    style: this.selected
  })


  private ParkingAreas: ParkingArea[] = []
  areFiltersLoading: boolean = false;

  filters$: Subject<FiltersPayload> = new Subject();
  // parkingAreaSelected$ : Subject<string> = new Subject();


  constructor(
    private parkingAreaSvc: ParkingAreaService,
    private FilterSvc: FilterService
  ) { }
  map!: Map;
  ngOnInit(): void {
    this.map = new Map({
      view: new View({
        center: fromLonLat([this.LONGITUDE, this.LATTTUDE]),
        zoom: 15,
      }),
      layers: [
        new TileLayer({
          source: new OSM(),
        }),
      ],
      target: 'map'
    });

    this.map.addInteraction(this.select)

    this.select.addEventListener('select',(event)=>{
      this.OnAreaSelected(( event as SelectEvent))
    })

    this.parkingAreaSvc.getParkingAreas()
      .pipe(
        take(1),
        tap((parkingAreas: ParkingArea[]) => {
          this.ParkingAreas = parkingAreas
          this.AddParkingAreaToMap()
        })
      ).subscribe(
      // (val) => console.log(val)
    )

    this.filters$ = this.FilterSvc.filterSubject
    //getting layers and then filter them with the layer searched

    this.filters$.pipe(
      tap(value => this.filterByParkingArea(value.parkingAreaName))
    ).subscribe()

    this.FilterSvc.filterLoading$.pipe(
      tap((value) => this.areFiltersLoading = value)

    ).subscribe()

  }

  public OnAreaSelected(event:SelectEvent):void{
    // console.log(event)
    let layer : VectorLayer;
    if(event.selected[0] != null){
      layer = this.select.getLayer(event.selected[0])
      let layerName:string =layer.get("name")
      this.parkingAreaSvc.parkingAreaSelected$.next(layerName)
      // debugger
    }else{
      alert("The area couldn't be found")
    }
       

    
    
  }
  getCoord(event: any) {
    var coordinate = this.map.getEventCoordinate(event);
    console.log(coordinate)

    // alert("you have clicked the following coordinates: " + coordinate.toString())
  }

  public AddParkingAreaToMap() {
    var color: number[] = [];
    this.ParkingAreas.forEach((el: ParkingArea) => {
      if (el.placesLeft < el.maxCapacity / 4)
        color = PlacesLeftColor.ALMOST_NO_PLACE_LEFT
      else if (el.placesLeft >= el.maxCapacity / 4 && el.placesLeft <= el.maxCapacity / 2)
        color = PlacesLeftColor.HALF_PLACE_LEFT
      else if (el.placesLeft > el.maxCapacity / 2)
        color = PlacesLeftColor.MAX_PLACE_LEFT;
      else
        color = PlacesLeftColor.UKNOWN


      this.addOSMLayer('parking-area-' + el.id, JSON.parse(el.area), color)
    })
  }

  public filterByParkingArea(parkingAreaName: string) {
    this.areFiltersLoading = true;
    
    if (parkingAreaName != null && parkingAreaName == "")
      this.resetFilters()



    this.map.getLayers().forEach(
      (layer: BaseLayer) => {
        var name: string = layer.get('name')
        // debugger
        if (name != undefined && !name.includes(parkingAreaName)) {
          layer.setVisible(false)
        } else {
          layer.setVisible(true)
        }

      }
    )
    this.areFiltersLoading = false
  }

  public parsingParkingAreasToJson(parkingAreas: ParkingArea[]) {
    let areas: PolygonCoordinates[] = []
    parkingAreas.forEach((el: ParkingArea) => {
      areas.push(JSON.parse(el.area))
    })

    var featureCollection = {
      type: "FeatureCollection",
      features: areas
    }
    // debugger
    return featureCollection;
  }

  public resetFilters() {
    this.map.getLayers().forEach((layer: BaseLayer) => layer.setVisible(true))
  }


  public addOSMLayer(name: string, geoJsonData?: any, color?: number[]) {
    // debugger
    if (geoJsonData == null)
      geoJsonData = Utils.getData(name)
    const vectorSource = new Vector({
      features: new GeoJSON({
        dataProjection: 'EPSG:4326',
        featureProjection: 'EPSG:3857'
      }).readFeatures(geoJsonData)
    });

    // Creating a vector Layer      
    const vectorLayer = new VectorLayer({
      properties:
      {
        "name": name
      }
      ,
      source: vectorSource,
      // background: 'white',
      // to refactor
      style: new Style({
        fill: new Fill({
          color: color != undefined ?
            color :
            PlacesLeftColor.UKNOWN,
        })
      })
    });

    vectorLayer.setVisible(true);
    if (vectorLayer != undefined && this.map != undefined)
      this.map.addLayer(vectorLayer);
  }




  selectStyle(feature: any): Style {
    const color = feature.get('COLOR') || '#eeeeee';
    // debugger
    console.log(this.selected)
    // if(this.selected != null)
    var test = this.selected.getFill()
    this.selected.getFill()?.setColor(color);
    return this.selected;
  }

}
