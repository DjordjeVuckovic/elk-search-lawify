import {useState} from 'react';
import {GoogleMap, useLoadScript, Marker, CircleF} from '@react-google-maps/api';
import {Loader} from "../shared/ui/loader/loader.tsx";
import {FaSearch} from "react-icons/fa";
import {Button} from "../shared/ui/button/button.tsx";
import {ContractHit, GeoSearchRequest} from "./types/contract.ts";
import {useMutation, useQueryClient} from "react-query";
import {geoSearch} from "./contract-service.ts";
import {toastError, toastSuccess} from "../shared/toast/toast.ts";
import {FileTypes} from "../shared/types/file-types.ts";
import contractIcon from "../assets/contract.png";
import {highlightLightShort} from "../shared/utils/search.ts";
import {DocumentCard} from "../shared/ui/file-card/document-card.tsx";
import {downloadFile} from "../shared/services/content-service.ts";
import {AxiosError} from "axios";
import useContractStore from "./store/contracts-store.ts";
import {useNavigate} from "react-router-dom";

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
    const {isLoaded} = useLoadScript({
        googleMapsApiKey: API_KEY
    });
    const [circleCenter, setCircleCenter] = useState<LatLng>(center);
    const [circleRadius, setCircleRadius] = useState<number>(1000);
    const [markerPosition, setMarkerPosition] = useState<LatLng | null>(null);
    const [documents, setDocuments] = useState<ContractHit[]>([]);
    const queryClient = useQueryClient();
    const {setContract} = useContractStore();
    const navigate = useNavigate();

    const {mutate} = useMutation(
        geoSearch,
        {
            onSuccess: (data) => {
                // Handle the successful response here
                console.log('Documents fetched:', data);
                toastSuccess("Documents geo data fetched successfully!")
                // Store the fetched data in state
                // You can update a query here if needed
                setDocuments(data);
                queryClient.setQueryData('documents', data);
            },
            onError: (error) => {
                console.log('Error while fetching documents:', error);
                setDocuments([]);
            }
        }
    );

    const downloadMutation = useMutation(downloadFile, {
        onError: (error: AxiosError) => {
            console.log(error)
            toastError('Error while downloading file')
        },
        onSuccess: () => {
            toastSuccess('You are successfully downloaded file!')
        }
    });
    const onLoad = async (_: google.maps.Map) => {
        console.log('map loaded')
    };
    const handleRadiusChange = (event: any) => {
        const newRadius = parseFloat(event.target.value);
        if (!isNaN(newRadius)) {
            setCircleRadius(newRadius);
        }
    };
    const handleMapClick = (event: any) => {
        if (!event.latLng) {
            console.log('lat:', event.latLng.lat(), 'lng:', event.latLng.lng());
        }
        const newLat = event.latLng.lat();
        const newLng = event.latLng.lng();
        setCircleCenter({lat: newLat, lng: newLng});
        setMarkerPosition({lat: newLat, lng: newLng});
    };
    const searchGeo = () => {
        console.log('searching geo')
        const geoReq: GeoSearchRequest = {
            lat: circleCenter.lat,
            lon: circleCenter.lng,
            radiusM: circleRadius
        }
        mutate(geoReq);
    }
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
                <Button onClick={searchGeo} type={'submit'}>Search <FaSearch/></Button>
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
                        <>
                            <CircleF
                                center={circleCenter || center}
                                radius={circleRadius}
                                options={{
                                    strokeColor: 'blue',
                                    strokeOpacity: 0.8,
                                    strokeWeight: 2,
                                    fillColor: 'blue',
                                    fillOpacity: 0.35
                                }}
                            />
                            {markerPosition && (
                                <Marker
                                    position={markerPosition}
                                    icon={{
                                        path: google.maps.SymbolPath.CIRCLE,
                                        scale: 10,
                                        fillColor: 'red',
                                        fillOpacity: 1,
                                        strokeColor: 'white',
                                        strokeWeight: 1,
                                    }}
                                />
                            )}
                            {documents.map((hit) => (
                                <Marker
                                    key={hit.id}
                                    position={{lat: hit.geoLocation!.lat, lng: hit.geoLocation!.lng}}
                                />
                            ))}
                            {documents.map((hit) => (
                                <Marker
                                    key={hit.id}
                                    position={{lat: hit.geoLocation!.lat, lng: hit.geoLocation!.lng}}
                                />
                            ))}
                        </>
                    )}
                </GoogleMap>
            )
            }
            <div className={'grid-3-c'}>
                {
                    documents.map((contract) =>
                        <DocumentCard fileName={contract.fileName}
                                      fileType={FileTypes.Contract}
                                      imageRef={contractIcon}
                                      key={contract.id}
                                      shortHighlight={highlightLightShort(contract.highlight!)}
                                      onDownloadClick={() => {
                                          downloadMutation.mutate({
                                              filename: contract.fileName,
                                              type: FileTypes.Law
                                          })
                                      }}
                                      onViewClick={() => {
                                          setContract(contract);
                                          navigate('/contracts/item')
                                      }}
                        />)
                }
            </div>
        </div>
    );
};
