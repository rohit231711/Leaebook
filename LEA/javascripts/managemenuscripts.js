function searching(){
	var alpha = /^[a-zA-Z]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	$("#searchmsg").html('');
	var flag = 0;
	var regflag = 0;
	if($("#txt_uniqId").val() == '' && $("#txt_srchfname").val() == '' && $("#txt_srchlname").val() == '' && $("#txt_srchemailid").val() == '' && $("#txt_usercountry").val() == '' && $("#txt_userstate").val() == '' && $("#txt_usercity").val() == '' && $("#txt_mobilenum").val() == ''){
		flag = 1;
	}
	if(flag == 1){
		parent.$.fancybox.close();
		$("#search").val('0');
		newdata();
	}
	else
	{
		if(regflag == 0)
		{
			$("#searchmsg").html('');
			var options = {
				beforeSubmit:  showRequest,
				success:       showResponse_search,
				url:       site_url+'controllers/ajax_controller/manage-menu-ajax-controller.php', 
				type: "POST"
			};
			$('#form_search').submit(function()
			{
				$(this).ajaxSubmit(options);				
				return false;
			});
		}
	 }
}
function showResponse_search(data, statusText)  {
	if (statusText == 'success') {
		parent.$.fancybox.close();
		$("#manage-menu").html(data);
	} 
}

