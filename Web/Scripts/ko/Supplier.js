


var objectMap = function(o, handler){
    var res = {};
    for(var key in o) {
        res[key] = handler(o[key])
    }
    return res;
};


var SupplierViewModel = function() {
    var self = this;
    var url = "/Home/GetAllSuppliers";
    
    var getAllSuppliers = function () {
        $.getJSON(url, {}, function (data) {

           var datastr = JSON.stringify(ko.toJS(data));
            /**

            [
            
            {"Id":1,"Name":"Suppliera","MeterDTO":[],"ReportDTO":null}
            
            ]
            **/
            
            var objectsArray = ko.utils.arrayMap(data, function (item) {

                var supplier = new Supplier(item);

                var meters = ko.utils.arrayMap(item.MeterDTO, function (meterObject) {
                    var meter = new Meter(meterObject);
                    return meter;
                });

                supplier.MeterDTO(meters);

                return supplier;
            });

            self.SuppliersArray(objectsArray);
           
        });
    };
    

    self.SuppliersArray = ko.observableArray([]);


    function Supplier(data){
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;
        self.MeterDTO = ko.observableArray([])
        self.ReportDTO = ko.observableArray([])
    }


    function Meter(data){
        var self = this;
        self.Id = ko.observable(data.Id);
        self.Name = ko.observable(data.Name);
        self.Serial = ko.observable(data.Serial);
        self.FeederPoint = ko.observable(data.FeederPoint);
        self.SupplierId = ko.observable(data.SupplierId);
        self.SubStationId = ko.observable(data.SubStationId);
        self.ProvinceId = ko.observable(data.ProvinceId);
    }       


    // Public operations
    self.CreateSupplier = function () {
        window.location.href = '/Home/SupplierCreateEdit/0';
    };

    self.EditSupplier = function (obj) {
        window.location.href = '/Home/SupplierCreateEdit/' + obj.Id;
    };



    self.RemoveSupplier = function (obj) {
        if (confirm("Are you sure you want to delete this Province?")) {
            var id = obj.Id;
            //waitingDialog({});
            //Hae to implement this later

            var urlDelete='/Home/DeleteSupplier/' + id;

            $.ajax({
                type: 'DELETE', url: urlDelete,
                success: function () { self.SuppliersArray.remove(obj); },
                error: function (err) {
                    var error = JSON.parse(err.responseText);
                    $("<div></div>").html(error.Message).dialog({ modal: true, title: "Error", buttons: { "Ok": function () { $(this).dialog("close"); } } }).show();
                },
                complete: function () {
                    //closeWaitingDialog();
                    //have to implement that later
                }
            });
        }
    };
    
    getAllSuppliers();
}

ko.applyBindings(new SupplierViewModel())
