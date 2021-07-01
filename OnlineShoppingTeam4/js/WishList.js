//Local Storage Class
function WishListProducts(productId, quantity) {
    this.ID = productId;
    this.Quantity = quantity;
}

//export local storage to server (when user is loged in)
function exportLocalWishListToServer() {
    if (isLogedIn == 1 && localStorage.getItem("wishlist") != null && localStorage.getItem("wishlist") != "") {
        $.ajax({
            url: "https://" + window.location.host + "/wishlist/ImportFromLocal",
            "data": {
                "json": localStorage.getItem("wishlist"),
            },
            dataType: "json",
            type: "POST"
        })
            .done(function () {
                localStorage.setItem("wishlist", "");
            })
            .fail(function () {
                alert("An error occurred while sending the local product list to the server");
            })
    }
}

//animate shopcart when a product is added
function animateWishList() {
    $(".wishlistcontainer #wishlist-productcount").finish().animate(
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
function AddToWishList(productToAdd) {
    debugger
    if (isLogedIn == 0) {
        //need to check if this customer is loged in
        var productsInStorage = localStorage.getItem("wishlist") ? JSON.parse(localStorage.getItem("wishlist")) : new Array();
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
        localStorage.setItem("wishlist", JSON.stringify(productsInStorage));
        UpdateWishList();
        animateWishList();
    }
    else {
        $.ajax({
            url: "https://" + window.location.host + "/wishlist/Create",
            data: {
                id: productToAdd.ID,
                quantity: productToAdd.Quantity
            }
        })
            .done(function () {
                //ar trebui modificat
                UpdateWishList();
                animateWishList();
                $("#WishListTable").DataTable({
                    destroy: true,
                });
                CreateWishListDataTable("WishListTable");
            })
            .fail(function () {
                alert("Something went wrong");
            });
    }
}
//add product in wishlist
function AddToWishList(productToAdd) {
    if (isLogedIn == 0) {
        //need to check if this customer is loged in
        var productsInStorage = localStorage.getItem("wishlist") ? JSON.parse(localStorage.getItem("wishlist")) : new Array();
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
        localStorage.setItem("wishlist", JSON.stringify(productsInStorage));
        UpdateWishList();
        animateWishList();
    }
    else {
        $.ajax({
            url: "https://" + window.location.host + "/WishList/Create",
            data: {
                id: productToAdd.ID,
                quantity: productToAdd.Quantity
            }
        })
            .done(function () {
                //ar trebui modificat
                UpdateWishList();
                animateWishList();
                $("#WishListTable").DataTable().destroy();
                CreateWishListDataTable("WishListTable");
            })
            .fail(function () {
                alert("Something went wrong");
            });
    }
}


//change product quantity for product with "id"
function ChangeQuantity(id, quantity) {
    if (quantity < 1 || quantity > 32767) {
        $("#WishListTable").DataTable().destroy();
        CreateWishListDataTable("WishListTable");
    }
    else {
        if (isLogedIn == 0) {
            var productsInStorage = localStorage.getItem("wishlist") ? JSON.parse(localStorage.getItem("wishlist")) : new Array();
            var i = 0;
            for (; i < productsInStorage.length; i++) {
                if (productsInStorage[i].ID == id) {
                    break;
                }
            }
            productsInStorage[i].Quantity = quantity;
            localStorage.setItem("wishlist", JSON.stringify(productsInStorage));
            $("#WishListTable").DataTable().destroy();
            CreateShopCartDataTable("WishListTable");
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
                    $("#WishListTable").DataTable().destroy();
                    CreateShopCartDataTable("WishListTable");
                })
                .fail(function () {
                    alert("Something went wrong");
                });
        }
    }
}

//delete a product from shopcart (local/server)
function RemoveFromWishList(id) {

    if (isLogedIn == 0) {
        var productsInStorage = localStorage.getItem("wishlist") ? JSON.parse(localStorage.getItem("wishlist")) : new Array();

        var i = 0;
        for (; i < productsInStorage.length; i++) {
            if (productsInStorage[i].ID == id) {
                break;
            }
        }
        productsInStorage.splice(i, 1);
        localStorage.setItem("wishlist", JSON.stringify(productsInStorage));
        UpdateWishList();
        animateWishList();
        $("#WishListTable").DataTable().destroy();
        CreateShopCartDataTable("WishListTable");
    }
    else {
        $.ajax({
            url: searchControllerPath() + "/Delete?id=" + id,
        })
            .done(function () {
                UpdateWishList();
                animateWishList();
                $("#WishListTable").DataTable().destroy();
                CreateShopCartDataTable("WishListTable");
            })
            .fail(function () {
                alert("Something went wrong");
            });
    }
}

//get array of products
function getLocalWishListProducts() {
    //also we need to see how we manage this
    return localStorage.getItem("wishlist") ? JSON.parse(localStorage.getItem("wishlist")) : new Array();
}

//count number of product in shopcart (local)
function getLocalWishListCount() {

    return (localStorage.getItem("wishlist") ? JSON.parse(localStorage.getItem("wishlist")).length : new Array().length);
}

//count number of product in shopcart (server)
function getServerWishListCount() {
    if (isLogedIn == 1) {
        $.ajax({
            url: "https://" + window.location.host + "/wishlist/GetCartCount",
            dataType: "json",
            success: function (data) {
                $("#wishlist-productcount").text(data);
            }
        })
    }
}

//update products count
function UpdateWishList() {
    if (isLogedIn == 1) {
        getServerCartCount();
    }
    else {
        $("#wishlist-productcount").text(getLocalWishListCount());
    }

}

//update products count
function UpdateWishList() {
    if (isLogedIn == 1) {
        getServerWishListCount();
    }
    else {
        $("#wishlist-productcount").text(getLocalWishListCount());
    }

}

//when page was loaded
$("document").ready(function () {

    //shopcart count item
    if (isLogedIn == 1) {
        if (getLocalCartCount()) {
            var sendToServer = confirm("Do you have products in the wishlist, do you want to add them to those in the database?");
            if (sendToServer) {
                exportLocalShopCartToServer();
                UpdateShop();
            }
            else {
                localStorage.setItem("wishlist", "");
            }
        }

        //discontinued product make it unavailable
        $(".discontinued").css("color", "black").prop("title", "Product Unavailable");
        $(".discontinued .shopcartcontainer-products").detach();
        $(".discontinued img").addClass("grayscale90");
    }
    UpdateWishList();
})