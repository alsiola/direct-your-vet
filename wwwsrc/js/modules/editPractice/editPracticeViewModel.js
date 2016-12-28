define(function (require) {

    const SubscriberUserViewModel = require('./subscriberUserViewModel');

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

        self.practiceDetails = function() {
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
                success: function (response) {
                    console.log(response);
                    if (response.success) {
                        nm.notyMessage("Practice name saved.");
                    } else {
                        nm.notyMessage("Saving failed.");
                        self.practiceName(self.lastGoodName());
                    }
                },
                error: function () {
                    nm.notyMessage("Could not reach server.");
                    self.practiceName(self.lastGoodName());
                },
                complete: function () {
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
                success: function (data) {
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
                error: function() {
                    self.errorData(true);
                },
                complete: function () {
                    self.loading(false);
                }
            });
        };

        self.loadData();
    };
});