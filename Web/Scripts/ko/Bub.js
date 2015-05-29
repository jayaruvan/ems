var urlBase = "/Home";
var urlCurrent = window.location.pathname;
var substationId = urlCurrent.substring(urlCurrent.lastIndexOf('/') + 1);


/**

For edit we shouldnt allow them to change province ....

**/
$(function () {
    var self = this;
    var urlGetAll = "/Home/GetAllProvincesAreas";

    var SubStation = function (objSubStation) {
        var self = this;
        self.Id = ko.observable(objSubStation ? objSubStation.Id : 0);
        self.Name = ko.observable(objSubStation ? objSubStation.Name : '').extend({ required: true, maxLength: 20 });
        self.MetersDTO = ko.observableArray([]);
    };

    var SubStationViewModel = function () {

        var items = { Id: 4, Name: "Province B", "AreaDTO": [{ Id: 2, Name: "ProB Area 1", "ProvinceId": 4, "SubStationDTO": [] }], "SubStationDTO": [] }, { Id: 1, Name: "Provicne A", "AreaDTO": [{ Id: 1, Name: "Area az", "ProvinceId": 1, "SubStationDTO": [{ Id: 1, Name: "SubStationA", "ProvinceId": 1, "AreaId": 1, "MeterDTO": null }, { Id: 4, Name: "SubStationB", "ProvinceId": 1, "AreaId": 1, "MeterDTO": null }] }], "SubStationDTO": [{ Id: 1, Name: "SubStationA", "ProvinceId": 1, "AreaId": 1, "MeterDTO": null }, { Id: 4, Name: "SubStationB", "ProvinceId": 1, "AreaId": 1, "MeterDTO": null }] };

        this.ProvincesArray = ko.observableArray(items);
        this.selectedProvince = ko.observable();
        this.selectedArea = ko.observable();

        function getById(items, Id) {
            if (!Id) {
                return [];
            }

            var result = ko.utils.arrayFirst(items, function (item) {
                return item.Id === Id;
            });

            return result && result.AreaDTO || [];
        }

        this.AreasArray = ko.computed(function () {
            var items = this.ProvincesArray();
            var id = this.selectedProvince()
            return getById(items, id);
        }, this);

        function Province(data) {
            var self = this;
            self.Id = data.Id;
            self.Name = data.Name;
            self.AreaDTO = ko.observableArray([]);
        }
        function Area(data) {
            var self = this;
            self.Id = ko.observable(data.Id);
            self.Name = ko.observable(data.Name);
            self.ProvinceId = ko.observable(data.ProvinceId);
        }


        var LoadProvinceNamesList = function () {

            $.getJSON(urlGetAll, {}, function (data) {
                var datastr = JSON.stringify(ko.toJS(data));
                /*
                var objectsArray = ko.utils.arrayMap(data, function (item) {
                    var prov = new Province(item);
                    return prov;
                });
                self.ProvincesArray(objectsArray);
                **/
            });
        }



        //if Id is 0, It means Create new Province
        if (substationId == 0) {

        }
        else {

        }



        self.BackToProvinceList = function () { window.location.href = '/Home'; };

        self.SaveArea = function () {

 
        };


        LoadProvinceNamesList();
    };



    ko.applyBindings(new SubStationViewModel());
});

var clone = (function () {
    return function (obj) {
        Clone.prototype = obj;
        return new Clone()
    };
    function Clone() { }
}());