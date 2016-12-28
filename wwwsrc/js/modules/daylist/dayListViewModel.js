define(function () {
    const DayListPlace = require('./dayListPlace');

            return class DayListViewModel {
            
                constructor() {
                    let self = this;

                    this.rendered = $.Deferred();

                    this.urlBase = "http://" + window.location.host + "/SubscriberDashboard/";
                    this.apiUrl = this.urlBase + "GetDayListDetails/" + $('#dayListId').val();
                    this.saveUrl = this.urlBase + "SaveDayListDetails";

                    this.dayList = ko.observableArray([]);
                    this.dayListId = ko.observable();
                    this.dayListName = ko.observable();
                    this.dateAdded = ko.observable();

                    this.errorData = ko.observable(false);
                    this.loading = ko.observable(true);

                    this.noData = ko.observable(true);
                    this.hasData = ko.computed(() => {
                        return !this.noData();
                    });

                    this.title = ko.computed(() => {
                        return this.dayListName() + " (" + this.dateAdded() + ")";
                    });

                    this.allPlacesMap = ko.observable();
                    this.allPlacesMapMarkers = ko.observableArray();
                    this.allPlacesBound = new google.maps.LatLngBounds();

                    this.allPlacesZoom = ko.observable(0);

                    $.when(this.initialData(), this.rendered).then(() => {
                        this.initiateMaps();
                    });
                }

                initialData() {
                    this.loading(true);
                    const dfd = $.Deferred();
                    $.ajax({
                        type: "GET",
                        dataType: "json",
                        url: this.apiUrl,
                        success: data => {
                            if (data === null) {
                                this.dayListName("Day List Not Found");
                                this.dateAdded("");
                                this.errorData(true);
                            } else {
                                this.dayListId(data.id);
                                this.dayListName(data.name);
                                this.dateAdded(data.dateAdded);

                                if (data.allPlacesMapZoom > 0) {
                                    this.allPlacesZoom(data.allPlacesMapZoom);
                                }

                                var temp = [];
                                data.dayListPlaces.forEach(function (dayListPlace) {
                                    temp.push(new DayListPlace(dayListPlace));
                                });
                                this.dayList(temp);
                                this.noData(false);
                            }
                        },
                        error: () => {
                            this.errorData(true);
                        },
                        complete: () => {
                            this.loading(false);
                            dfd.resolve();
                        }
                    });
                    return dfd.promise();
                }

                renderComplete() {
                    this.rendered.resolve();
                }

                initiateMaps() {
                    if (this.dayList && $('#rowContainer').children().length === this.dayList().length) {

                        const firstLatLng = this.dayList()[0].latLng;

                        this.allPlacesMap = new google.maps.Map(document.getElementById('allPlacesMap'), {
                            center: firstLatLng,
                            zoom: 3,
                            disableDoubleClickZoom: true,
                            streetViewControl: false
                        });

                        this.allPlacesMap.addListener("zoom_changed", () => {
                            this.allPlacesZoom(this.allPlacesMap.getZoom());
                        });

                        this.dayList().forEach(dlp => {
                            dlp.mapInit();

                            this.allPlacesBound.extend(dlp.latLng);

                            let marker = new google.maps.Marker(
                                {
                                    position: dlp.latLng,
                                    map: this.allPlacesMap,
                                    title: dlp.place.name()
                                }
                            );

                            let infoWindow = new google.maps.InfoWindow({
                                content: dlp.place.name()
                            });

                            marker.addListener("click", () => {
                                infoWindow.open(this.allPlacesMap, marker);
                            });

                            this.allPlacesMapMarkers.push(marker);
                        });

                        if (this.allPlacesZoom() === 0) {
                            this.resetBounds();
                        } else {
                            this.allPlacesMap.setZoom(this.allPlacesZoom());
                        }
                    }
                }

                resetBounds() {
                    this.allPlacesMap.fitBounds(this.allPlacesBound);
                    this.allPlacesZoom(this.allPlacesMap.getZoom());
                }

                printPage() {
                    window.print();
                }

                saveZooms() {
                    const placeData = [];

                    for (var place of this.dayList()) {
                        placeData.push({
                            'dayListId': place.dayListId(),
                            'placeId': place.place.id(),
                            'zoomLevel': place.zoomLevel()
                        });
                    }

                    const data = {
                        "allPlacesMapZoom": this.allPlacesZoom(),
                        "placeZooms": placeData
                    };

                    $.ajax({
                        url: this.saveUrl,
                        type: "POST",
                        data: JSON.stringify(data),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: (response) => {
                            if (response.success) {
                                nm.notyMessage("Save successful.");
                            } else {
                                nm.notyMessage("Save failed.");
                            }
                        },
                        error: () => {
                            nm.notyMessage("Could not reach server.");
                        }
                    });
                }
        };        
});

