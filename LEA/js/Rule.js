// Standard Rules

var objUsername = ".0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz";
var objNumber = ".0123456789";
var objMoney = ".,0123456789";
var objWholeNumber = "0123456789";
var objPhone = "-+()#0123456789 ";
var objAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz ";
var objAlphaNum = ".-()0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz ";
var objAlphaNumChar = ".-,&#()/\@$%^0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz ";
var objZip = ".-()0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz";
var objWebEmail = ".-:/@0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz ";

// onkeyup="isRule(this,objPhone,100);"
// onkeyup="isRule(this,objAlphaNumChar,200);"
// onkeyup="isRule(this,objAlphaNum,100);"
// onkeyup="isRule(this,objWebEmail,100);" --FOr email
// onkeyup="isRule(this,objAlpha,100);"
// onkeyup="isRule(this,objUsername,100);"

function isRule(oComp, sRule, nLength, fdecimal) {
    if (fdecimal == "" || typeof (fdecimal) == "undefined") {
        fdecimal = false;
    }

    //If the object is not specified return false
    if (typeof (oComp) == 'undefined' || oComp == null || oComp == '') {
        alert('Error: Input object not specified.');
        return false;
    }
        //If neither rule nor max length is specified, return false
    else if (typeof (sRule) == 'undefined' && typeof (nLength) == 'undefined') {
        alert('Error: No rule/maximum length for input object specified.');
        return false;
    }

    var noErrorFlg = true;

    //If object is specified and either of rule is specified,
    if (typeof (sRule) != 'undefined' && sRule != null) {
        var temp;
        sRule = sRule + "";
        var discardChars = false;
        if (sRule.length > 0 && sRule.charAt(0) == "~") {
            sRule = sRule.substring(1);
            discardChars = true;
        }

        if (typeof (oComp) == "undefined" || typeof (sRule) == "undefined")
            return false;

        for (var i = 0; i < oComp.value.length; i++) {
            temp = oComp.value.charAt(i);

            if ((!discardChars && sRule.indexOf(temp) == -1) || (discardChars && sRule.indexOf(temp) >= 0)) {
                //alert("Field disobeys entry rule.  Following are the valid characters:\n" + sRule);
                //alert("Invalid Character!");
                oComp.value = oComp.value.substring(0, i);// + (oComp.value.length > i ? oComp.value.substring(i+1):"");
                noErrorFlg = false;
                break;
            }
        }
    }

    if (nLength) {
        if (fdecimal) {
            nLength -= fdecimal;
            var dp = oComp.value.indexOf(".");
            var p1;
            var p2 = "";
            if (dp >= 0) {
                p1 = oComp.value.substring(0, dp);
                p2 = oComp.value.substring(dp + 1);
            }
            else {
                p1 = oComp.value;
            }
            if (p1.length > nLength) {
                oComp.value = oComp.value.substring(0, nLength);
                return noErrorFlg;
            }
            for (var i = 0; i < p2.length; i++) {
                var ch = p2.charAt(i);
                if (ch < '0' || ch > '9') {
                    oComp.value = p1 + "." + p2.substring(0, i);
                    return noErrorFlg;
                }
            }
            if (p2.length > fdecimal) {
                oComp.value = p1 + "." + p2.substring(0, fdecimal);
            }
        }
        else if (oComp.value.length > nLength) {
            oComp.value = oComp.value.substring(0, nLength);
        }
    }
    return noErrorFlg;
}

function isImage(obj) {
    if (obj.value.length > 0) {
        if (obj.value.length > 4) {
            var ext = obj.value.substring(obj.value.length - 3, obj.value.length);
            if (ext == 'jpg' || ext == 'JPG' || ext == 'gif' || ext == 'GIF' || ext == 'png' || ext == 'PNG') {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
}

function isDate(IsItReal) {
    if (IsItReal.value != "") {
        var valDate = IsItReal.value;
        var reg = /^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.]((19|20)[0-9][0-9]+)$/;
        if (reg.test(valDate))
            return true;
        else {
            alert('Invalid date format.\n\nPlease enter date in mm/dd/yyyy format, e.g."07/28/2006".');
            IsItReal.focus();
            IsItReal.select();
            return false;
        }
    }
    else
        return true;
}

function DisplayDiv(mode) {
    if (mode == "login") {
        $(".panel").hide();
        $("#divAccount").show();
        $("#divMainLogin").hide();
    }
    else if (mode == "logout") {
        $("#divAccount").hide();
        $("#divMainLogin").show();
    }

}
function CharacterCount(obj, total) {
    if (total == null) total = 500;
    var len = obj.value.length;
    var newdiv;

    if (document.getElementById("note" + obj.id) == null) {
        newdiv = document.createElement('div');
        newdiv.id = "note" + obj.id;
        obj.parentNode.appendChild(newdiv);
    }
    else {
        newdiv = document.getElementById("note" + obj.id);
    }

    if (len == 0) {
        obj.parentNode.removeChild(newdiv);
    }

    if (len >= total) {
        var temp = obj.value.substring(0, total)
        obj.value = temp;
        newdiv.innerHTML = "You have reached maximum characters limit of <b>" + total + "</b>."
    }
    else {
        newdiv.innerHTML = "Your maximum characters limit is: <b>" + total + "</b>. <br>Current character count: <b>" + len + "</b>.";
    }
}

function ShowError(objDiv, msg) {
    $("#" + objDiv).html(msg).fadeIn("slow");
    setTimeout(function () { $("#" + objDiv).fadeOut() }, 5000);
}
function getQuerystring(key, default_) {
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
}
