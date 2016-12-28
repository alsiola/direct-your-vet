define(function (require) {
    const DashboardViewModel = require('./dashboardViewModel');
    ko.applyBindings(new DashboardViewModel());
});