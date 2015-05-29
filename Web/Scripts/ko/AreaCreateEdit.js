var urlBase = "/Home";
var urlCurrent = window.location.pathname;
var areaId = urlCurrent.substring(urlCurrent.lastIndexOf('/') + 1);


/**

For edit we shouldnt allow them to change province ....

**/
$(function () {
    var self = this;
    var url = "/Home/GetAllProvinceNames";

    // -- Get All Province Names --------------------
    function Province(data){
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;
    }  

    var Area = function (objArea) {
        var self = this;
        self.Id = ko.observable(objArea ? objArea.Id : 0);
        self.ProvinceId = ko.observable(objArea ? objArea.ProvinceId : 0);
        self.Name = ko.observable(objArea ? objArea.Name : '').extend({ required: true, maxLength: 20 });
        self.SubStationDTO = ko.observableArray(objArea ? objArea.SubStationDTO : []);
    };

    var SubStation = function (objSubStation) {
        var self = this;
        self.Id = ko.observable(objSubStation ? objSubStation.Id : 0);
        self.Name = ko.observable(objSubStation ? objSubStation.Name : '').extend({ required: true, maxLength: 20 });
        self.MetersDTO = ko.observableArray([]);
    };

    var AreaViewModel = function () {
        var self = this;
        self.ProvinceNamesArray = ko.observableArray([]);
        self.selectedProvinceId = ko.observable(0);
        var LoadProvinceNamesList = function () {

            $.getJSON(url, {}, function (data) {

                var datastr = JSON.stringify(ko.toJS(data));
                /**
                **/
                var provinceNamesArray = ko.utils.arrayMap(data, function (item) {
                    var province = new Province(item);
                    if (self.selectedProvinceId == 0) { self.selectedProvinceId(province.Id); }
                    return province;
                });

                self.ProvinceNamesArray (provinceNamesArray);
            });
        }

        //if Id is 0, It means Create new Province
        if (areaId == 0) {
            self.area = ko.observable(new Area());
            self.SubStationsArray = ko.observableArray([new SubStation()]); 
        }
        else {
            $.ajax({
                url: urlBase + '/GetAreaOfficeById/' + areaId,
                async: false,
                dataType: 'json',
                success: function (json) {
                    self.area = ko.observable(new Area(json));

                    self.SubStationsArray = ko.observableArray(ko.utils.arrayMap(json.SubStationDTO, function (objSubStations) {
                        return objSubStations;
                    }));
                }
            });
        }



/**

        self.AddArea = function () { self.AreasArray.push(new Area()) };
        self.RemoveArea = function (objArea) { self.AreasArray.remove(objArea) };
        self.AddSubStation = function () { self.SubStationsArray.push(new SubStation()) };
        self.RemoveSubStation = function (objSubStation) { self.SubStationsArray.remove(objSubStation) };
        **/



        self.BackToProvinceList = function () { window.location.href = '/Home'; };

        // self.provinceErrors = ko.validation.group(self.area());
        // self.areaErrors = ko.validation.group(self.AreasArray(), { deep: true });
        // self.subStationErrors = ko.validation.group(self.SubStationsArray(), { deep: true });

        self.SaveArea = function () {

            var isValid = true;

            if (isValid) {

                //Save
                //waitingDialog({});
                //Hae to implement this later

                //Propoerty set.. for new Area
                self.area().ProvinceId = self.selectedProvinceId._latestValue;
                self.area().SubStationDTO = null;
                var httpMethod = 'POST';
                var urlSaveUpdate = (urlBase + '/SaveAreaOfficeInformation');
                var dataString = JSON.stringify(ko.toJS(self.area()));


                //Update
                var objId = self.area().Id._latestValue;
                if (objId > 0) {

                     httpMethod = 'PUT';
                     urlSaveUpdate = (urlBase + '/UpdateAreaOfficeInformation/' + objId);

                     self.area().SubStationDTO = self.SubStationsArray;

                     self.area().Id = objId;
                    //This should be origianal value .. Fix here...
                     self.area.ProvinceId = self.selectedProvinceId._latestValue;

                     dataString = JSON.stringify(ko.toJS(self.area()));

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
                                errors += key.replace("area.", "") + " : " + err[key];
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


        LoadProvinceNamesList();
    };



    ko.applyBindings(new AreaViewModel());
});

var clone = (function () {
    return function (obj) {
        Clone.prototype = obj;
        return new Clone()
    };
    function Clone() { }
}());