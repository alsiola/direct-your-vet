define(function (require) {

    var msg = $('#message').val();
    if (msg.length > 0) {
        nm.notyMessage(msg);
    }

    var ManageViewModel = require("./manageAccountViewModel");
    ko.applyBindings(new ManageViewModel());

});