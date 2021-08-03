//For BAND MEMBER 
function view_bmydetail(id){
	var step1=$("#hid_step1").val();
	var step1_1=step1.split("|");
	var step2=$("#hid_step2").val();
	var step2_1=step2.split("|");
	
	if(step1_1.length==1 && step2_1.length==1){			
		step2="";
	}else if(step1_1.length==step2_1.length){	
		var step2_2=(step2_1.length-1);
		var new_step2="";
		var i;
		for(i=0;i<step2_2;i++){
			if(new_step2==''){
				new_step2=step2_1[i];
			}else{
				new_step2=new_step2+","+step2_1[i];
			}
		}
		var step2=new_step2;
	}		
	if(step2==''){
		var newvalue="view_bmydetail('"+id+"')";
	}else{
		var newvalue=step2+"|view_bmydetail('"+id+"')";
	}
	$("#hid_step2").val(newvalue);
	jQuery('#bmember_gallery').removeClass('active_small_tab');
	jQuery('#bmember_albums').removeClass('active_small_tab');
	jQuery('#bmember_followers').removeClass('active_small_tab');
	
	jQuery('#bmydetail').addClass('active_small_tab');
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/bandmember-ajax-controller.php', 
		type : 'post',
		data: "view=1&id="+id,
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
		}
	});
}
function upload_bmpopup(id){
	$.ajax( {
		url : site_url+'controllers/ajax_controller/upload-image-ajax-controller.php',
		type : 'post',
		data: 'uploadpopup=1&path=bmem_image/bmem_gallery&forwhat=bmem&bmemid='+id,
		success : function(result)
		{
			$("#view_detail").html(result);
			$("#common").html('Upload Images');
			$("#various_1").fancybox().trigger('click');
		}
	});
}
/*function view_bandmemberdetail(id,mainid,tabid)
{
	var backvalue=$("#divforback").html();
	$.ajax({
		type: "POST",
		url: site_url+'controllers/ajax_controller/showgallery-ajax-controller.php',
		data: 'viewbmemdetails=1&fid='+id,
		cache: false,
		success: function(data){			
			$("#band").html(data);
			if(backvalue==""){
				$("#divforback").html("<input class=\"button_bg\" type=\"button\" value=\"Back\" name=\"btn_back\" onclick=\"viewdetails_new('"+mainid+"','show_member')\">");
			}else{
				var changestr=backvalue.replace("viewdetails_new","viewdetails_new2");
				var changestr=changestr.replace("')","','"+id+"','view_bandmemberdetail','show_mevent')");
				//alert(changestr);
				$("#divforback").html(changestr);
			}
		}
	});
}*/

function view_bandmemberdetail(id,mainid,tabid)
{
	var step1=$("#hid_step1").val();
	if(step1==''){
		var newvalue="view_bandmemberdetail('"+id+"','"+mainid+"','"+tabid+"')";
	}else{
		var newvalue=step1+"|view_bandmemberdetail('"+id+"','"+mainid+"','"+tabid+"')";
	}
	$("#hid_step1").val(newvalue);
	$.ajax({
		type: "POST",
		url: site_url+'controllers/ajax_controller/showgallery-ajax-controller.php',
		data: 'viewbmemdetails=1&fid='+id+'&mainid='+mainid,
		cache: false,
		success: function(data){			
			$("#band").html(data);			
			$("#divforback").html("<input class=\"button_bg\" type=\"button\" value=\"Back\" name=\"btn_back\" onclick=\"common_back()\">");
		}
	});
}
function removerbandmfollowerask(id,bid)
{
	$("#action_line").html('Are you sure want to delete selected follower?');
	$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removerbandmfollower('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
	$("#various_33").fancybox().trigger('click');
}
function removerbandmfollower(id,bid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/followertab-ajax-controller.php', 
		type : 'post',
		data: "removebandmfollower=1&id="+id,
		success : function(resp)
		{
			parent.$.fancybox.close();
			show_mfollower(bid);
		}
	});	
}
function show_mfollower(id)
{
	var step1=$("#hid_step1").val();
	var step1_1=step1.split("|");
	var step2=$("#hid_step2").val();
	var step2_1=step2.split("|");
	
	if(step1_1.length==1 && step2_1.length==1){			
		step2="";
	}else if(step1_1.length==step2_1.length){
		var step2_2=(step2_1.length-1);
		var new_step2="";
		var i;
		for(i=0;i<step2_2;i++){
			if(new_step2==''){
				new_step2=step2_1[i];
			}else{
				new_step2=new_step2+","+step2_1[i];
			}
		}
		var step2=new_step2;
	}
	if(step2==''){
		var newvalue="show_mfollower('"+id+"')";
	}else{
		var newvalue=step2+"|show_mfollower('"+id+"')";
	}
	$("#hid_step2").val(newvalue);
	jQuery('#bmember_gallery').removeClass('active_small_tab');
	jQuery('#bmember_events').removeClass('active_small_tab');
	jQuery('#bmember_albums').removeClass('active_small_tab');
	jQuery('#bmember_followers').removeClass('active_small_tab');
	jQuery('#bmydetail').removeClass('active_small_tab');
	
	jQuery('#bmember_followers').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/followertab-ajax-controller.php', 
		type : 'post',
		data: "showfollowerbandm=1&id="+id+"&pfunction=show_paging_13",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
		}
	});	
}
function show_paging_13(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/followertab-ajax-controller.php', 
		type : 'post',
		 data: "showfollowerbandm=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function removerbandmalbumask(id,bid)
{
	$("#action_line").html('Are you sure want to remove from selected album?');
	$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removerbandmalbum('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
	$("#various_33").fancybox().trigger('click');
}
function removerbandmalbum(id,bid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/albumtab-ajax-controller.php', 
		type : 'post',
		data: "removebandmevent=1&id="+id,
		success : function(resp)
		{
			parent.$.fancybox.close();
			show_malbum(bid);
		}
	});	
}
function show_malbum(id)
{
	var step1=$("#hid_step1").val();
	var step1_1=step1.split("|");
	var step2=$("#hid_step2").val();
	var step2_1=step2.split("|");
	
	if(step1_1.length==1 && step2_1.length==1){			
		step2="";
	}else if(step1_1.length==step2_1.length){	
		var step2_2=(step2_1.length-1);
		var new_step2="";
		var i;
		for(i=0;i<step2_2;i++){
			if(new_step2==''){
				new_step2=step2_1[i];
			}else{
				new_step2=new_step2+","+step2_1[i];
			}
		}
		var step2=new_step2;
	}		
	if(step2==''){
		var newvalue="show_malbum('"+id+"')";
	}else{
		var newvalue=step2+"|show_malbum('"+id+"')";
	}
	$("#hid_step2").val(newvalue);
	
	jQuery('#bmember_gallery').removeClass('active_small_tab');
	jQuery('#bmember_events').removeClass('active_small_tab');
	jQuery('#bmember_albums').removeClass('active_small_tab');
	jQuery('#bmember_followers').removeClass('active_small_tab');
	jQuery('#bmydetail').removeClass('active_small_tab');
	
	jQuery('#bmember_albums').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/albumtab-ajax-controller.php', 
		type : 'post',
		data: "showalbumbandm=1&id="+id+"&pfunction=show_paging_12",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
		}
	});	
}
function show_paging_12(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/albumtab-ajax-controller.php', 
		type : 'post',
		 data: "showalbumbandm=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function removerbandmeventask(id,bid)
{
	$("#action_line").html('Are you sure want to unfollow selected event?');
	$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removerbandmevent('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
	$("#various_33").fancybox().trigger('click');
}
function removerbandmevent(id,bid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/eventtab-ajax-controller.php', 
		type : 'post',
		data: "removebandmevent=1&id="+id,
		success : function(resp)
		{
			parent.$.fancybox.close();
			show_mevent(bid);
		}
	});	
}
function show_mevent(id)
{
	jQuery('#bmember_gallery').removeClass('active_small_tab');
	jQuery('#bmember_events').removeClass('active_small_tab');
	jQuery('#bmember_albums').removeClass('active_small_tab');
	jQuery('#bmember_followers').removeClass('active_small_tab');
	jQuery('#bmydetail').removeClass('active_small_tab');
	
	jQuery('#bmember_events').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/eventtab-ajax-controller.php', 
		type : 'post',
		data: "showeventbandm=1&id="+id,
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
		}
	});	
}
function removerbandmimageask(id,bid)
{
		$("#action_line").html('Are you sure to delete selected image?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removerbandmimage('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#inline_33").attr('style', 'width:350px;');
		$("#various_33").fancybox().trigger('click');
}
function removerbandmimage(id,fid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		data: "removebmemimg=1&id="+id,
		success : function(resp)
		{
			//alert("OK");
			parent.$.fancybox.close();
			show_mgallery(fid);
		}
	});	
}
function show_mgallery(id)
{
	var step1=$("#hid_step1").val();
	var step1_1=step1.split("|");
	var step2=$("#hid_step2").val();
	var step2_1=step2.split("|");
	
	if(step1_1.length==1 && step2_1.length==1){			
		step2="";
	}else if(step1_1.length==step2_1.length){	
		var step2_2=(step2_1.length-1);
		var new_step2="";
		var i;
		for(i=0;i<step2_2;i++){
			if(new_step2==''){
				new_step2=step2_1[i];
			}else{
				new_step2=new_step2+","+step2_1[i];
			}
		}
		var step2=new_step2;
	}		
	if(step2==''){
		var newvalue="show_mgallery('"+id+"')";
	}else{
		var newvalue=step2+"|show_mgallery('"+id+"')";
	}
	$("#hid_step2").val(newvalue);
	
	jQuery('#bmember_gallery').removeClass('active_small_tab');
	jQuery('#bmember_events').removeClass('active_small_tab');
	jQuery('#bmember_albums').removeClass('active_small_tab');
	jQuery('#bmember_followers').removeClass('active_small_tab');
	jQuery('#bmydetail').removeClass('active_small_tab');
	
	jQuery('#bmember_gallery').addClass('active_small_tab');
	//alert(id);
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		data: "showgallerybmem=1&id="+id+"&pfunction=show_paging_11",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
		}
	});	
}
function show_paging_11(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		 data: "showgallerybmem=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}


// FOR ALBUM

function view_albumdetail(id,mainid)
{
	$.ajax({
		type: "POST",
		url: site_url+'controllers/ajax_controller/showgallery-ajax-controller.php',
		data: 'viewalbumdetails=1&fid='+id,
		cache: false,
		success: function(data){
			$("#band").html(data);
			$("#divforback").html("<input class=\"button_bg\" type=\"button\" value=\"Back\" name=\"btn_back\" onclick=\"viewdetails_new('"+mainid+"','show_album')\">");
		}
	});
}

function removeralbumartistask(id,bid)
{
	$("#action_line").html('Are you sure want to delete selected follower?');
	$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removeralbumartist('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
	$("#various_33").fancybox().trigger('click');
}
function removeralbumartist(id,bid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/artisttab-ajax-controller.php', 
		type : 'post',
		data: "removealbumartist=1&id="+id,
		success : function(resp)
		{
			parent.$.fancybox.close();
			show_alartist(bid);
		}
	});	
}
function show_alartist(id)
{
	jQuery('#album_gallery').removeClass('active_small_tab');
	jQuery('#album_artist').removeClass('active_small_tab');
	jQuery('#album_purchase').removeClass('active_small_tab');
	jQuery('#album_review').removeClass('active_small_tab');
	jQuery('#album_rating').removeClass('active_small_tab');
	
	jQuery('#album_artist').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/artisttab-ajax-controller.php', 
		type : 'post',
		data: "showalbumartist=1&id="+id,
		success : function(resp)
		{
			jQuery("#bottom_container").html(resp);
		}
	});	
}
function removealimageask(id,bid)
{
		$("#action_line").html('Are you sure to delete selected image?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removealimage('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#inline_33").attr('style', 'width:350px;');
		$("#various_33").fancybox().trigger('click');
}
function removealimage(id,fid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		data: "removealbumimg=1&id="+id,
		success : function(resp)
		{
			parent.$.fancybox.close();
			show_algallery(fid);
		}
	});	
}
function show_algallery(id)
{
	jQuery('#album_gallery').removeClass('active_small_tab');
	jQuery('#album_artist').removeClass('active_small_tab');
	jQuery('#album_purchase').removeClass('active_small_tab');
	jQuery('#album_review').removeClass('active_small_tab');
	jQuery('#album_rating').removeClass('active_small_tab');
	
	jQuery('#album_gallery').addClass('active_small_tab');
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		data: "showgalleryalbum=1&id="+id+"&pfunction=show_paging_14",
		success : function(resp)
		{
			jQuery("#bottom_container").html(resp);
		}
	});	
}

