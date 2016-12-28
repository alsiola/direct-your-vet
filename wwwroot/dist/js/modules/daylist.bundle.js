webpackJsonp([2],{

/***/ 11:
/***/ function(module, exports, __webpack_require__) {

"use strict";
var __WEBPACK_AMD_DEFINE_RESULT__;'use strict';

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

!(__WEBPACK_AMD_DEFINE_RESULT__ = function () {
    var Place = __webpack_require__(2);

    return function () {
        function DayListPlace(data) {
            _classCallCheck(this, DayListPlace);

            var self = this;

            this.clientName = data.clientName;
            this.dayListId = ko.observable(data.dayListId);
            this.zoomLevel = ko.observable(data.zoomLevel);
            this.place = new Place(data.place);
            this.latLng = new google.maps.LatLng(this.place.latitude(), this.place.longitude());
            this.divId = 'map-' + this.place.id();
        }

        _createClass(DayListPlace, [{
            key: 'mapInit',
            value: function mapInit() {
                var _this = this;

                this.map = new google.maps.Map(document.getElementById(this.divId), {
                    center: this.latLng,
                    zoom: this.zoomLevel(),
                    disableDoubleClickZoom: true,
                    streetViewControl: false
                });

                this.map.addListener("zoom_changed", function () {
                    _this.zoomLevel(_this.map.getZoom());
                });

                this.marker = new google.maps.Marker({
                    position: this.latLng,
                    map: this.map,
                    title: this.place.name()
                });
            }
        }]);

        return DayListPlace;
    }();
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));

/***/ },

/***/ 15:
/***/ function(module, exports, __webpack_require__) {

"use strict";
/* WEBPACK VAR INJECTION */(function(gmaps) {var __WEBPACK_AMD_DEFINE_RESULT__;"use strict";

!(__WEBPACK_AMD_DEFINE_RESULT__ = function (require) {
    var DayListViewModel = __webpack_require__(5);

    gmaps.KEY = "AIzaSyDmN8YtEYRrv5rkzljADvn82I2pwCbaWlE";
    gmaps.LIBRARIES = ["places"];

    gmaps.load(function (google) {
        ko.applyBindings(new DayListViewModel());
    });
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(1)))

/***/ },

/***/ 5:
/***/ function(module, exports, __webpack_require__) {

"use strict";
/* WEBPACK VAR INJECTION */(function(nm) {var __WEBPACK_AMD_DEFINE_RESULT__;"use strict";

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

!(__WEBPACK_AMD_DEFINE_RESULT__ = function () {
    var DayListPlace = __webpack_require__(11);

    return function () {
        function DayListViewModel() {
            var _this = this;

            _classCallCheck(this, DayListViewModel);

            var self = this;

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
            this.hasData = ko.computed(function () {
                return !_this.noData();
            });

            this.title = ko.computed(function () {
                return _this.dayListName() + " (" + _this.dateAdded() + ")";
            });

            this.allPlacesMap = ko.observable();
            this.allPlacesMapMarkers = ko.observableArray();
            this.allPlacesBound = new google.maps.LatLngBounds();

            this.allPlacesZoom = ko.observable(0);

            $.when(this.initialData(), this.rendered).then(function () {
                _this.initiateMaps();
            });
        }

        _createClass(DayListViewModel, [{
            key: "initialData",
            value: function initialData() {
                var _this2 = this;

                this.loading(true);
                var dfd = $.Deferred();
                $.ajax({
                    type: "GET",
                    dataType: "json",
                    url: this.apiUrl,
                    success: function success(data) {
                        if (data === null) {
                            _this2.dayListName("Day List Not Found");
                            _this2.dateAdded("");
                            _this2.errorData(true);
                        } else {
                            _this2.dayListId(data.id);
                            _this2.dayListName(data.name);
                            _this2.dateAdded(data.dateAdded);

                            if (data.allPlacesMapZoom > 0) {
                                _this2.allPlacesZoom(data.allPlacesMapZoom);
                            }

                            var temp = [];
                            data.dayListPlaces.forEach(function (dayListPlace) {
                                temp.push(new DayListPlace(dayListPlace));
                            });
                            _this2.dayList(temp);
                            _this2.noData(false);
                        }
                    },
                    error: function error() {
                        _this2.errorData(true);
                    },
                    complete: function complete() {
                        _this2.loading(false);
                        dfd.resolve();
                    }
                });
                return dfd.promise();
            }
        }, {
            key: "renderComplete",
            value: function renderComplete() {
                this.rendered.resolve();
            }
        }, {
            key: "initiateMaps",
            value: function initiateMaps() {
                var _this3 = this;

                if (this.dayList && $('#rowContainer').children().length === this.dayList().length) {

                    var firstLatLng = this.dayList()[0].latLng;

                    this.allPlacesMap = new google.maps.Map(document.getElementById('allPlacesMap'), {
                        center: firstLatLng,
                        zoom: 3,
                        disableDoubleClickZoom: true,
                        streetViewControl: false
                    });

                    this.allPlacesMap.addListener("zoom_changed", function () {
                        _this3.allPlacesZoom(_this3.allPlacesMap.getZoom());
                    });

                    this.dayList().forEach(function (dlp) {
                        dlp.mapInit();

                        _this3.allPlacesBound.extend(dlp.latLng);

                        var marker = new google.maps.Marker({
                            position: dlp.latLng,
                            map: _this3.allPlacesMap,
                            title: dlp.place.name()
                        });

                        var infoWindow = new google.maps.InfoWindow({
                            content: dlp.place.name()
                        });

                        marker.addListener("click", function () {
                            infoWindow.open(_this3.allPlacesMap, marker);
                        });

                        _this3.allPlacesMapMarkers.push(marker);
                    });

                    if (this.allPlacesZoom() === 0) {
                        this.resetBounds();
                    } else {
                        this.allPlacesMap.setZoom(this.allPlacesZoom());
                    }
                }
            }
        }, {
            key: "resetBounds",
            value: function resetBounds() {
                this.allPlacesMap.fitBounds(this.allPlacesBound);
                this.allPlacesZoom(this.allPlacesMap.getZoom());
            }
        }, {
            key: "printPage",
            value: function printPage() {
                window.print();
            }
        }, {
            key: "saveZooms",
            value: function saveZooms() {
                var placeData = [];

                var _iteratorNormalCompletion = true;
                var _didIteratorError = false;
                var _iteratorError = undefined;

                try {
                    for (var _iterator = this.dayList()[Symbol.iterator](), _step; !(_iteratorNormalCompletion = (_step = _iterator.next()).done); _iteratorNormalCompletion = true) {
                        var place = _step.value;

                        placeData.push({
                            'dayListId': place.dayListId(),
                            'placeId': place.place.id(),
                            'zoomLevel': place.zoomLevel()
                        });
                    }
                } catch (err) {
                    _didIteratorError = true;
                    _iteratorError = err;
                } finally {
                    try {
                        if (!_iteratorNormalCompletion && _iterator.return) {
                            _iterator.return();
                        }
                    } finally {
                        if (_didIteratorError) {
                            throw _iteratorError;
                        }
                    }
                }

                var data = {
                    "allPlacesMapZoom": this.allPlacesZoom(),
                    "placeZooms": placeData
                };

                $.ajax({
                    url: this.saveUrl,
                    type: "POST",
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function success(response) {
                        if (response.success) {
                            nm.notyMessage("Save successful.");
                        } else {
                            nm.notyMessage("Save failed.");
                        }
                    },
                    error: function error() {
                        nm.notyMessage("Could not reach server.");
                    }
                });
            }
        }]);

        return DayListViewModel;
    }();
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(0)))

/***/ }

},[15]);