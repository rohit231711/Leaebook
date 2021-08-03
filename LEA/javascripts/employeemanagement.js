function searching(){
	$("#searchmsg").html('');
	var emailchk = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	var flag = 0;
	var reg = 0;
	if($("#txt_srchfname").val() == '' && $("#txt_srchlname").val() == '' && $("#txt_srchemail").val() == '' && $("#sel_srchusertypr").val() == '' && $("#sel_srchstatus").val() == ''){
		flag = 1;
	}
	if(flag == 1){
		parent.$.fancybox.close();
		$("#search").val('0');
		newdata();
	}else{
		if(reg == 0){ 
			var options = {
			beforeSubmit:  showRequest,
			success:       showResponse_search,
			url:       site_url+'controllers/ajax_controller/employeemanagement-ajax-controller.php', 
			type: "POST"
			};
			$('#form_search').submit(function() {
				$(this).ajaxSubmit(options);
				return false;
			});
		}
	 }
}
function showResponse_search(data, statusText)  {
	if (statusText == 'success') {
		$("#searchmsg").html('');
		parent.$.fancybox.close();
		$("#employeemanagement").html(data);
	} 
}

function add(){
	var alpha = /^[a-zA-Z]+$/;
	var alphanum = /^[a-zA-Z0-9]+$/;
	var emailchk = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	var mobnum=/^[0-9]{10,12}$/;
	var phonum=/^[0-9]{10,14}$/;
	var num=/^[0-9]+$/;
	var decnum=/^[0-9.]+$/;
	var domain=/[^,\s]+\.{1,}[^,\s]{2,}/;
	var url=/^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/;
	if($("#sel_usertype").val() == ''){
		$("#error-innersel_usertype").show().fadeOut(5000);
		$("#error-innersel_usertype").html('This field is required');
		$("#sel_usertype").focus();
		return false;
	}else if($("#txt_firstname").val() == ''){
		$("#error-innertxt_firstname").show().fadeOut(5000);
		$("#error-innertxt_firstname").html('This field is required');
		$("#txt_firstname").focus();
		return false;
	}else if(alpha.test($("#txt_firstname").val()) ==  false){
		$("#error-innertxt_firstname").show().fadeOut(5000);
		$("#error-innertxt_firstname").html('Invalid first name');
		$("#txt_firstname").focus();
		return false;
	}else if($("#txt_lastname").val() == ''){
		$("#error-innertxt_lastname").show().fadeOut(5000);
		$("#error-innertxt_lastname").html('This field is required');
		$("#txt_lastname").focus();
		return false;
	}else if(alpha.test($("#txt_lastname").val()) ==  false){
		$("#error-innertxt_lastname").show().fadeOut(5000);
		$("#error-innertxt_lastname").html('Invalid last name');
		$("#txt_lastname").focus();
		return false;
	}else if($("#txt_username").val() == ''){
		$("#error-innertxt_username").show().fadeOut(5000);
		$("#error-innertxt_username").html('This field is required');
		$("#txt_username").focus();
		return false;
	}else if(alphanum.test($("#txt_username").val()) ==  false){
		$("#error-innertxt_username").show().fadeOut(5000);
		$("#error-innertxt_username").html('Invalid user name');
		$("#txt_username").focus();
		return false;
	}else if($("#txt_password").val() == ''){
		$("#error-innertxt_password").show().fadeOut(5000);
		$("#error-innertxt_password").html('This field is required');
		$("#txt_password").focus();
		return false;
	}else if(alphanum.test($("#txt_password").val()) ==  false){
		$("#error-innertxt_password").show().fadeOut(5000);
		$("#error-innertxt_password").html('Invalid password ');
		$("#txt_password").focus();
		return false;
	}else if($("#txt_password").val().length < 5){
		$("#error-innertxt_password").show().fadeOut(5000);
		$("#error-innertxt_password").html('Enter minimum 5 character');
		$("#txt_password").focus();
		return false;
	}else if($("#txt_cpassword").val() == ''){
		$("#error-innertxt_cpassword").show().fadeOut(5000);
		$("#error-innertxt_cpassword").html('This field is required');
		$("#txt_cpassword").focus();
		return false;
	}else if($("#txt_cpassword").val() != $("#txt_password").val()){
		$("#error-innertxt_cpassword").show().fadeOut(5000);
		$("#error-innertxt_cpassword").html('Confirm password not match');
		$("#txt_cpassword").focus();
		return false;
	}else if($("#txt_emailid").val() == ''){
		$("#error-innertxt_emailid").show().fadeOut(5000);
		$("#error-innertxt_emailid").html('This field is required');
		$("#txt_emailid").focus();
		return false;
	}else if(emailchk.test($("#txt_emailid").val()) ==  false){
		$("#error-innertxt_emailid").show().fadeOut(5000);
		$("#error-innertxt_emailid").html('Invalid email address');
		$("#txt_emailid").focus();
		return false;
	}else
		{
	 	var options = {
			beforeSubmit:  showRequest,
			success:       showResponse,
			url:       site_url+'controllers/ajax_controller/employeemanagement-ajax-controller.php', 
			type: "POST"
		};
		$('#form_employeemgt').submit(function() {
			$(this).ajaxSubmit(options);
			return false;
		});
  }
}
function showRequest(formData, jqForm, options) {
	return true;
} 
function showResponse(data, statusText)  {
	if (statusText == 'success') {
		if(data == 0){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Employee user name already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);
			document.getElementById('succ').innerHTML = 'Employee added successfully.';
			newdata();
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while adding employee.';
		}else if(data == 3){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Email address already exist. Please try another.';
		}
	} 
	$('#form_employeemgt').unbind('submit').bind('submit',function() {
		});
}
function update(){
	var alpha = /^[a-zA-Z]+$/;
	var alphanum = /^[a-zA-Z0-9]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	if($("#sel_usertype").val() == ''){
		$("#error-innersel_usertype").show().fadeOut(5000);
		$("#error-innersel_usertype").html('This field is required');
		$("#sel_usertype").focus();
		return false;
	}else if($("#txt_firstname").val() == ''){
		$("#error-innertxt_firstname").show().fadeOut(5000);
		$("#error-innertxt_firstname").html('This field is required');
		$("#txt_firstname").focus();
		return false;
	}else if(alpha.test($("#txt_firstname").val()) ==  false){
		$("#error-innertxt_firstname").show().fadeOut(5000);
		$("#error-innertxt_firstname").html('Invalid first name');
		$("#txt_firstname").focus();
		return false;
	}else if($("#txt_lastname").val() == ''){
		$("#error-innertxt_lastname").show().fadeOut(5000);
		$("#error-innertxt_lastname").html('This field is required');
		$("#txt_lastname").focus();
		return false;
	}else if(alpha.test($("#txt_lastname").val()) ==  false){
		$("#error-innertxt_lastname").show().fadeOut(5000);
		$("#error-innertxt_lastname").html('Invalid last name');
		$("#txt_lastname").focus();
		return false;
	}else if($("#txt_username").val() == ''){
		$("#error-innertxt_username").show().fadeOut(5000);
		$("#error-innertxt_username").html('This field is required');
		$("#txt_username").focus();
		return false;
	}else if(alphanum.test($("#txt_username").val()) ==  false){
		$("#error-innertxt_username").show().fadeOut(5000);
		$("#error-innertxt_username").html('Invalid user name');
		$("#txt_username").focus();
		return false;
	}else if($("#txt_password").val() == ''){
		$("#error-innertxt_password").show().fadeOut(5000);
		$("#error-innertxt_password").html('This field is required');
		$("#txt_password").focus();
		return false;
	}else if($("#txt_emailid").val() == ''){
		$("#error-innertxt_emailid").show().fadeOut(5000);
		$("#error-innertxt_emailid").html('This field is required');
		$("#txt_emailid").focus();
		return false;
	}else if(email.test($("#txt_emailid").val()) ==  false){
		$("#error-innertxt_emailid").show().fadeOut(5000);
		$("#error-innertxt_emailid").html('Invalid email address');
		$("#txt_emailid").focus();
		return false;
	}
	else
	{
		var options = {
		beforeSubmit:  showRequest,
		success:       showRequest_update,
		url:       site_url+'controllers/ajax_controller/employeemanagement-ajax-controller.php', 
		type: "POST"
		};
		$('#form_employeemgt').submit(function() {
			$(this).ajaxSubmit(options);
		return false;
		});
	}
}

