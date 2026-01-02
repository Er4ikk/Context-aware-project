import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomepageComponent } from './components/homepage/homepage.component';
import { FilterComponent } from './widgets/filter/filter.component';
import { TemporalGraphComponent } from './widgets/temporal-graph/temporal-graph.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatFormFieldModule} from '@angular/material/form-field';
import { CanvasJSAngularChartsModule } from '@canvasjs/angular-charts';
import {MatSelectModule} from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { HeaderComponent } from './components/header/header.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ParkingAreaDetailComponent } from './components/parking-area-detail/parking-area-detail.component';
import { DataAnalysisComponent } from './components/data-analysis/data-analysis.component';
import { HeatMapComponent } from './components/heat-map/heat-map.component';
import { ClusteringComponent } from './components/clustering/clustering.component';




@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
    FilterComponent,
    TemporalGraphComponent,
    HeaderComponent,
    ParkingAreaDetailComponent,
    DataAnalysisComponent,
    HeatMapComponent,
    ClusteringComponent
  ],
  imports: [
    MatToolbarModule,
    MatIconModule,
    CanvasJSAngularChartsModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule,
    MatGridListModule,
    MatDatepickerModule,
    MatNativeDateModule
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
