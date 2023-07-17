$(document).ready(function () {
    
    bindDatatable();
});

function bindDatatable() {
    datatable = $('#dataTable_1')
        .DataTable({
            "sAjaxSource": "?handler=Data",
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "order": [[1, 'asc']],
            "language": {
                "oPaginate": { "sPrevious": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>', "sNext": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>' },
                "sInfo": "صفحه _PAGE_ از _PAGES_",
                "sSearch": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-search"><circle cx="11" cy="11" r="8"></circle><line x1="21" y1="21" x2="16.65" y2="16.65"></line></svg>',
                "sSearchPlaceholder": "جستجو کنید...",
                "sLengthMenu": "نتایج :  _MENU_",
            },
            "columns": [
                {
                    "data": "CntTitle",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "CntContractNum",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "CntTypeText",
                    "autoWidth": true,
                    "searchable": true
                },

                {
                    "data": "CntStartDateShamsi",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "CntEndDateShamsi",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "CntCreateonShamsi",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    data: null,
                    render: function (data, row, full) {
                        return generateButton(data);
                    },
                }

            ]
        });

    function generateButton(data) {

        return `<a title="تعیین نرخ" onclick="DefineRate('${data.CntId}')">
                                                        <svg style="color:#2196f3" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-info">
                                                            <circle cx="12" cy="12" r="10"></circle>
                                                            <line x1="12" y1="16" x2="12" y2="12"></line>
                                                            <line x1="12" y1="8" x2="12.01" y2="8"></line>
                                                        </svg>
                                                    </a>

                                                                            <a onclick="Edit('${data.CntId}')" asp-page="BaseData/AccountClub" asp-page-handler="Edit" title="ویرایش" class=" mb-2 mr-2">
                                                                    <svg style="color:#e2a03f !important" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-edit-2">
                                                                      <path d="M17 3a2.828 2.828 0 1 1 4 4L7.5 20.5 2 22l1.5-5.5L17 3z"></path>
                                                                   </svg>
                                                               </a>
                                                                                <a class="" onclick="Remove('${data.CntId}')" title="حذف">
                                                                   <svg style="color:#e7515a" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-trash-2">
                                                                       <polyline points="3 6 5 6 21 6"></polyline>
                                                                       <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path>
                                                                       <line x1="10" y1="11" x2="10" y2="17"></line>
                                                                       <line x1="14" y1="11" x2="14" y2="17"></line>
                                                                   </svg>
                                                               </a>` ;
    };

}

function AddProducts() {
    debugger;
    if ( $("#CdCreditLimit").val() == "" || $("#CdDiscountPercent").val() == "" || $("#CdDiscountRial").val() == "") {
        notify("top center", " فیلدهای ستاره دار را پر کنید", "error");
        return;
    }
    if ($("#CntCategory").val() == "") {
        notify("top center", "لطفا یک گروه کالا/خدمت انتخاب کنید", "error");
        // alert("لطفا یک گروه کالا/خدمت انتخاب کنید");
        return
    }
    debugger
    if ($("#CntCategory").val() == 0) {
        $.ajax({
            url: "?handler=GetAllProducts",
            type: "Get",
            success: function (result) {
                debugger
                $("#ProductCategory").html("");
                $("#ProductCategory").html(result);
            }
        });
    } else {
        debugger
        $.ajax({
            url: "?handler=GetProductsByCategory&id=" + $("#CntCategory").val(),
            type: "Get",
            success: function (result) {
                debugger
                $("#ProductCategory").html("");
                $("#ProductCategory").html(result);
                $("#Cancel").show();
                $("#AddProductsFinal").show();
            }
        });
    }
}

function Cancel() {
    window.location = "/BaseData/Contract";

}


    
$("#superseded").change(function () {
    debugger
        if (this.checked) {
            /*   if (item.isChecked == true) {*/
            $("#contractSelection").show();
        } else {
            $("#contractSelection").hide();
        }
    });
    


function AddProductsFinal() {
    debugger
    var i = 0;
    var list=[];
    var id = $("#contractId").val(); 
    var type = $("#contractType").val(); 
    var typeValue;
    switch (type) {
        case '0':
            typeValue = $("#CdCreditLimit").val(); 
            break;
        case '1':
            typeValue = $("#CdDiscountPercent").val(); 
            break;
        case '2':
            typeValue = $("#CdDiscountRial").val(); 
            break;
    }
    while (i < $('input[name="product-li"]').length) {

        $('input[name="product-li"]').each(function () {
            list.push(this.value);
            console.log(list);
            i++;
        });
    }

    $.ajax({
        url: "?handler=ProductContract&id=" + id + "&products=" + list + "&type=" + type + "&typeValue=" + typeValue,
        type: "Get",
        success: function (result) {
            debugger
            if (result.isSucceeded) {
                debugger
                swal(
                    'موفق!',
                    result.message,
                    'success'
                ).then(function () {
                    window.location = "/BaseData/Contract";
                });
            } else {
                swal(
                    'خطا!',
                    result.message,
                    'error'
                )
            }
        }
    });


}

