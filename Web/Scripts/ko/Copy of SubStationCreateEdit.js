var urlBase = "/Home";
var urlCurrent = window.location.pathname;
var areaId = urlCurrent.substring(urlCurrent.lastIndexOf('/') + 1);


/**

For edit we shouldnt allow them to change province ....

**/
$(function () {
    var self = this;
    var urlGetProvinceNames = "/Home/GetAllProvinces";

    // -- Get All Province Names --------------------
    function Province(data){
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;
        self.AreaList = ko.observableArray([]);
    }  

    function Area(data) {
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;    
        self.ProvineId=data.ProvinceId;
    }


    var SubStation = function (objSubStation) {
        var self = this;
        self.Id = ko.observable(objSubStation ? objSubStation.Id : 0);
        self.Name = ko.observable(objSubStation ? objSubStation.Name : '').extend({ required: true, maxLength: 20 });
        self.MetersDTO = ko.observableArray([]);
    };


    var SubStationViewModel = function () {
        var self = this;

        self.ProvinceList = ko.observableArray([]);
        self.AreasList= ko.observableArray([]);
        self.selectedProvinceId = ko.observable();
        self.selectedAreaId = ko.observable();

        var GetProvinceAreaList = function () {

            $.getJSON(urlGetProvinceNames, {}, function (data) {

                var datastr = JSON.stringify(ko.toJS(data));
                /**
                **/
                var provinceNamesArray = ko.utils.arrayMap(data, function (item) {
                    var province = new Province(item);
                    if (self.selectedProvinceId == 0) { self.selectedProvinceId(province.Id); }

                    var areaNamesArray = ko.utils.arrayMap(data.AreaDTO, function (item) {
                        var area = new Area(item);
                        if (self.selectedProvinceId == area.ProvineId) { self.selectedAreaId(area.Id); }
                        return area;
                    });

                    province.AreaList(areaNamesArray);

                    return province;
                });

                self.ProvinceList(provinceNamesArray);
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





        self.BackToProvinceList = function () { window.location.href = '/Home'; };


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


        GetProvinceAreaList();
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