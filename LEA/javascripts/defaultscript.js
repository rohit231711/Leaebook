	
function dateexpired()
{
	alert("Request Date Expired...");
}

function deleteselected(){
	
	$(".account-content").slideUp();
	$("#actions-box-slider").slideUp();
	var selected = new Array();
	$("input:checkbox:checked").each(function() {
		selected.push($(this).val());
	});
	if(selected == '')
	{
		$("#message-red").show().fadeOut(7000);
		document.getElementById('err').innerHTML = 'Please select '+pid_lower+' to delete.';
	}
	else
	{	
		$("#action_line").html('Are you sure want to delete selected '+pid_lower+'?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"deleteselected_ok('"+selected+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#various_33").fancybox().trigger('click');
	}
}

function deleteselected_ok(selected){
	$.ajax({
	
		type: "POST",
		url: site_url+'controllers/ajax_controller/'+pid+'-ajax-controller.php',
		data: 'deleselected=1&delete='+selected,
		cache: false,
		success: function(data){
			
		$("#message-red").hide();
		$("#message-green").show().fadeOut(5000);
		  document.getElementById('succ').innerHTML = pid_upper+' deleted successfully.';
		  parent.$.fancybox.close();
		  noofrow();
		}
	});
}

function statusactive(){
	$(".account-content").slideUp();
	$("#actions-box-slider").slideUp();
	var selected = new Array();
	$("input:checkbox:checked").each(function() {
		selected.push($(this).val());
	});
	if(selected == '')
	{
		$("#message-red").show().fadeOut(7000);
		document.getElementById('err').innerHTML = 'Please select '+pid_lower+' to make a status active.';
	}
	else
	{	
		$("#action_line").html('Are you sure to make status active to selected '+pid_lower+'?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"statusactive_ok('"+selected+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#various_33").fancybox().trigger('click');
	}
}

function statusactive_ok(selected){
	$.ajax({
		type: "POST",
		url: site_url+'controllers/ajax_controller/'+pid+'-ajax-controller.php',
		data: 'statusactive=1&active='+selected,
		cache: false,
		success: function(data)
		{
			//alert(data);
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);
			document.getElementById('succ').innerHTML = pid_upper+' status changed successfully.';
			parent.$.fancybox.close();
			noofrow();
		}
	});
}

function statusinactive(){
	$(".account-content").slideUp();
	$("#actions-box-slider").slideUp();
	var selected = new Array();
	$("input:checkbox:checked").each(function() {
		selected.push($(this).val());
	});
	if(selected == '')
	{
		$("#message-red").show().fadeOut(7000);
		document.getElementById('err').innerHTML = 'Please select '+pid_lower+' to make a status inactive.';
	}
	else
	{	
		$("#action_line").html('Are you sure to make status inactive to selected '+pid_lower+'?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"statusinactive_ok('"+selected+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#various_33").fancybox().trigger('click');
	}
}

function statusinactive_ok(selected){
	$.ajax({
		type: "POST",
		url: site_url+'controllers/ajax_controller/'+pid+'-ajax-controller.php',
		data: 'statusinactive=2&inactive='+selected,
		cache: false,
		success: function(data)
		{
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);
			document.getElementById('succ').innerHTML = pid_upper+' status changed successfully.';
			parent.$.fancybox.close();
			noofrow();
		}
	});
}

function view(id){
	//loader_show();	
	$.ajax( {
		url : site_url+'controllers/ajax_controller/'+pid+'-ajax-controller.php',
		type : 'post',
		data: 'view=1&id='+id,
		success : function(result)
		{
			//alert(result);
			//loader_hide();
			$("#view_detail").html(result);
			$("#common").html('View Detail');
			$("#various_1").fancybox().trigger('click');
			//loader_hide();
		}
	});
	//loader_hide();
}

function artist(id){
	$.ajax( {
		url : site_url+'controllers/ajax_controller/'+pid+'-ajax-controller.php',
		type : 'post',
		data: 'artist=1&id='+id,
		success : function(result)
		{
			$("#view_detail").html(result);
			$("#common").html('AlbumArtist Detail');
			$("#various_1").fancybox().trigger('click');
		}
	});
}


function changeStatus(Id){
	var obj = document.getElementById('status_'+Id).value;
	if (obj.indexOf('Active') != -1)
	{
		$("#h_status").val(Id);
		$("#h_statustype").val('0');
		$("#status_line").html('This will make the '+pid_lower+' inactive.');
		$("#various_3").fancybox().trigger('click');
	}
	else
	{
		$("#h_status").val(Id);
		$("#h_statustype").val('1');
		$("#status_line").html('This will make the '+pid_lower+' active.');
		$("#various_3").fancybox().trigger('click');
	}
}

