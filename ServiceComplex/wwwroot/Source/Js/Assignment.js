//$(".category-main-checkbox").change(function () {
//    if (this.checked) {
//        $("#prodcut-unit").modal('show')

//    }
////});
//$(document).on('change', 'input[name^="main-category-checkbox"]', function () {
//    var id = this.id;
//    if (this.checked) {
//          this.find("input: checkbox[id= ${id}]").prop("checked", true);
//    }
//        $('.mainCheckboxes').prop("checked", true);
   
  
//    $("input:checkbox[class=.mainCheckboxes]:checked").each(function () {
//        alert("Id: " + $(this).attr("id") + " Value: " + $(this).val());
//   // $('input:checkbox').not(this).prop('checked', this.checked);
//    alert(closest('li').find('#category-span').text());

//    }); 

    function checkboxControl(value) {
        debugger
        var ch = value.find(`input: checkbox[id=${value.id}]`);//.prop("id", value.id);// $(".main-category-checkbox").attr('id', value.id);
        if (ch.checked) {
            $(".mainCheckboxes").attr('id', value.id).prop("checked", true);
        } else {
            $(".mainCheckboxes").attr('id', value.id).prop("checked", false);
        }
    }