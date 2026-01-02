import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import Feature, { FeatureLike } from 'ol/Feature.js';
import Map from 'ol/Map.js';
import View from 'ol/View.js';
import { boundingExtent } from 'ol/extent.js';
import GeoJSON from 'ol/format/GeoJSON';
import { Geometry } from 'ol/geom';
import Point from 'ol/geom/Point.js';
import TileLayer from 'ol/layer/Tile.js';
import VectorLayer from 'ol/layer/Vector.js';
import Cluster from 'ol/source/Cluster.js';
import OSM from 'ol/source/OSM.js';
import VectorSource from 'ol/source/Vector.js';
import CircleStyle from 'ol/style/Circle.js';
import Fill from 'ol/style/Fill.js';
import Stroke from 'ol/style/Stroke.js';
import Style, { StyleLike } from 'ol/style/Style.js';
import Text from 'ol/style/Text.js';
import { ReplaySubject, takeUntil, tap } from 'rxjs';
import { ParkingArea, ParkingAreaCentroid } from 'src/app/entities/entities';
import { ParkingAreaService } from 'src/app/services/parking-area.service';

@Component({
  selector: 'app-clustering',
  templateUrl: './clustering.component.html',
  styleUrls: ['./clustering.component.scss']
})

export class ClusteringComponent implements OnInit, OnDestroy {

  distanceInput: FormControl = new FormControl('15');
  minDistanceInput: FormControl = new FormControl('15');
  LATTTUDE: number = 44.4938134;
  LONGITUDE: number = 11.3394883;
  parkingAreas: ParkingAreaCentroid[] = []

  count: number = 20000;
  features: Feature[] =[];
  e: number = 4500000;

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  source!: VectorSource<Feature>;

  clusterSource!: Cluster<Feature>;

  styleCache: { [key: number]: Style } = {};
  clusters!: VectorLayer;

  raster: TileLayer<OSM> = new TileLayer({
    source: new OSM(),
  });

  map!: Map;



  constructor(private parkingAreaSvc: ParkingAreaService) { }

  ngOnInit() {

    this.parkingAreaSvc.getParkingAreasCentroids()
      .pipe(
        takeUntil(this.destroyed$),
        tap((parkingAreas) => {
          this.parkingAreas = parkingAreas
          
          if (this.parkingAreas.length > 0) {
            this.count = this.parkingAreas.length

            this.parkingAreas.forEach((parkingArea) => {
              // debugger
              let geometry:Feature<Geometry>[] =new GeoJSON({

                dataProjection: 'EPSG:4326',
                featureProjection: 'EPSG:3857'
              }).readFeatures(parkingArea.center)
              this.features.push(geometry[0]);
            })
            this.InitClusterMap()

            this.map = new Map({
      layers: [this.raster],
      target: 'clustering-map',
      view: new View({
        center: [0, 0],
        zoom: 2,
      }),
    });

    if (this.map != null) {
      this.map.on('click', (e): void => {
        this.clusters.getFeatures(e.pixel).then((clickedFeatures) => {
          if (clickedFeatures.length) {
            let featuresArray = clickedFeatures[0].get('features');
            if (featuresArray.length > 1) {
              let extent = boundingExtent(
                featuresArray.map((r: { getGeometry: () => { (): any; new(): any; getCoordinates: { (): any; new(): any; }; }; }) => r.getGeometry().getCoordinates()),
              );
              this.map?.getView().fit(extent, { duration: 1000, padding: [50, 50, 50, 50] });
            }
          }
        });
      });

      this.map.addLayer(this.clusters)
    }

          }
        })
      ).subscribe()
    

    
    this.distanceInput.valueChanges.pipe(
      takeUntil(this.destroyed$),
      tap(value => this.clusterSource.setDistance(parseInt(value, 10)))
    ).subscribe()
    this.minDistanceInput.valueChanges.pipe(
      takeUntil(this.destroyed$),
      tap(value => this.clusterSource.setMinDistance(parseInt(value, 10)))
    ).subscribe()


  }

  public InitClusterMap() {
    // for (let i: number = 0; i < this.count; ++i) {
    //   let coordinates: [number, number] = [2 * this.e * Math.random() - this.e, 2 * this.e * Math.random() - this.e];
    //   this.features[i] = new Feature(new Point(coordinates));
    // }

    this.source = new VectorSource({
      features: this.features,
    });

    this.clusterSource = new Cluster({
      distance: parseInt(this.distanceInput.value, 10),
      minDistance: parseInt(this.minDistanceInput.value, 10),
      source: this.source,
    });

    this.clusters = new VectorLayer({
      source: this.clusterSource,
      style: function (feature:FeatureLike) :Style{
        const size: number = feature.get('features').length;
        let style = new Style({
        image: new CircleStyle({
          radius: 10,
          stroke: new Stroke({
            color: '#fff',
          }),
          fill: new Fill({
            color: '#3399CC',
          }),
        }),
        text: new Text({
          text: size.toString(),
          fill: new Fill({
            color: '#fff',
          }),
        }),
      })

      return style;
      }

    });
  }

  ngOnDestroy(): void {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }

}
