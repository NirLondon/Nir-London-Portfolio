function AddToCart(id) {
    var cookie = getCookie("cart", "productsIds");
    setCookie("cart", "productsIds", cookie + id + ",");

    post("/Home/SaveProductForUser", { productId: id, userId: getCookie("user", "userId") });
}

function RemoveFromCart(id) {
    RemoveProductIdFromCookie(id);

    post("/Home/ReleaseProduct", { productId: id });
}


function RemoveProductIdFromCookie(productId) {
    var ids = getCookie("cart", "productsIds");
    var idsArr = ids.split(",");
    ids = "";
    for (var i = 0; i < idsArr.length - 1; i++)
        if (idsArr[i] != productId)
            ids += idsArr[i] + ",";
    setCookie("cart", "productsIds", ids);
}