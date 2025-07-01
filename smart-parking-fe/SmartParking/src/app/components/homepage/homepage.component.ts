import { Component, OnInit } from '@angular/core';
import Map from 'ol/Map';
import View from 'ol/View';
import TileLayer from 'ol/layer/Tile';
import OSM from 'ol/source/OSM';
import { fromLonLat } from 'ol/proj';
import VectorLayer from 'ol/layer/Vector';

import GeoJSON from 'ol/format/GeoJSON.js';
import { Vector } from 'ol/source';
import Icon from 'ol/style/Icon';
import Fill from 'ol/style/Fill';
import Style from 'ol/style/Style';
import Stroke from 'ol/style/Stroke';
import Select from 'ol/interaction/Select';

import { Utils } from 'src/app/entities/utils';
import { Feature } from 'ol';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent implements OnInit {


  LATTTUDE: number = 44.4938134;
  LONGITUDE: number = 11.3394883;

  selected :Style = new Style({
  fill: new Fill({
    color: '#eeeeee',
  }),
  stroke: new Stroke({
    color: 'rgba(10, 1, 1, 0.7)',
    width: 2,
  }),
});

selectSingleClick = new Select({style: this.selectStyle});

selectElement = document.getElementById('type');

  constructor() { }
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

    console.log(this.selected)
    this.addOSMLayer('green-area')
    this.map.addInteraction(this.selectSingleClick)
    
  }


   getCoord(event: any){
    var coordinate = this.map.getEventCoordinate(event);
    console.log(coordinate)
    
    alert("you have clicked the following coordinates: " + coordinate.toString())
 }


  public addOSMLayer(name: string) {
    var geoJsonData = Utils.getData(name)
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
          "name":name
        }
        ,
      source: vectorSource,
      // background: 'white',
      // to refactor
      style: new Style({
         fill: new Fill({
         color: [52, 235, 137,0.5],
     })
      })
    });

    vectorLayer.setVisible(true);
    if (vectorLayer != undefined && this.map != undefined)
      this.map.addLayer(vectorLayer);
  }


  

selectStyle(feature:any):Style {
  const color = feature.get('COLOR') || '#eeeeee';
  debugger
  console.log(this.selected)
  // if(this.selected != null)
  var  test = this.selected.getFill()
    this.selected.getFill()?.setColor(color);
  return this.selected;
}

}
