define(
    function () {
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
                    data: JSON.stringify(
                        {
                            'SubscriberUserId': self.id(),
                            'PracticeId': self.practiceId()
                        }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log(response);
                        if (response.success) {
                            nm.notyMessage("User removed.");
                        } else {
                            nm.notyMessage("Removing failed.");
                        }
                    },
                    error: function () {
                        nm.notyMessage("Could not reach server.");
                    }
                });
            };

            self.makeManager = function (newIsManager) {
                return $.ajax({
                    url: self.urlBase + "MakeUserManager",
                    type: "POST",
                    data: JSON.stringify(
                        {
                            'SubscriberUserId': self.id(),
                            'PracticeId': self.practiceId(),
                            'isManager' : newIsManager
                        }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log(response);
                        if (response.success) {
                            nm.notyMessage("Manager change succeeded.");
                        } else {
                            nm.notyMessage("Manager change failed.");
                        }
                    },
                    error: function () {
                        nm.notyMessage("Could not reach server.");
                    }
                });
            };
        };

    }
);