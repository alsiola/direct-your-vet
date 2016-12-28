define(function () {
    return class Place {
        constructor(data) {
            this.id = ko.observable(data.id);
            this.clientName = ko.observable(data.clientName);
            this.name = ko.observable(data.name);
            this.address1 = ko.observable(data.address1);
            this.postcode = ko.observable(data.postCode);
            this.city = ko.observable(data.city);
            this.county = ko.observable(data.county);
            this.country = ko.observable(data.country);
            this.dateAdded = ko.observable(data.dateAdded);
            this.detailsUrl = "/SubscriberDashboard/Details/" + data.id;
            this.latitude = ko.observable(data.latitude);
            this.longitude = ko.observable(data.longitude);
        }        
    };
});
