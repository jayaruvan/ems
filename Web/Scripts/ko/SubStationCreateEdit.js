var urlBase = "/Home";
var urlCurrent = window.location.pathname;
var subStationId = urlCurrent.substring(urlCurrent.lastIndexOf('/') + 1);


$(function () {

    var SubStationViewModel = function () {
        var self = this;

        self.ProvincesArray = ko.observableArray();
        self.selectedProvince = ko.observable();
        self.selectedArea = ko.observable();
        self.AreasArray = ko.computed(function () {
            var items = this.ProvincesArray();
            var id = this.selectedProvince()
            return getById(items, id);
        }, this);
        function getById(items, Id) {
            if (!Id) {
                return [];
            }
            var result = ko.utils.arrayFirst(items, function (item) {
                return item.Id === Id;
            });
            return result && result.AreaDTO || [];
        }
        var SubStation = function (objSubStation) {
            var self = this;
            self.Id = ko.observable(objSubStation ? objSubStation.Id : 0);
            self.Name = ko.observable(objSubStation ? objSubStation.Name : '').extend({ required: true, maxLength: 20 });
            self.ProvinceId = ko.observable(objSubStation ? objSubStation.ProvinceId : 0);
            self.AreaId = ko.observable(objSubStation ? objSubStation.AreaId : 0);
            self.MeterDTO = ko.observableArray([]);
        };        
        var Meter = function (obj) {
            var self = this;
            self.Id = ko.observable(obj ? obj.Id : 0);
            self.Serial = ko.observable(obj ? obj.Name : '').extend({ required: true, maxLength: 20 });
            self.Name = ko.observable(obj ? obj.Name : '');
            self.FeederPoint = ko.observable(obj ? obj.Name : '');
            self.SupplierId = ko.observable(obj ? obj.SupplierId : 0).extend({ required: true });
            self.SubStationId = ko.observable(obj ? obj.SubStationId : 0).extend({ required: true });
            self.MeterReadingDTO = ko.observableArray([]);
        };
        function LoadProvincesAndAreas() {
            var urlGetAll = "/Home/GetAllProvincesAreas";
            $.getJSON(urlGetAll, {}, function (data) {

                var objectsArray = ko.utils.arrayMap(data, function (item) {
                    return item;
                });

                self.ProvincesArray(objectsArray);
            });
        }

        self.BackToProvinceList = function () { window.location.href = '/Home'; };

        self.SaveSubStation = function () {
            var isValid = true;

            if ((self.saveMode())&&
                ((typeof self.selectedProvince._latestValue === 'undefined') ||
                (typeof self.selectedArea._latestValue === 'undefined'))) {
                //should be UI display..
                alert(" Please Select Propeor Province and Area");
                 isValid = false;
                return;
            }



            if (!isValid) { return; }


            //Save
            //waitingDialog({});
            //Hae to implement this later

            //Propoerty set.. for new Area



            var httpMethod = 'POST';
            var urlSaveUpdate = (urlBase + '/SavePrimarySubstationInformation');
            var dataString = "";


            //Update
            var objId = self.subStation().Id._latestValue;


            if (objId == 0) {
                self.subStation().AreaId = self.selectedArea._latestValue;
                self.subStation().ProvinceId = self.selectedProvince._latestValue;
                self.subStation().MeterDTO = ko.observableArray([]);
                dataString = JSON.stringify(ko.toJS(self.subStation()));
            }
            else{

                httpMethod = 'PUT';
                urlSaveUpdate = (urlBase + '/UpdatePrimarySubstationInformation/' + objId);
                self.subStation().Id = objId;
                self.subStation().MeterDTO = self.MeterArray;
                dataString = JSON.stringify(ko.toJS(self.subStation()));

            }
            //waitingDialog({});
            //Hae to implement this later

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




           // alert("SAve Function");
        }

        //if Id is 0, It means Create new Province
        if (subStationId == 0) {
            self.subStation = ko.observable(new SubStation());
            self.MeterArray = ko.observableArray([new Meter()]);
            self.saveMode = ko.observable(true);
        }

        if (subStationId >  0) {

            var urlGetById = urlBase + '/GetSubStationById/' + subStationId;
            $.ajax({
                url: urlGetById,
                async: false,
                dataType: 'json',
                success: function (json) {

                    self.subStation = ko.observable(new SubStation(json));

                    self.MeterArray = ko.observableArray(ko.utils.arrayMap(json.MetersDTO, function (objMeter) {
                        return objMeter;
                    }));

                    self.saveMode = ko.observable(false);
                }
            });


        }


        LoadProvincesAndAreas();

}


    ko.applyBindings(new SubStationViewModel());
});

var clone = (function () {
    return function (obj) {
        Clone.prototype = obj;
        return new Clone()
    };
    function Clone() { }
}());