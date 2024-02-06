import {useMemo, useState} from 'react';
import {GoogleMap, Circle, useLoadScript} from '@react-google-maps/api';
import {Loader} from "../shared/ui/loader/loader.tsx";
const API_KEY = import.meta.env.VITE_GOOGLE_MAP_KEY;
const containerStyle = {
    width: '100%',
    height: '450px'
};
export type LatLng = {
    lat: number,
    lng: number
}
const center: LatLng = {
    lat: 45.2542,
    lng: 19.811541
};
export const GeoSearchComponent = () => {
    const { isLoaded } = useLoadScript({
        googleMapsApiKey: API_KEY
    });
    const [circleCenter, setCircleCenter] = useState<LatLng>(center);
    const [circleRadius, setCircleRadius] = useState<number>(1000);
    const onLoad = async (_: google.maps.Map) => {
        console.log('loaded')
    };
    const handleRadiusChange = (event: any) => {
        const newRadius = parseFloat(event.target.value);
        if (!isNaN(newRadius)) {
            setCircleRadius(newRadius);
        }
    };
    const handleMapClick = (event) => {
        if (event.latLng) {
            const newLat = event.latLng.lat();
            const newLng = event.latLng.lng();
            setCircleCenter({ lat: newLat, lng: newLng });
        }
    };

    return (
        <div className={'padding-base flex-col-center g-1'}>
            <div className={'flex-center g-5'}>
                <label className={'main-text'}>Radius (in meters):</label>
                <input
                    className={'input-base'}
                    type="number"
                    value={circleRadius}
                    onChange={handleRadiusChange}
                    placeholder="Enter radius (in meters)"
                />
            </div>
            {!isLoaded ? (
                <Loader/>
            ) : (
                <GoogleMap
                    mapContainerStyle={containerStyle}
                    center={circleCenter}
                    zoom={12}
                    onLoad={onLoad}
                    onClick={handleMapClick}
                    id={'google-maps'}
                >
                    {circleCenter && (
                        <Circle
                            center={circleCenter}
                            radius={circleRadius}
                            options={{
                                strokeColor: '#blue',
                                strokeOpacity: 0.8,
                                strokeWeight: 2,
                                fillColor: '#FF0000',
                                fillOpacity: 0.35,
                            }}
                        />
                    )}
                </GoogleMap>
            )
            }
        </div>
    );
};
