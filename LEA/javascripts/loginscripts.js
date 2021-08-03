// JavaScript Document
function validatelogin(){
	$("#sessionlogin").hide();  
 if($("#txt_username").val() == ''){
	$("#login").show(); 
	$("#login").html('Username is required');
	$("#txt_username").focus();
	return false;
  }else if($("#txt_password").val() == ''){
	 $("#login").show(); 
	$("#login").html('Password is required');
	$("#txt_password").focus();
	return false;
  }else{
	$("#login").hide();  
	$("#login").html('');  
    document.Form_login.submit();
	return true;
  }
}