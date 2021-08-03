function searching(){
	var alpha = /^[a-zA-Z]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	$("#searchmsg").html('');
	var flag = 0;
	var regflag = 0;if($("#txt_srcid").val() == '' && $("#txt_srctitle").val() == '' && $("#txt_srclink").val() == ''){
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
				url:       site_url+'controllers/ajax_controller/advertise-ajax-controller.php', 
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
			$("#advertise").html(data);
		} 
	}
	function adddata(){
	var alpha = /^[a-zA-Z]+$/;
	var alphanum = /^[a-zA-Z0-9]+$/;
	var emailchk = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	var mobnum=/^[0-9]{10,12}$/;
	var phonum=/^[0-9]{10,14}$/;
	var num=/^[0-9]$/;
	var decnum=/^[0-9.]$/;
	var domain=/[^,\s]+\.{1,}[^,\s]{2,}/;
	var url=/^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/; 
	if($("#txt_addtitle").val() == ''){
	  $("#error-innertxt_addtitle").show().fadeOut(5000);
	  $("#error-innertxt_addtitle").html('This field is required');
	  $("#txt_addtitle").focus();
	  return false;
	}else if($("#txt_addlink").val() == ''){
	  $("#error-innertxt_addlink").show().fadeOut(5000);
	  $("#error-innertxt_addlink").html('This field is required');
	  $("#txt_addlink").focus();
	  return false;
	}else if($("#txt_addimage").val() == ''){
	  $("#error-innertxt_addimage").show().fadeOut(5000);
	  $("#error-innertxt_addimage").html('This field is required');
	  $("#txt_addimage").focus();
	  return false;
	}else
	{
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse,
			url:       site_url+'controllers/ajax_controller/advertise-ajax-controller.php', 
			type: "POST"
		};
		$('#form_advertiseadd').submit(function() {
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
			document.getElementById('err').innerHTML = 'Advertise already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);		   
			document.getElementById('succ').innerHTML = 'Advertise added successfully.';
			newdata();				 
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while adding advertise.';
		}
		$('#form_advertiseadd').unbind('submit').bind('submit',function() {
		});
	}
	}
	function updatedata(){
	var alpha = /^[a-zA-Z]+$/;
	var alphanum = /^[a-zA-Z0-9]+$/;
	var emailchk = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	var mobnum=/^[0-9]{10,12}$/;
	var phonum=/^[0-9]{10,14}$/;
	var num=/^[0-9]$/;
	var decnum=/^[0-9.]$/;
	var domain=/[^,\s]+\.{1,}[^,\s]{2,}/;
	var url=/^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/; 
	if($("#txt_addtitle").val() == ''){
	  $("#error-innertxt_addtitle").show().fadeOut(5000);
	  $("#error-innertxt_addtitle").html('This field is required');
	  $("#txt_addtitle").focus();
	  return false;
	}else if($("#txt_addlink").val() == ''){
	  $("#error-innertxt_addlink").show().fadeOut(5000);
	  $("#error-innertxt_addlink").html('This field is required');
	  $("#txt_addlink").focus();
	  return false;
	}else
	{
	   var options = {
			beforeSubmit:  showRequest_update,
			success:       showResponse_update,
			url:       site_url+'controllers/ajax_controller/advertise-ajax-controller.php', 
			type: "POST"
		};
		$('#form_advertiseadd').submit(function() {
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
				document.getElementById('err').innerHTML = 'Advertise already exist. Please try another.';
			}else if(data == 1){
				$("#message-red").hide();
				$("#message-green").show().fadeOut(5000);				
				document.getElementById('succ').innerHTML = 'Advertise updated successfully.';
				newdata();
			}else if(data == 2){
				$.scrollTo(0,500);
				$("#message-red").show().fadeOut(7000);
				$("#message-green").hide();
				document.getElementById('err').innerHTML = 'Some error occurred while updating advertise.';
			}
		}
		$('#form_advertiseadd').unbind('submit').bind('submit',function() {
		});
	}