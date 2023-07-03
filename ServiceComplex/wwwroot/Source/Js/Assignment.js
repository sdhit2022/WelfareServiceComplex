
function checkboxControl(value) {
    $("input:checkbox[id=" + value.id + "]").prop('checked', $(value).prop('checked'));
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

            alert(result);
        }
        });

}
