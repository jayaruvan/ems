var urlBase = "/Home";
var urlCurrent = window.location.pathname;
var meterId = urlCurrent.substring(urlCurrent.lastIndexOf('/') + 1);


$(function () {

    var MeterCreateEditViewModel = function () {
        var self = this;

        //---------------- Drop Down Start ---------------------------------
        //Drop Down List Propoerties and Funcitons
        self.SuppliersArray = ko.observableArray();
        self.selectedSupplier = ko.observable();
        self.ProvincesArray = ko.observableArray();
        self.selectedProvince = ko.observable();
        self.selectedArea = ko.observable();
        self.selectedSubSation = ko.observable();
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
        self.SubStationsArray = ko.computed(function () {
            var items = this.AreasArray();
            var id = this.selectedArea()
            return getBySubId(items, id);
        }, this);
        function getBySubId(items, Id) {
            if (!Id) {
                return [];
            }
            var result = ko.utils.arrayFirst(items, function (item) {
                return item.Id === Id;
            });
            return result && result.SubStationDTO || [];
        }
        /*
NG [{"Id":1,"Name":"Provicne A","AreaDTO":[{"Id":1,"Name":"A","ProvinceId":1,"SubStationDTO":null}],"SubStationDTO":[{"Id":1,"Name":"SubA","ProvinceId":1,"AreaId":1,"MeterDTO":null}]}]

OK [{"Id":1,"Name":"Provicne A","AreaDTO":[{"Id":1,"Name":"A","ProvinceId":1,"SubStationDTO":[{"Id":1,"Name":"SubA","ProvinceId":1,"AreaId":1,"MeterDTO":null}]}],"SubStationDTO":null}]

        */
        function LoadLocationDetails() {
            var urlGetAll = "/Home/GetAllLocationDetails";
            $.getJSON(urlGetAll, {}, function (data) {
                //var datastr = JSON.stringify(ko.toJS(data));
                var objectsArray = ko.utils.arrayMap(data, function (item) {
                    return item;
                });
                self.ProvincesArray(objectsArray);
            });
        }
        function LoadSupplierDetails() {
            var urlGetSupplierAll = "/Home/GetAllSupplierNames";
            $.getJSON(urlGetSupplierAll, {}, function (data) {
                var objectsArray = ko.utils.arrayMap(data, function (item) {
                    return item;
                });
                self.SuppliersArray(objectsArray);
            });
        }
        //------------------------------------------------------------------

        var Meter = function (obj) {
            var self = this;
            self.Id = ko.observable(obj ? obj.Id : 0);
            self.Serial = ko.observable(obj ? obj.Serial : '').extend({ required: true, maxLength: 20 });
            self.Name = ko.observable(obj ? obj.Name : '');
            self.FeederPoint = ko.observable(obj ? obj.FeederPoint : '');
            self.SupplierId = ko.observable(obj ? obj.SupplierId : 0).extend({ required: true });
            self.SubStationId = ko.observable(obj ? obj.SubStationId : 0).extend({ required: true });
            self.MeterReadingDTO = ko.observableArray([]);
        };
        var Supplier = function (obj) {
            var self = this;
            self.Id = ko.observable(obj ? obj.Id : 0);
            self.Name = ko.observable(obj ? obj.Name : '');
        };

        //if Id is 0, It means Create new Meter
        if (meterId == 0) {
            self.meter = ko.observable(new Meter());
            SuppliersArray = ko.observable(new Supplier());
            self.saveMode = ko.observable(true);
        }

        if (meterId > 0) {
            var urlGetById = urlBase + '/GetMeterById/' + meterId;
            $.ajax({
                url: urlGetById,
                async: false,
                dataType: 'json',
                success: function (json) {
                    self.meter = ko.observable(new Meter(json));
                    self.saveMode = ko.observable(false);
                }
            });
        }




        self.SaveMeter = function () {
            var isValid = true;

            if ((self.saveMode()) &&
                ((typeof self.selectedProvince._latestValue === 'undefined') ||
                (typeof self.selectedArea._latestValue === 'undefined') ||
                (typeof self.selectedSubSation._latestValue === 'undefined'))) {
                //should be UI display..
                alert(" Please Select Propeor Province , Area and SubStation");
                isValid = false;
                return;
            }

            if ((self.saveMode()) &&
                (typeof self.selectedSupplier._latestValue === 'undefined') ) {
                //should be UI display..
                alert(" Please Select Suppler ");
                isValid = false;
                return;
            }

            if (!isValid) { return; }


            //Save
            //waitingDialog({});
            //Hae to implement this later
            //Propoerty set.. for new Area



            var httpMethod = 'POST';
            var urlSaveUpdate = (urlBase + '/SaveMeterInformation');
            var dataString = "";


            //Update
            var objId = self.meter().Id._latestValue;
            if (objId == 0) {
                self.meter().AreaId = self.selectedArea._latestValue;
                self.meter().SubStationId = self.selectedSubSation._latestValue;
                self.meter().MeterReadingDTO = ko.observableArray([]);
                self.meter().SupplierId = self.selectedSupplier._latestValue;
                dataString = JSON.stringify(ko.toJS(self.meter()));
            }
            else {

                httpMethod = 'PUT';
                urlSaveUpdate = (urlBase + '/UpdateMeterInformation/' + objId);
                self.meter().Id = objId;



                dataString = JSON.stringify(ko.toJS(self.meter()));

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

        //Call Functions
        LoadLocationDetails();
        LoadSupplierDetails();
    }



    //Links List 
    self.BackToHome = function () { window.location.href = '/Home'; };
    self.SaveMeter = function () {
        alert(" Save Method Called");
    }
    //Bind to view
    ko.applyBindings(new MeterCreateEditViewModel());
});

var clone = (function () {
    return function (obj) {
        Clone.prototype = obj;
        return new Clone()
    };
    function Clone() { }
}());