function DefineRate(id) {
    
    $.ajax({
        url: "?handler=DefineRate&id=" + id,
        type: "Get",
        success: function (result) {
            debugger

            $("#content").html("");
            $("#content").html(result);
        }
    });
}

//function ProductCategory(item) {
//    debugger
//    $.ajax({
//        url: "?handler=ProductCategory&id=" + item.value,
//        type: "Get",
//        success: function (result) {
//            debugger
//            $("#ProductCategory").html("");
//            $("#ProductCategory").html(result);
//        }
//    });
//}

function Create() {
    $("#createContract").modal('show');
}

function Remove(id) {
    swal({
        title: 'آیا مطمئنید؟',
        text: "این عمل قابل بازگشت نیست!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'حذف',
        padding: '2em'
    }).then(function (result) {

        if (result.value) {

            $.ajax({
                url: "?handler=Remove&id=" + id,
                type: "get",
                success: function (result) {

                    if (result) {
                        swal(
                            'موفق!',
                            result.message,
                            'success'
                        ).then(function () {
                            window.location = "/BaseData/Contract";
                        });
                    } else {
                        swal(
                            'خطا!',
                            result.message,
                            'error'
                        )
                    }
                }

            });
        }
    });
}
var Contract;
function Edit(id) {
    //todo
    $("#editContract").modal('show');
    debugger
    $.ajax({
        type: "GET",
        url: "?handler=Edit&id=" + id,
        success: function (result) {
            debugger
            Contract = JSON.parse(result);
            $("#CntTitle").val(Contract.CntTitle);
            $("#CntStartDateShamsi").val(Contract.CntStartDateShamsi);
            $("#CntEndDateShamsi").val(Contract.CntEndDateShamsi);
            $("#CntType").val(Contract.CntType);
            $("#CntContractNum").val(Contract.CntContractNum);
            $("#CntId").val(Contract.CntId);
        }
    });
}

var editError = document.querySelector("#EditError");
var createError = document.querySelector("#CreateError");
var error = document.querySelector("#error");
var edit_error = document.querySelector("#editError");
function saveEdit() {
    var checkexist = true;
    debugger
    $.ajax({
        type: "get",
        url: "?handler=CheckContractNameExists",
        data: { name: $("#CntTitle").val(), id: $("#CntId").val() },
        success: function (result) {
            debugger
            if (result == true) {
                edit_error.innerHTML = "نام قرارداد تکراری است";
                editError.style.display = "block";
                checkexist = false;
            }
            if ($("#CntTitle").val() == "" || $("#CntStartDateShamsi").val() == ""
                || $("#CntEndDateShamsi").val() == "" || $("#CntType").val() == ""
                || $("#CntContractNum").val() == "") {
                edit_error.innerHTML = "فیلدها را به درستی پر کنید";
                editError.style.display = "block";
                checkexist = false;
            }

            if (checkexist) {
                debugger
                //Contract.CntId = $("#CntId").val();
                //Contract.CntTitle = $("#CntTitle").val();
                //Contract.CntStartDateShamsi = $("#CntStartDateShamsi").val();
                //Contract.CntEndDateShamsi = $("#CntEndDateShamsi").val();
                //Contract.CntType = $("#CntType").val();
                //Contract.CntContractNum = $("#CntContractNum").val();
                //$.ajax({
                //    type: "Post",
                //    url: "?handler=Edit",
                //    data: {Contract.stringify( },
                //    success: function (result) {}
                //});
                editError.style.display = "none";
                document.getElementById("EditForm").submit();
                $("#editContract").modal('toggle');
            }
        }
    });
}
function saveCreate() {
    $.ajax({
        type: "get",
        url: "?handler=CheckName&name=" + $("#CntTitle_create").val(),
        success: function (result) {
            debugger
            if (result == true) {
                createError.style.display = "block";
                error.innerHTML = "نام قرارداد تکراری است";
                return
            }
            else {
                if ($("#CntTitle_create").val() == "" || $("#CntStartDateShamsi_create").val() == ""
                    || $("#CntEndDateShamsi_create").val() == "" || $("#CntType_create").val() == ""
                    || $("#CntContractNum_create").val() == "") {
                    createError.style.display = "block";
                    error.innerHTML = "فیلدها را به درستی پر کنید";
                    return
                }//createError.style.display = "none";
                debugger
                if ($("#superseded").prop('checked') == true && $("#CntFrContract").val()==0) {
                    createError.style.display = "block";
                    error.innerHTML = "قرارداد فسخ شده را انتخاب کنید";
                    return
                }else {
                    createError.style.display = "none";
                    document.getElementById("CreateForm").submit();
                    $("#createContract").modal('toggle');
                    $("#CntTitle_create").val("");
                    $("#CntStartDateShamsi_create").val("");
                    $("#CntEndDateShamsi_create").val("")
                    $("#CntType_create").val("");
                    $("#CntContractNum_create").val("");
                    $("#superseded").prop('checked', false);
                }
            }
        }
    });

}


