
import * as GREEN_AREA_GEOJSON from '../../assets/data/aree-verdi.json';
export class Utils{
    



    public static getStyles(name:string):string |null {
        switch (name) {
            case 'museum':
                return "../../../assets/icons/museum_icon.png"
            case 'wifi':
                return "../../../assets/icons/wifi_icon.png"
            case 'sport-plant':
                return "../../../assets/icons/sport_icon.png"
            default:
                return null
    
        }
    }
    
    public  static getData(name:string) {
        switch (name) {
            // case 'museum':
            //     return MUSEUM_GEOJSON
            // case 'wifi':
            //     return WIFI_GEOJSON
            // case 'sport-plant':
            //     return SPORT_PLANT_GEOJSON
            case 'green-area':
                return  GREEN_AREA_GEOJSON
            default:
                return null
    
        }
    }


public static getDataFilePaths(name:string) :string{
    switch (name) {
        case 'museum':
            return'../../../assets/data/musei.json'
        case 'wifi':
            return '../../../assets/data/wifi_hotspot.json'
        case 'sport-plant':
            return '../../../assets/data/aree-verdi.json'
        case 'green-area':
            return '../../../assets/data/impianti_sportivi.json'
        default:
            return ''

    }
}

}


