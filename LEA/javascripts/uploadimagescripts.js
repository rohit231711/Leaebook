function upload_popup(){
	$.ajax( {
		url : site_url+'controllers/ajax_controller/upload-image-ajax-controller.php',
		type : 'post',
		data: 'uploadpopup=1',
		success : function(result)
		{
			$("#view_detail").html(result);
			$("#common").html('Upload Images');
			$("#various_1").fancybox().trigger('click');
		}
	});
}