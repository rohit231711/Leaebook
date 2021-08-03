function searching(){
	var alpha = /^[a-zA-Z]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	$("#searchmsg").html('');
	var flag = 0;
	var regflag = 0;if($("#txt_srcid").val() == '' && $("#txt_srccategoryName").val() == ''){
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
				url:       site_url+'controllers/ajax_controller/category-ajax-controller.php', 
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
			$("#category").html(data);
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
					
					/*if($("#txt_addid").val() == ''){
					  $("#error-innertxt_addid").show().fadeOut(5000);
					  $("#error-innertxt_addid").html('This field is required');
					  $("#txt_addid").focus();
					  return false;
					}else if(mobnum.test($("#txt_addid").val()) == false){
					  $("#error-innertxt_addid").show().fadeOut(5000);
					  $("#error-innertxt_addid").html('Enter correct number');
					  $("#txt_addid").focus();
					  return false;
					}else*/ 
					if($("#txt_addcategoryName").val() == ''){
				  $("#error-innertxt_addcategoryName").show().fadeOut(5000);
				  $("#error-innertxt_addcategoryName").html('This field is required');
				  $("#txt_addcategoryName").focus();
				  return false;
				}/*else if($("#txt_addcategoryImage").val() == ''){
				  $("#error-innertxt_addcategoryImage").show().fadeOut(5000);
				  $("#error-innertxt_addcategoryImage").html('This field is required');
				  $("#txt_addcategoryImage").focus();
				  return false;
				}*/else
	{
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse,
			url:       site_url+'controllers/ajax_controller/category-ajax-controller.php', 
			type: "POST"
		};
		$('#form_categoryadd').submit(function() {
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
			document.getElementById('err').innerHTML = 'Category already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);		   
			document.getElementById('succ').innerHTML = 'Category added successfully.';
			newdata();				 
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while adding category.';
		}
		$('#form_categoryadd').unbind('submit').bind('submit',function() {
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
					/*if($("#txt_addid").val() == ''){
					  $("#error-innertxt_addid").show().fadeOut(5000);
					  $("#error-innertxt_addid").html('This field is required');
					  $("#txt_addid").focus();
					  return false;
					}elseif(mobnum.test($("#txt_addid").val()) == false){
					  $("#error-innertxt_addid").show().fadeOut(5000);
					  $("#error-innertxt_addid").html('Enter correct number');
					  $("#txt_addid").focus();
					  return false;
					}else*/ 
					if($("#txt_addcategoryName").val() == ''){
				  $("#error-innertxt_addcategoryName").show().fadeOut(5000);
				  $("#error-innertxt_addcategoryName").html('This field is required');
				  $("#txt_addcategoryName").focus();
				  return false;
				}/*else if($("#txt_addcategoryImage").val() == ''){
				  $("#error-innertxt_addcategoryImage").show().fadeOut(5000);
				  $("#error-innertxt_addcategoryImage").html('This field is required');
				  $("#txt_addcategoryImage").focus();
				  return false;
				}*/else
	{
	   var options = {
			beforeSubmit:  showRequest_update,
			success:       showResponse_update,
			url:       site_url+'controllers/ajax_controller/category-ajax-controller.php', 
			type: "POST"
		};
		$('#form_categoryadd').submit(function() {
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
		//alert(data);
		if (statusText == 'success') 
		{
			if(data == 0){
				$.scrollTo(0,500);
				$("#message-red").show().fadeOut(7000);
				$("#message-green").hide();
				document.getElementById('err').innerHTML = 'Category already exist. Please try another.';
			}else if(data == 1){
				$("#message-red").hide();
				$("#message-green").show().fadeOut(5000);				
				document.getElementById('succ').innerHTML = 'Category updated successfully.';
				newdata();
			}else if(data == 2){
				$.scrollTo(0,500);
				$("#message-red").show().fadeOut(7000);
				$("#message-green").hide();
				document.getElementById('err').innerHTML = 'Some error occurred while updating category.';
			}
		}
		$('#form_categoryadd').unbind('submit').bind('submit',function() {
		});
	}