var urlProvince = "/Home";
var urlCurrent = window.location.pathname;
var provinceId = urlCurrent.substring(urlCurrent.lastIndexOf('/') + 1);



$(function () {

    var Province = function (objProvince) {
        var self = this;
        self.Id = ko.observable(objProvince ? objProvince.Id : 0).extend({ required: true });
        self.Name = ko.observable(objProvince ? objProvince.Name : '').extend({ required: true, maxLength: 20 });
        self.AreaDTO = ko.observableArray(objProvince ? objProvince.AreaDTO : []);
        self.SubStationDTO = ko.observableArray(objProvince ? objProvince.SubStationDTO : []);
    };

    var Area = function (objArea) {
        var self = this;
        self.Id = ko.observable(objArea ? objArea.Id : 0);
        self.Name = ko.observable(objArea ? objArea.Name : '').extend({ required: true, maxLength: 20 });
        self.SubStationDTO = ko.observableArray(objArea ? objArea.SubStationDTO : []);
    };

    var SubStation = function (objSubStation) {
        var self = this;
        self.Id = ko.observable(objSubStation ? objSubStation.Id : 0);
        self.Name = ko.observable(objSubStation ? objSubStation.Name : '').extend({ required: true, maxLength: 20 });
        self.MetersDTO = ko.observableArray([]);
    };


    var ProvinceCollection = function () {
        var self = this;
        //if Id is 0, It means Create new Province
        if (provinceId == 0) {
            self.province = ko.observable(new Province());
            self.AreasArray = ko.observableArray([new Area()]);
            self.SubStationsArray = ko.observableArray([new SubStation()]);
        }
        else {
            $.ajax({
                url: urlProvince + '/GetProvinceById/' + provinceId,
                async: false,
                dataType: 'json',
                success: function (json) {
                    self.province = ko.observable(new Province(json));
                    self.AreasArray = ko.observableArray(ko.utils.arrayMap(json.AreaDTO, function (objArea) {
                        return objArea;
                    }));
                    self.SubStationsArray = ko.observableArray(ko.utils.arrayMap(json.SubStationDTO, function (objSubStations) {
                        return objSubStations;
                    }));
                }
            });
        }

        self.AddArea = function () { self.AreasArray.push(new Area()) };

        self.RemoveArea = function (objArea) { self.AreasArray.remove(objArea) };

        self.AddSubStation = function () { self.SubStationsArray.push(new SubStation()) };

        self.RemoveSubStation = function (objSubStation) { self.SubStationsArray.remove(objSubStation) };

        self.BackToProvinceList = function () { window.location.href = '/Home'; };

        // self.provinceErrors = ko.validation.group(self.province());
        // self.areaErrors = ko.validation.group(self.AreasArray(), { deep: true });
        // self.subStationErrors = ko.validation.group(self.SubStationsArray(), { deep: true });

        self.SaveProvince = function () {

            var isValid = true;

            if (isValid) {

                self.province().AreaDTO = self.AreasArray;
                self.province().SubStationDTO = self.SubStationsArray;



                //Save
                //waitingDialog({});
                //Hae to implement this later
                var httpMethod = 'POST';
                var urlSaveUpdate = (urlProvince + '/SaveProvinceInformation');
                var dataString = JSON.stringify(ko.toJS(self.province()));


                //Update
                var objId = self.province().Id._latestValue;
                if (objId > 0) {
                     httpMethod = 'PUT';
                     urlSaveUpdate = (urlProvince + '/UpdateProvinceInformation/' + objId);
                     dataString = JSON.stringify(ko.toJS(self.province()));
                }
                //waitingDialog({});
                //Hae to implement this later

                $.ajax({
                    type: httpMethod,
                    cache: false,
                    dataType: 'json',
                    url: urlSaveUpdate,
                    data: dataString,
                    contentType: 'application/json; charset=utf-8',
                    async: false,
                    success: function (data) {
                        window.location.href = '/Home';
                    },
                    error: function (err) {
                        var err = JSON.parse(err.responseText);
                        var errors = "";
                        for (var key in err) {
                            if (err.hasOwnProperty(key)) {
                                errors += key.replace("province.", "") + " : " + err[key];
                            }
                        }
                        $("<div></div>").html(errors).dialog({ modal: true, title: JSON.parse(err.responseText).Message, buttons: { "Ok": function () { $(this).dialog("close"); } } }).show();
                    },
                    complete: function () {

                        //closeWaitingDialog();
                        //have to implement that later
                    }
                });
   
            }
        };
    };

    ko.applyBindings(new ProvinceCollection());
});

var clone = (function () {
    return function (obj) {
        Clone.prototype = obj;
        return new Clone()
    };
    function Clone() { }
}());