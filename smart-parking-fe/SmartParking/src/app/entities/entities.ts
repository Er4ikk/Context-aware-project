export interface ParkingArea {
  id: number
  area: string
  maxCapacity: number
  placesLeft: number
}
export interface ParkingEvent {
  id: number
  parkingAreaId: number
  timeStamp: string
  eventType: string
}

export interface PolygonCoordinates {
  type: string
  coordinates: number[][][]
}

export interface FiltersPayload{
  parkingAreaName:string,
  dateRange:TimeRange
}

export interface TimeRange{
  start:string ,
  end:string
}

export const PlacesLeftColor = {
  ALMOST_NO_PLACE_LEFT: [255, 51, 0, 0.7],
  HALF_PLACE_LEFT: [255, 204, 0, 0.7],
  MAX_PLACE_LEFT: [0, 204, 102, 0.7],
  UKNOWN:[153, 102, 51, 0.7]


}

export const EventType={

     PARKING : "Parking",
     LEAVING : "Leaving"
    

}

