import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParkingAreaDetailComponent } from './parking-area-detail.component';

describe('ParkingAreaDetailComponent', () => {
  let component: ParkingAreaDetailComponent;
  let fixture: ComponentFixture<ParkingAreaDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ParkingAreaDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParkingAreaDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
