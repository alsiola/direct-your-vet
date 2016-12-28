webpackJsonp([5],{

/***/ 13:
/***/ function(module, exports, __webpack_require__) {

"use strict";
var __WEBPACK_AMD_DEFINE_RESULT__;'use strict';

!(__WEBPACK_AMD_DEFINE_RESULT__ = function () {
    var DYVMaps = __webpack_require__(3);
    new DYVMaps();
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));

/***/ },

/***/ 3:
/***/ function(module, exports, __webpack_require__) {

"use strict";
/* WEBPACK VAR INJECTION */(function(gmaps) {var __WEBPACK_AMD_DEFINE_RESULT__;"use strict";

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

!(__WEBPACK_AMD_DEFINE_RESULT__ = function () {
    return function () {
        function DYVMaps() {
            var _this = this;

            _classCallCheck(this, DYVMaps);

            $('#geoLocateButton').click(function () {
                _this.GeoLocate().then(function (pos) {
                    if (pos) {
                        _this.map.setCenter(pos);
                        _this.AddMarker(pos);
                        _this.map.setZoom(5);
                    }
                });
            });

            gmaps.KEY = "AIzaSyDmN8YtEYRrv5rkzljADvn82I2pwCbaWlE";
            gmaps.LIBRARIES = ["places"];

            gmaps.load(function (google) {
                _this.mapInit();
            });
        }

        _createClass(DYVMaps, [{
            key: "mapInit",
            value: function mapInit() {
                var _this2 = this;

                console.log("initmap");

                this.map = new google.maps.Map(document.getElementById('map'), {
                    center: new google.maps.LatLng(52, 52),
                    zoom: 5,
                    disableDoubleClickZoom: true,
                    streetViewControl: false
                });

                if ($('#isSubscriber').val() !== "subscriber") {
                    this.map.addListener('dblclick', function (e) {
                        var pos = {
                            lat: e.latLng.lat(),
                            lng: e.latLng.lng()
                        };
                        _this2.AddMarker(pos, google.maps.Animation.DROP);
                    });
                } else {
                    $('#myLocButton').click(function () {
                        console.log("cl");
                        _this2.GeoLocate().then(function (pos) {

                            if (_this2.selfMarker !== undefined) {
                                _this2.selfMarker.setMap(null);
                            }
                            if (pos) {
                                _this2.selfMarker = new google.maps.Marker({
                                    position: pos,
                                    map: _this2.map,
                                    icon: 'http://' + window.location.host + '/dist/img/car-icon.png'
                                });

                                var bounds = new google.maps.LatLngBounds();
                                bounds.extend(pos);
                                bounds.extend(_this2.marker.getPosition());
                                _this2.map.fitBounds(bounds);
                            }
                        });
                    });
                }

                var input = document.getElementById('mapsearch');

                this.map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

                this.searchBox = new google.maps.places.SearchBox(input);

                this.searchBox.addListener('places_changed', function () {
                    var places = _this2.searchBox.getPlaces();

                    if (places.length === 0) {
                        return;
                    }

                    _this2.map.panTo(places[0].geometry.location);
                    _this2.map.setZoom(13);
                });

                if ($('#isEdit').val() === "edit" || $('#isSubscriber').val() === "subscriber") {
                    console.log("initmarker");
                    var latLng = new google.maps.LatLng($('#Latitude').val(), $('#Longitude').val());
                    this.AddMarker(latLng, null);
                    this.map.setCenter(latLng);
                    this.map.setZoom(12);
                }
            }
        }, {
            key: "AddMarker",
            value: function AddMarker(latLng, animation, iconUrl) {

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
        }, {
            key: "GeoLocate",
            value: function GeoLocate() {
                var dfd = $.Deferred();
                // Try HTML5 geolocation.
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(function (position) {
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
                    alert(browserHasGeolocation ? 'Error: The Geolocation service failed.' : 'Error: Your browser doesn\'t support geolocation.');
                    dfd.resolve(null);
                }

                return dfd.promise();
            }
        }]);

        return DYVMaps;
    }();
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(1)))

/***/ }

},[13]);