function show_paging_14(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		 data: "showgalleryalbum=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}

function upload_alpopup(id){
	$.ajax( {
		url : site_url+'controllers/ajax_controller/upload-image-ajax-controller.php',
		type : 'post',
		data: 'uploadpopup=1&path=album_image/album_gallery&forwhat=album&albumid='+id,
		success : function(result)
		{
			$("#view_detail").html(result);
			$("#common").html('Upload Images');
			$("#various_1").fancybox().trigger('click');
		}
	});
}

// FOR FAN	

function view_fansdetail(id)
{
	var step1=$("#hid_step1").val();
	if(step1==''){
		var newvalue="view_fansdetail('"+id+"')";
	}else{
		var newvalue=step1+"|view_fansdetail('"+id+"')";
	}
	$("#hid_step1").val(newvalue);
	loader_show();
	$.ajax({
		type: "POST",
		url: site_url+'controllers/ajax_controller/favoriteband-ajax-controller.php',
		data: 'viewfandetails=1&fid='+id+"&pfunction=show_paging_18",
		cache: false,
		success: function(data){
			$("#band").html(data);			
			$("#divforback").html("<input class=\"button_bg\" type=\"button\" value=\"Back\" name=\"btn_back\" onclick=\"common_back()\">");
			loader_hide();
		}
	});	
}

function view_fandetail(id,mainid)
{
	$.ajax({
		type: "POST",
		url: site_url+'controllers/ajax_controller/showgallery-ajax-controller.php',
		data: 'viewfandetails=1&fid='+id,
		cache: false,
		success: function(data){
			$("#band").html(data);
			$("#divforback").html("<input class=\"button_bg\" type=\"button\" value=\"Back\" name=\"btn_back\" onclick=\"common_back()\">");
		}
	});
}

function removefanimageask(id,bid)
{
		$("#action_line").html('Are you sure to delete selected image?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removefanimage('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#inline_33").attr('style', 'width:350px;');
		$("#various_33").fancybox().trigger('click');
}
function removefanimage(id,fid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		data: "removefanimg=1&id="+id,
		success : function(resp)
		{
			parent.$.fancybox.close();
			show_fgallery(fid);
		}
	});	
}
function upload_fpopup(id){
	$.ajax( {
		url : site_url+'controllers/ajax_controller/upload-image-ajax-controller.php',
		type : 'post',
		data: 'uploadpopup=1&path=fan_image/fan_gallery&forwhat=fans&fansid='+id,
		success : function(result)
		{
			$("#view_detail").html(result);
			$("#common").html('Upload Images');
			$("#various_1").fancybox().trigger('click');
		}
	});
}

