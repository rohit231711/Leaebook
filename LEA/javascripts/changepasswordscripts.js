function checkpassword(password)
{
	$.ajax( {
	url : site_url+'controllers/ajax_controller/changepassword-ajax-controller.php', 
	type : 'post',
	data: 'getpassword=1&password='+password,		
	success : function( resp ) {
	if(resp==0)
		{
		 $("#h_password").val('0');
			$("#errortxt_oldpassword").show().fadeOut(5000);
			$("#error-innertxt_oldpassword").html('Enter correct password');
			$("#txt_oldpassword").focus();
			return false;
		}
		else
		{
			$("#h_password").val('1');
			$("#error-innertxt_oldpassword").html('');
			$("#errortxt_oldpassword").hide();		
			$("#txt_newpassword").focus();
		}
	}
});

}

function adddata()
{
	var alphanum = /^[a-zA-Z0-9]+$/;
	if($("#txt_oldpassword").val() == ''){
		$("#error-innertxt_oldpassword").show().fadeOut(5000);
		$("#error-innertxt_oldpassword").html('This field is required');
		$("#txt_oldpassword").focus();
		return false;
	}else if($("#h_password").val() == '0'){
		$("#error-innertxt_oldpassword").show().fadeOut(5000);
		$("#error-innertxt_oldpassword").html('Enter correct password');
		$("#txt_oldpassword").focus();
		return false;
	}else if($("#txt_newpassword").val() == ''){
		$("#error-innertxt_newpassword").show().fadeOut(5000);
		$("#error-innertxt_newpassword").html('This field is required');
		$("#txt_newpassword").focus();
		return false;
	}else if(alphanum.test($("#txt_newpassword").val()) ==  false){
		$("#error-innertxt_newpassword").show().fadeOut(5000);
		$("#error-innertxt_newpassword").html('Invalid new password ');
		$("#txt_newpassword").focus();
		return false;
	}else if($("#txt_newpassword").val().length < 5){
		$("#error-innertxt_newpassword").show().fadeOut(5000);
		$("#error-innertxt_newpassword").html('Enter minimum 5 character');
		$("#txt_newpassword").focus();
		return false;
	}else if($("#txt_confirmpassword").val() == ''){
		$("#error-innertxt_confirmpassword").show().fadeOut(5000);
		$("#error-innertxt_confirmpassword").html('This field is required');
		$("#txt_confirmpassword").focus();
		return false;
	}else if($("#txt_confirmpassword").val() != $("#txt_newpassword").val()){
		$("#error-innertxt_confirmpassword").show().fadeOut(5000);
		$("#error-innertxt_confirmpassword").html('Confirm password not match');
		$("#txt_confirmpassword").focus();
		return false;
	}
	else{	  
			var options = {
				beforeSubmit:  showRequest,
				success:       showResponse,
				url:       site_url+'controllers/ajax_controller/changepassword-ajax-controller.php', 
				type: "POST"
			};
			$('#form_changepassword').submit(function() {
				$(this).ajaxSubmit(options);
				return false;
			});    
	  }	   
}

function showRequest(formData, jqForm, options) {
	return true;
} 

function showResponse(data, statusText)
{
	if (statusText == 'success') 
	{
		if(data == 1){
			$("#message-green").show().fadeOut(5000);
			$("#message-red").hide();
			document.getElementById('succ').innerHTML = 'Password changed successfully.';
			newdata();		   
		}
		else if(data==2)
		{
			$("#message-green").hide();
			$("#message-red").show().fadeOut(7000);
			document.getElementById('err').innerHTML = 'Some error occurred while updating password.';
		}
	}
	$('#form_changepassword').unbind('submit').bind('submit',function() {
		});
}

function newdata(){
   showviewdiv();
}

function showviewdiv(){
$.scrollTo(0,500);
$.ajax( { 
		url : site_url+'controllers/ajax_controller/changepassword-ajax-controller.php', 
		type : 'post',
		data: 'viewdiv='+ 1,				
		success : function( resp ) {
		document.getElementById('changepassword').style.display= 'block';
		$("#changepassword").html(resp);
		}
	});

}