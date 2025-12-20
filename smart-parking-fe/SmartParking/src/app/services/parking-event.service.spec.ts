import { TestBed } from '@angular/core/testing';

import { ParkingEventService } from './parking-event.service';

describe('ParkingEventService', () => {
  let service: ParkingEventService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ParkingEventService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