function show_fgallery(id)
{
	jQuery('#fan_gallery').removeClass('active_small_tab');
	jQuery('#fan_fband').removeClass('active_small_tab');
	jQuery('#fan_fartist').removeClass('active_small_tab');
	jQuery('#fan_events').removeClass('active_small_tab');
	jQuery('#fan_merchandise').removeClass('active_small_tab');
	jQuery('#fan_wishlist').removeClass('active_small_tab');
	jQuery('#fan_donation').removeClass('active_small_tab');
	
	jQuery('#fan_gallery').addClass('active_small_tab');
	//alert(id);
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		data: "showgallery=1&id="+id,
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
		}
	});	
}

	function show_ffavoriteband(id)
	{
		jQuery('#fan_gallery').removeClass('active_small_tab');
		jQuery('#fan_fband').removeClass('active_small_tab');
		jQuery('#fan_fartist').removeClass('active_small_tab');
		jQuery('#fan_events').removeClass('active_small_tab');
		jQuery('#fan_merchandise').removeClass('active_small_tab');
		jQuery('#fan_wishlist').removeClass('active_small_tab');
		jQuery('#fan_donation').removeClass('active_small_tab');
		
		jQuery('#fan_fband').addClass('active_small_tab');
		
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/favoriteband-ajax-controller.php', 
			type : 'post',
			data: "showfavoriteband=1&id="+id+"&pfunction=show_paging_18",
			success : function(resp)
			{
				//alert(resp);
				jQuery("#bottom_container").html(resp);
			}
		});	
	}
	function show_paging_18(pageno,perpage,id,pfunction)
	{
		loader_show();
		var combototal=jQuery("#combo_totalpage").val();
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/favoriteband-ajax-controller.php', 
			type : 'post',
			 data: "showfavoriteband=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
			 success : function(resp){
				//jQuery.scrollTo(130,500);
				jQuery("#bottom_container").html(resp);
				loader_hide();
			}
		});	
	}
	
	function remove_ffavoriteband(id,bid)
	{
		$("#action_line").html('Are you sure want to delete selected band?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removeffavoriteband('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#various_33").fancybox().trigger('click');
	}
	function removeffavoriteband(id,bid)
	{
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/favoriteband-ajax-controller.php', 
			type : 'post',
			data: "removefavoriteband=1&id="+id,
			success : function(resp)
			{
				parent.$.fancybox.close();
				show_ffavoriteband(bid);
			}
		});	
	}
	
	function show_ffavoriteartist(id)
	{
		jQuery('#fan_gallery').removeClass('active_small_tab');
		jQuery('#fan_fband').removeClass('active_small_tab');
		jQuery('#fan_fartist').removeClass('active_small_tab');
		jQuery('#fan_events').removeClass('active_small_tab');
		jQuery('#fan_merchandise').removeClass('active_small_tab');
		jQuery('#fan_wishlist').removeClass('active_small_tab');
		jQuery('#fan_donation').removeClass('active_small_tab');
		
		jQuery('#fan_fartist').addClass('active_small_tab');
		
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/favoriteartist-ajax-controller.php', 
			type : 'post',
			data: "showfavoriteartist=1&id="+id+"&pfunction=show_paging_19",
			success : function(resp)
			{
				//alert(resp);
				jQuery("#bottom_container").html(resp);
			}
		});	
	}
	function show_paging_19(pageno,perpage,id,pfunction)
	{
		loader_show();
		var combototal=jQuery("#combo_totalpage").val();
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/favoriteartist-ajax-controller.php', 
			type : 'post',
			 data: "showfavoriteartist=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
			 success : function(resp){
				//jQuery.scrollTo(130,500);
				jQuery("#bottom_container").html(resp);
				loader_hide();
			}
		});	
	}
	
	function remove_ffavoriteartist(id,bid)
	{
		$("#action_line").html('Are you sure want to delete selected band member?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removeffavoriteartist('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#various_33").fancybox().trigger('click');
	}
	function removeffavoriteartist(id,bid)
	{
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/favoriteartist-ajax-controller.php', 
			type : 'post',
			data: "removefavoriteartist=1&id="+id,
			success : function(resp)
			{
				parent.$.fancybox.close();
				show_ffavoriteartist(bid);
			}
		});	
	}
	
	function show_fdonation(id)
	{
		jQuery('#fan_gallery').removeClass('active_small_tab');
		jQuery('#fan_fband').removeClass('active_small_tab');
		jQuery('#fan_fartist').removeClass('active_small_tab');
		jQuery('#fan_events').removeClass('active_small_tab');
		jQuery('#fan_merchandise').removeClass('active_small_tab');
		jQuery('#fan_wishlist').removeClass('active_small_tab');
		jQuery('#fan_donation').removeClass('active_small_tab');
		
		jQuery('#fan_donation').addClass('active_small_tab');
		
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/mydonation-ajax-controller.php', 
			type : 'post',
			data: "showdonation=1&id="+id+"&pfunction=show_paging_23",
			success : function(resp)
			{
				//alert(resp);
				jQuery("#bottom_container").html(resp);
			}
		});	
	}
	function show_paging_23(pageno,perpage,id,pfunction)
	{
		loader_show();
		var combototal=jQuery("#combo_totalpage").val();
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/mydonation-ajax-controller.php', 
			type : 'post',
			 data: "showdonation=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
			 success : function(resp){
				//jQuery.scrollTo(130,500);
				jQuery("#bottom_container").html(resp);
				loader_hide();
			}
		});	
	}
	
	function remove_fdonation(id,fid)
	{
		$("#action_line").html('Are you sure want to delete selected donation?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removefdonation('"+id+"','"+fid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#various_33").fancybox().trigger('click');
	}
	function removefdonation(id,fid)
	{
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/mydonation-ajax-controller.php', 
			type : 'post',
			data: "removedonation=1&id="+id+"&fid="+fid,
			success : function(resp)
			{
				parent.$.fancybox.close();
				show_fdonation(fid);
			}
		});	
	}
	
	function show_fwishlist(id)
	{
		jQuery('#fan_gallery').removeClass('active_small_tab');
		jQuery('#fan_fband').removeClass('active_small_tab');
		jQuery('#fan_fartist').removeClass('active_small_tab');
		jQuery('#fan_events').removeClass('active_small_tab');
		jQuery('#fan_merchandise').removeClass('active_small_tab');
		jQuery('#fan_wishlist').removeClass('active_small_tab');
		jQuery('#fan_donation').removeClass('active_small_tab');
		
		jQuery('#fan_wishlist').addClass('active_small_tab');
		
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/wishlist-ajax-controller.php', 
			type : 'post',
			data: "showwishlist=1&id="+id+"&pfunction=show_paging_22",
			success : function(resp)
			{
				jQuery("#bottom_container").html(resp);
			}
		});	
	}
	function show_paging_22(pageno,perpage,id,pfunction)
	{
		loader_show();
		var combototal=jQuery("#combo_totalpage").val();
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/wishlist-ajax-controller.php', 
			type : 'post',
			 data: "showwishlist=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
			 success : function(resp){
				//jQuery.scrollTo(130,500);
				jQuery("#bottom_container").html(resp);
				loader_hide();
			}
		});	
	}
	
	function remove_fwishlist(id,fid)
	{
		$("#action_line").html('Are you sure want to delete selected product?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removefwishlist('"+id+"','"+fid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#various_33").fancybox().trigger('click');
	}
	function removefwishlist(id,fid)
	{
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/wishlist-ajax-controller.php', 
			type : 'post',
			data: "removewishlist=1&id="+id+"&fid="+fid,
			success : function(resp)
			{
				parent.$.fancybox.close();
				show_fwishlist(fid);
			}
		});	
	}
	
	function show_fevents(id)
	{
		jQuery('#fan_gallery').removeClass('active_small_tab');
		jQuery('#fan_fband').removeClass('active_small_tab');
		jQuery('#fan_fartist').removeClass('active_small_tab');
		jQuery('#fan_events').removeClass('active_small_tab');
		jQuery('#fan_merchandise').removeClass('active_small_tab');
		jQuery('#fan_wishlist').removeClass('active_small_tab');
		jQuery('#fan_donation').removeClass('active_small_tab');
		
		jQuery('#fan_events').addClass('active_small_tab');
		
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/eventtab-ajax-controller.php', 
			type : 'post',
			data: "showevents=1&id="+id+"&pfunction=show_paging_20",
			success : function(resp)
			{
				//alert(resp);
				jQuery("#bottom_container").html(resp);
			}
		});	
	}
	function show_paging_20(pageno,perpage,id,pfunction)
	{
		loader_show();
		var combototal=jQuery("#combo_totalpage").val();
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/eventtab-ajax-controller.php', 
			type : 'post',
			 data: "showevents=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
			 success : function(resp){
				//jQuery.scrollTo(130,500);
				jQuery("#bottom_container").html(resp);
				loader_hide();
			}
		});	
	}
	function remove_fevents(id,fid)
	{
		$("#action_line").html('Are you sure want to delete selected event?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removefevents('"+id+"','"+fid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#various_33").fancybox().trigger('click');
	}
	function removefevents(id,fid)
	{
		jQuery.ajax({
			url : site_url+'controllers/ajax_controller/eventtab-ajax-controller.php', 
			type : 'post',
			data: "removeevents=1&id="+id+"&fid="+fid,
			success : function(resp)
			{
				parent.$.fancybox.close();
				show_fevents(fid);
			}
		});	
	}
	
// FOR EVENT