function showRequest_update(data, statusText)  {
	if (statusText == 'success') {
		if(data == 0){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Employee user name already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);
			document.getElementById('succ').innerHTML = 'Employee updated successfully.';
			newdata();
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while updating employee.';
		}else if(data == 3){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Email address already exist. Please try another.';
		}
	} 
	$('#form_employeemgt').unbind('submit').bind('submit',function() {
		});
}
function showsection(id){
	if(id == 1){
		$("#rollmanagement").show();
		$("#rollmanagement1").show();
	}else{
		$("#rollmanagement").hide();
		$("#rollmanagement1").hide();
	}
}

function check_main(mainid)
{
	var totalcheckbox=$("#hid_cntsubsection"+mainid).val();
	var checked="yes";
	var counter=0;
	for(i=0;i<totalcheckbox;i++)	
	{
		if(document.getElementById('chk_subsection'+mainid+i).checked)
		{
			var counter=parseInt(counter)+parseInt(1);
		}		
	}
	if(counter==0)
	{
		document.getElementById('chk_mainsection'+mainid).checked=false;
	}
	else
	{
		document.getElementById('chk_mainsection'+mainid).checked=true;
	}
}
function checkall(mainid)
{
	
	var totalcheckbox=$("#hid_cntsubsection"+mainid).val();

	if(document.getElementById('chk_mainsection'+mainid).checked)
	{
		for(i=0;i<totalcheckbox;i++)	
		{
			document.getElementById('chk_subsection'+mainid+i).checked=true;	
		}
	}
	else
	{
		for(i=0;i<totalcheckbox;i++)	
		{
			document.getElementById('chk_subsection'+mainid+i).checked=false;	
		}
	}
}