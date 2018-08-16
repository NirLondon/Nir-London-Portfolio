function setCookie(cookieName, key, value/*, exdays*/, path) {
    //var d = new Date();
    //d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    //var expires = "expires=" + d.toUTCString();
    var cookie = getCookie(cookieName);
    key += "=";
    if (cookie) {
        var keyVals = cookie.split("&");
        for (var i = 0; i < keyVals.length; i++) {
            if (keyVals[i].startsWith(key)) {
                var keyVal = keyVals[i].split(key);
                keyVal[1] = value;
                keyVals[i] = keyVal.join("");
                value = keyVals.join("");
                break;
            }
        }
    }
    document.cookie = cookieName + "=" + key + value + /*";"+ expires +*/ "; path=/" + (function () { if (path) return path; return ""; })();
}

function getCookie(cookieName, key) {
    cookieName += "=";
    var cookies = document.cookie;
    if (cookies) {
        cookies = decodeURIComponent(cookies).split(';');
        for (var i = 0; i < cookies.length; i++) {
            var cookie = cookies[i];
            while (cookie.charAt(0) === " ") {
                cookie = cookie.substring(1);
            }
            if (cookie.startsWith(cookieName)) {
                if (key) {
                    key += "=";
                    cookie = cookie.substring(cookieName.length, cookie.length).split("&");
                    for (i = 0; i < cookie.length; i++) {
                        if (cookie[i].startsWith(key)) {
                            return cookie[i].substring(key.length, cookie[i].length);
                        }
                    }
                }
                return cookie.substring(cookieName.length, cookie.length);
            }
        }
    }
    return "";
}

function PrintCookies() {
    return document.cookie;
}