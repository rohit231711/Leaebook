function getStates(){ 
	$("#message-red").hide();
	$("#message-green").hide();
	$("#txt_city").html("<option value=''>Select City</option>");
	var abc=$("#txt_country").val();
	$.ajax( {
		url : site_url+'controllers/ajax_controller/zipcode-ajax-controller.php', 
		type : 'post',
		data: 'getstateid='+abc,
		success : function( resp ) {
			$("#stateajax").html(resp);
		}
	});
}

function getCities(){ 
	$("#message-red").hide();
	$("#message-green").hide();
	var abc=$("#txt_state").val();
	$.ajax( {
		url : site_url+'controllers/ajax_controller/zipcode-ajax-controller.php', 
		type : 'post',
		data: 'getcityid='+abc,		
		success : function( resp ) {
			$("#cityajax").html(resp);
		}
	});
}

function searching(){
	var alpha = /^[a-zA-Z]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	$("#searchmsg").html('');
	var flag = 0;
	var regflag = 0;
	if($("#txt_srchfname").val() == '' && $("#txt_citya").val() == '' && $("#txt_statea").val() == '' && $("#txt_countrya").val() == ''){
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
				url:       site_url+'controllers/ajax_controller/zipcode-ajax-controller.php',
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
		$("#zipcode").html(data);
	} 
}

function adddata(){
	var zcode=/^[0-9]{4,6}$/;
	var alpha = /^[a-zA-Z]+$/;
	var alphanum = /^[a-zA-Z0-9]+$/;
    if($("#txt_country").val() == ''){
	  $("#error-innertxt_country").show().fadeOut(5000);
	  $("#error-innertxt_country").html('This field is required');
	  $("#txt_country").focus();
	  return false;
	}else if($("#txt_state").val() == ''){
	  $("#error-innertxt_state").show().fadeOut(5000);
	  $("#error-innertxt_state").html('This field is required');
	  $("#txt_state").focus();
	  return false;
	}else if($("#txt_city").val() == ''){
	  $("#error-innertxt_city").show().fadeOut(5000);
	  $("#error-innertxt_city").html('This field is required');
	  $("#txt_city").focus();
	  return false;
	}else if($("#txt_zipcode").val() == ''){
	  $("#error-innertxt_zipcode").show().fadeOut(5000);
	  $("#error-innertxt_zipcode").html('This field is required');
	  $("#txt_zipcode").focus();
	  return false;
	}else if(alphanum.test($("#txt_zipcode").val()) == false){
	  $("#error-innertxt_zipcode").show().fadeOut(5000);
	  $("#error-innertxt_zipcode").html('Zipcode is invalid');
	  $("#txt_zipcode").focus();
	  return false;
	}
	else
	{
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse,
			url:       site_url+'controllers/ajax_controller/zipcode-ajax-controller.php', 
			type: "POST"
		};
		$('#form_useradd').submit(function() {
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
			document.getElementById('err').innerHTML = 'Zipcode already exist. Please try another.';
		}else if(data == 1){

			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);
			document.getElementById('succ').innerHTML = 'Zipcode added successfully.';
			newdata();
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while adding zipcode.';
		}
	}
	$('#form_useradd').unbind('submit').bind('submit',function() {
		});
}

function updatedata(){
	var alpha = /^[a-zA-Z]+$/;
	var zcode=/^[0-9]{4,6}$/;
	var alphanum = /^[a-zA-Z0-9]+$/;
    if($("#txt_country").val() == ''){
	  $("#error-innertxt_country").show().fadeOut(5000);
	  $("#error-innertxt_country").html('This field is required');
	  $("#txt_country").focus();
	  return false;
	}else if($("#txt_state").val() == ''){
	  $("#error-innertxt_state").show().fadeOut(5000);
	  $("#error-innertxt_state").html('This field is required');
	  $("#txt_state").focus();
	  return false;
	}else if($("#txt_city").val() == ''){
	  $("#error-innertxt_city").show().fadeOut(5000);
	  $("#error-innertxt_city").html('This field is required');
	  $("#txt_city").focus();
	  return false;
	}else if($("#txt_zipcode").val() == ''){
	  $("#error-innertxt_zipcode").show().fadeOut(5000);
	  $("#error-innertxt_zipcode").html('This field is required');
	  $("#txt_zipcode").focus();
	  return false;
	}else if(alphanum.test($("#txt_zipcode").val()) == false){
	  $("#error-innertxt_zipcode").show().fadeOut(5000);
	  $("#error-innertxt_zipcode").html('Zipcode is invalid');
	  $("#txt_zipcode").focus();
	  return false;
	}
	else
	{
	   var options = {
			beforeSubmit:  showRequest_update,
			success:       showResponse_update,
			url:       site_url+'controllers/ajax_controller/zipcode-ajax-controller.php', 
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
			document.getElementById('err').innerHTML = 'Zipcode already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);
			document.getElementById('succ').innerHTML = 'Zipcode updated successfully.';
			newdata();
		}else if(data == 2){
			$.scrollTo(0,500);
		   $("#message-red").show().fadeOut(7000);
		   $("#message-green").hide();
		   document.getElementById('err').innerHTML = 'Some error occurred while updating zipcode.';
		}
	}

	$('#form_useradd').unbind('submit').bind('submit',function() {
		});
}