function status_ok()
{
	var Id=$("#h_status").val();
	var statustype=$("#h_statustype").val();
	$.ajax({
		type: "POST",
		url: site_url+'controllers/ajax_controller/'+pid+'-ajax-controller.php',
		data: 'status='+statustype+'&statusid='+Id,
		cache: false,
		success: function(data){
		$("#message-green").show().fadeOut(5000);
		  document.getElementById('succ').innerHTML = pid_upper+' status changed successfully.';
		  if(statustype==1)
		  {
			  $("#status_"+Id).val('Active');
			  $("#d_"+Id).html("<a style='cursor:pointer' title='Active' class='icon-active info-tooltip' onclick='changeStatus("+Id+")'></a>");
		  }
		  else
		  {
			  $("#status_"+Id).val('Inactive');
			  $("#d_"+Id).html("<a style='cursor:pointer' title='Inactive' class='icon-inactive info-tooltip' onclick='changeStatus("+Id+")'></a>");
		  }
		  //alert("Yes");
		  parent.$.fancybox.close();
		}
	});
}

function deleteuser(id){
    alert('called');
	$("#h_delete").val(id);
	$("#delete_line").html('Are you sure to delete this ?');
	$("#various_4").fancybox().trigger('click');
}

function delete_ok()
{
	var Id=$("#h_delete").val();
	$.ajax({
		type: "POST",
		url: site_url+'controllers/ajax_controller/'+pid+'-ajax-controller.php',
		data: 'delete=1&id='+Id,
		cache: false,
		scroll:true,
		success: function(data){
			//alert(data);
			parent.$.fancybox.close();
			$("#message-green").show().fadeOut(5000);
			document.getElementById('succ').innerHTML = pid_upper+' deleted successfully.';
			noofrow();
		}
	});
}

function status_cancel()
{
	parent.$.fancybox.close();
	$("#message-red").hide();
}

function newdata(){
	showviewdiv(0,20,0,0,0,0,0,'asc');
	$("#main_addbutton").show();
	$('#content-table-inner').css("background-color", "#FFFFFF");
	loader_hide();
}

function showviewdiv(prevnext,row,curr_page,searchval,last,first,fieldname,orderby){
	/*if($("#h_orderby").val()=="")
	{
		$("#h_orderby").val("asc");
		var orderby="asc";
	}
	else if($("#h_orderby").val()=="asc")
	{
		$("#h_orderby").val("desc");
		var orderby="desc";
	}
	else if($("#h_orderby").val()=="desc")
	{
		$("#h_orderby").val("asc");
		var orderby="asc";
	}*/
	
	$("#h_orderby").val(orderby);
	$.scrollTo(0,500);
	loader_show();
	$.ajax( {
		url : site_url+'controllers/ajax_controller/'+pid+'-ajax-controller.php', 
		type : 'post',
		data: 'viewdiv=1&prevnext='+prevnext+'&row='+row+'&curr_page='+curr_page+'&search='+searchval+'&last='+last+'&first='+first+'&fieldname='+fieldname+'&orderby='+orderby,
		success : function( resp ) {
			document.getElementById(pid).style.display = 'block';
			$("#"+pid).html(resp);
			if(fieldname != ''){
				var myid2=fieldname.replace(".", "_").trim();
				
				if(orderby=='desc'){
					$('#'+myid2).css('background', 'url(images/table/table_sort_arrow.gif) right no-repeat');	
				}else{
					$('#'+myid2).css('background', 'url(images/table/table_sort_arrow_2.gif) right no-repeat');
				}
			}
			
			loader_hide();
		}
	});
}

function edit(id,show){ 

	loader_show();
	$("#message-red").hide();	
	$("#message-green").hide();      
	$.ajax( {
		url : site_url+'controllers/ajax_controller/'+pid+'-ajax-controller.php', 
		type : 'post',
		data: 'edit=1&id='+id,				
		success : function( resp ) {
			$("#main_addbutton").hide();
			$("#"+show).html(resp);
			loader_hide();
		}
	});
}

function noofrow(){
	var last = 0;
	var first =0;
	var prevnext = $("#hid_prevnext").val();
	var row =  $("#sel_noofrow").val();

	if($("#hidsearch").val() == ''){
		var searchval = $("#hidsearch").val();
	}else{
		var searchval = 0;
	}

	var curr_page = $("#hid_curr_page").val();
	if($("#hid_fieldname").val() != ''){
		var fieldname = $("#hid_fieldname").val();
	}else{
		var fieldname = 0;
	}
	var orderby=$("#hid_orderby").val();
	showviewdiv(prevnext,row,curr_page,searchval,last,first,fieldname,orderby);
	loader_hide();
}

