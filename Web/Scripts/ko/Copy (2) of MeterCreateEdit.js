var urlBase = "/Home";
var urlCurrent = window.location.pathname;
var subStationId = urlCurrent.substring(urlCurrent.lastIndexOf('/') + 1);


$(function () {

    function ViewModel(items) {
        var self = this;
        self.ProvincesArray = ko.observableArray();
        self.selectedProvince = ko.observable();
        self.selectedArea = ko.observable();
        self.selectedSubStation = ko.observable();

        function getById(items, value) {
            if (!value) {
                return [];
            }

            var result = ko.utils.arrayFirst(items, function (item) {
                return item.value === value;
            });

            return result && result.AreaDTO || [];
        }
        /*
[{"Id":1,"Name":"Provicne A","AreaDTO":[{"Id":1,"Name":"A","ProvinceId":1,"SubStationDTO":null}],"SubStationDTO":[{"Id":1,"Name":"SubA","ProvinceId":1,"AreaId":1,"MeterDTO":null}]}]
        */
        this.AreasArray = ko.computed(function () {
            var items = this.ProvincesArray();
            var id = this.selectedProvince()
            return getById(items, id);
        }, this);

        this.SubStationsArray = ko.computed(function () {
            var items = this.AreasArray();
            var id = this.selectedArea()
            return getById(items, id);
        }, this);



        function LoadLocationDetails() {
            var urlGetAll = "/Home/GetAllLocationDetails";
            $.getJSON(urlGetAll, {}, function (data) {
                var datastr = JSON.stringify(ko.toJS(data));
                var objectsArray = ko.utils.arrayMap(data, function (item) {
                    return item;
                });

                self.ProvincesArray(objectsArray);
            });
        }

        LoadLocationDetails();

    }

    var items = [
        {
            text: 'Ford', value: 1, childItems:
             [
                 {
                     text: 'F-150', value: 1, childItems:
                      [
                          { text: 'Gasoline', value: 1, childItems: [] },
                          { text: 'Diesel', value: 2, childItems: [] }
                      ]
                 },
                 {
                     text: 'F-250', value: 2, childItems:
                      [
                          { text: 'Gasoline', value: 3, childItems: [] },
                          { text: 'Diesel', value: 4, childItems: [] }
                      ]
                 }
             ]
        },
        {
            text: 'Honda', value: 2, childItems:
             [
                 {
                     text: 'Civic', value: 5, childItems:
                      [
                          { text: 'Gasoline', value: 5, childItems: [] },
                          { text: 'Electric', value: 6, childItems: [] }
                      ]
                 },
                 {
                     text: 'Accord', id: 6, childItems:
                      [
                          { text: 'Gasoline', value: 7, childItems: [] },
                          { text: 'Electric', value: 8, childItems: [] }
                      ]
                 }
             ]
        }
    ];

    var module = {};

    module.viewModel = new ViewModel(items);

    ko.applyBindings(module.viewModel);
});

var clone = (function () {
    return function (obj) {
        Clone.prototype = obj;
        return new Clone()
    };
    function Clone() { }
}());



