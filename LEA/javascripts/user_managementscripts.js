function searching(){
	var alpha = /^[a-zA-Z]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	$("#searchmsg").html('');
	var flag = 0;
	var regflag = 0;
	
	if($("#txt_srcref_id").val() == '' && $("#txt_srcusername").val() == '' && $("#txt_srcemailid").val() == '' && $("#txt_srccontactno").val() == ''){
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
				url:       site_url+'controllers/ajax_controller/user_management-ajax-controller.php', 
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
			$("#user_management").html(data);
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
					}
					else 
					if(mobnum.test($("#txt_addid").val()) == false){
					  $("#error-innertxt_addid").show().fadeOut(5000);
					  $("#error-innertxt_addid").html('Enter correct number');
					  $("#txt_addid").focus();
					  return false;
					}
					else*/ 
					if($("#txt_addfullname").val() == ''){
				  $("#error-innertxt_addfullname").show().fadeOut(5000);
				  $("#error-innertxt_addfullname").html('This field is required');
				  $("#txt_addfullname").focus();
				  return false;
				}else if($("#txt_addusername").val() == ''){
				  $("#error-innertxt_addusername").show().fadeOut(5000);
				  $("#error-innertxt_addusername").html('This field is required');
				  $("#txt_addusername").focus();
				  return false;
				}else if($("#txt_addemailid").val() == ''){
				  $("#error-innertxt_addemailid").show().fadeOut(5000);
				  $("#error-innertxt_addemailid").html('This field is required');
				  $("#txt_addemailid").focus();
				  return false;
				}else if(emailchk.test($("#txt_addemailid").val()) == false){
				  $("#error-innertxt_addemailid").show().fadeOut(5000);
				  $("#error-innertxt_addemailid").html('Invalid email id');
				  $("#txt_addemailid").focus();
				  return false;
				}else if($("#txt_addpassword").val() == ''){
				  $("#error-innertxt_addpassword").show().fadeOut(5000);
				  $("#error-innertxt_addpassword").html('This field is required');
				  $("#txt_addpassword").focus();
				  return false;
				}else if(alphanum.test($("#txt_addpassword").val()) == false){
				  $("#error-innertxt_addpassword").show().fadeOut(5000);
				  $("#error-innertxt_addpassword").html('Invalid password');
				  $("#txt_addpassword").focus();
				  return false;
				}else if($("#txt_addpassword").val().length < 6){
				  $("#error-innertxt_addpassword").show().fadeOut(5000);
				  $("#error-innertxt_addpassword").html('Minimum 6 character required');
				  $("#txt_addpassword").focus();
				  return false;
				}else if($("#txt_addcpassword").val() == ''){
				  $("#error-innertxt_addcpassword").show().fadeOut(5000);
				  $("#error-innertxt_addcpassword").html('This field is required');
				  $("#txt_addcpassword").focus();
				  return false;
				}else if($("#txt_addcpassword").val() != $("#txt_addpassword").val()){
				  $("#error-innertxt_addcpassword").show().fadeOut(5000);
				  $("#error-innertxt_addcpassword").html('Confirm password not match');
				  $("#txt_addcpassword").focus();
				  return false;
				}else if($("#txt_addcontactno").val() == ''){
				  $("#error-innertxt_addcontactno").show().fadeOut(5000);
				  $("#error-innertxt_addcontactno").html('This field is required');
				  $("#txt_addcontactno").focus();
				  return false;
				}else if($("#txt_planId").val() == ''){
				  $("#error-innertxt_planId").show().fadeOut(5000);
				  $("#error-innertxt_planId").html('This field is required');
				  $("#txt_planId").focus();
				  return false;
				}
				
				
				/*else if($("#txt_addaddress").val() == ''){
				  $("#error-innertxt_addaddress").show().fadeOut(5000);
				  $("#error-innertxt_addaddress").html('This field is required');
				  $("#txt_addaddress").focus();
				  return false;
				}*//*else if($("#txt_addplanId").val() == ''){
					  $("#error-innertxt_addplanId").show().fadeOut(5000);
					  $("#error-innertxt_addplanId").html('This field is required');
					  $("#txt_addplanId").focus();
					  return false;
					}else if(mobnum.test($("#txt_addplanId").val()) == false){
					  $("#error-innertxt_addplanId").show().fadeOut(5000);
					  $("#error-innertxt_addplanId").html('Enter correct number');
					  $("#txt_addplanId").focus();
					  return false;
					}else if($("#txt_addpaymentId").val() == ''){
					  $("#error-innertxt_addpaymentId").show().fadeOut(5000);
					  $("#error-innertxt_addpaymentId").html('This field is required');
					  $("#txt_addpaymentId").focus();
					  return false;
					}else if(mobnum.test($("#txt_addpaymentId").val()) == false){
					  $("#error-innertxt_addpaymentId").show().fadeOut(5000);
					  $("#error-innertxt_addpaymentId").html('Enter correct number');
					  $("#txt_addpaymentId").focus();
					  return false;
					}else if($("#txt_addplanDate").val() == ''){
				  $("#error-innertxt_addplanDate").show().fadeOut(5000);
				  $("#error-innertxt_addplanDate").html('This field is required');
				  $("#txt_addplanDate").focus();
				  return false;
				}else if($("#txt_addlast_login").val() == ''){
				  $("#error-innertxt_addlast_login").show().fadeOut(5000);
				  $("#error-innertxt_addlast_login").html('This field is required');
				  $("#txt_addlast_login").focus();
				  return false;
				}*/else
	{
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse,
			url:       site_url+'controllers/ajax_controller/user_management-ajax-controller.php', 
			type: "POST"
		};
		$('#form_user_managementadd').submit(function() {
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
			document.getElementById('err').innerHTML = 'User_management already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);		   
			document.getElementById('succ').innerHTML = 'User_management added successfully.';
			newdata();				 
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while adding user_management.';
		}
		$('#form_user_managementadd').unbind('submit').bind('submit',function() {
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
					}
					else 
					if(mobnum.test($("#txt_addid").val()) == false){
					  $("#error-innertxt_addid").show().fadeOut(5000);
					  $("#error-innertxt_addid").html('Enter correct number');
					  $("#txt_addid").focus();
					  return false;
					}
					else */
				if($("#txt_addfullname").val() == ''){
				  $("#error-innertxt_addfullname").show().fadeOut(5000);
				  $("#error-innertxt_addfullname").html('This field is required');
				  $("#txt_addfullname").focus();
				  return false;
				}else if($("#txt_addusername").val() == ''){
				  $("#error-innertxt_addusername").show().fadeOut(5000);
				  $("#error-innertxt_addusername").html('This field is required');
				  $("#txt_addusername").focus();
				  return false;
				}else if($("#txt_addemailid").val() == ''){
				  $("#error-innertxt_addemailid").show().fadeOut(5000);
				  $("#error-innertxt_addemailid").html('This field is required');
				  $("#txt_addemailid").focus();
				  return false;
				}else if(emailchk.test($("#txt_addemailid").val()) == false){
				  $("#error-innertxt_addemailid").show().fadeOut(5000);
				  $("#error-innertxt_addemailid").html('Invalid email id');
				  $("#txt_addemailid").focus();
				  return false;
				}/*else if($("#txt_addpassword").val() == ''){
				  $("#error-innertxt_addpassword").show().fadeOut(5000);
				  $("#error-innertxt_addpassword").html('This field is required');
				  $("#txt_addpassword").focus();
				  return false;
				}*/else if($("#txt_addpassword").val() != '' && alphanum.test($("#txt_addpassword").val()) == false){
				  $("#error-innertxt_addpassword").show().fadeOut(5000);
				  $("#error-innertxt_addpassword").html('Invalid password');
				  $("#txt_addpassword").focus();
				  return false;
				}else if($("#txt_addpassword").val() != '' && $("#txt_addpassword").val().length < 6){
				  $("#error-innertxt_addpassword").show().fadeOut(5000);
				  $("#error-innertxt_addpassword").html('Minimum 6 character required');
				  $("#txt_addpassword").focus();
				  return false;
				}/*else if($("#txt_addcpassword").val() == ''){
				  $("#error-innertxt_addcpassword").show().fadeOut(5000);
				  $("#error-innertxt_addcpassword").html('This field is required');
				  $("#txt_addcpassword").focus();
				  return false;
				}*/else if($("#txt_addcpassword").val() != '' && $("#txt_addcpassword").val() != $("#txt_addpassword").val()){
				  $("#error-innertxt_addcpassword").show().fadeOut(5000);
				  $("#error-innertxt_addcpassword").html('Confirm password not match');
				  $("#txt_addcpassword").focus();
				  return false;
				}else if($("#txt_addcontactno").val() == ''){
				  $("#error-innertxt_addcontactno").show().fadeOut(5000);
				  $("#error-innertxt_addcontactno").html('This field is required');
				  $("#txt_addcontactno").focus();
				  return false;
				}else if($("#txt_planId").val() == ''){
				  $("#error-innertxt_planId").show().fadeOut(5000);
				  $("#error-innertxt_planId").html('This field is required');
				  $("#txt_planId").focus();
				  return false;
				}/*else if($("#txt_addaddress").val() == ''){
				  $("#error-innertxt_addaddress").show().fadeOut(5000);
				  $("#error-innertxt_addaddress").html('This field is required');
				  $("#txt_addaddress").focus();
				  return false;
				}*/
				/*else
				if($("#txt_addplanId").val() == ''){
					  $("#error-innertxt_addplanId").show().fadeOut(5000);
					  $("#error-innertxt_addplanId").html('This field is required');
					  $("#txt_addplanId").focus();
					  return false;
					}else if(mobnum.test($("#txt_addplanId").val()) == false){
					  $("#error-innertxt_addplanId").show().fadeOut(5000);
					  $("#error-innertxt_addplanId").html('Enter correct number');
					  $("#txt_addplanId").focus();
					  return false;
					}else if($("#txt_addpaymentId").val() == ''){
					  $("#error-innertxt_addpaymentId").show().fadeOut(5000);
					  $("#error-innertxt_addpaymentId").html('This field is required');
					  $("#txt_addpaymentId").focus();
					  return false;
					}else if(mobnum.test($("#txt_addpaymentId").val()) == false){
					  $("#error-innertxt_addpaymentId").show().fadeOut(5000);
					  $("#error-innertxt_addpaymentId").html('Enter correct number');
					  $("#txt_addpaymentId").focus();
					  return false;
					}else if($("#txt_addplanDate").val() == ''){
				  $("#error-innertxt_addplanDate").show().fadeOut(5000);
				  $("#error-innertxt_addplanDate").html('This field is required');
				  $("#txt_addplanDate").focus();
				  return false;
				}else if($("#txt_addlast_login").val() == ''){
				  $("#error-innertxt_addlast_login").show().fadeOut(5000);
				  $("#error-innertxt_addlast_login").html('This field is required');
				  $("#txt_addlast_login").focus();
				  return false;
				}*/else
	{
	   var options = {
			beforeSubmit:  showRequest_update,
			success:       showResponse_update,
			url:       site_url+'controllers/ajax_controller/user_management-ajax-controller.php', 
			type: "POST"
		};
		$('#form_user_managementadd').submit(function() {
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
				document.getElementById('err').innerHTML = 'User_management already exist. Please try another.';
			}else if(data == 1){
				$("#message-red").hide();
				$("#message-green").show().fadeOut(5000);				
				document.getElementById('succ').innerHTML = 'User_management updated successfully.';
				newdata();
			}else if(data == 2){
				$.scrollTo(0,500);
				$("#message-red").show().fadeOut(7000);
				$("#message-green").hide();
				document.getElementById('err').innerHTML = 'Some error occurred while updating user_management.';
			}
		}
		$('#form_user_managementadd').unbind('submit').bind('submit',function() {
		});
	}