function noofrow1(){
	var last = 0;
	var first =0;
    var prevnext = $("#hid_prevnext").val();
    var row =  $("#sel_noofrow").val();
	if($("#hidsearch").val() != ''){
		var searchval = $("#hidsearch").val();
	}else{
		var searchval = 0;
	}
   if($("#hid_fieldname").val() != ''){
		var fieldname = $("#hid_fieldname").val();
	 }else{
	    var fieldname = 0;
	 }
	 var orderby=$("#hid_orderby").val();
	showviewdiv(0,row,0,searchval,last,first,fieldname,orderby);
}

function pageprev(){
	var last = 0;
	var first =0;
	var prevnext = $("#hid_prevnext").val();
	var row =  $("#sel_noofrow").val();

	if($("#hidsearch").val() != ''){
		var searchval = $("#hidsearch").val();
	}else{
		var searchval = 0;
	}

	var finalprevnext = parseInt(prevnext)-parseInt(row);
	$("#hid_prevnext").val(finalprevnext);
	var curr_page = $("#hid_curr_page").val();
	var curr_page = parseInt(curr_page)-1; 

	if($("#hid_fieldname").val() != ''){
		var fieldname = $("#hid_fieldname").val();
	}else{
		var fieldname = 0;
	}
	var orderby=$("#hid_orderby").val();
	showviewdiv(finalprevnext,row,curr_page,searchval,last,first,fieldname,orderby);
}

function sortingbyfield(fieldname,orderby){
  	var last = 0;
  	var first =0;
  	var row =  $("#sel_noofrow").val();

	if($("#hidsearch").val() != ''){
		var searchval = $("#hidsearch").val();
	}else{
		var searchval = 0;
	}
	showviewdiv(0,row,0,searchval,last,first,fieldname,orderby);
}

function pagenext(){
	var last = 0;
	var first =0;
	var prevnext = $("#hid_prevnext").val();
	var row =  $("#sel_noofrow").val();

	if($("#hidsearch").val() != ''){
		var searchval = $("#hidsearch").val();
	}else{
		var searchval = 0;
	}
	if($("#hid_fieldname").val() != ''){
		var fieldname = $("#hid_fieldname").val();
	}else{
		var fieldname = 0;
	}

	var finalprevnext = parseInt(prevnext)+parseInt(row);
	$("#hid_prevnext").val(finalprevnext);
	var curr_page = $("#hid_curr_page").val();
	var curr_page = parseInt(curr_page)+1; 
	var orderby=$("#hid_orderby").val();
	showviewdiv(finalprevnext,row,curr_page,searchval,last,first,fieldname,orderby);
}

function pagelast(totalpage){
	var last = 0;
	var first = 1;
	var row =  $("#sel_noofrow").val();

	if($("#hidsearch").val() != ''){
		var searchval = $("#hidsearch").val();
	}else{
		var searchval = 0;
	}

	if($("#hid_fieldname").val() != ''){
		var fieldname = $("#hid_fieldname").val();
	}else{
		var fieldname = 0;
	}

	var finalprevnext = totalpage;
	var orderby=$("#hid_orderby").val();
	showviewdiv(finalprevnext,row,0,searchval,last,first,fieldname,orderby);
}

function pagefirst(){
	var last = 1;
	var first = 0;
	var row =  $("#sel_noofrow").val();

	if($("#hidsearch").val() != ''){
		var searchval = $("#hidsearch").val();
	}else{
		var searchval = 0;
	}

	if($("#hid_fieldname").val() != ''){
		var fieldname = $("#hid_fieldname").val();
	}else{
		var fieldname = 0;
	}
	var orderby=$("#hid_orderby").val();
	showviewdiv(0,row,0,searchval,last,first,fieldname,orderby);
}

function showadd1(show){

	$("#message-red").hide();	
	$("#message-green").hide();
    edit(0,show);
}
function show_search(){
	document.getElementById('search').value = 1;		
	$("#various_2").fancybox().trigger('click');
}

function hide(){
	document.getElementById('search').value = 0;	
	Popup.hide('search');
}

function show_upload(){
  $("#various_5").fancybox().trigger('click');
  $("#errorfile_uploadcsv").hide();
}

function uploadcsv(){
	var csv = $("#file_uploadcsv").val();
	var ext = csv.split(".");
	var n = ext.length-1;
	var ext = ext[n];
	if(ext == ''){
		$("#errorfile_uploadcsv").show().fadeOut(5000);
		$("#error-innerfile_uploadcsv").html('Please select CSV file.');
		$("#file_uploadcsv").focus();
		return false;
	}else if(ext != "xls"){
		$("#errorfile_uploadcsv").show().fadeOut(5000);
		$("#error-innerfile_uploadcsv").html('Invalid file format.');
		$("#file_uploadcsv").focus();
		return false;
    }
	else{
		$("#errorfile_uploadcsv").hide();
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse_uplaod,
			url:       site_url+'controllers/ajax_controller/'+pid+'-ajax-controller.php', 
			type: "POST"
		};

		$('#form_uploadcsv').submit(function() {
			$(this).ajaxSubmit(options);
			return false;
		});
	 }
}

