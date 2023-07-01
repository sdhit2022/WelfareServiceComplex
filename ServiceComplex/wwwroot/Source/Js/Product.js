


//const table = $('#property-dataTable').DataTable({
//    paging: false,
//    ordering: true,
//    info: false,
//    searching: false,
//});


//const pictureTable = $('#picture-dataTable').DataTable({
//    paging: false,
//    ordering: true,
//    info: false,
//    searching: false,
//});

//var secondUpload = new FileUploadWithPreview('mySecondImage');




$(".checkbox").change(function () {
    if (this.checked) {
        $("#prodcut-unit").modal('show')

    }
});
$(".product-unit").change(function () {
    if (this.checked) {
        $("#related-products").modal('show')
    }
});


$("input[name='Command.PrdDiscountType']").change(function () {
    
    var input = $("input[name='Command.PrdDiscount");
    input.prop('disabled', false);

    if (this.value == 0)
        input.prop('max', 100);
    else input.prop('max', null);

    var text = "";
    if (this.value == 0)
        text = "درصد";
    else text = "مبلغ"

    $("#dicountDis").text("تخفیف را به " + text + " " + "وارد کنید  ")
});


function ChangeDiscountValue() {
    

    var input = $("input[name='Command.PrdDiscountType']:checked");

    if (input.val() == 0)
        input.prop('max', 100);
    else input.prop('max', null);

    var text = "";
    if (input.val() == 0)
        text = "درصد";
    else text = "مبلغ"

    $("#dicountDis").text("تخفیف را به " + text + " " + "وارد کنید  ")
};







function CheckControl(value) {
    
    $.ajax({
        url: "?handler=CheckCode&Code=" + value,
        type: "Get",
        success: function (result) {
            
            $("#validateCode").text("")
            $("#validateCode").text(result)
        }
    })
}


var format = true;

function readURL(input) {
   
    if (input.files && input.files[0]) {
       
        var reader = new FileReader();
        reader.onload = (function (theFile) {

            var image = new Image();
            image.src = theFile.target.result;

            var preview = document.getElementById("preViewImg");
            preview.src = image.src;

            image.onload = function () {
         
                if (this.currentSrc.indexOf('webp') > -1) {
                    $("#validateImg").text("فرمت عکس معتبر نیست")
                    format = false;
                    return;
                }
                if (this.width === 600 && this.height === 600) {
                    {

                        $("#validateImg").text("")
                        //TO Do validation
                    }
                } else {
                    $("#validateImg").text("سایز عکس معتبر نیست")

                }

            };
        });
        reader.readAsDataURL(input.files[0]);
    }
}

$("#imgCourseUp").change(function () {
    readURL(this);
});

var submitForm1 = false;
var submitForm2 = false;

$("#first-form-edit").on('click', function (env) {
    env.preventDefault();

    debugger
    var form = $("#ProductForm-edit");
    form.validate();
    if (form.valid() === false) {

        notify("top center", "فیلدهای ستاره دار را پر کنید", "error");
        return false;
    }
    
    if (divunit.style.display == "block" && $('#ProductUnit-edit').val() =="") {
        notify("top center", "فیلدهای ستاره دار را پر کنید", "error");
        return false;
    }
    var vali = form.validate();

    if (!vali.valid()) {
        notify("top center", "فرم را به درستی پر کنید", "error");
        return false;
    }
    if (format == false) {
        notify("top center", "فرمت عکس صحیح نیست", "error");
        format = true;
        return false;
    }


    if ($("#productUnit-edit").prop('checked') == true) {

        var unit2 = $("#FkProductUnit2-edit").val();
        var Coefficient = $("#PrdCoefficient-edit").val();
        var bigger = $("input[name='inlineRadioOptions-edit']").val();

        $("#Command_FkProductUnit2").val(unit2);
        $("#Command_PrdCoefficient").val(Coefficient);
        $("#Command_PrdIsUnit1Bigger").val(bigger);

        if (unit2 === "" || Coefficient === "") {
            notify("top center", "واحد شمارش 2 و ضریب آن را وارد کنید!", "error")
            return false;
        }
    };
    debugger
    notify("top center", "فرم تایید شد", "success");
    //$("#justify-profile-tab").removeClass("disabled");
    document.getElementById("justify-profile-tab").classList.remove("disabled");

    //document.getElementById("justify-home-tab").style.display = "none";
    //document.getElementById("justify-profile-tab").style.display = "block";

    //document.getElementById("justify-home-tab").classList.remove("active");
    //document.getElementById("justify-profile-tab").classList.add("active");



   
    submitForm1 = true;

});
$("#first-form").on('click', function (env) {
    env.preventDefault();

    debugger
    var form = $("#ProductForm");
    form.validate();
    if (form.valid() === false) {

        notify("top center", "فیلدهای ستاره دار را پر کنید", "error");
        return false;
    }
    
    if (divunit.style.display == "block" && $('#ProductUnit').val() =="") {
        notify("top center", "فیلدهای ستاره دار را پر کنید", "error");
        return false;
    }
    var vali = form.validate();

    if (!vali.valid()) {
        notify("top center", "فرم را به درستی پر کنید", "error");
        return false;
    }
    if (format == false) {
        notify("top center", "فرمت عکس صحیح نیست", "error");
        format = true;
        return false;
    }


    if ($("#productUnit").prop('checked') == true) {

        var unit2 = $("#FkProductUnit2").val();
        var Coefficient = $("#PrdCoefficient").val();
        var bigger = $("input[name='inlineRadioOptions']").val();

        $("#Command_FkProductUnit2").val(unit2);
        $("#Command_PrdCoefficient").val(Coefficient);
        $("#Command_PrdIsUnit1Bigger").val(bigger);

        if (unit2 === "" || Coefficient === "") {
            notify("top center", "واحد شمارش 2 و ضریب آن را وارد کنید!", "error")
            return false;
        }
    };
    debugger
    notify("top center", "فرم تایید شد", "success");
    //$("#justify-profile-tab").removeClass("disabled");
    document.getElementById("justify-profile-tab").classList.remove("disabled");

    //document.getElementById("justify-home-tab").style.display = "none";
    //document.getElementById("justify-profile-tab").style.display = "block";

    //document.getElementById("justify-home-tab").classList.remove("active");
    //document.getElementById("justify-profile-tab").classList.add("active");



   
    submitForm1 = true;

});




