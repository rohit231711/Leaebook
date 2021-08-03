
//$(document).ready(function() {
// var spanSelectedMenu = document.getElementById("spanSelectedMenu");
// var li = document.getElementById(spanSelectedMenu.innerHTML);
// if (li != null) {
// //li.className = "button_bg-active";
// li.className = "current_page_item";
// var parentLI = li.parentNode.parentNode;
// try {
// while (parentLI != null && parentLI.tagName == "LI") {
// parentLI.className = "current_page_item";
// parentLI = parentLI.parentNode.parentNode;
// }
// }
// catch (err)
// { }
// }
//});
//
$(document).ready(function () {    
    var spanSelectedMenu = $('#spanSelectedMenu');
    var test = spanSelectedMenu.text();
    var li = document.getElementById(spanSelectedMenu.text());
    //document.getElementById(spanSelectedMenu.text());
    if (li != null) {
        li.className = "sub_show";
        // alert(li);
        $(li).parent().parent().parent().parent().removeClass();
        $(li).parent().parent().removeClass();
        $(li).parent().parent().parent().parent().addClass("current");
        $(li).parent().parent().addClass("select_sub show");
    }
});
