webpackJsonp([1],{

/***/ 12:
/***/ function(module, exports, __webpack_require__) {

"use strict";
/* WEBPACK VAR INJECTION */(function(nm) {var __WEBPACK_AMD_DEFINE_RESULT__;"use strict";

!(__WEBPACK_AMD_DEFINE_RESULT__ = function () {
    return function SubscriberUserViewModel(data, practiceId) {
        var self = this;

        self.id = ko.observable(data.id);
        self.practiceId = ko.observable(practiceId);
        self.name = ko.observable(data.name);
        self.email = ko.observable(data.email);
        self.isManager = ko.observable(data.isManager);

        self.isSubscriber = ko.computed(function () {
            return !self.isManager();
        });

        self.role = ko.computed(function () {
            if (self.isManager()) {
                return "Practice Manager";
            } else {
                return "Subscriber";
            }
        });

        self.urlBase = "http://" + window.location.host + "/Admin/";

        self.remove = function () {
            return $.ajax({
                url: self.urlBase + "RemoveUserFromPractice",
                type: "POST",
                data: JSON.stringify({
                    'SubscriberUserId': self.id(),
                    'PracticeId': self.practiceId()
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function success(response) {
                    console.log(response);
                    if (response.success) {
                        nm.notyMessage("User removed.");
                    } else {
                        nm.notyMessage("Removing failed.");
                    }
                },
                error: function error() {
                    nm.notyMessage("Could not reach server.");
                }
            });
        };

        self.makeManager = function (newIsManager) {
            return $.ajax({
                url: self.urlBase + "MakeUserManager",
                type: "POST",
                data: JSON.stringify({
                    'SubscriberUserId': self.id(),
                    'PracticeId': self.practiceId(),
                    'isManager': newIsManager
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function success(response) {
                    console.log(response);
                    if (response.success) {
                        nm.notyMessage("Manager change succeeded.");
                    } else {
                        nm.notyMessage("Manager change failed.");
                    }
                },
                error: function error() {
                    nm.notyMessage("Could not reach server.");
                }
            });
        };
    };
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(0)))

/***/ },

/***/ 16:
/***/ function(module, exports, __webpack_require__) {

"use strict";
var __WEBPACK_AMD_DEFINE_RESULT__;'use strict';

!(__WEBPACK_AMD_DEFINE_RESULT__ = function (require) {
    var EditPracticeViewModel = __webpack_require__(6);

    ko.applyBindings(new EditPracticeViewModel());
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));

/***/ },

/***/ 6:
/***/ function(module, exports, __webpack_require__) {

"use strict";
/* WEBPACK VAR INJECTION */(function(nm) {var __WEBPACK_AMD_DEFINE_RESULT__;'use strict';

!(__WEBPACK_AMD_DEFINE_RESULT__ = function (require) {

    var SubscriberUserViewModel = __webpack_require__(12);

    return function EditPracticeViewModel() {
        var self = this;

        self.loading = ko.observable(true);
        self.noData = ko.observable(true);
        self.errorData = ko.observable(false);
        self.hasData = ko.computed(function () {
            return !self.noData();
        });
        self.saving = ko.observable(false);

        self.lastGoodName = ko.observable();
        self.practiceName = ko.observable();
        self.subscriberUsers = ko.observableArray();
        self.id = ko.observable();

        self.practiceDetails = function () {
            return {
                'id': self.id(),
                'name': self.practiceName()
            };
        };

        self.urlBase = "http://" + window.location.host + "/Admin/";
        self.apiUrl = self.urlBase + "EditDetails/" + $('#practiceId').val();

        self.removeUser = function (user) {
            nm.notyConfirm("Do you wish to remove " + user.name() + " from the practice?").then(function (response) {
                if (response) {
                    user.remove().then(function () {
                        self.loadData();
                    });
                }
            });
        };

        self.makeManager = function (user, managerStatus) {
            nm.notyConfirm(managerStatus ? "Make " + user.name() + " a manager?" : "Remove " + user.name() + " as manager?").then(function (response) {
                if (response) {
                    user.makeManager(managerStatus).then(function () {
                        self.loadData();
                    });
                }
            });
        };

        self.saveName = function () {
            self.saving(true);
            $.ajax({
                url: self.urlBase + "SaveDetails",
                type: "POST",
                data: JSON.stringify(self.practiceDetails()),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function success(response) {
                    console.log(response);
                    if (response.success) {
                        nm.notyMessage("Practice name saved.");
                    } else {
                        nm.notyMessage("Saving failed.");
                        self.practiceName(self.lastGoodName());
                    }
                },
                error: function error() {
                    nm.notyMessage("Could not reach server.");
                    self.practiceName(self.lastGoodName());
                },
                complete: function complete() {
                    self.saving(false);
                }
            });
        };

        self.loadData = function () {
            self.loading(true);
            $.ajax({
                type: "GET",
                dataType: "json",
                url: self.apiUrl,
                success: function success(data) {
                    if (data === null) {
                        self.errorData(true);
                    } else {
                        self.id(data.id);
                        self.practiceName(data.name);
                        self.lastGoodName(data.name);
                        var temp = [];
                        data.subscriberUsers.forEach(function (user) {
                            temp.push(new SubscriberUserViewModel(user, data.id));
                        });
                        self.subscriberUsers(temp);
                        self.noData(false);
                    }
                },
                error: function error() {
                    self.errorData(true);
                },
                complete: function complete() {
                    self.loading(false);
                }
            });
        };

        self.loadData();
    };
}.call(exports, __webpack_require__, exports, module), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(0)))

/***/ }

},[16]);