function view_eventdetail(id,mainid)
{
	var backvalue=$("#divforback").html();
	//alert(backvalue);
	//alert(id);
	$.ajax({
		type: "POST",
		url: site_url+'controllers/ajax_controller/showgallery-ajax-controller.php',
		data: 'vieweventdetails=1&fid='+id,
		cache: false,
		success: function(data){
			$("#band").html(data);
			if(backvalue==""){
				$("#divforback").html("<input class=\"button_bg\" type=\"button\" value=\"Back\" name=\"btn_back\" onclick=\"viewdetails_new('"+mainid+"','show_event')\">");
			}else{
				var changestr=backvalue.replace("viewdetails_new","viewdetails_new2");
				var changestr=changestr.replace("')","','"+id+"','view_bandmemberdetail','show_mevent')");
				//alert(changestr);
				$("#divforback").html(changestr);
			}
		}
	});
}
function upload_epopup(id){
	$.ajax( {
		url : site_url+'controllers/ajax_controller/upload-image-ajax-controller.php',
		type : 'post',
		data: 'uploadpopup=1&path=event_image/event_gallery&forwhat=event&eventid='+id,
		success : function(result)
		{
			$("#view_detail").html(result);
			$("#common").html('Upload Images');
			$("#various_1").fancybox().trigger('click');
		}
	});
}

