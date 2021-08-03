function searching(){
	var alpha = /^[a-zA-Z]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	$("#searchmsg").html('');
	var flag = 0;
	var regflag = 0;if($("#txt_srcbannerLink").val() == '' && $("#txt_srctitle").val() == ''){
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
				url:       site_url+'controllers/ajax_controller/banner-ajax-controller.php', 
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
			$("#banner").html(data);
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
	}else if($("#txt_addbannerPosition").val() == ''){
	  $("#error-innertxt_addbannerPosition").show().fadeOut(5000);
	  $("#error-innertxt_addbannerPosition").html('This field is required');
	  $("#txt_addbannerPosition").focus();
	  return false;
	}else if($("#txt_addbannerImage").val() == ''){
	  $("#error-innertxt_addbannerImage").show().fadeOut(5000);
	  $("#error-innertxt_addbannerImage").html('This field is required');
	  $("#txt_addbannerImage").focus();
	  return false;
	}else if($("#txt_addbannerLink").val() == ''){
	  $("#error-innertxt_addbannerLink").show().fadeOut(5000);
	  $("#error-innertxt_addbannerLink").html('This field is required');
	  $("#txt_addbannerLink").focus();
	  return false;
	}else if($("#txt_addstartdate").val() == ''){
	  $("#error-innertxt_addstartdate").show().fadeOut(5000);
	  $("#error-innertxt_addstartdate").html('This field is required');
	  $("#txt_addstartdate").focus();
	  return false;
	}else if($("#txt_addenddate").val() == ''){
	  $("#error-innertxt_addenddate").show().fadeOut(5000);
	  $("#error-innertxt_addenddate").html('This field is required');
	  $("#txt_addenddate").focus();
	  return false;
	}else if($("#txt_maximpression").val() == ''){
	  $("#error-innertxt_maximpression").show().fadeOut(5000);
	  $("#error-innertxt_maximpression").html('This field is required');
	  $("#txt_maximpression").focus();
	  return false;
	}else if($("#txt_maxclick").val() == ''){
	  $("#error-innertxt_maxclick").show().fadeOut(5000);
	  $("#error-innertxt_maxclick").html('This field is required');
	  $("#txt_maxclick").focus();
	  return false;
	}else
	{
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse,
			url:       site_url+'controllers/ajax_controller/banner-ajax-controller.php', 
			type: "POST"
		};
		$('#form_banneradd').submit(function() {
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
		//alert(data);
		if(data == 0)
		{
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Banner already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);		   
			document.getElementById('succ').innerHTML = 'Banner added successfully.';
			newdata();				 
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while adding banner.';
		}
		$('#form_banneradd').unbind('submit').bind('submit',function() {
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
	}else if($("#txt_addbannerPosition").val() == ''){
	  $("#error-innertxt_addbannerPosition").show().fadeOut(5000);
	  $("#error-innertxt_addbannerPosition").html('This field is required');
	  $("#txt_addbannerPosition").focus();
	  return false;
	}else if($("#txt_addbannerLink").val() == ''){
	  $("#error-innertxt_addbannerLink").show().fadeOut(5000);
	  $("#error-innertxt_addbannerLink").html('This field is required');
	  $("#txt_addbannerLink").focus();
	  return false;
	}else if($("#txt_addstartdate").val() == ''){
	  $("#error-innertxt_addstartdate").show().fadeOut(5000);
	  $("#error-innertxt_addstartdate").html('This field is required');
	  $("#txt_addstartdate").focus();
	  return false;
	}else if($("#txt_addenddate").val() == ''){
	  $("#error-innertxt_addenddate").show().fadeOut(5000);
	  $("#error-innertxt_addenddate").html('This field is required');
	  $("#txt_addenddate").focus();
	  return false;
	}else if($("#txt_maximpression").val() == ''){
	  $("#error-innertxt_maximpression").show().fadeOut(5000);
	  $("#error-innertxt_maximpression").html('This field is required');
	  $("#txt_maximpression").focus();
	  return false;
	}else if($("#txt_maxclick").val() == ''){
	  $("#error-innertxt_maxclick").show().fadeOut(5000);
	  $("#error-innertxt_maxclick").html('This field is required');
	  $("#txt_maxclick").focus();
	  return false;
	}else
	{
	   var options = {
			beforeSubmit:  showRequest_update,
			success:       showResponse_update,
			url:       site_url+'controllers/ajax_controller/banner-ajax-controller.php', 
			type: "POST"
		};
		$('#form_banneradd').submit(function() {
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
			//alert(data);
			if(data == 0){
				$.scrollTo(0,500);
				$("#message-red").show().fadeOut(7000);
				$("#message-green").hide();
				document.getElementById('err').innerHTML = 'Banner already exist. Please try another.';
			}else if(data == 1){
				$("#message-red").hide();
				$("#message-green").show().fadeOut(5000);				
				document.getElementById('succ').innerHTML = 'Banner updated successfully.';
				newdata();
			}else if(data == 2){
				$.scrollTo(0,500);
				$("#message-red").show().fadeOut(7000);
				$("#message-green").hide();
				document.getElementById('err').innerHTML = 'Some error occurred while updating banner.';
			}
		}
		$('#form_banneradd').unbind('submit').bind('submit',function() {
		});
	}