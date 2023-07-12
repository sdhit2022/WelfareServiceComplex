

function submitEdit(){

    var form = $("#submitForm");
    form.validate();
    if (form.valid() === false) {
        return false;
    }

    $.ajax({
        url: "/BaseData/AccountClub?handler=Edit",
        data: new FormData(document.forms.submitForm),
        contentType: false,
        processData: false,
        type: 'POST',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },

        success: function (result) {

            if (result.isSucceeded) {
                notify("top center", "عملیات با موفقیت انجام شد", "success")
                window.location.href = "/BaseData/AccountClub";
            } else {
                notify("top center", result.message, "error")
                return false;
            }

        }
    })

}


//$("#editSubmit").on('click', function (evn) {
//    evn.preventDefault();
    

//    var form = $("#submitEditForm");
//    form.validate();
//    if (form.valid() === false) {
//        return false;
//    }

//    $.ajax({
//        url: "?handler=Edit",
//        data: new FormData(document.forms.submitEditForm),
//        contentType: false,
//        processData: false,
//        type: 'POST',
//        headers: {
//            RequestVerificationToken:
//                $('input:hidden[name="__RequestVerificationToken"]').val()
//        },

//        success: function (result) {
            
//            if (result.isSucceeded) {
//                notify("top center", "عملیات با موفقیت انجام شد", "success")
//                window.location.href = "/BaseData/AccountClupType";
//            } else {
//                notify("top center", result.message, "error")
//                return false;
//            }

//        }
//    })

//})


$("#addSubmit").on('click', function (evn) {
    evn.preventDefault();
    

    var form = $("#submitAddForm");
    form.validate();
    if (form.valid() === false) {
        return false;
    }

    $.ajax({
        url: "",
        data: new FormData(document.forms.submitAddForm),
        contentType: false,
        processData: false,
        type: 'POST',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (result) {
            
            if (result.isSucceeded) {
                notify("top center", "عملیات با موفقیت انجام شد", "success")
                window.location.href = "/BaseData/AccountClupType";
            } else {
                notify("top center", result.message, "error")
                return false;
            }

        }
    })

})

//function Add() {

//    $("#add").modal("show");

//}









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
                    
                    if (result.isSucceeded) {
                        swal(
                            'موفق!',
                            result.message,
                            'success'
                        ).then(function () {
                            window.location = "/BaseData/AccountClupType";
                        });
                    } else {
                        swal(
                            'خطا!',
                            result.message,
                            'error'
                        )
                    }
                }

            })

        }
    })
};

//function Edit(id, name, discountType, priceInvoice, percentDiscount, detDiscount) {
//    debugger
//    $("#edit").modal("show");
//    $("#Command_Name").val(name);
//    $("#Command_DiscountType").val(discountType);
//    $("#Command_PriceInvoice").val(priceInvoice);
//    $("#Command_PercentDiscount").val(percentDiscount);
//    $("#Command_DetDiscount").val(detDiscount);
//    $("#Command_Id").val(id);
//}

function Edit(id) {
    debugger
    $.ajax({
        url: "?handler=Edit&id=" + id,
        type: "Get",
        success: function (result) {
            debugger
            $("#content").html("");
            $("#content").html(result);
        }
    });
}


function AddAccount() {
    debugger
    $.ajax({
        url: "?handler=CreateAccount",
        type: "Get",
        success: function (result) {
            debugger
            $("#content").html("");
            $("#content").html(result);
        }
    });
    //debugger
    //$("#content").load("/Partial/_CreateAccountClub");

}
//function bindDatatable() {

//    datatable = $('#dataTable_1')
//        .DataTable({

