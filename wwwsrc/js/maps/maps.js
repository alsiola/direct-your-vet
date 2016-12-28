define(function() {
    return class DYVMaps {
        constructor() {

            $('#geoLocateButton').click(() => {
                this.GeoLocate().then(pos => {
                    if (pos) {
                        this.map.setCenter(pos);
                        this.AddMarker(pos);
                        this.map.setZoom(5);
                    }
                });
            });

            gmaps.KEY = "REDACTED";
            gmaps.LIBRARIES = ["places"];

            gmaps.load(google => {
                this.mapInit();
            });
        }

        mapInit() {
            console.log("initmap");

            this.map = new google.maps.Map(document.getElementById('map'), {
                center: new google.maps.LatLng(52, 52),
                zoom: 5,
                disableDoubleClickZoom: true,
                streetViewControl: false
            });

            if ($('#isSubscriber').val() !== "subscriber") {
                this.map.addListener('dblclick', e => {
                    var pos = {
                        lat: e.latLng.lat(),
                        lng: e.latLng.lng()
                    };
                    this.AddMarker(pos, google.maps.Animation.DROP);
                });
            } else {
                $('#myLocButton').click(() => {
                    console.log("cl");
                    this.GeoLocate().then(pos => {

                        if (this.selfMarker !== undefined) {
                            this.selfMarker.setMap(null);
                        }
                        if (pos) {
                            this.selfMarker = new google.maps.Marker({
                                position: pos,
                                map: this.map,
                                icon: 'http://' + window.location.host + '/dist/img/car-icon.png'
                            });

                            const bounds = new google.maps.LatLngBounds();
                            bounds.extend(pos);
                            bounds.extend(this.marker.getPosition());
                            this.map.fitBounds(bounds);
                        }
                    });
                });
            }

            const input = document.getElementById('mapsearch');

            this.map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

            this.searchBox = new google.maps.places.SearchBox(input);

            this.searchBox.addListener('places_changed', () => {
                const places = this.searchBox.getPlaces();

                if (places.length === 0) {
                    return;
                }

                this.map.panTo(places[0].geometry.location);
                this.map.setZoom(13);
            });

            if ($('#isEdit').val() === "edit" || $('#isSubscriber').val() === "subscriber") {
                console.log("initmarker");
                const latLng = new google.maps.LatLng($('#Latitude').val(), $('#Longitude').val());
                this.AddMarker(latLng, null);
                this.map.setCenter(latLng);
                this.map.setZoom(12);
            }
        }

        AddMarker(latLng, animation, iconUrl) {

            // remove existing marker if present
            if (this.marker !== undefined) {
                this.marker.setMap(null);
            }

            this.marker = new google.maps.Marker({
                position: latLng,
                map: this.map,
                title: 'Your Location',
                animation: animation,
                icon: iconUrl
            });

            $('#Latitude').val(latLng.lat);
            $('#Longitude').val(latLng.lng);
        }

        GeoLocate() {
            const dfd = $.Deferred();
            // Try HTML5 geolocation.
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(position => {
                    dfd.resolve({
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    });
                }, function () {
                    handleLocationError(true);
                });
            } else {
                // Browser doesn't support Geolocation
                handleLocationError(false);
            }

            function handleLocationError(browserHasGeolocation) {
                alert(browserHasGeolocation ?
                                      'Error: The Geolocation service failed.' :
                                      'Error: Your browser doesn\'t support geolocation.');
                dfd.resolve(null);
            }

            return dfd.promise();
        }
    };
});
