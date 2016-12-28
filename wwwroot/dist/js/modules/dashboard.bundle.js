webpackJsonp([0],{

/***/ 10:
/***/ function(module, exports, __webpack_require__) {

"use strict";
var __WEBPACK_AMD_DEFINE_RESULT__;"use strict";

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

!(__WEBPACK_AMD_DEFINE_RESULT__ = function () {
    return function SaveDayList(data) {
        _classCallCheck(this, SaveDayList);

        this.name = data.name;
        this.dateAdded = data.dateAdded;
        this.detailsUrl = "/SubscriberDashboard/DayLists/" + data.id;
    };
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));

/***/ },

/***/ 14:
/***/ function(module, exports, __webpack_require__) {

"use strict";
var __WEBPACK_AMD_DEFINE_RESULT__;'use strict';

!(__WEBPACK_AMD_DEFINE_RESULT__ = function (require) {
    var DashboardViewModel = __webpack_require__(4);
    ko.applyBindings(new DashboardViewModel());
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));

/***/ },

/***/ 4:
/***/ function(module, exports, __webpack_require__) {

"use strict";
var __WEBPACK_AMD_DEFINE_RESULT__;'use strict';

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

!(__WEBPACK_AMD_DEFINE_RESULT__ = function () {
    var Place = __webpack_require__(2),
        DayList = __webpack_require__(9);

    return function () {
        function DashboardViewModel() {
            var _this = this;

            _classCallCheck(this, DashboardViewModel);

            this.places = ko.observableArray([]);

            this.searchClient = ko.observable("").extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 400 } });
            this.searchPlace = ko.observable("").extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 400 } });
            this.searchAddress = ko.observable("").extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 400 } });

            this.hasResults = ko.observable(false);

            this.dayList = ko.observable(new DayList());

            this.page = ko.observable(1);
            this.numPerPage = ko.observable(5);
            this.numResults = ko.observable(0);

            this.numPages = ko.computed(function () {
                return Math.ceil(_this.numResults() / _this.numPerPage());
            });

            this.pageArray = ko.computed(function () {
                var pageArr = [];
                for (var i = 0; i < _this.numPages(); i++) {
                    pageArr.push(i);
                }
                return pageArr;
            });

            this.startedDayList = ko.computed(function () {
                return _this.dayList().dayListPlaces().length > 0;
            });

            this.resultSizes = ko.observableArray([3, 5, 10, 20, 50, 100]);

            ko.computed(function () {
                _this.hasResults(false);
                var apiUrl = "/SearchPlaces/?page=" + _this.page() + "&take=" + _this.numPerPage() + "&placeTerm=" + _this.searchPlace() + "&clientTerm=" + _this.searchClient() + "&addressTerm=" + _this.searchAddress();
                return $.ajax({
                    type: "GET",
                    dataType: "json",
                    url: window.location.href + apiUrl,
                    success: function success(data) {
                        var temp = [];

                        data.places.forEach(function (place) {
                            temp.push(new Place(place));
                        });

                        _this.places(temp);
                        _this.numResults(data.totalResults);

                        if ((_this.page() - 1) * _this.numPerPage() > data.totalResults) {
                            _this.page(1);
                        }

                        _this.hasResults(true);
                    }
                });
            }).extend({ deferred: true });

            this.dayList().loadDayLists();
        }

        _createClass(DashboardViewModel, [{
            key: 'clearSearch',
            value: function clearSearch() {
                this.searchClient("");
                this.searchAddress("");
                this.searchPlace("");
            }
        }, {
            key: 'setPage',
            value: function setPage(pageNum) {
                if (pageNum > 0 && pageNum <= this.numPages()) {
                    this.page(pageNum);
                }
            }
        }, {
            key: 'log',
            value: function log(item) {
                console.log(item);
            }
        }]);

        return DashboardViewModel;
    }();
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));

/***/ },

/***/ 9:
/***/ function(module, exports, __webpack_require__) {

"use strict";
/* WEBPACK VAR INJECTION */(function(nm) {var __WEBPACK_AMD_DEFINE_RESULT__;"use strict";

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

!(__WEBPACK_AMD_DEFINE_RESULT__ = function () {

    var SavedDayList = __webpack_require__(10);

    return function () {
        function DayList() {
            _classCallCheck(this, DayList);

            this.name = ko.observable("New Day List");

            this.dayListPlaces = ko.observableArray([]);

            this.savedDayLists = ko.observableArray([]);

            ko.bindingHandlers.slideOutUp = {
                init: function init(element, valueAccessor) {
                    // Initially set the element to be instantly visible/hidden depending on the value
                    var value = valueAccessor();
                    $(element).toggle(ko.unwrap(value)); // Use "unwrapObservable" so we can handle values that may or may not be observable
                },
                update: function update(element, valueAccessor) {
                    // Whenever the value subsequently changes, slowly fade the element in or out
                    var value = valueAccessor();
                    ko.unwrap(value) ? $(element).slideDown() : $(element).slideUp();
                }
            };
        }

        _createClass(DayList, [{
            key: "canAddToList",
            value: function canAddToList(item) {
                return !this.dayListPlaces().some(function (current) {
                    return current.id() === item.id();
                });
            }
        }, {
            key: "canRemoveFromList",
            value: function canRemoveFromList(item) {
                return this.dayListPlaces().some(function (current) {
                    return current.id() === item.id();
                });
            }
        }, {
            key: "tryDiscard",
            value: function tryDiscard() {
                var _this = this;

                nm.notyConfirm("Discard this daylist?").then(function (status) {
                    if (status) {
                        _this.discard();
                    }
                });
            }
        }, {
            key: "discard",
            value: function discard() {
                this.name("New Day List");
                this.dayListPlaces.removeAll();
            }
        }, {
            key: "addToList",
            value: function addToList(place) {
                if (this.dayListPlaces.indexOf(place) < 0) {
                    this.dayListPlaces.push(place);
                }
            }
        }, {
            key: "removeFromList",
            value: function removeFromList(place) {
                var temp = [];
                this.dayListPlaces().forEach(function (current) {
                    if (current.id() !== place.id()) {
                        temp.push(current);
                    }
                });
                this.dayListPlaces(temp);
            }
        }, {
            key: "save",
            value: function save() {
                var _this2 = this;

                $.ajax({
                    url: window.location.href + "/SaveDayList/",
                    type: "POST",
                    data: ko.toJSON(this),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function success(data) {
                        if (data.success) {
                            nm.notyMessage("Daylist saved.");
                            _this2.loadDayLists();
                            _this2.discard();
                        } else {
                            data.errors.forEach(function (error) {
                                nm.notyMessage(error);
                            });
                        }
                    },
                    error: function error() {
                        nm.notyMessage("Server unavailable, daylist has not been saved.");
                    }
                });
            }
        }, {
            key: "loadDayLists",
            value: function loadDayLists() {
                var _this3 = this;

                $.ajax({
                    type: "GET",
                    dataType: "json",
                    url: window.location.href + "/DayLists/",
                    success: function success(data) {
                        var temp = [];
                        data.dayLists.forEach(function (dayList) {
                            temp.push(new SavedDayList(dayList));
                        });
                        _this3.savedDayLists(temp);
                    }
                });
            }
        }, {
            key: "fadeOut",
            value: function fadeOut(elem) {
                $(elem).slideUp(function () {
                    $(elem).remove();
                });
            }
        }, {
            key: "fadeIn",
            value: function fadeIn(elem) {
                $(elem).hide().fadeIn();
            }
        }]);

        return DayList;
    }();
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(0)))

/***/ }

},[14]);