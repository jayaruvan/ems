var ProvinceViewModel = function () {
    var self = this;
    var urlGetAll = "/Home/GetAllProvinces";

     var refresh = function () {
        
        $.getJSON(urlGetAll, {}, function (resultOfCallingUrlGetAll) {

            var datastr = JSON.stringify(ko.toJS(resultOfCallingUrlGetAll));
            //var parsejson = ko.utils.parseJson(resultOfCallingUrlGetAll);

            self.ProvincesArray = ko.toJS(resultOfCallingUrlGetAll);

            ko.mapping.fromJS(resultOfCallingUrlGetAll, {}, self.ProvincesArray);

        });
    };

    // Public data properties
    self.ProvincesArray = ko.observableArray([]);


    self.addLevel = 2;
    self.addItem = function () {
        var child = self.items()[0];
        for (i = 0; i < self.addLevel; i++) {
            child = child.children()[0];
        }
        child.children.push(
            {
                "Id": 0,
                "Name": ko.observable("new item"),
                "AreaDTO": ko.observableArray()
            }
        );
        self.addLevel = self.addLevel + 1;
    };


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
            //waitingDialog({});
            //Hae to implement this later
            
            $.ajax({
                type: 'DELETE', url: 'Home/DeleteProvince/' + id,
                success: function () { self.ProvincesArray.remove(objProvince); },
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
    refresh();
};
ko.applyBindings(new ProvinceViewModel());