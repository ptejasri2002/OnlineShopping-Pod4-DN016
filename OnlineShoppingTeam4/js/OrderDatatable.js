﻿/*find correct pathc for search*/
function searchControllerPath() {
    var path = window.location.href;
    var a = path.split("/");
    if (path.indexOf("http://") + 1) {
        return a[0] + '//' + a[2] + '/' + (a[3].split("?"))[0];
    }
    else {
        return a[0] + '/' + a[1];
    }
}

$(document).ready(function () {
    $('#Home').DataTable({
        "processing": true,
        "serverSide": true,
        "responsive": true,
        "autoWidth": false,
        "bFilter": false,
        "bLengthChange": false,
        "columnDefs": [
            { responsivePriority: 1, targets: 0 },
            { responsivePriority: 2, targets: -1 }
        ],
        "ajax": {
            "type": "GET",
            "url": searchControllerPath() + "/JsonTableFill",
            "dataSrc": function (json) {
                //Make your callback here.
                $.each(json.data, function (index, item) {
                    item.OrderID = '<a href= "' + searchControllerPath() + '/Home?orderID=' + item.OrderID + '" class="coloronwhite"/>'+item.OrderID+'</a >';

                });

                return json.data;
            },


        },
        "columns": [
            { 'data': 'OrderID' },
            { 'data': 'OrderDate' },
            { 'data': 'CompanyName' },
            { 'data': 'ShipperName' }

        ]

    });
    $('#HomeAdmin').DataTable({
        "processing": true,
        "serverSide": true,
        "responsive": true,
        "autoWidth": false,
        "bFilter": false,
        "bLengthChange": false,
        "columnDefs": [
            { responsivePriority: 1, targets: 0 },
            { responsivePriority: 2, targets: -1 }
        ],
        "ajax": {
            "type": "GET",
            "url": searchControllerPath() + "/JsonTableAdminFill",
            "dataSrc": function (json) {
                //Make your callback here.
                $.each(json.data, function (index, item) {
                    item.OrderID = '<a href= "' + searchControllerPath() + '/HomeAdmin?orderID=' + item.OrderID + '" class="coloronwhite"/>' + item.OrderID + '</a >';

                });

                return json.data;
            },


        },
        "columns": [
            { 'data': 'OrderID' },
            { 'data': 'OrderDate' },
            { 'data': 'CompanyName' },
            { 'data': 'ShipperName' }

        ]

    });
    $('#HomeCustomer').DataTable({
        "processing": true,
        "serverSide": true,
        "responsive": true,
        "autoWidth": false,
        "bFilter": false,
        "bLengthChange": false,
        "columnDefs": [
            { responsivePriority: 1, targets: 0 },
            { responsivePriority: 2, targets: -1 }
        ],
        "ajax": {
            "type": "GET",
            "url": searchControllerPath() + "/JsonTableCustomerFill",
            "dataSrc": function (json) {
                //Make your callback here.
                $.each(json.data, function (index, item) {
                    item.OrderID = '<a href= "' + searchControllerPath() + '/HomeCustomer?orderID=' + item.OrderID + '" class="coloronwhite"/>' + item.OrderID + '</a >';

                });

                return json.data;
            },


        },
        "columns": [
            { 'data': 'OrderID' },
            { 'data': 'OrderDate' },
            { 'data': 'CompanyName' },
            { 'data': 'ShipperName' }

        ]

    });
})