function adddata(){
	var alpha = /^[a-zA-Z]+$/;
	var alphanum = /^[a-zA-Z0-9]+$/;
	var num=/^[0-9]+$/;

	if($("#txt_sectionname").val() == ''){
	  $("#error-innertxt_sectionname").show().fadeOut(5000);
	  $("#error-innertxt_sectionname").html('This field is required');
	  $("#txt_sectionname").focus();
	  return false;
	}
	else if($("#txt_sectionorder").val() == ''){
	  $("#error-innertxt_sectionorder").show().fadeOut(5000);
	  $("#error-innertxt_sectionorder").html('This field is required');
	  $("#txt_sectionorder").focus();
	  return false;
	}
	else if($("#txt_pageid").val() == ''){
	  $("#error-innertxt_pageid").show().fadeOut(5000);
	  $("#error-innertxt_pageid").html('This field is required');
	  $("#txt_pageid").focus();
	  return false;
	}
	else
	{
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse,
			url:       site_url+'controllers/ajax_controller/manage-menu-ajax-controller.php', 
			type: "POST"
		};
		$('#form_menuadd').submit(function() {
			$(this).ajaxSubmit(options);
			return false;
		});
	}
}
function showRequest(formData, jqForm, options) {
	return true;
} 
function showResponse(data, statusText)  { 
	if(statusText == 'success')
	{
		if(data == 0)
		{
			$.scrollTo(0,500);
		   $("#message-red").show().fadeOut(7000);
		   $("#message-green").hide();
		   document.getElementById('err').innerHTML = 'Menu already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
 		    $("#message-green").show().fadeOut(5000);		   
		    document.getElementById('succ').innerHTML = 'Manu added successfully.';
		  newdata();
		   window.location.href=site_url+"index.php?pid=manage-menu";
		}else if(data == 2){
			$.scrollTo(0,500);
		   $("#message-red").show().fadeOut(7000);
		   $("#message-green").hide();
		   document.getElementById('err').innerHTML = 'Some error occurred while adding menu.';
		}
	}
	$('#form_menuadd').unbind('submit').bind('submit',function() {
		});
}
function updatedata(){
	var alpha = /^[a-zA-Z]+$/;
	var mobnum=/^[0-9]{10,12}$/;
	var phonum=/^[0-9]{10,14}$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
    if($("#txt_firstname").val() == ''){
	  $("#errortxt_firstname").show().fadeOut(5000);
	  $("#error-innertxt_firstname").html('This field is required');
	  $("#txt_firstname").focus();
	  return false;
	}else if(alpha.test($("#txt_firstname").val()) == false){
	  $("#errortxt_firstname").show().fadeOut(5000);
	  $("#error-innertxt_firstname").html('First name is invalid');
	  $("#txt_firstname").focus();
	  return false;
	}else if($("#txt_lastname").val() == ''){
	  $("#errortxt_lastname").show().fadeOut(5000);
	  $("#error-innertxt_lastname").html('This field is required');
	  $("#txt_lastname").focus();
	  return false;
	}else if(alpha.test($("#txt_lastname").val()) == false){
	  $("#errortxt_lastname").show().fadeOut(5000);
	  $("#error-innertxt_lastname").html('Last name is invalid');
	  $("#txt_lastname").focus();
	  return false;
	}else if($("#txt_company").val() == ''){
	  $("#errortxt_company").show().fadeOut(5000);
	  $("#error-innertxt_company").html('This field is required');
	  $("#txt_company").focus();
	  return false;
	}else if($("#txt_registerno").val() == ''){
	  $("#errortxt_registerno").show().fadeOut(5000);
	  $("#error-innertxt_registerno").html('This field is required');
	  $("#txt_registerno").focus();
	  return false;
	}else if($("#txt_compaddress").val() == ''){
	  $("#errortxt_compaddress").show().fadeOut(5000);
	  $("#error-innertxt_compaddress").html('This field is required');
	  $("#txt_compaddress").focus();
	  return false;
	}else if($("#txt_address").val() == ''){
	  $("#errortxt_address").show().fadeOut(5000);
	  $("#error-innertxt_address").html('This field is required');
	  $("#txt_address").focus();
	  return false;
	}else if($("#txt_country").val() == ''){
	  $("#errortxt_country").show().fadeOut(5000);
	  $("#error-innertxt_country").html('This field is required');
	  $("#txt_country").focus();
	  return false;
	}else if($("#txt_state").val() == ''){
	  $("#errortxt_state").show().fadeOut(5000);
	  $("#error-innertxt_state").html('This field is required');
	  $("#txt_state").focus();
	  return false;
	}else if($("#txt_city").val() == ''){
	  $("#errortxt_city").show().fadeOut(5000);
	  $("#error-innertxt_city").html('This field is required');
	  $("#txt_city").focus();
	  return false;
	}else if($("#txt_zipcode").val() == ''){
	  $("#errortxt_zipcode").show().fadeOut(5000);
	  $("#error-innertxt_zipcode").html('This field is required');
	  $("#txt_zipcode").focus();
	  return false;
	}else if($("#txt_phoneno1").val() == ''){
	  $("#errortxt_phoneno1").show().fadeOut(5000);
	  $("#error-innertxt_phoneno1").html('This field is required');
	  $("#txt_phoneno1").focus();
	  return false;
	}
	else
	{
	   var options = {
			beforeSubmit:  showRequest_update,
			success:       showResponse_update,
			url:       site_url+'controllers/ajax_controller/manage-menu-ajax-controller.php', 
			type: "POST"
		};
		$('#form_menuadd').submit(function() {
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
		   document.getElementById('err').innerHTML = 'Email id already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
		    $("#message-green").show().fadeOut(5000);		  
		    document.getElementById('succ').innerHTML = 'Menu updated successfully.';
		  newdata();
		}else if(data == 2){
			$.scrollTo(0,500);
		   $("#message-red").show().fadeOut(7000);
		   $("#message-green").hide();
		   document.getElementById('err').innerHTML = 'Some error occurred while updating menu.';
		}
	}
	$('#form_menuadd').unbind('submit').bind('submit',function() {
		});
}

function addFormField() {
	var id = document.getElementById("id").value;
	$.ajax( {
		url : site_url+'controllers/ajax_controller/manage-menu-ajax-controller.php', 
		type : 'post',
		data: 'addsubsectionform=1&id='+id,				
		success : function(result)
		{
			jQuery("#divTxt").append(result);
			id = (id - 1) + 2;
			document.getElementById("id").value = id;
		}
	});	
}

function removeFormField(id)
{
	document.getElementById("id").value = document.getElementById("id").value-1;
	jQuery(id).remove();
}