//            "sAjaxSource": "?handler=Data",
//            "bServerSide": true,
//            "bProcessing": true,
//            "bSearchable": true,
//            "order": [[1, 'asc']],
//            "language": {
//                "oPaginate": { "sPrevious": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>', "sNext": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>' },
//                "sInfo": "صفحه _PAGE_ از _PAGES_",
//                "sSearch": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-search"><circle cx="11" cy="11" r="8"></circle><line x1="21" y1="21" x2="16.65" y2="16.65"></line></svg>',
//                "sSearchPlaceholder": "جستجو کنید...",
//                "sLengthMenu": "نتایج :  _MENU_",
//            },
//            "columns": [
//                {
//                    "data": "AccClbName",
//                    "autoWidth": true,
//                    "searchable": true
//                },
//                {
//                    "data": "AccClbCode",
//                    "autoWidth": true,
//                    "searchable": true
//                },
//                //{
//                //    "data": "AccClbBrithday",
//                //    "autoWidth": true,
//                //    "searchable": true
//                //},

//                //{
//                //    "data": "AccClubType",
//                //    "autoWidth": true,
//                //    "searchable": true
//                //},
//                //{
//                //    "data": "AccRatioType",
//                //    "autoWidth": true,
//                //    "searchable": true
//                //},
//                {
//                    "data": "AccClbSex",
//                    "autoWidth": true,
//                    "searchable": true
//                },
//                {
//                    "data": "AccClbMobile",
//                    "autoWidth": true,
//                    "searchable": true
//                }

//                //,
//                //{
//                //    data: null,
//                //    render: function (data, row, full) {
//                //        return generateButton(data);
//                //    },
//                //}

//            ]
//        });

//    function generateButton(data) {

//        return `<center><button onclick="Edit('${data.Id}','${data.Name}','${data.DiscountType}','${data.PriceInvoice}','${data.PercentDiscount}','${data.DetDiscount}')" class="btn btn-warning btn-rounded btn-sm">ویرایش</button>&nbsp; <button onclick="Remove('${data.Id}')" class="btn btn-danger btn-rounded btn-sm"> حذف </button></center>`;
//    };

//    function generateButton(data) {

//        return `<center><button onclick="Edit('${data.AccClbUid}')" class="btn btn-warning btn-rounded btn-sm">ویرایش</button>&nbsp; <button onclick="Remove('${data.Id}')" class="btn btn-danger btn-rounded btn-sm"> حذف </button></center>`;
//    };
//}

//    $(document).ready(function () {
//        debugger
//        bindDatatable();
//    });



$(document).ready(function () {
    debugger
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
                    "data": "AccClbName",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "AccClbCode",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "ShamsiBirthDay",
                    "autoWidth": true,
                    "searchable": true
                },

                //{
                //    "data": "AccClubType",
                //    "autoWidth": true,
                //    "searchable": true
                //},
                //{
                //    "data": "AccRatioType",
                //    "autoWidth": true,
                //    "searchable": true
                //},
                {
                    "data": "AccClbMobile",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "AccClbSex",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "JobName",
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

        return `<a title="جزییات بیشتر" onclick="showModal('${data.AccClbUid}')">
                                                <svg style="color:#2196f3" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-info">
                                                    <circle cx="12" cy="12" r="10"></circle>
                                                    <line x1="12" y1="16" x2="12" y2="12"></line>
                                                    <line x1="12" y1="8" x2="12.01" y2="8"></line>
                                                </svg>
                                            </a>

                                                    <a onclick="Edit('${data.AccClbUid}')" asp-page="BaseData/AccountClub" asp-page-handler="Edit" title="ویرایش" class=" mb-2 mr-2">
                                                    <svg style="color:#e2a03f !important" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-edit-2">
                                                      <path d="M17 3a2.828 2.828 0 1 1 4 4L7.5 20.5 2 22l1.5-5.5L17 3z"></path>
                                                   </svg>
                                               </a>
                                                        <a class="" onclick="Remove('${data.AccClbUid}')" title="حذف">
                                                   <svg style="color:#e7515a" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-trash-2">
                                                       <polyline points="3 6 5 6 21 6"></polyline>
                                                       <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path>
                                                       <line x1="10" y1="11" x2="10" y2="17"></line>
                                                       <line x1="14" y1="11" x2="14" y2="17"></line>
                                                   </svg>
                                               </a>` ;
    };

}



       


