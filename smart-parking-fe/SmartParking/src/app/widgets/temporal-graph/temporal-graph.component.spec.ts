import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TemporalGraphComponent } from './temporal-graph.component';

describe('TemporalGraphComponent', () => {
  let component: TemporalGraphComponent;
  let fixture: ComponentFixture<TemporalGraphComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TemporalGraphComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TemporalGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
