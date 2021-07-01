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

Number.prototype.formatMoney = function (c, d, t) {
    var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

function CreateShopCartDataTable(tableId) {
    $('#' + tableId + '').DataTable({
        "processing": true,
        "serverSide": true,
        "responsive": true,
        "autoWidth": false,
        "paging": false,
        "searching": false,
        "info": false,
        "columnDefs": [
            { responsivePriority: 1, targets: 0 },
            { responsivePriority: 2, targets: -1 }
        ],
        "ajax": {
            "type": "GET",
            "url": searchControllerPath() + "/JsonTableFill",
            "data": {
                "json": localStorage.getItem("cart"),
            },
            "dataSrc": function (json) {
                var totalPrice = 0;
                //Make your callback here.
                $.each(json.data, function (index, item) {
                    item.ProductName = '<img src="/images/' + item.Category + '/' + item.ID + '.jpg" class="shopCartImage"/> <span>' + item.ProductName + '</span>';
                    item.Remove = '<a href="javascript:" onclick="RemoveFromCart(' + item.ID + ')" /> <i class="fa fa-remove coloronwhite"></i></a >';
                    item.TotalPrice = item.UnitPrice * item.Quantity;
                    item.Quantity = '<input type="number" min="1" max="255" value=' + item.Quantity + ' onblur="ChangeQuantity(' + item.ID + ',value)"/> ';
                    totalPrice = totalPrice + item.TotalPrice;
                })
                $('#totalComanda').text(totalPrice.formatMoney(2, ',', ' ') + " lei");
                return json.data;
            },
        },
        "columns": [
            { 'data': 'ProductName' },
            { 'data': 'Quantity' },
            {
                'data': 'UnitPrice',
                'render': function (data, type, row) {
                    if (data == 1)
                        return data + " leu";
                    return data + " lei";
                }
            },
            {
                'data': 'TotalPrice',
                'render': function (data, type, row) {
                    if (data == 1)
                        return data + " leu";
                    return data + " lei";
                }
            },
            {
                'data': 'Remove',
                'orderable': false,
            },
        ]

    });
}

$(document).ready(function () {
    CreateShopCartDataTable("ShopCartTable");
});