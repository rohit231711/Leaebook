function getbrand(){ 
	$("#message-red").hide();
	$("#message-green").hide();
	var abc=$("#txt_addcategoryId").val();
	$.ajax( {
		url : site_url+'controllers/ajax_controller/product-ajax-controller.php', 
		type : 'post',
		data: 'getbrandid='+abc,
		success : function( resp ) {
			//alert(resp);
			$("#brandajax").html(resp);
		}
	});
}

function getmodel(){ 

	$("#message-red").hide();
	$("#message-green").hide();
	var abc=$("#txt_addbrandId").val();
	$.ajax( {
		url : site_url+'controllers/ajax_controller/product-ajax-controller.php', 
		type : 'post',
		data: 'getmodelid='+abc,
		success : function( resp ) {
				//alert(resp);
			$("#modelajax").html(resp);
		}
	});
}


function searching(){
	var alpha = /^[a-zA-Z]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	$("#searchmsg").html('');
	var flag = 0;
	var regflag = 0;if($("#txt_srcid").val() == '' && $("#txt_srcuserId").val() == '' && $("#txt_srccategoryId").val() == '' && $("#txt_srcbrandId").val() == '' && $("#txt_srcmodelId").val() == '' && $("#txt_srcyear").val() == '' && $("#txt_srctransmissionType").val() == '' && $("#txt_srcmileage").val() == '' && $("#txt_srcpriceRange").val() == '' && $("#txt_srccolor").val() == ''){
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
				url:       site_url+'controllers/ajax_controller/product-ajax-controller.php', 
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
			$("#product").html(data);
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
					 if($("#txt_adduserId").val() == ''){
					  $("#error-innertxt_adduserId").show().fadeOut(5000);
					  $("#error-innertxt_adduserId").html('This field is required');
					  $("#txt_adduserId").focus();
					  return false;
					}else if($("#txt_addcategoryId").val() == ''){
					  $("#error-innertxt_addcategoryId").show().fadeOut(5000);
					  $("#error-innertxt_addcategoryId").html('This field is required');
					  $("#txt_addcategoryId").focus();
					  return false;
					}else if($("#txt_addbrandId").val() == ''){
					  $("#error-innertxt_addbrandId").show().fadeOut(5000);
					  $("#error-innertxt_addbrandId").html('This field is required');
					  $("#txt_addbrandId").focus();
					  return false;
					}else if($("#txt_addmodelId").val() == ''){
					  $("#error-innertxt_addmodelId").show().fadeOut(5000);
					  $("#error-innertxt_addmodelId").html('This field is required');
					  $("#txt_addmodelId").focus();
					  return false;
					}else if($("#txt_addyear").val() == ''){
					  $("#error-innertxt_addyear").show().fadeOut(5000);
					  $("#error-innertxt_addyear").html('This field is required');
					  $("#txt_addyear").focus();
					  return false;
					}/*else if($("#txt_addtransmissionType").val() == ''){
				  $("#error-innertxt_addtransmissionType").show().fadeOut(5000);
				  $("#error-innertxt_addtransmissionType").html('This field is required');
				  $("#txt_addtransmissionType").focus();
				  return false;
				}else if($("#txt_addmileage").val() == ''){
				  $("#error-innertxt_addmileage").show().fadeOut(5000);
				  $("#error-innertxt_addmileage").html('This field is required');
				  $("#txt_addmileage").focus();
				  return false;
				}else if($("#txt_addpriceRange").val() == ''){
				  $("#error-innertxt_addpriceRange").show().fadeOut(5000);
				  $("#error-innertxt_addpriceRange").html('This field is required');
				  $("#txt_addpriceRange").focus();
				  return false;
				}else if($("#txt_addcolor").val() == ''){
				  $("#error-innertxt_addcolor").show().fadeOut(5000);
				  $("#error-innertxt_addcolor").html('This field is required');
				  $("#txt_addcolor").focus();
				  return false;
				}*/else if($("#txt_addcountry").val() == ''){
				  $("#error-innertxt_addcountry").show().fadeOut(5000);
				  $("#error-innertxt_addcountry").html('This field is required');
				  $("#txt_addcountry").focus();
				  return false;
				}else if($("#txt_addimages").val() == ''){
				  $("#error-innertxt_addimages").show().fadeOut(5000);
				  $("#error-innertxt_addimages").html('This field is required');
				  $("#txt_addimages").focus();
				  return false;
				}else
	{
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse,
			url:       site_url+'controllers/ajax_controller/product-ajax-controller.php', 
			type: "POST"
		};
		$('#form_productadd').submit(function() {
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
			document.getElementById('err').innerHTML = 'Product already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);		   
			document.getElementById('succ').innerHTML = 'Product added successfully.';
			newdata();				 
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while adding product.';
		}
		$('#form_productadd').unbind('submit').bind('submit',function() {
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
					if($("#txt_adduserId").val() == ''){
					  $("#error-innertxt_adduserId").show().fadeOut(5000);
					  $("#error-innertxt_adduserId").html('This field is required');
					  $("#txt_adduserId").focus();
					  return false;
					}else if($("#txt_addcategoryId").val() == ''){
					  $("#error-innertxt_addcategoryId").show().fadeOut(5000);
					  $("#error-innertxt_addcategoryId").html('This field is required');
					  $("#txt_addcategoryId").focus();
					  return false;
					}else if($("#txt_addbrandId").val() == ''){
					  $("#error-innertxt_addbrandId").show().fadeOut(5000);
					  $("#error-innertxt_addbrandId").html('This field is required');
					  $("#txt_addbrandId").focus();
					  return false;
					}else if($("#txt_addmodelId").val() == ''){
					  $("#error-innertxt_addmodelId").show().fadeOut(5000);
					  $("#error-innertxt_addmodelId").html('This field is required');
					  $("#txt_addmodelId").focus();
					  return false;
					}else if($("#txt_addyear").val() == ''){
					  $("#error-innertxt_addyear").show().fadeOut(5000);
					  $("#error-innertxt_addyear").html('This field is required');
					  $("#txt_addyear").focus();
					  return false;
					}/*else if($("#txt_addtransmissionType").val() == ''){
				  $("#error-innertxt_addtransmissionType").show().fadeOut(5000);
				  $("#error-innertxt_addtransmissionType").html('This field is required');
				  $("#txt_addtransmissionType").focus();
				  return false;
				}else if($("#txt_addmileage").val() == ''){
				  $("#error-innertxt_addmileage").show().fadeOut(5000);
				  $("#error-innertxt_addmileage").html('This field is required');
				  $("#txt_addmileage").focus();
				  return false;
				}else if($("#txt_addpriceRange").val() == ''){
				  $("#error-innertxt_addpriceRange").show().fadeOut(5000);
				  $("#error-innertxt_addpriceRange").html('This field is required');
				  $("#txt_addpriceRange").focus();
				  return false;
				}else if($("#txt_addcolor").val() == ''){
				  $("#error-innertxt_addcolor").show().fadeOut(5000);
				  $("#error-innertxt_addcolor").html('This field is required');
				  $("#txt_addcolor").focus();
				  return false;
				}*/else if($("#txt_addcountry").val() == ''){
				  $("#error-innertxt_addcountry").show().fadeOut(5000);
				  $("#error-innertxt_addcountry").html('This field is required');
				  $("#txt_addcountry").focus();
				  return false;
				}/*else if($("#txt_addimages").val() == ''){
				  $("#error-innertxt_addimages").show().fadeOut(5000);
				  $("#error-innertxt_addimages").html('This field is required');
				  $("#txt_addimages").focus();
				  return false;
				}*/else
	{
	   var options = {
			beforeSubmit:  showRequest_update,
			success:       showResponse_update,
			url:       site_url+'controllers/ajax_controller/product-ajax-controller.php', 
			type: "POST"
		};
		$('#form_productadd').submit(function() {
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
				document.getElementById('err').innerHTML = 'Product already exist. Please try another.';
			}else if(data == 1){
				$("#message-red").hide();
				$("#message-green").show().fadeOut(5000);				
				document.getElementById('succ').innerHTML = 'Product updated successfully.';
				newdata();
			}else if(data == 2){
				$.scrollTo(0,500);
				$("#message-red").show().fadeOut(7000);
				$("#message-green").hide();
				document.getElementById('err').innerHTML = 'Some error occurred while updating product.';
			}
		}
		$('#form_productadd').unbind('submit').bind('submit',function() {
		});
	}
function changefStatus(Id){
	var obj = document.getElementById('feature_'+Id).value;
	if (obj.indexOf('RFeatured') != -1)
	{
		$("#h_activestatus").val(Id);
		$("#h_activestatustype").val('0');
		$("#activestatus_line").html('This will make the product unfeatured.');
		$("#various_3s").fancybox().trigger('click');
	}
	else
	{
		$("#h_activestatus").val(Id);
		$("#h_activestatustype").val('1');
		$("#activestatus_line").html('This will make the product featured.');
		$("#various_3s").fancybox().trigger('click');
	}
}

function status_okf()
{
	var Id=$("#h_activestatus").val();
	var statustype=$("#h_activestatustype").val();
	$.ajax({
		type: "POST",
		url: site_url+'controllers/ajax_controller/'+pid+'-ajax-controller.php',
		data: 'statusfeatured='+statustype+'&statusid='+Id,
		cache: false,
		success: function(data){
		$("#message-green").show().fadeOut(5000);
		  document.getElementById('succ').innerHTML = pid_upper+' status changed successfully.';
		  if(statustype==1)
		  {
			  $("#feature_"+Id).val('RFeatured');
			  $("#df_"+Id).html("<a style='cursor:pointer' title='Remove Featured' class='icon-feature info-tooltip' onclick='changefStatus("+Id+")'></a>");
		  }
		  else
		  {
			  $("#feature_"+Id).val('MFeatured');
			  $("#df_"+Id).html("<a style='cursor:pointer' title='Make Featured' class='icon-addfeature info-tooltip' onclick='changefStatus("+Id+")'></a>");
		  }
		  //alert("Yes");
		  parent.$.fancybox.close();
		}
	});
}