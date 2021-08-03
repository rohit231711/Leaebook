function responsiveMobileMenu() {
    $('.rmm').each(function () {
        $(this).children('ul').addClass('rmm-main-list'); var $style = $(this).attr('data-menu-style'); if (typeof $style == 'undefined' || $style == false)
        { $(this).addClass('graphite'); }
        else { $(this).addClass($style); }
        var $width = 0; $(this).find('ul li').each(function () { $width += $(this).outerWidth(); }); if ($.support.leadingWhitespace) { $(this).css('max-width', $width * 100 + '%'); }
        else { $(this).css('width', $width * 100 + '%'); }
    });
}
function getMobileMenu() {
    $('.rmm').each(function () {
        var menutitle = $(this).attr("data-menu-title"); if (menutitle == "") { menutitle = "Menu"; }
        else if (menutitle == undefined) { menutitle = "Menu"; }
        var $menulist = $(this).children('.rmm-main-list').html(); var $menucontrols = "<div class='rmm-toggled-controls'><div class='rmm-toggled-title'>" + menutitle + "</div><div class='rmm-button'><span> </span><span> </span><span> </span></div></div>"; $(this).prepend("<div class='rmm-toggled rmm-closed'>" + $menucontrols + "<ul>" + $menulist + "</ul></div>");
    });
}
function adaptMenu() {
    $('.rmm').each(function () {
        var $width = $(this).css('max-width'); $width = $width.replace('px', ''); if ($(this).parent().width() < $width * 1.05) { $(this).children('.rmm-main-list').hide(0); $(this).children('.rmm-toggled').show(0); }
        else { $(this).children('.rmm-main-list').show(0); $(this).children('.rmm-toggled').hide(0); }
    });
}
$(function () {
    responsiveMobileMenu(); getMobileMenu(); adaptMenu(); $('.rmm-toggled, .rmm-toggled .rmm-button').click(function () {
        if ($(this).is(".rmm-closed")) { $(this).find('ul').stop().show(300); $(this).removeClass("rmm-closed"); }
        else { $(this).find('ul').stop().hide(300); $(this).addClass("rmm-closed"); }
    });
}); $(window).resize(function () { adaptMenu(); }); $(document).ready(function () { $('.nav-toggle').click(function () { var collapse_content_selector = $(this).attr('href'); var toggle_switch = $(this); $(collapse_content_selector).toggle(function () { if ($(this).css('display') == 'none') { toggle_switch.html('Show'); } else { toggle_switch.html('Show'); } }); }); });