function show_egallery(id)
{
	loader_show();
	jQuery('#event_gallery').removeClass('active_small_tab');
	jQuery('#event_video').removeClass('active_small_tab');
	jQuery('#event_poll').removeClass('active_small_tab');
	jQuery('#event_ticket').removeClass('active_small_tab');
	jQuery('#event_voucher').removeClass('active_small_tab');
	jQuery('#event_rating').removeClass('active_small_tab');
	jQuery('#event_review').removeClass('active_small_tab');
	jQuery('#event_mydetail').removeClass('active_small_tab');
	
	jQuery('#event_gallery').addClass('active_small_tab');
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		data: "showgalleryevent=1&id="+id+"&pfunction=show_paging_24",
		success : function(resp)
		{
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_paging_24(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		 data: "showgalleryevent=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function removeeventimageask(id,bid)
{
		$("#action_line").html('Are you sure to delete selected image?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removeeventimage('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#inline_33").attr('style', 'width:350px;');
		$("#various_33").fancybox().trigger('click');
}
function removeeventimage(id,fid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		data: "removeeventimg=1&id="+id,
		success : function(resp)
		{
			//alert("OK");
			parent.$.fancybox.close();
			show_gallery(fid);
		}
	});	
}

function viewaddvideoevent(id){
	$.ajax({
		url : site_url+'controllers/ajax_controller/videotab-ajax-controller.php', 
		type : 'post',
		data: 'viewaddvideoeventform=1&id='+id,				
		success : function(result)
		{
			$("#view_detail").html(result);
			$("#common").html('Add Video');
			$("#various_1").fancybox().trigger('click');
		}
	});
}
function removeeventvideoask(id,bid)
{
	$("#action_line").html('Are you sure want to delete selected video?');
	$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removeeventvideo('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
	$("#various_33").fancybox().trigger('click');
}
function removeeventvideo(id,bid)
{
	//alert(id);
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/videotab-ajax-controller.php', 
		type : 'post',
		data: "removeeventvideo=1&id="+id,
		success : function(resp)
		{
			//alert("OK");
			parent.$.fancybox.close();
			show_evideo(bid);
		}
	});	
} 
function show_evideo(id)
{
	loader_show();
	jQuery('#event_gallery').removeClass('active_small_tab');
	jQuery('#event_video').removeClass('active_small_tab');
	/*jQuery('#event_follower').removeClass('active_small_tab');*/
	jQuery('#event_poll').removeClass('active_small_tab');
	jQuery('#event_ticket').removeClass('active_small_tab');
	jQuery('#event_voucher').removeClass('active_small_tab');
	jQuery('#event_rating').removeClass('active_small_tab');
	jQuery('#event_review').removeClass('active_small_tab');
	jQuery('#event_mydetail').removeClass('active_small_tab');
	
	jQuery('#event_video').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/videotab-ajax-controller.php', 
		type : 'post',
		data: "showvideoevent=1&id="+id+"&pfunction=show_paging_25",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_paging_25(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/videotab-ajax-controller.php', 
		type : 'post',
		 data: "showvideoevent=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}

function removeeventpollask(id,bid)
{
	$("#action_line").html('Are you sure want to delete selected poll?');
	$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removeeventpoll('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
	$("#various_33").fancybox().trigger('click');
}
function removeeventpoll(id,bid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/followertab-ajax-controller.php', 
		type : 'post',
		data: "removeeventpoll=1&id="+id,
		success : function(resp)
		{
			parent.$.fancybox.close();
			show_epoll(bid);
		}
	});	
}
function show_eventcoupon(id)
{
	loader_show();
	jQuery('#event_gallery').removeClass('active_small_tab');
	jQuery('#event_video').removeClass('active_small_tab');
	jQuery('#event_poll').removeClass('active_small_tab');
	jQuery('#event_ticket').removeClass('active_small_tab');
	jQuery('#event_voucher').removeClass('active_small_tab');
	jQuery('#event_rating').removeClass('active_small_tab');
	jQuery('#event_review').removeClass('active_small_tab');
	jQuery('#event_mydetail').removeClass('active_small_tab');
	
	jQuery('#event_voucher').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/coupontab-ajax-controller.php', 
		type : 'post',
		data: "show_eventcoupon=1&id="+id+"&pfunction=show_paging_29",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_paging_29(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/coupontab-ajax-controller.php', 
		type : 'post',
		 data: "show_eventcoupon=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_eventticket(id)
{
	loader_show();
	jQuery('#event_gallery').removeClass('active_small_tab');
	jQuery('#event_video').removeClass('active_small_tab');
	jQuery('#event_poll').removeClass('active_small_tab');
	jQuery('#event_ticket').removeClass('active_small_tab');
	jQuery('#event_voucher').removeClass('active_small_tab');
	jQuery('#event_rating').removeClass('active_small_tab');
	jQuery('#event_review').removeClass('active_small_tab');
	jQuery('#event_mydetail').removeClass('active_small_tab');
	
	jQuery('#event_ticket').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/tickettab-ajax-controller.php', 
		type : 'post',
		data: "show_eventticket=1&id="+id+"&pfunction=show_paging_27",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_paging_27(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/tickettab-ajax-controller.php', 
		type : 'post',
		 data: "show_eventticket=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function view_ticketdetail(id)
{
	loader_show();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/tickettab-ajax-controller.php', 
		type : 'post',
		data: "view_ticketdetail=1&id="+id+"&pfunction=show_paging_28",
		success : function(resp)
		{
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_paging_28(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/tickettab-ajax-controller.php', 
		type : 'post',
		 data: "view_ticketdetail=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function view_ticket(id)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/tickettab-ajax-controller.php', 
		type : 'post',
		data: "view_ticket=1&id="+id,
		success : function(resp)
		{
			jQuery("#div_voucherprint").html(resp);
			jQuery("#various_32").fancybox().trigger('click');
		}
	});	
}
function PrintContent(Id) 
{
	var DocumentContainer = document.getElementById(Id);
	var WindowObject = window.open('', 'PrintWindow', 'width=700,height=400,top=50,left=50,toolbars=no,scrollbars=yes,status=no,resizable=yes');
	WindowObject.document.writeln(DocumentContainer.innerHTML);
	WindowObject.document.close();
	WindowObject.focus();
	WindowObject.print();
	WindowObject.close();
}
function view_eventcoupon(id){
	$.ajax( {
		url : site_url+'controllers/ajax_controller/coupon-ajax-controller.php',
		type : 'post',
		data: 'view=1&id='+id,
		success : function(result)
		{
			$("#view_detail").html(result);
			$("#common").html('View Detail');
			$("#various_1").fancybox().trigger('click');
		}
	});
}
function show_epoll(id)
{
	loader_show();
	jQuery('#event_gallery').removeClass('active_small_tab');
	jQuery('#event_video').removeClass('active_small_tab');
	/*jQuery('#event_follower').removeClass('active_small_tab');*/
	jQuery('#event_poll').removeClass('active_small_tab');
	jQuery('#event_ticket').removeClass('active_small_tab');
	jQuery('#event_voucher').removeClass('active_small_tab');
	jQuery('#event_rating').removeClass('active_small_tab');
	jQuery('#event_review').removeClass('active_small_tab');
	jQuery('#event_mydetail').removeClass('active_small_tab');
	
	jQuery('#event_poll').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/polltab-ajax-controller.php', 
		type : 'post',
		data: "showpollevent=1&id="+id+"&pfunction=show_paging_26",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_paging_26(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/polltab-ajax-controller.php', 
		type : 'post',
		 data: "showpollevent=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}

function show_erating(id)
{
	loader_show();
	jQuery('#event_gallery').removeClass('active_small_tab');
	jQuery('#event_video').removeClass('active_small_tab');
	/*jQuery('#event_follower').removeClass('active_small_tab');*/
	jQuery('#event_poll').removeClass('active_small_tab');
	jQuery('#event_ticket').removeClass('active_small_tab');
	jQuery('#event_voucher').removeClass('active_small_tab');
	jQuery('#event_rating').removeClass('active_small_tab');
	jQuery('#event_review').removeClass('active_small_tab');
	jQuery('#event_mydetail').removeClass('active_small_tab');
	
	jQuery('#event_rating').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/ratingtab-ajax-controller.php', 
		type : 'post',
		data: "showratingevent=1&id="+id+"&pfunction=show_paging_30",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_paging_30(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/ratingtab-ajax-controller.php', 
		type : 'post',
		 data: "showratingevent=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function removereventreviewask(id,bid)
{
	$("#action_line").html('Are you sure want to delete selected review?');
	$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removeeventreview('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
	$("#various_33").fancybox().trigger('click');
}
function removeeventreview(id,bid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/reviewtab-ajax-controller.php', 
		type : 'post',
		data: "removeeventreviewremove=1&id="+id,
		success : function(resp)
		{
			parent.$.fancybox.close();
			show_ereview(bid);
		}
	});	
}
function vieweventreviewpopup(id,bid){
	$.ajax( {
		url : site_url+'controllers/ajax_controller/reviewtab-ajax-controller.php',
		type : 'post',
		data: 'vieweventreviewdetail=1&id='+id+'&bid='+bid,
		success : function(result)
		{
			$("#view_detail").html(result);
			$("#common").html('View Detail');
			$("#various_1").fancybox().trigger('click');
		}
	});
}
function show_ereview(id)
{
	loader_show();
	jQuery('#event_gallery').removeClass('active_small_tab');
	jQuery('#event_video').removeClass('active_small_tab');
	/*jQuery('#event_follower').removeClass('active_small_tab');*/
	jQuery('#event_poll').removeClass('active_small_tab');
	jQuery('#event_ticket').removeClass('active_small_tab');
	jQuery('#event_voucher').removeClass('active_small_tab');
	jQuery('#event_rating').removeClass('active_small_tab');
	jQuery('#event_review').removeClass('active_small_tab');
	jQuery('#event_mydetail').removeClass('active_small_tab');
	
	jQuery('#event_review').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/reviewtab-ajax-controller.php', 
		type : 'post',
		data: "showeventreview=1&id="+id+"&pfunction=show_paging_31",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_paging_31(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/reviewtab-ajax-controller.php', 
		type : 'post',
		 data: "showeventreview=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}



function removeeventfollowerask(id,bid)
{
	$("#action_line").html('Are you sure want to delete selected follower?');
	$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removeeventfollower('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
	$("#various_33").fancybox().trigger('click');
}
function removeeventfollower(id,bid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/followertab-ajax-controller.php', 
		type : 'post',
		data: "removeeventfollower=1&id="+id,
		success : function(resp)
		{
			parent.$.fancybox.close();
			show_efollower(bid);
		}
	});	
}
function show_efollower(id)
{
	loader_show();
	jQuery('#event_gallery').removeClass('active_small_tab');
	jQuery('#event_video').removeClass('active_small_tab');
	/*jQuery('#event_follower').removeClass('active_small_tab');*/
	jQuery('#event_poll').removeClass('active_small_tab');
	jQuery('#event_ticket').removeClass('active_small_tab');
	jQuery('#event_voucher').removeClass('active_small_tab');
	jQuery('#event_rating').removeClass('active_small_tab');
	jQuery('#event_review').removeClass('active_small_tab');
	jQuery('#event_mydetail').removeClass('active_small_tab');
	
	jQuery('#event_follower').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/followertab-ajax-controller.php', 
		type : 'post',
		data: "showfollowerevent=1&id="+id,
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function addeventvideoformsubmit()
{
	//var url=/http:\/\/(?:www\.)?youtube.*watch\?v=([a-zA-Z0-9\-_]+)/;
	
	var url=/http:\/\/(?:www\.|player\.)?(vimeo|youtube)\.com\/(?:embed\/|video\/)?(.*?)(?:\z|$|\?)/;

	//alert('test');
	if($("#txt_video_title_pop").val() == ''){
	  $("#error-innertxt_video_title_pop").show().fadeOut(5000);
	  $("#error-innertxt_video_title_pop").html('This field is required');
	  $("#txt_video_title_pop").focus();
	  return false;
	}else if($("#txt_video_url_pop").val() == ''){
	  $("#error-innertxt_video_url_pop").show().fadeOut(5000);
	  $("#error-innertxt_video_url_pop").html('This field is required');
	  $("#txt_video_url_pop").focus();
	  return false;
	}else if(url.test($("#txt_video_url_pop").val()) == false){
	  $("#error-innertxt_video_url_pop").show().fadeOut(5000);
	  $("#error-innertxt_video_url_pop").html('Invalid video url');
	  $("#txt_video_url_pop").focus();
	  return false;
	}
	else
	{
		var options = {
			beforeSubmit:  showRequest_updateab,
			success:       showResponse_updateab,
			url:       site_url+'controllers/ajax_controller/videotab-ajax-controller.php', 
			type: "POST"
		};
		$('#form_eventaddform').submit(function() {
			$(this).ajaxSubmit(options);
			return false;
		});
	}
}
function showRequest_updateab(formData, jqForm, options) {
	return true;
} 
function showResponse_updateab(data, statusText)  
{
	
	if (statusText == 'success')
	{ 
		if(data == 1){
			$("#message-green").show().fadeOut(5000);
			$("#message-red").hide();
			document.getElementById('succ').innerHTML = 'Video added successfully.';
			show_evideo($("#hid_eventaddId").val());
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while adding video.';
		}
		parent.$.fancybox.close();
	}
}

function view_emydetail(id){
	loader_show();
	jQuery('#event_gallery').removeClass('active_small_tab');
	jQuery('#event_video').removeClass('active_small_tab');
	jQuery('#event_poll').removeClass('active_small_tab');
	jQuery('#event_ticket').removeClass('active_small_tab');
	jQuery('#event_voucher').removeClass('active_small_tab');
	jQuery('#event_rating').removeClass('active_small_tab');
	jQuery('#event_review').removeClass('active_small_tab');
	jQuery('#event_mydetail').removeClass('active_small_tab');
	
	jQuery('#event_mydetail').addClass('active_small_tab');
	
	$.ajax( {
		url : site_url+'controllers/ajax_controller/event-ajax-controller.php',
		type : 'post',
		data: 'view=1&id='+id,
		success : function(result)
		{
			jQuery("#bottom_container").html(result);
			loader_hide();
		}
	});
}

//songs

function show_songartist(id)
{
	loader_show();
	jQuery('#song_detailtab').removeClass('active_small_tab');
	jQuery('#song_artisttab').removeClass('active_small_tab');
	
	jQuery('#song_artisttab').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/artisttab-ajax-controller.php', 
		type : 'post',
		data: "showsongartist=1&id="+id+"&pfunction=show_paging_32",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}

function show_paging_32(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/artisttab-ajax-controller.php', 
		type : 'post',
		 data: "showsongartist=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}

// FOR MERCHANDISE PRODUCT

function view_merchandisedetail(id,mainid)
{
	$.ajax({
		type: "POST",
		url: site_url+'controllers/ajax_controller/showgallery-ajax-controller.php',
		data: 'viewmerchandisedetails=1&fid='+id,
		cache: false,
		success: function(data){
			$("#band").html(data);
			$("#divforback").html("<input class=\"button_bg\" type=\"button\" value=\"Back\" name=\"btn_back\" onclick=\"viewdetails_new('"+mainid+"','show_merchandise')\">");
		}
	});
}
function upload_mppopup(id){
	$.ajax( {
		url : site_url+'controllers/ajax_controller/upload-image-ajax-controller.php',
		type : 'post',
		data: 'uploadpopup=1&path=mproduct_image/mproduct_gallery&forwhat=mproduct&mproductid='+id,
		success : function(result)
		{
			$("#view_detail").html(result);
			$("#common").html('Upload Images');
			$("#various_1").fancybox().trigger('click');
		}
	});
}
function removermproductimageask(id,bid)
{
		$("#action_line").html('Are you sure to delete selected image?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removermproductimage('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#inline_33").attr('style', 'width:350px;');
		$("#various_33").fancybox().trigger('click');
}
function removermproductimage(id,bid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		data: "removemproductimg=1&id="+id,
		success : function(resp)
		{
			parent.$.fancybox.close();
			show_mpgallery(bid);
		}
	});	
}
function show_mpgallery(id)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		data: "showgallerymproduct=1&id="+id+"&pfunction=show_paging_33",
		success : function(resp)
		{
			jQuery("#bottom_container").html(resp);
		}
	});	
}
function show_paging_33(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		 data: "showgallerymproduct=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_mproductcoupon(id){
	loader_show();
	jQuery('#member_mpgallery').removeClass('active_small_tab');
	jQuery('#member_mpcoupon').removeClass('active_small_tab');
	jQuery('#member_mpmydetail').removeClass('active_small_tab');
	
	jQuery('#member_mpcoupon').addClass('active_small_tab');
	
	$.ajax( {
		url : site_url+'controllers/ajax_controller/coupontab-ajax-controller.php',
		type : 'post',
		data: 'show_mproductcoupon=1&id='+id+"&pfunction=show_paging_34",
		success : function(result)
		{
			jQuery("#bottom_container").html(result);
			loader_hide();
		}
	});
}
function show_paging_34(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/coupontab-ajax-controller.php', 
		type : 'post',
		 data: "show_mproductcoupon=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function add_bandmember_html(bandid){ 
	loader_show();
	$("#message-red").hide();	
	$("#message-green").hide();      
	$.ajax( {
		url : site_url+'controllers/ajax_controller/bandmember-ajax-controller.php', 
		type : 'post',
		data: 'edit=1&bandid='+bandid,				
		success : function( resp ) {
			$("#bottom_container").html(resp);
			loader_hide();
		}
	});
}


function add_bandmember(){
	var alpha = /^[a-zA-Z]+$/;
	var alphanum = /^[a-zA-Z0-9]+$/;
	var emailchk = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	var mobnum=/^[0-9]{10,12}$/;
	var phonum=/^[0-9]{10,14}$/;
	var decnum=/^[0-9.]+$/;
	var domain=/[^,\s]+\.{1,}[^,\s]{2,}/;
	var url=/^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/;
	
	var csv = $("#txt_addimage").val();
	var ext = csv.split(".");
	var n = ext.length-1;
	var ext = ext[n];
	
	if($("#txt_addfirst_name").val() == ''){
	  $("#error-innertxt_addfirst_name").show().fadeOut(5000);
	  $("#error-innertxt_addfirst_name").html('This field is required');
	  $("#txt_addfirst_name").focus();
	  return false;
	}else if(alpha.test($("#txt_addfirst_name").val()) == false){
	   $("#error-innertxt_addfirst_name").show().fadeOut(5000);
	  $("#error-innertxt_addfirst_name").html('Enter valid first name');
	  $("#txt_addfirst_name").focus();
	  return false;
	}else if($("#txt_addlast_name").val() == ''){
	  $("#error-innertxt_addlast_name").show().fadeOut(5000);
	  $("#error-innertxt_addlast_name").html('This field is required');
	  $("#txt_addlast_name").focus();
	  return false;
	}else if(alpha.test($("#txt_addlast_name").val()) == false){
	   $("#error-innertxt_addlast_name").show().fadeOut(5000);
	  $("#error-innertxt_addlast_name").html('Enter valid last name');
	  $("#txt_addlast_name").focus();
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
	}else if($("#txt_addmobileno").val() == ''){
	  $("#error-innertxt_addmobileno").show().fadeOut(5000);
	  $("#error-innertxt_addmobileno").html('This field is required');
	  $("#txt_addmobileno").focus();
	  return false;
	}else if(mobnum.test($("#txt_addmobileno").val()) == false){
	  $("#error-innertxt_addmobileno").show().fadeOut(5000);
	  $("#error-innertxt_addmobileno").html('Enter correct number');
	  $("#txt_addmobileno").focus();
	  return false;
	}else if($("#txt_addbirthdate").val() == ''){
	  $("#error-innertxt_addbirthdate").show().fadeOut(5000);
	  $("#error-innertxt_addbirthdate").html('This field is required');
	  $("#txt_addbirthdate").focus();
	  return false;
	}else if(ext != "" && ext != "jpeg" && ext != "jpg" && ext != "bnp" && ext != "gif" && ext != "png" ){
	$("#error-innertxt_addimage").show().fadeOut(5000);
	  $("#error-innertxt_addimage").html('Invalid file type, plz upload image file.');
	  $("#txt_addimage").focus();
	return false;
	}else
	{
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse_bm,
			url:       site_url+'controllers/ajax_controller/bandmember-ajax-controller.php', 
			type: "POST"
		};
		$('#form_bandmemberadd').submit(function() {
			$(this).ajaxSubmit(options);
			return false;
		});
	}
}
function showResponse_bm(data, statusText)  {
	if(statusText == 'success')
	{
		if(data == 0)
		{
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Email id already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);		   
			document.getElementById('succ').innerHTML = 'Band member added successfully.';
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while adding band member.';
		}else{
			var test=data.split(',');
			show_member($.trim(test[1]));
		}
		$('#form_bandmemberadd').unbind('submit').bind('submit',function() {
		});
	}
}

function add_album_html(bandid){ 
	loader_show();
	$("#message-red").hide();	
	$("#message-green").hide();      
	$.ajax( {
		url : site_url+'controllers/ajax_controller/album-ajax-controller.php', 
		type : 'post',
		data: 'edit=1&bandid='+bandid,				
		success : function( resp ) {
			$("#bottom_container").html(resp);
			loader_hide();
		}
	});
}

function albumadddata(){
	var alpha = /^[a-zA-Z]+$/;
	var alphanum = /^[a-zA-Z0-9]+$/;
	var emailchk = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	var mobnum=/^[0-9]{10,12}$/;
	var phonum=/^[0-9]{10,14}$/;
	//var num=/^[0-9]$/;
	var decnum=/^[0-9.]+$/;
	var domain=/[^,\s]+\.{1,}[^,\s]{2,}/;
	var url=/^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/;
	if($("#txt_addtitle").val() == ''){
		$("#error-innertxt_addtitle").show().fadeOut(5000);
		$("#error-innertxt_addtitle").html('This field is required');
		$("#txt_addtitle").focus();
		return false;
	}else{
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse_addalbum,
			url:       site_url+'controllers/ajax_controller/album-ajax-controller.php', 
			type: "POST"
		};
		$('#form_albumadd').submit(function() {
			$(this).ajaxSubmit(options);
			return false;
		});
	}
}
function showResponse_addalbum(data, statusText)  {
	if(statusText == 'success')
	{
		if(data == 0)
		{
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Album already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);		   
			document.getElementById('succ').innerHTML = 'Album added successfully.';
			newdata();				 
		}else{
			/*$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);		   
			document.getElementById('succ').innerHTML = 'Album added successfully.';*/
			var test=data.split(',');
			show_album($.trim(test[1]));	
		}
		$('#form_albumadd').unbind('submit').bind('submit',function() {
		});
	}
}

function add_event_html(bandid){ 
	loader_show();
	$("#message-red").hide();	
	$("#message-green").hide();      
	$.ajax( {
		url : site_url+'controllers/ajax_controller/event-ajax-controller.php', 
		type : 'post',
		data: 'edit=1&bandid='+bandid,				
		success : function( resp ) {
			$("#bottom_container").html(resp);
			loader_hide();
		}
	});
}


function addeventdata(){
	var alpha = /^[a-zA-Z]+$/;
	var email = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	var num=/^[0-9]+$/;
	var decnum=/^[0-9.]+$/;
	var abcd=$("#txt_startdate").val();
	//alert(abcd);
	var MD_Y=abcd.substring(0,4);
	
	var MD_M=abcd.substring(5,7);
	
	var MD_D=abcd.substring(8,10);
	MD_M=MD_M-1; // Jan-Dec=00-11
	DObja=new Date(MD_Y, MD_M, MD_D); 
	
	var currentTime = new Date();
	currentTime.setDate(currentTime.getDate()-1);
	
	var abcde=$("#txt_enddate").val();

	var MD_Y=abcde.substring(0,4);
	var MD_M=abcde.substring(5,7);
	var MD_D=abcde.substring(8,10);
	MD_M=MD_M-1; // Jan-Dec=00-11
	DObjb=new Date(MD_Y, MD_M, MD_D); 
	
	
	var csv = $("#txt_logo").val();
	//alert(csv);
	var ext = csv.split(".");
	//alert(ext);
	var n = ext.length-1;
	//alert(n);
	var ext = ext[n];
	
	if($("#txt_eventcategory").val() == ''){
	  $("#error-innertxt_eventcategory").show().fadeOut(5000);
	  $("#error-innertxt_eventcategory").html('This field is required');
	  $("#txt_eventcategory").focus();
	  return false;
	}else if($("#txt_event").val() == ''){
	  $("#error-innertxt_event").show().fadeOut(5000);
	  $("#error-innertxt_event").html('This field is required');
	  $("#txt_event").focus();
	  return false;
	}else if($("#txt_startdate").val() == ''){
	  $("#error-innertxt_startdate").show().fadeOut(5000);
	  $("#error-innertxt_startdate").html('This field is required');
	  $("#txt_startdate").focus();
	  return false;
	}else if($("#txt_enddate").val() == ''){
	  $("#error-innertxt_enddate").show().fadeOut(5000);
	  $("#error-innertxt_enddate").html('This field is required');
	  $("#txt_enddate").focus();
	  return false;
	}else if(DObja<currentTime){
		$("#error-innertxt_startdate").show().fadeOut(5000);
		$("#error-innertxt_startdate").html('Invalid Date');
		$("#txt_startdate").focus();
		return false;	
	}else if(DObjb<currentTime){
		$("#error-innertxt_enddate").show().fadeOut(5000);
		$("#error-innertxt_enddate").html('Invalid Date');
		$("#txt_enddate").focus();
		return false;	
	}else if(DObjb<DObja){
		$("#error-innertxt_enddate").show().fadeOut(5000);
		$("#error-innertxt_enddate").html('Invalid Date');
		$("#txt_enddate").focus();
		return false;	
	}else if($("#txt_location").val() == ''){
	  $("#error-innertxt_location").show().fadeOut(5000);
	  $("#error-innertxt_location").html('This field is required');
	  $("#txt_location").focus();
	  return false;
	}else if($("#txt_aviticket").val() == ''){
	  $("#error-innertxt_aviticket").show().fadeOut(5000);
	  $("#error-innertxt_aviticket").html('This field is required');
	  $("#txt_aviticket").focus();
	  return false;
	}else if(num.test($("#txt_aviticket").val()) == false){
	  $("#error-innertxt_aviticket").show().fadeOut(5000);
	  $("#error-innertxt_aviticket").html('Enter valid available ticket');
	  $("#txt_aviticket").focus();
	  return false;
	}else if($("#txt_perprice").val() == ''){
	  $("#error-innertxt_perprice").show().fadeOut(5000);
	  $("#error-innertxt_perprice").html('This field is required');
	  $("#txt_perprice").focus();
	  return false;
	}else if(decnum.test($("#txt_perprice").val()) == false){
	  $("#error-innertxt_perprice").show().fadeOut(5000);
	  $("#error-innertxt_perprice").html('Enter valid price per ticket');
	  $("#txt_perprice").focus();
	  return false;
	}else if($("#txt_baselimit").val() == ''){
	  $("#error-innertxt_baselimit").show().fadeOut(5000);
	  $("#error-innertxt_baselimit").html('This field is required');
	  $("#txt_baselimit").focus();
	  return false;
	}else if(num.test($("#txt_baselimit").val()) == false){
	  $("#error-innertxt_baselimit").show().fadeOut(5000);
	  $("#error-innertxt_baselimit").html('Enter valid ticket per person');
	  $("#txt_baselimit").focus();
	  return false;
	}else if(ext != "" && ext != "jpeg" && ext != "jpg" && ext != "bnp" && ext != "gif" && ext != "png" ){
	$("#error-innertxt_logo").show().fadeOut(5000);
	  $("#error-innertxt_logo").html('Invalid file type, plz upload image file.');
	  $("#txt_logo").focus();
	return false;
	}else
	{
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse_addevent,
			url:       site_url+'controllers/ajax_controller/event-ajax-controller.php', 
			type: "POST"
		};
		$('#form_useradd').submit(function() {
			$(this).ajaxSubmit(options);
			return false;
		});
	}
}
function showResponse_addevent(data, statusText)  {
	if(statusText == 'success')
	{
		if(data == 0)
		{
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Event already exist. Please try another.';
		}else if(data == 1){
			tinyMCE.execCommand('mceRemoveControl', false, 'txt_description1');
			$("#message-green").show().fadeOut(5000);
			$("#message-red").hide();
			document.getElementById('succ').innerHTML = 'Event added successfully.';
			newdata();
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while adding event.';
		}else{
			var test=data.split(',');
			show_event($.trim(test[1]));
		}
	}
	$('#form_useradd').unbind('submit').bind('submit',function() {
	});
}

function add_mproduct_html(bandid){ 
	loader_show();
	$("#message-red").hide();	
	$("#message-green").hide();      
	$.ajax( {
		url : site_url+'controllers/ajax_controller/merchandiseproduct-ajax-controller.php', 
		type : 'post',
		data: 'edit=1&bandid='+bandid,				
		success : function( resp ) {
			$("#bottom_container").html(resp);
			loader_hide();
		}
	});
}
function addmerchandisedata(){
	var alpha = /^[a-zA-Z]+$/;
	var alphanum = /^[a-zA-Z0-9]+$/;
	var emailchk = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-Z0-9]{2,4}$/;
	var mobnum=/^[0-9]{10,12}$/;
	var phonum=/^[0-9]{10,14}$/;
	var num=/^[0-9]+$/;
	var decnum=/^[0-9.]+$/;
	var domain=/[^,\s]+\.{1,}[^,\s]{2,}/;
	var url=/^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/;
	
	
	var csv = $("#txt_addimage").val();
	//alert(csv);
	var ext = csv.split(".");
	//alert(ext);
	var n = ext.length-1;
	//alert(n);
	var ext = ext[n];
	
	if($("#txt_addcategory_id").val() == ''){
	  $("#error-innertxt_addcategory_id").show().fadeOut(5000);
	  $("#error-innertxt_addcategory_id").html('This field is required');
	  $("#txt_addcategory_id").focus();
	  return false;
	}else if($("#txt_addproduct_title").val() == ''){
	  $("#error-innertxt_addproduct_title").show().fadeOut(5000);
	  $("#error-innertxt_addproduct_title").html('This field is required');
	  $("#txt_addproduct_title").focus();
	  return false;
	}else if($("#txt_addprice").val() == ''){
	  $("#error-innertxt_addprice").show().fadeOut(5000);
	  $("#error-innertxt_addprice").html('This field is required');
	  $("#txt_addprice").focus();
	  return false;
	}else if(decnum.test($("#txt_addprice").val()) == false){
		$("#error-innertxt_addprice").show().fadeOut(5000);
		$("#error-innertxt_addprice").html('Enter valid price');
		$("#txt_addprice").focus();
		return false;
	}else if($("#txt_addquantity").val() == ''){
	  $("#error-innertxt_addquantity").show().fadeOut(5000);
	  $("#error-innertxt_addquantity").html('This field is required');
	  $("#txt_addquantity").focus();
	  return false;
	}else if(num.test($("#txt_addquantity").val()) == false){
		$("#error-innertxt_addquantity").show().fadeOut(5000);
		$("#error-innertxt_addquantity").html('Enter valid quantity');
		$("#txt_addquantity").focus();
		return false;
	}else if(ext != "" && ext != "jpeg" && ext != "jpg" && ext != "bnp" && ext != "gif" && ext != "png" ){
		$("#error-innertxt_addimage").show().fadeOut(5000);
		  $("#error-innertxt_addimage").html('Invalid file type, plz upload image file.');
		  $("#txt_addimage").focus();
		return false;
		}else{
		var options = {
			beforeSubmit:  showRequest,
			success:       showResponse_addmproduct,
			url:       site_url+'controllers/ajax_controller/merchandiseproduct-ajax-controller.php', 
			type: "POST"
		};
		$('#form_merchandiseproductadd').submit(function() {
			$(this).ajaxSubmit(options);
			return false;
		});
	}
}
function showResponse_addmproduct(data, statusText)  {
	if(statusText == 'success')
	{
		if(data == 0)
		{
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Merchandise product already exist. Please try another.';
		}else if(data == 1){
			$("#message-red").hide();
			$("#message-green").show().fadeOut(5000);		   
			document.getElementById('succ').innerHTML = 'Merchandise product added successfully.';
			newdata();				 
		}else if(data == 2){
			$.scrollTo(0,500);
			$("#message-red").show().fadeOut(7000);
			$("#message-green").hide();
			document.getElementById('err').innerHTML = 'Some error occurred while adding merchandise product.';
		}else{
			var test=data.split(',');
			show_merchandise($.trim(test[1]));
		}
		$('#form_merchandiseproductadd').unbind('submit').bind('submit',function() {
		});
	}
}




function removeralbumpurchaseask(id,bid)
{
	$("#action_line").html('Are you sure want to delete selected follower?');
	$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removeralbumpurchase('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
	$("#various_33").fancybox().trigger('click');
}
function removeralbumpurchase(id,bid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/purchasetab-ajax-controller.php', 
		type : 'post',
		data: "removealbumpurchase=1&id="+id,
		success : function(resp)
		{
			parent.$.fancybox.close();
			show_alpurchase(bid);
		}
	});	
}
function show_alpurchase(id)
{
	loader_show();
	jQuery('#album_gallery').removeClass('active_small_tab');
	jQuery('#album_artist').removeClass('active_small_tab');
	jQuery('#album_purchase').removeClass('active_small_tab');
	jQuery('#album_review').removeClass('active_small_tab');
	jQuery('#album_rating').removeClass('active_small_tab');
	jQuery('#album_mydetail').removeClass('active_small_tab');
	
	jQuery('#album_purchase').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/purchasetab-ajax-controller.php', 
		type : 'post',
		data: "showalbumpurchase=1&id="+id+"&pfunction=show_paging_15",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_paging_15(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/purchasetab-ajax-controller.php', 
		type : 'post',
		 data: "showalbumpurchase=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function removealimageask(id,bid)
{
		$("#action_line").html('Are you sure to delete selected image?');
		$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removealimage('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
		$("#inline_33").attr('style', 'width:350px;');
		$("#various_33").fancybox().trigger('click');
}
function removealimage(id,fid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/showgallery-ajax-controller.php', 
		type : 'post',
		data: "removealbumimg=1&id="+id,
		success : function(resp)
		{
			//alert("OK");
			parent.$.fancybox.close();
			show_algallery(fid);
		}
	});	
}
function show_alrating(id)
{
	loader_show();
	jQuery('#album_gallery').removeClass('active_small_tab');
	jQuery('#album_purchase').removeClass('active_small_tab');
	jQuery('#album_review').removeClass('active_small_tab');
	jQuery('#album_rating').removeClass('active_small_tab');
	jQuery('#album_mydetail').removeClass('active_small_tab');
	
	jQuery('#album_rating').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/ratingtab-ajax-controller.php', 
		type : 'post',
		data: "showratingalbum=1&id="+id+"&pfunction=show_paging_16",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_paging_16(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/ratingtab-ajax-controller.php', 
		type : 'post',
		 data: "showratingalbum=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}

function removeralbumreviewask(id,bid)
{
	$("#action_line").html('Are you sure want to delete selected album?');
	$("#action_button").html("<input class=\"button_bg\" type=\"button\" value=\"YES\" name=\"btn_yes\" onclick=\"removeralbumreview('"+id+"','"+bid+"')\">&nbsp;<input class=\"button_bg\" type=\"button\" value=\"NO\" name=\"btn_no\" onclick=\"status_cancel()\">");
	$("#various_33").fancybox().trigger('click');
}
function removeralbumreview(id,bid)
{
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/reviewtab-ajax-controller.php', 
		type : 'post',
		data: "removealbumreviewremove=1&id="+id,
		success : function(resp)
		{
			parent.$.fancybox.close();
			show_alreview(bid);
		}
	});	
}
function viewalbumreviewpopup(id,bid){
	$.ajax( {
		url : site_url+'controllers/ajax_controller/reviewtab-ajax-controller.php',
		type : 'post',
		data: 'viewalbumreviewdetail=1&id='+id+'&bid='+bid,
		success : function(result)
		{
			$("#view_detail").html(result);
			$("#common").html('View Detail');
			$("#various_1").fancybox().trigger('click');
		}
	});
}
function show_alreview(id)
{
	loader_show();
	jQuery('#album_gallery').removeClass('active_small_tab');
	jQuery('#album_purchase').removeClass('active_small_tab');
	jQuery('#album_review').removeClass('active_small_tab');
	jQuery('#album_rating').removeClass('active_small_tab');
	jQuery('#album_mydetail').removeClass('active_small_tab');
	
	jQuery('#album_review').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/reviewtab-ajax-controller.php', 
		type : 'post',
		data: "showalbumreview=1&id="+id+"&pfunction=show_paging_17",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_paging_17(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/reviewtab-ajax-controller.php', 
		type : 'post',
		 data: "showalbumreview=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function view_amydetail(id){
	loader_show();
	jQuery('#album_gallery').removeClass('active_small_tab');
	jQuery('#album_purchase').removeClass('active_small_tab');
	jQuery('#album_review').removeClass('active_small_tab');
	jQuery('#album_rating').removeClass('active_small_tab');
	jQuery('#album_mydetail').removeClass('active_small_tab');
	
	jQuery('#album_mydetail').addClass('active_small_tab');
	
	$.ajax( {
		url : site_url+'controllers/ajax_controller/album-ajax-controller.php',
		type : 'post',
		data: 'view=1&id='+id,
		success : function(result)
		{
			jQuery("#bottom_container").html(result);
			loader_hide();
		}
	});
}


function view_songsdetail(id)
{
	var step1=$("#hid_step1").val();
	if(step1==''){
		var newvalue="view_songsdetail('"+id+"')";
	}else{
		var newvalue=step1+"|view_songsdetail('"+id+"')";
	}
	$("#hid_step1").val(newvalue);

	loader_show();
	$.ajax({
		type: "POST",
		url: site_url+'controllers/ajax_controller/artisttab-ajax-controller.php',
		data: 'viewsongdetails=1&fid='+id,
		cache: false,
		success: function(data){
			$("#band").html(data);
			$("#divforback").html("<input class=\"button_bg\" type=\"button\" value=\"Back\" name=\"btn_back\" onclick=\"common_back()\">");
			loader_hide();
		}
	});
}

function show_songartist(id)
{
	loader_show();
	jQuery('#song_detailtab').removeClass('active_small_tab');
	jQuery('#song_artisttab').removeClass('active_small_tab');
	
	jQuery('#song_artisttab').addClass('active_small_tab');
	
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/artisttab-ajax-controller.php', 
		type : 'post',
		data: "showsongartist=1&id="+id+"&pfunction=show_paging_32",
		success : function(resp)
		{
			//alert(resp);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function show_paging_32(pageno,perpage,id,pfunction)
{
	loader_show();
	var combototal=jQuery("#combo_totalpage").val();
	jQuery.ajax({
		url : site_url+'controllers/ajax_controller/artisttab-ajax-controller.php', 
		type : 'post',
		 data: "showsongartist=1&pageno="+pageno+"&perpage="+perpage+"&id="+id+"&pfunction="+pfunction+"&totalrecord="+combototal,
		 success : function(resp){
			//jQuery.scrollTo(130,500);
			jQuery("#bottom_container").html(resp);
			loader_hide();
		}
	});	
}
function view_songdetail(id){
	loader_show();
	jQuery('#song_detailtab').removeClass('active_small_tab');
	jQuery('#song_artisttab').removeClass('active_small_tab');
	
	jQuery('#song_detailtab').addClass('active_small_tab');
	
	$.ajax({
		url : site_url+'controllers/ajax_controller/song-ajax-controller.php',
		type : 'post',
		data: 'view=1&id='+id,
		success : function(result)
		{
			jQuery("#bottom_container").html(result);
			loader_hide();
		}
	});
}