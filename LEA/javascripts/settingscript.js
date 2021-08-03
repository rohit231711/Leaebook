function updatedata(){
	var alpha = /^[a-zA-Z]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	var zcode=/^[0-9]{4,6}$/;
    if($("#txt_email").val() == ''){
	  $("#error-innertxt_email").show().fadeOut(5000);
	  $("#error-innertxt_email").html('This field is required');
	  $("#txt_email").focus();
	  return false;
	}else if(email.test($("#txt_email").val()) == false){
	  $("#error-innertxt_email").show().fadeOut(5000);
	  $("#error-innertxt_email").html('email address is invalid');
	  $("#txt_email").focus();
	  return false;
	}else if($("#txt_esig").val() == ''){
	  $("#error-innertxt_esig").show().fadeOut(5000);
	  $("#error-innertxt_esig").html('This field is required');
	  $("#txt_esig").focus();
	  return false;
	}else if($("#txt_eftext").val() == ''){
	  $("#error-innertxt_eftext").show().fadeOut(5000);
	  $("#error-innertxt_eftext").html('This field is required');
	  $("#txt_eftext").focus();
	  return false;
	}else if($("#txt_currency").val() == ''){
	  $("#error-innertxt_currency").show().fadeOut(5000);
	  $("#error-innertxt_currency").html('This field is required');
	  $("#txt_currency").focus();
	  return false;
	}else if($("#txt_theme").val() == ''){
	  $("#error-innertxt_theme").show().fadeOut(5000);
	  $("#error-innertxt_theme").html('This field is required');
	  $("#txt_theme").focus();
	  return false;
	}
	else
	{
	   var options = {
			beforeSubmit:  showRequest_update,
			success:       showResponse_update,
			url:       site_url+'controllers/ajax_controller/setting-ajax-controller.php', 
			type: "POST"
		};
		$('#form_useradd').submit(function() {
			$(this).ajaxSubmit(options);
			return false;
		});
	}
}
function showRequest_update(formData, jqForm, options) {
	return true;
} 
function showResponse_update(data, statusText)  
{
	if (statusText == 'success') 
	{
		if(data == 0){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Settings already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);			
			document.getElementById('succ').innerHTML = 'Settings updated successfully.';
			//newdata();
			window.location.href='index.php?pid=setting';
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while updating settings.';
		}
	}
	$('#form_useradd').unbind('submit').bind('submit',function() {
		});
}