function searching(){
	var alpha = /^[a-zA-Z]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	$("#searchmsg").html('');
	var flag = 0;
	var regflag = 0;if($("#txt_srcahesan_1").val() == '' && $("#txt_srcahesan_2").val() == '' && $("#txt_srcahesan_3").val() == '' && $("#txt_srcahesan_4").val() == ''){
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
				url:       site_url+'controllers/ajax_controller/ahesan-ajax-controller.php', 
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
			$("#ahesan").html(data);
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
	var url=/^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/;if($("#txt_addahesan_1").val() == ''){
				  $("#error-innertxt_addahesan_1").show().fadeOut(5000);
				  $("#error-innertxt_addahesan_1").html('This field is required');
				  $("#txt_addahesan_1").focus();
				  return false;
				}else if($("#txt_addahesan_2").val() == ''){
				  $("#error-innertxt_addahesan_2").show().fadeOut(5000);
				  $("#error-innertxt_addahesan_2").html('This field is required');
				  $("#txt_addahesan_2").focus();
				  return false;
				}else if($("#txt_addahesan_3").val() == ''){
				  $("#error-innertxt_addahesan_3").show().fadeOut(5000);
				  $("#error-innertxt_addahesan_3").html('This field is required');
				  $("#txt_addahesan_3").focus();
				  return false;
				}else if($("#txt_addahesan_4").val() == ''){
				  $("#error-innertxt_addahesan_4").show().fadeOut(5000);
				  $("#error-innertxt_addahesan_4").html('This field is required');
				  $("#txt_addahesan_4").focus();
				  return false;
				}else
	{
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse,
			url:       site_url+'controllers/ajax_controller/ahesan-ajax-controller.php', 
			type: "POST"
		};
		$('#form_ahesanadd').submit(function() {
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
			document.getElementById('err').innerHTML = 'Ahesan already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);		   
			document.getElementById('succ').innerHTML = 'Ahesan added successfully.';
			newdata();				 
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while adding ahesan.';
		}
		$('#form_ahesanadd').unbind('submit').bind('submit',function() {
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
	var url=/^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/;if($("#txt_addahesan_1").val() == ''){
				  $("#error-innertxt_addahesan_1").show().fadeOut(5000);
				  $("#error-innertxt_addahesan_1").html('This field is required');
				  $("#txt_addahesan_1").focus();
				  return false;
				}else if($("#txt_addahesan_2").val() == ''){
				  $("#error-innertxt_addahesan_2").show().fadeOut(5000);
				  $("#error-innertxt_addahesan_2").html('This field is required');
				  $("#txt_addahesan_2").focus();
				  return false;
				}else if($("#txt_addahesan_3").val() == ''){
				  $("#error-innertxt_addahesan_3").show().fadeOut(5000);
				  $("#error-innertxt_addahesan_3").html('This field is required');
				  $("#txt_addahesan_3").focus();
				  return false;
				}else if($("#txt_addahesan_4").val() == ''){
				  $("#error-innertxt_addahesan_4").show().fadeOut(5000);
				  $("#error-innertxt_addahesan_4").html('This field is required');
				  $("#txt_addahesan_4").focus();
				  return false;
				}else
	{
	   var options = {
			beforeSubmit:  showRequest_update,
			success:       showResponse_update,
			url:       site_url+'controllers/ajax_controller/ahesan-ajax-controller.php', 
			type: "POST"
		};
		$('#form_ahesanadd').submit(function() {
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
				document.getElementById('err').innerHTML = 'Ahesan already exist. Please try another.';
			}else if(data == 1){
				$("#message-red").hide();
				$("#message-green").show().fadeOut(5000);				
				document.getElementById('succ').innerHTML = 'Ahesan updated successfully.';
				newdata();
			}else if(data == 2){
				$.scrollTo(0,500);
				$("#message-red").show().fadeOut(7000);
				$("#message-green").hide();
				document.getElementById('err').innerHTML = 'Some error occurred while updating ahesan.';
			}
		}
		$('#form_ahesanadd').unbind('submit').bind('submit',function() {
		});
	}