/*
=========================================
|                                       |
|           Scroll To Top               |
|                                       |
=========================================
*/
$('.scrollTop').click(function () {
    $("html, body").animate({ scrollTop: 0 });
});


$('.navbar .dropdown.notification-dropdown > .dropdown-menu, .navbar .dropdown.message-dropdown > .dropdown-menu ').click(function (e) {
    e.stopPropagation();
});

/*
=========================================
|                                       |
|       Multi-Check checkbox            |
|                                       |
=========================================
*/

function checkall(clickchk, relChkbox) {

    var checker = $('#' + clickchk);
    var multichk = $('.' + relChkbox);


    checker.click(function () {
        multichk.prop('checked', $(this).prop('checked'));
    });
}


/*
=========================================
|                                       |
|           MultiCheck                  |
|                                       |
=========================================
*/

/*
    This MultiCheck Function is recommanded for datatable
*/

function multiCheck(tb_var) {
    tb_var.on("change", ".chk-parent", function () {
        var e = $(this).closest("table").find("td:first-child .child-chk"), a = $(this).is(":checked");
        $(e).each(function () {
            a ? ($(this).prop("checked", !0), $(this).closest("tr").addClass("active")) : ($(this).prop("checked", !1), $(this).closest("tr").removeClass("active"))
        })
    }),
        tb_var.on("change", "tbody tr .new-control", function () {
            $(this).parents("tr").toggleClass("active")
        })
}

/*
=========================================
|                                       |
|           MultiCheck                  |
|                                       |
=========================================
*/

function checkall(clickchk, relChkbox) {

    var checker = $('#' + clickchk);
    var multichk = $('.' + relChkbox);


    checker.click(function () {
        multichk.prop('checked', $(this).prop('checked'));
    });
}

/*
=========================================
|                                       |
|               Tooltips                |
|                                       |
=========================================
*/

$('.bs-tooltip').tooltip();

/*
=========================================
|                                       |
|               Popovers                |
|                                       |
=========================================
*/

$('.bs-popover').popover();


/*
================================================
|                                              |
|               Rounded Tooltip                |
|                                              |
================================================
*/

$('.t-dot').tooltip({
    template: '<div class="tooltip status rounded-tooltip" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
})


/*
================================================
|            IE VERSION Dector                 |
================================================
*/

function GetIEVersion() {
    var sAgent = window.navigator.userAgent;
    var Idx = sAgent.indexOf("MSIE");

    // If IE, return version number.
    if (Idx > 0)
        return parseInt(sAgent.substring(Idx + 5, sAgent.indexOf(".", Idx)));

    // If IE 11 then look for Updated user agent string.
    else if (!!navigator.userAgent.match(/Trident\/7\./))
        return 11;

    else
        return 0; //It is not IE
}




//position: top left, top right, middle,bottom,top,center,right,top right, 
//type: success,warn ,error,info
https://notifyjs.jpillora.com/

function notify(position, text, type) {
    $.notify(text, {
        className: type,
        clickToHide: true,
        autoHide: true,
        globalPosition: position
    });
}


//cookie


function getCookie(cname) {
    var name = cname + '=';
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return '';
}

function getParseCookie(cname) {
    var stringify = getCookie(cname);
    return JSON.parse(stringify);
}

function setCookie(cname, cvalue, path = "/", exdays = 1) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = 'expires=' + d.toUTCString();
    document.cookie = cname + '=' + JSON.stringify(cvalue) + ';' + expires + ';path=' + path;
}


function setCookieList(cname, cvalue, productId, path = "/", exdays = 1) {
    
    var cookie = getCookie(cname);

    if (cookie === "")
        setCookie(cname, cvalue, path, exdays);
    else {
        var parse = JSON.parse(cookie);

        const found = parse.find(element => element.productId === productId);
        if (found != undefined) {
            var test = parse.filter(element => element.productId === found.productId);

            test.push.apply(test, cvalue);
            setCookie(cname, parse);
        }
        else {
            cvalue.push.apply(cvalue, parse);
            setCookie(cname, cvalue, path, exdays);
        }

    }
}

function deleteAllCookies() {
    const cookies = document.cookie.split(";");

    for (let i = 0; i < cookies.length; i++) {
        const cookie = cookies[i];
        const eqPos = cookie.indexOf("=");
        const name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
    }
}
function deleteCookie(name, path = "/") {
    document.cookie = name + '=; Path=' + path + '; Expires = Thu, 01 Jan 1970 00: 00: 01 GMT; ';
}


$(document).on('click', '[data-dismiss="modal"]', function () {

    //To Do when close modal

})

function reinitialise(dataTableId) {
    if ($.fn.DataTable.isDataTable("#" + dataTableId)) {

        $("#" + dataTableId).dataTable().fnClearTable();
        $("#" + dataTableId).dataTable().fnDestroy();
    }
}


function showLoader() {
   $("#requestLoader").removeClass("d-none");
}
function hideLoader() {
    $("#requestLoader").addClass("d-none");

}