function searching(){
	var alpha = /^[a-zA-Z]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	$("#searchmsg").html('');
	var flag = 0;
	var regflag = 0;
	if($("#txt_srchfname").val() == '' && $("#txt_country").val() == '' ){
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
				url:       site_url+'controllers/ajax_controller/state-ajax-controller.php', 
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
		$("#state").html(data);
	}
}

function adddata(){
	var alpha = /^[-\sa-zA-Z]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
    if($("#txt_countrya").val() == ''){
	  $("#error-innertxt_countrya").show().fadeOut(5000);
	  $("#error-innertxt_countrya").html('This field is required');
	  $("#txt_countrya").focus();
	  return false;
	}else if($("#txt_state").val() == ''){
	  $("#error-innertxt_state").show().fadeOut(5000);
	  $("#error-innertxt_state").html('This field is required');
	  $("#txt_state").focus();
	  return false;
	}else if(alpha.test($("#txt_state").val()) == false){
	  $("#error-innertxt_state").show().fadeOut(5000);
	  $("#error-innertxt_state").html('State name is invalid');
	  $("#txt_state").focus();
	  return false;
	}else 
	{
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse,
			url:       site_url+'controllers/ajax_controller/state-ajax-controller.php', 
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
			document.getElementById('err').innerHTML = 'State name already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);
			document.getElementById('succ').innerHTML = 'State added successfully.';
			newdata();
		}else if(data == 2){
			$.scrollTo(0,500);
		   $("#message-red").show().fadeOut(7000);
		   $("#message-green").hide();
		   document.getElementById('err').innerHTML = 'Some error occurred while adding state.';
		}
	}

	$('#form_useradd').unbind('submit').bind('submit',function() {
		});
}

function updatedata(){
	var alpha = /^[-\sa-zA-Z]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
    if($("#txt_countrya").val() == ''){
	  $("#error-innertxt_countrya").show().fadeOut(5000);
	  $("#error-innertxt_countrya").html('This field is required');
	  $("#txt_countrya").focus();
	  return false;
	} else if($("#txt_state").val() == ''){
	  $("#error-innertxt_state").show().fadeOut(5000);
	  $("#error-innertxt_state").html('This field is required');
	  $("#txt_state").focus();
	  return false;
	}else if(alpha.test($("#txt_state").val()) == false){
	  $("#error-innertxt_state").show().fadeOut(5000);
	  $("#error-innertxt_state").html('State name is invalid');
	  $("#txt_state").focus();
	  return false;
	}else 
	{
	   var options = {
			beforeSubmit:  showRequest_update,
			success:       showResponse_update,
			url:       site_url+'controllers/ajax_controller/state-ajax-controller.php', 
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
		   document.getElementById('err').innerHTML = 'State name already exist. Please try another.';
		}else if(data == 1){
		  $("#message-green").show().fadeOut(5000);
		  $("#message-red").hide();
		   document.getElementById('succ').innerHTML = 'State updated successfully.';
		  newdata();
		}else if(data == 2){
			$.scrollTo(0,500);
		   $("#message-red").show().fadeOut(5000);
		   $("#message-green").hide();
		   document.getElementById('err').innerHTML = 'Some error occurred while updating state.';
		}
	}
	$('#form_useradd').unbind('submit').bind('submit',function() {
		});
}
