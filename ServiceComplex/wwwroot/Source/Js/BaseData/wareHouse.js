

$(document).ready(function () {
    bindDatatable();
});

var editError = document.querySelector("#EditError");
var createError = document.querySelector("#CreateError");
var error = document.querySelector("#error");
var edit_error = document.querySelector("#editError");

$("#editSubmit").on('click', function (evn) {
    evn.preventDefault();
    debugger

    var form = $("#submitEditForm");
    form.validate();
    if (form.valid() === false) {
        return false;
    }

    if ($("#Name").val() == "" || $("#Code").val() == "") {
        edit_error.innerHTML = "فیلدهای ستاره دار را پر کنید";
        editError.style.display = "block";
        return

    }

    $.ajax({
        url: "?handler=Edit",
        data: new FormData(document.forms.submitEditForm),
        contentType: false,
        processData: false,
        type: 'POST',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },

        success: function (result) {
            debugger
            if (result.isSucceeded) {
                notify("top center", "عملیات با موفقیت انجام شد", "success")
                window.location.href = "/BaseData/WareHouse";
            } else {
                notify("top center", result.message, "error")
                return false;
            }

        }
    })

})


$("#addSubmit").on('click', function (evn) {
    debugger

    var form = $("#submitAddForm");
    form.validate();
    if (form.valid() === false) {
        return false;
    }
    if ($("#Name_create").val() == "" || $("#Code_create").val() == "") {
        createError.style.display = "block";
        error.innerHTML = "فیلدهای ستاره دار را پر کنید";
        return
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
            debugger
            if (result.isSucceeded) {
                notify("top center", "عملیات با موفقیت انجام شد", "success")
                window.location.href = "/BaseData/WareHouse";
            } else {
                notify("top center", result.message, "error")
                return false;
            }
        }
    })
})

function Edit(id, name, code) {
    debugger
    editError.style.display = "none";
    $("#edit").modal("show");
    $("#Name").val(name);
    $("#Code").val(code);
    $("#Id").val(id);
}

function Add() {
    debugger
    createError.style.display = "none";
    $("#add").modal("show");
}

function Remove(id) {
    debugger

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
                    debugger
                    if (result.isSucceeded) {
                        swal(
                            'موفق!',
                            result.message,
                            'success'
                        ).then(function () {
                            window.location = "/BaseData/WareHouse";
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
                    "data": "Name",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "Code",
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
        debugger
        return `   <a onclick="Edit('${data.Id}','${data.Name}','${data.Code}')" title="ویرایش" class=" mb-2 mr-2">
                                                                    <svg style="color:#e2a03f !important" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-edit-2">
                                                                      <path d="M17 3a2.828 2.828 0 1 1 4 4L7.5 20.5 2 22l1.5-5.5L17 3z"></path>
                                                                   </svg>
                                                               </a>
                                                                   <a class="" onclick="Remove('${data.Id}')" title="حذف">
                                                                   <svg style="color:#e7515a" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-trash-2">
                                                                       <polyline points="3 6 5 6 21 6"></polyline>
                                                                       <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path>
                                                                       <line x1="10" y1="11" x2="10" y2="17"></line>
                                                                       <line x1="14" y1="11" x2="14" y2="17"></line>
                                                                   </svg>
                                                               </a>`
    };




}

