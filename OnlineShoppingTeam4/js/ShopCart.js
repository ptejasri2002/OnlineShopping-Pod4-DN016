//Local Storage Class
function CartProducts(productId, quantity) {
    this.ID = productId;
    this.Quantity = quantity;
}

//export local storage to server (when user is loged in)
function exportLocalShopCartToServer() {
    if (isLogedIn == 1 && localStorage.getItem("cart") != null && localStorage.getItem("cart") != "") {
        $.ajax({
            url: "https://" + window.location.host + "/ShopCart/ImportFromLocal",
            "data": {
                "json": localStorage.getItem("cart"),
            },
            dataType: "json",
            type: "POST"
        })
            .done(function () {
                localStorage.setItem("cart", "");
            })
            .fail(function () {
                alert("An error occurred while sending the local product list to the server");
            })
    }
}

//animate shopcart when a product is added
function animateShopCart() {
    $(".shopcartcontainer #shopcart-productcount").finish().animate(
        {
            "font-size": "+=12px",
        },
        200,
        function () {
            //complete
            $(this).animate(
                {
                    "font-size": "-=12px",
                },
                200
            )
        }

    );
}

//add product in cart
function AddToCart(productToAdd) {
    debugger
    if (isLogedIn == 0) {
        //need to check if this customer is loged in
        var productsInStorage = localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : new Array();
        var add = true;
        var i = 0;
        for (; i < productsInStorage.length; i++) {
            if (productsInStorage[i].ID == productToAdd.ID) {
                add = false;
                break;
            }
        }
        if (add) {
            productsInStorage.push(productToAdd);
        }
        else {
            productsInStorage[i].Quantity++;
        }
        localStorage.setItem("cart", JSON.stringify(productsInStorage));
        UpdateShop();
        animateShopCart();
    }
    else {
        $.ajax({
            url: "https://" + window.location.host + "/ShopCart/Create",
            data: {
                id: productToAdd.ID,
                quantity: productToAdd.Quantity
            }
        })
            .done(function () {
                //ar trebui modificat
                UpdateShop();
                animateShopCart();
                $("#ShopCartTable").DataTable({
                    destroy: true,
                });
                CreateShopCartDataTable("ShopCartTable");
            })
            .fail(function () {
                alert("Ceva nu a mers bine");
            });
    }
}
//add product in wishlist
function AddToWishList(productToAdd) {
    if (isLogedIn == 0) {
        //need to check if this customer is loged in
        var productsInStorage = localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : new Array();
        var add = true;
        var i = 0;
        for (; i < productsInStorage.length; i++) {
            if (productsInStorage[i].ID == productToAdd.ID) {
                add = false;
                break;
            }
        }
        if (add) {
            productsInStorage.push(productToAdd);
        }
        else {
            productsInStorage[i].Quantity++;
        }
        localStorage.setItem("cart", JSON.stringify(productsInStorage));
        UpdateShop();
        animateShopCart();
    }
    else {
        $.ajax({
            url: "https://" + window.location.host + "/ShopCart/Create",
            data: {
                id: productToAdd.ID,
                quantity: productToAdd.Quantity
            }
        })
            .done(function () {
                //ar trebui modificat
                UpdateShop();
                animateShopCart();
                $("#ShopCartTable").DataTable().destroy();
                CreateShopCartDataTable("ShopCartTable");
            })
            .fail(function () {
                alert("Something went wrong");
            });
    }
}


//change product quantity for product with "id"
function ChangeQuantity(id, quantity) {
    if (quantity < 1 || quantity > 32767) {
        $("#ShopCartTable").DataTable().destroy();
        CreateShopCartDataTable("ShopCartTable");
    }
    else {
        if (isLogedIn == 0) {
            var productsInStorage = localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : new Array();
            var i = 0;
            for (; i < productsInStorage.length; i++) {
                if (productsInStorage[i].ID == id) {
                    break;
                }
            }
            productsInStorage[i].Quantity = quantity;
            localStorage.setItem("cart", JSON.stringify(productsInStorage));
            $("#ShopCartTable").DataTable().destroy();
            CreateShopCartDataTable("ShopCartTable");
        }
        else {
            $.ajax({
                url: searchControllerPath() + "/UpdateQuantity",
                data: {
                    id: id,
                    quantity: quantity
                }
            })
                .done(function () {
                    //ar trebui modificat
                    $("#ShopCartTable").DataTable().destroy();
                    CreateShopCartDataTable("ShopCartTable");
                })
                .fail(function () {
                    alert("Something went wrong");
                });
        }
    }
}

//delete a product from shopcart (local/server)
function RemoveFromCart(id) {

    if (isLogedIn == 0) {
        var productsInStorage = localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : new Array();

        var i = 0;
        for (; i < productsInStorage.length; i++) {
            if (productsInStorage[i].ID == id) {
                break;
            }
        }
        productsInStorage.splice(i, 1);
        localStorage.setItem("cart", JSON.stringify(productsInStorage));
        UpdateShop();
        animateShopCart();
        $("#ShopCartTable").DataTable().destroy();
        CreateShopCartDataTable("ShopCartTable");
    }
    else {
        $.ajax({
            url: searchControllerPath() + "/Delete?id=" + id,
        })
            .done(function () {
                UpdateShop();
                animateShopCart();
                $("#ShopCartTable").DataTable().destroy();
                CreateShopCartDataTable("ShopCartTable");
            })
            .fail(function () {
                alert("Something went wrong");
            });
    }
}

//get array of products
function getLocalCartProducts() {
    //also we need to see how we manage this
    return localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : new Array();
}

//count number of product in shopcart (local)
function getLocalCartCount() {

    return (localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")).length : new Array().length);
}

//count number of product in shopcart (server)
function getServerCartCount() {
    if (isLogedIn == 1) {
        $.ajax({
            url: "https://" + window.location.host + "/ShopCart/GetCartCount",
            dataType: "json",
            success: function (data) {
                $("#shopcart-productcount").text(data);
            }
        })
    }
}

//update products count
function UpdateShop() {
    if (isLogedIn == 1) {
        getServerCartCount();
    }
    else {
        $("#shopcart-productcount").text(getLocalCartCount());
    }

}

//when page was loaded
$("document").ready(function () {

    //shopcart count item
    if (isLogedIn == 1) {
        if (getLocalCartCount()) {
            var sendToServer = confirm("Do you have products in the shopcart, do you want to add them to those in the database?");
            if (sendToServer) {
                exportLocalShopCartToServer();
                UpdateShop();
            }
            else {
                localStorage.setItem("cart", "");
            }
        }

        //discontinued product make it unavailable
        $(".discontinued").css("color", "black").prop("title", "Product Unavailable");
        $(".discontinued .shopcartcontainer-products").detach();
        $(".discontinued img").addClass("grayscale90");
    }
    UpdateShop();
})