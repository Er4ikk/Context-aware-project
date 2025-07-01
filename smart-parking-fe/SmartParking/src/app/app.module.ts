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



@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
    FilterComponent,
    TemporalGraphComponent,
    HeaderComponent
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
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
