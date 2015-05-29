var urlBase = "/Home";
var urlCurrent = window.location.pathname;
var meterId = urlCurrent.substring(urlCurrent.lastIndexOf('/') + 1);


/**

For edit we shouldnt allow them to change province ....

**/
$(function () {
    var self = this;
    var urlProvinceNames = "/Home/GetAllProvinceNames";
    var urlAreaNames = "/Home/GetProvinceAreas/";
    var urlSubStationNames = "/Home/GetAreaSubStations/";

    // -- Get All Province Names --------------------
    function Province(data){
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;
    }
    // -- Get All Area Names --------------------
    function Area(data) {
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;
    }
    // -- Get All SubStation Names --------------------
    function SubStation(data) {
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;
    }


    function Meter(data) {
        var self = this;
        self.Id =ko.observable( data ? data.Id:0);
        self.Serial = ko.observable( data ? data.Serial:"");
        self.Name =ko.observable( data ?  data.Name:"");
        self.FeederPoint =ko.observable( data ?  data.FeederPoint:"");
        self.SubStationId = ko.observable( data ? data.SubStationId:0);
        self.SupplierId = ko.observable(data ? data.SupplierId : 0);
    }



    var MeterEditViewModel = function () {
        var self = this;

        self.ProvinceNamesArray = ko.observableArray([]);
        self.AreaNamesArray = ko.observableArray([]);
        self.SubStationsArray = ko.observableArray([]);


        self.selectedProvinceId = ko.observable(0);
        self.selectedAreaId = ko.observable(0);
        self.selectedSubStationId = ko.observable(0);



        var LoadProvinceNamesList = function () {

            $.getJSON(urlProvinceNames, {}, function (data) {

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
        if (meterId == 0) {
            self.area = ko.observable(new Area());
            self.SubStationsArray = ko.observableArray([new SubStation()]); 
        }
        else {
            $.ajax({
                url: urlBase + '/GetAreaOfficeById/' + meterId,
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


        LoadProvinceNamesList();
    };



    ko.applyBindings(new MeterEditViewModel());
});

