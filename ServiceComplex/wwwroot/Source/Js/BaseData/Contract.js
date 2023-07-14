

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

        return `

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

function Edit(id) {
    //todo 
    $("#editContract").modal('show');
    debugger
    $.ajax({
        type: "GET",
        url: "?handler=Edit&SLN_ID=" + id,
        success: function (result) {
            debugger
            var Contract = JSON.parse(result);
            $("#SlnName").val(Contract.SlnName);
            $("#SlnType").val(Contract.SlnType);
            //if (Contract.FrWarHosUid !=null){
            debugger
            $("#FrWarHosUid").val(Contract.FrWarHosUid);
            //}
            $("#SlnId").val(Contract.SlnId);
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
        data: { name: $("#SlnName").val(), id: $("#SlnId").val() },
        success: function (result) {
            debugger
            if (result == true) {
                edit_error.innerHTML = "نام سالن تکراری است";
                editError.style.display = "block";
                checkexist = false;
            }
            if ($("#SlnName").val() == "" || $("#SlnName").val() == null) {
                edit_error.innerHTML = "نام سالن را وارد کنید";
                editError.style.display = "block";
                checkexist = false;
            }
            //editError.style.display = "none";
            if ($("#SlnType").val() == "0") {
                edit_error.innerHTML = "نوع سالن را نتخاب کنید";
                editError.style.display = "block";
                checkexist = false;
            }
            if (checkexist) {
                debugger
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
        url: "?handler=CheckName&name=" + $("#SlnName_create").val(),
        success: function (result) {
            debugger
            if (result == true) {
                createError.style.display = "block";
                error.innerHTML = "نام سالن تکراری است";
                return
            }
            else {
                if ($("#SlnName_create").val() == "" || $("#SlnName_create").val() == null) {
                    createError.style.display = "block";
                    error.innerHTML = "نام سالن را وارد کنید";
                    return
                }
                createError.style.display = "none";
                if ($("#SlnType_create").val() == "0") {
                    error.innerHTML = "نوع سالن را نتخاب کنید";
                    createError.style.display = "block";
                    return
                }
                else {
                    createError.style.display = "none";
                    document.getElementById("CreateForm").submit();
                    $("#createContract").modal('toggle');

                }
            }
        }
    });

}


