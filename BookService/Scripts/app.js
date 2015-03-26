var ViewModel = function () {
    var self = this;
    self.cars = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    
    self.getCarDetail = function (item) {
        ajaxHelper(carsUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }
    
    var carsUri = '/api/cars/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllCars() {
        ajaxHelper(carsUri, 'GET').done(function (data) {
            self.cars(data);
        });
    }

    // Fetch the initial data.
    getAllCars();



    self.owners = ko.observableArray();
    self.newCar = {
        Owner: ko.observable(),
        Price: ko.observable(),
        Title: ko.observable(),
        Year: ko.observable()
    }

    var ownersUri = '/api/owners/';

    function getOwners() {
        ajaxHelper(ownersUri, 'GET').done(function (data) {
            self.owners(data);
        });
    }

    self.addCar = function (formElement) {
        var car = {
            OwnerId: self.newCar.Owner().Id,
            Price: self.newCar.Price(),
            Title: self.newCar.Title(),
            Year: self.newCar.Year()
        };

        ajaxHelper(carsUri, 'POST', car).done(function (item) {
            self.cars.push(item);
        });
    }

    getOwners();
};

ko.applyBindings(new ViewModel());

