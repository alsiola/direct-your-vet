define(function () {
    const Place = require('../dashboard/place');
    
        return class DayListPlace {

            constructor(data) {
                var self = this;

                this.clientName = data.clientName;
                this.dayListId = ko.observable(data.dayListId);
                this.zoomLevel = ko.observable(data.zoomLevel);
                this.place = new Place(data.place);
                this.latLng = new google.maps.LatLng(this.place.latitude(), this.place.longitude());
                this.divId = 'map-' + this.place.id();
            }

            mapInit() {
                this.map = new google.maps.Map(document.getElementById(this.divId), {
                    center: this.latLng,
                    zoom: this.zoomLevel(),
                    disableDoubleClickZoom: true,
                    streetViewControl: false
                });

                this.map.addListener("zoom_changed",() => {
                    this.zoomLevel(this.map.getZoom());
                });

                this.marker = new google.maps.Marker({
                    position: this.latLng,
                    map: this.map,
                    title: this.place.name()
                });
            }            
        };
});