$("#final-submit").on('click', function (env) {
    env.preventDefault();
    debugger
    var form = $("#ProductForm");
    form.validate();
    if (form.valid() === false) {

        notify("top center", "فیلدهای ستاره دار را پر کنید", "error");
        return false;
    }
    
    if (times.style.display == "block" && $("#hasTiming").val() =="true") {
        if ($('#PrdBaseTime').val() == "" || $('#PrdBaseCost').val() == "" || $('#PrdExtraTime').val() == "" ||
            $('#PrdExtraCost').val() == "" || $('#PrdMinTime').val() == "" || $('#PrdMaxTime').val() == "" ||
            $('#PrdMinCharge').val() == "") {
            notify("top center", "فیلدهای ستاره دار را پر کنید", "error");
            return false;
        }
    } else {
        if ($('#PrdPricePerUnit1').val() == "") {
            notify("top center", "فیلدهای ستاره دار را پر کنید", "error");
            return false;
        }

    }
    var vali = form.validate();

    if (!vali.valid()) {
        notify("top center", "فرم را به درستی پر کنید", "error");
        return false;
    }
    debugger
    notify("top center", "فرم تایید شد", "success");
    $("#justify-contact-tab").removeClass("disabled");
    $("#justify-pictures-tab").removeClass("disabled");

    submitForm2 = true;

 

    $.ajax({
        url: '',
        data: new FormData(document.forms.ProductForm),
        contentType: false,
        processData: false,
        type: 'POST',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },

        success: function (result) {
            
            if (result.isSucceeded) {
                notify("top center", "اطلاعات با موفقیت ثبت شد", "success");
                window.location.href = "/Products/Index";
            } else {
                notify("top center", result.message, "error");
            }

        }

    });
});
$("#final-submit-edit").on('click', function (env) {
    env.preventDefault();
    debugger
    var form = $("#ProductForm-edit");
    form.validate();
    if (form.valid() === false) {

        notify("top center", "فیلدهای ستاره دار را پر کنید", "error");
        return false;
    }
    
    if (times.style.display == "block" && $("#hasTiming-edit").val() =="true") {
        if ($('#PrdBaseTime-edit').val() == "" || $('#PrdBaseCost-edit').val() == "" || $('#PrdExtraTime-edit').val() == "" ||
            $('#PrdExtraCost-edit').val() == "" || $('#PrdMinTime-edit').val() == "" || $('#PrdMaxTime-edit').val() == "" ||
            $('#PrdMinCharge-edit').val() == "") {
            notify("top center", "فیلدهای ستاره دار را پر کنید", "error");
            return false;
        }
    } else {
        if ($('#PrdPricePerUnit1-edit').val() == "") {
            notify("top center", "فیلدهای ستاره دار را پر کنید", "error");
            return false;
        }

    }
    var vali = form.validate();

    if (!vali.valid()) {
        notify("top center", "فرم را به درستی پر کنید", "error");
        return false;
    }
    debugger
    notify("top center", "فرم تایید شد", "success");
    $("#justify-contact-tab").removeClass("disabled");
    $("#justify-pictures-tab").removeClass("disabled");

    submitForm2 = true;

 

    $.ajax({
        url: '',
        data: new FormData(document.forms.ProductForm-edit),
        contentType: false,
        processData: false,
        type: 'POST',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },

        success: function (result) {
            
            if (result.isSucceeded) {
                notify("top center", "اطلاعات با موفقیت ثبت شد", "success");
                window.location.href = "/Products/Index";
            } else {
                notify("top center", result.message, "error");
            }

        }

    });
});


var div = document.querySelector("#isService");
var divunit2 = document.querySelector("#prdUnit2");
var divunit = document.querySelector("#prdunit");
var productUnit = document.getElementById("prdunit");
var prices = document.querySelector("#prices");
var times = document.querySelector("#times");
var isServiceValue = document.getElementById("selectType").value;
function CheckValue(value) {

   
    isServiceValue = value;
    if (value == 2) {

        div.style.display = "block";
        divunit2.style.display = "none";
        divunit.style.display = "none";
    }
    else {
        $("#hasTiming").prop('checked', false);
        prices.style.display = "block";
        times.style.display = "none";
        div.style.display = "none";
        divunit2.style.display = "block";
        divunit.style.display = "block";
        //document.getElementById("isService").style.display = "none";
    }



}
var hastiming = false;
$('#hasTiming').change(function () {
    
    if (isServiceValue == 2 && this.checked) {
        prices.style.display = "none";
        times.style.display = "block";
        hastiming = true;

    }
    else {
        prices.style.display = "block";
        times.style.display = "none";
        hastiming = false;

    }
});
