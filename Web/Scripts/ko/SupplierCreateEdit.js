var urlHome = "/Home";
var urlCurrent = window.location.pathname;
var supplierId = urlCurrent.substring(urlCurrent.lastIndexOf('/') + 1);



$(function () {

    function Supplier(data) {
        var self = this;
        self.Id = ko.observable(data ? data.Id : 0).extend({ required: true });
        self.Name = ko.observable(data ? data.Name : '').extend({ required: true, maxLength: 20 });
        self.MeterDTO = ko.observable(data ? data.MeterDTO : []);
        self.ReportDTO = ko.observable(data ? data.ReportDTO : []);
    }


    function Meter(data) {
        var self = this;
        self.Id = ko.observable(data ? data.Id : 0).extend({ required: true });
        self.Name = ko.observable(data ? data.Name : '').extend({ required: true, maxLength: 20 });
        self.Serial = ko.observable(data ? data.Serial : '').extend({ required: true });

        self.FeederPoint = ko.observable(data ? data.FeederPoint : '');
        self.SupplierId = ko.observable(data ? data.SupplierId : 0);
        self.SubStationId = ko.observable(data ? data.SubStationId : 0);
        self.ProvinceId = ko.observable(data ? data.ProvinceId : 0);
    }



    var SupplierCollection = function () {
        var self = this;
        //if Id is 0, It means Create new Province
        if (supplierId == 0) {
            self.supplier = ko.observable(new Supplier());
            self.SuppliersArray = ko.observableArray([new Supplier()]);
        }
        else {
            $.ajax({
                url: urlHome + '/GetSupplierById/' + supplierId,
                async: false,
                dataType: 'json',
                success: function (json) {
                    self.supplier = ko.observable(new Supplier(json));
                }
            });
        }

        self.AddSupplier = function () { self.SuppliersArray.push(new Supplier()) };

        self.RemoveSupplier = function (objSupplier) { self.SuppliersArray.remove(objSupplier) };


        self.BackToProvinceList = function () { window.location.href = '/Home'; };


        self.SaveSupplier = function () {

            var isValid = true;

            if (isValid) {


                //Save
                //waitingDialog({});
                //Hae to implement this later
                var httpMethod = 'POST';
                var urlSaveUpdate = (urlHome + '/SaveSupplierInformation');
                var dataString = JSON.stringify(ko.toJS(self.supplier()));

                var objId = self.supplier().Id._latestValue;
                if (objId > 0) {
                    //waitingDialog({});
                    //Hae to implement this later
                     httpMethod = 'PUT';
                     urlSaveUpdate = (urlHome + '/UpdateSupplierInformation/' + objId);
                     dataString = JSON.stringify(ko.toJS(self.supplier()));
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
                                errors += key.replace("supplier.", "") + " : " + err[key];
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

    ko.applyBindings(new SupplierCollection());
});

var clone = (function () {
    return function (obj) {
        Clone.prototype = obj;
        return new Clone()
    };
    function Clone() { }
}());