


var ProvinceViewModel = function () {
    var self = this;
    var url = "/Home/GetAllProvinces";
    var refresh = function () {
        $.getJSON(url, {}, function (data) {
            self.ProvincesArray(data);
        });
    };

    // Public data properties
    self.ProvincesArray = ko.observableArray([]);

    // Public operations
    self.CreateProvince= function () {
        window.location.href = '/Home/ProvinceCreateEdit/0';
    };

    self.EditProvince = function (objProvince) {
        window.location.href = '/Home/ProvinceCreateEdit/' + objProvince.Id;
    };

    self.RemoveProvince = function (objProvince) {
        if (confirm("Are you sure you want to delete this Province?")) {
            var id = objProvince.Id;
            waitingDialog({});
            $.ajax({
                type: 'DELETE', url: 'Contact/DeleteProvince/' + id,
                success: function () { self.ProvincesArray.remove(objProvince); },
                error: function (err) {
                    var error = JSON.parse(err.responseText);
                    $("<div></div>").html(error.Message).dialog({ modal: true, title: "Error", buttons: { "Ok": function () { $(this).dialog("close"); } } }).show();
                },
                complete: function () { closeWaitingDialog(); }
            });
        }
    };
    refresh();
};
ko.applyBindings(new ProvinceViewModel());