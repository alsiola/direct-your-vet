define(function () {

    return function sendSMSViewModel() {
        var self = this;

        self.urlBase = "http://" + window.location.host + "/ClientRelations/";

        self.loading = ko.observable(true);
        self.noData = ko.observable(true);
        self.errorData = ko.observable(false);
        self.isSending = ko.observable(false);

        self.hasData = ko.computed(() => !self.noData());

        self.canSend = ko.computed(() => !self.isSending() && !self.loading());

        self.smsQuota = ko.observable();
        self.practiceSlug = ko.observable();
        self.numPhoneNumbers = ko.observable(0);

        self.senderInput = ko.observable("DYV");

        self.previewSender = ko.computed(() => self.senderInput().length > 0 ? self.senderInput() : "DYV");

        self.messageText = ko.observable("");

        self.addPracticeLink = ko.observable(true);
        self.useMarketingCode = ko.observable(true);

        self.practiceLink = ko.computed(function () {
            var link = 'http://' + window.location.host + "/Register/" + self.practiceSlug();
            if (self.useMarketingCode()) {
                link += "/ALfh529ghs";
            }
            return link;
        });

        self.previewMessage = ko.computed(function () {
            var msgText = self.messageText();
            if (self.addPracticeLink()) {
                msgText += " " + self.practiceLink();
            }
            return msgText;
        });

        self.smsChars = ko.computed(function () {
            return self.previewMessage().length;
        });

        self.numSmsByLength = ko.computed(function () {
            if (self.smsChars() < 160) {
                return 1;
            } else {
                return Math.ceil(self.smsChars() / 153);
            }
        });
        
        self.smsToBeUsed = ko.computed(function () {
            return self.numSmsByLength() * self.numPhoneNumbers();
        });

        self.isValidSend = function () {
            var valid = true;

            if (self.numSmsByLength() > 6) {
                nm.notyMessage("Your message exceeds the maximum length of 918 characters.");
                valid = false;
            }

            if (self.numPhoneNumbers() <= 0) {
                nm.notyMessage("Please enter at least one phone number.");
                valid = false;
            }

            if (self.messageText().length <= 0) {
                nm.notyMessage("Please enter a message.");
                valid = false;
            }

            if (self.numPhoneNumbers() * self.numSmsByLength() > self.smsQuota()) {
                nm.notyMessage("You do not have enough credits to send the messages. Please purchase more.");
                valid = false;
            }

            return valid;
        };

        self.sendMessages = function () {
            if (!self.isValidSend()) {
                return;
            }

            self.isSending(true);

            nm.notyConfirm("You are sending messages to " + self.numPhoneNumbers() + " numbers, which will use " + self.numSmsByLength() * self.numPhoneNumbers() + " credits.")
                .then(function (status) {
                    if (status) {
                        var data = {
                            "numbers": $('#tokenize').tokenize().toArray(),
                            "message": self.previewMessage()
                        };

                        $.ajax({
                            type: "POST",
                            data: JSON.stringify(data),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            url: self.urlBase + "SendMessages",
                            success: function (data) {
                                console.log(data);
                                if (data.success) {
                                    window.location = data.returnData;
                                } else {
                                    nm.notyMessage("Message sending failed.");
                                    self.isSending(false);
                                }
                            },
                            error: function () {
                                nm.notyMessage("Server did not respond in time. Please try again.");
                                self.isSending(false);
                            }
                        });
                    } else {
                        self.isSending(false);
                    }
            });
        };

        $.ajax({
            type: "GET",
            dataType: "json",
            url: self.urlBase + "SendSMSData",
            success: function (data) {
                if (data === null || !data.success) {
                    self.errorData(true);
                } else {
                    self.smsQuota(data.returnData.smsRemaining);
                    self.practiceSlug(data.returnData.practiceSlug);
                    self.noData(false);
                }
            },
            error: function () {
                self.errorData(true);
            },
            complete: function () {
                self.loading(false);
            }
        });
    };
});