function showResponse_uplaod(data, statusText)  { // this function use to display the message after entering data
if (statusText == 'success') {
	 parent.$.fancybox.close();
		if(data == 0){
			$.scrollTo(0,500);
		   $("#message-red").show().fadeOut(7000);
		   $("#message-green").hide();
		   document.getElementById('err').innerHTML = 'All this data are already added.';
		   $("#file_uploadcsv").val('');
		}else if(data == 1){
		  $("#message-green").show().fadeOut(5000);
		   $("#message-red").hide();
		   document.getElementById('succ').innerHTML = 'CSV uploaded successfully.';
		    $("#file_uploadcsv").val('');
		   newdata();
		}else if(data == 2){
			$.scrollTo(0,500);
		   $("#message-red").show().fadeOut(7000);
		   $("#message-green").hide();
		   document.getElementById('err').innerHTML = 'Some error occuring while uploading CSV';
		   $("#file_uploadcsv").val('');
		}else if(data == 3){
			$.scrollTo(0,500);
		   $("#message-red").show().fadeOut(7000);
		   $("#message-green").hide();
		   document.getElementById('err').innerHTML = 'Some field are missing in CSV';
		   $("#file_uploadcsv").val('');
		}
	} else {
	}
}
function loader_show()
{
	var v = jQuery(document).height();
	var wheight=jQuery(window).height();
	var wheight=parseInt(wheight)/parseInt(2);
	var scrolling = jQuery(window).scrollTop();
	var $marginTop = parseInt(wheight)+parseInt(scrolling)-parseInt(50);
	
	var v2 = parseInt(v)-parseInt($marginTop);
	jQuery("#div_loader2").css({'margin-top': $marginTop});
	document.getElementById('div_loader').style.height=v+'px';
	jQuery('#div_loader').fadeIn();
}
function loader_hide()
{
	jQuery('#div_loader').fadeOut();
}

function change_theme(mypid){
	$.ajax( {
		url : site_url+'controllers/ajax_controller/theme-ajax-controller.php',
		type : 'post',
		data: 'showtheme=1&mypid='+mypid,
		success : function(result)
		{
			$("#view_detail").html(result);
			$("#common").html('Change Theme');
			$("#various_1").fancybox().trigger('click');
		}
	});
}
function active_theme(themename,mypid){
	$.ajax( {
		url : site_url+'controllers/ajax_controller/theme-ajax-controller.php',
		type : 'post',
		data: 'activatetheme=1&themename='+themename+'&mypid='+mypid,
		success : function(result)
		{
			window.location.href='index.php?pid='+result;
		}
	});
}


function common_back(){
	var step1=$("#hid_step1").val();
	var step2=$("#hid_step2").val();
	
	//alert($("#hid_step1").val());
	//alert($("#hid_step2").val());
	
	var step1_1=step1.split("|");
	var step1_2=(step1_1.length-2);
	var new_step1="";
	var i;
	for(i=0;i<step1_2;i++){
		if(new_step1==''){
			new_step1=step1_1[i];
		}else{
			new_step1=new_step1+"|"+step1_1[i];
		}
	}
	var myfunction1=step1_1[step1_2];
	
	var step2_1=step2.split("|");
	
	if(step1_1.length==step2_1.length){
		var step2_2=(step2_1.length-2);
		var new_step2="";
		var i;
		for(i=0;i<step2_2;i++){
			if(new_step2==''){
				new_step2=step2_1[i];
			}else{
				new_step2=new_step2+","+step2_1[i];
			}
		}
		var myfunction2=step2_1[step2_2];
	}else if(step1_1.length>parseInt(step2_1.length)+parseInt(1)){
		myfunction2="";
		var new_step2="";
		var i;
		for(i=0;i<step2_1.length;i++){
			if(new_step2==''){
				new_step2=step2_1[i];
			}else{
				new_step2=new_step2+"|"+step2_1[i];
			}
		}
		//alert('no function call');
	}else{
		if(step2_1.length==1){
			var new_step2=step2;
			var myfunction2=step2;
		}else{
			var step2_2=(step2_1.length-1);
			var new_step2="";
			var i;
			for(i=0;i<step2_2;i++){
				if(new_step2==''){
					new_step2=step2_1[i];
				}else{
					new_step2=new_step2+"|"+step2_1[i];
				}
			}
			var myfunction2=step2_1[step2_2];
		}
	}	
	
	$("#hid_step1").val(new_step1);
	$("#hid_step2").val(new_step2);
	
	/*alert($("#hid_step1").val());
	alert($("#hid_step2").val());*/
	//alert(myfunction2);
	eval(myfunction1);
	
	setTimeout(function () {
            eval(myfunction2);
        }, 2000);	
}