
function checkboxControl(value) {
    $("input:checkbox[id=" + value.id + "]").prop('checked', $(value).prop('checked'));
}

function removeSelected() {
    var i = 0;
    var list = new Array();

    while (i<$('input[name="product-checkbox"]:checked').length) {
        
        $('input[name="product-checkbox"]:checked').each(function () {
            list[i] = this.value;
            i++;
        });

    }
    
    debugger
    $.ajax({
        url: "?handler=RemoveSelectedProducts&products=" + list,
        type: "Get",
        success: function (result) {
            debugger
            $("#content").html("");
            $("#content").html(result);
        }
        });

}
function removeAll() {
    var i = 0;
    var list = new Array();

    while (i<$('input[name="product-checkbox"]').length) {
        
        $('input[name="product-checkbox"]').each(function () {
            list[i] = this.value;
            i++;
        });

    }
    
    debugger
    $.ajax({
        url: "?handler=RemoveSelectedProducts&products=" + list,
        type: "Get",
        success: function (result) {
            debugger
            $("#content").html("");
            $("#content").html(result);
        }
        });

}
function moveSelected() {
    var i = 0;
    var list = new Array();

    while (i<$('input[name="main-product-checkbox"]:checked').length) {
        
        $('input[name="main-product-checkbox"]:checked').each(function () {
            list[i] = this.value;
            i++;
        });

    }
    
    debugger
    $.ajax({
        url: "?handler=MoveSelectedProducts&products=" + list,
        type: "Get",
        success: function (result) {
            debugger
            $("#content").html("");
            $("#content").html(result);
        }
        });

}
function moveAll() {
    var i = 0;
    var list = new Array();

    while (i<$('input[name="main-product-checkbox"]').length) {
        
        $('input[name="main-product-checkbox"]').each(function () {
            list[i] = this.value;
            i++;
        });

    }
    
    debugger
    $.ajax({
        url: "?handler=MoveSelectedProducts&products=" + list,
        type: "Get",
        success: function (result) {
            debugger
            $("#content").html("");
            $("#content").html(result);
        }
        });

}

function saveAll() {
    $.ajax({
        url: "?handler=SaveChanges" ,
        type: "Get",
        success: function (result) {
            if (result) {
                notify("top center", "اطلاعات با موفقیت ثبت شد", "success");
                window.location.href = "/BaseData/Salon";
            } else
                notify("top center", "خطا در ثبت اطلاعات", "error");
        }
    });
}

function mainSelection(item) {
    if (item.value == 0) {
        $("input:checkbox[name=main-product-checkbox]").prop('checked',false);
    }
    if (item.value == 1) {
        $("input:checkbox[name=main-product-checkbox]").prop('checked',true);
    }
    
}
function selection(item) {
    if (item.value == 0) {
        $("input:checkbox[name=product-checkbox]").prop('checked',false);
    }
    if (item.value == 1) {
        $("input:checkbox[name=product-checkbox]").prop('checked',true);
    }
    
}
