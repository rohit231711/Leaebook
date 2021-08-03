<?php
$targetFolder = str_replace($_SESSION['sitefoldername'],"",$_REQUEST['folder']);
include('../includes/thumb_new.php');
include('../includes/resize-class.php');
$upload_dir =$targetFolder;
$upload_dirthumb =$targetFolder."thumb/";
$upload_dirgallery =$targetFolder."gallery/";

if (!empty($_FILES)) {
	$tempFile = $_FILES['Filedata']['tmp_name'];

	$targetPath = $targetFolder;
	$newname = $_FILES['Filedata']['name'];
	$abc=date("dmyHi");
	$imgname=$abc.$newname;
	$targetFile = rtrim($targetPath,'/') . '/' .$imgname;

	$fileTypes = array('jpg','jpeg','gif','png');
	$fileParts = pathinfo($_FILES['Filedata']['name']);	
	
	if(in_array($fileParts['extension'],$fileTypes))
	{
		list($width, $height, $type, $attr) = getimagesize($tempFile); 
		
		if($width < 150 || $height < 150)
		{
			echo "Invalid height or width of image '".$_FILES['Filedata']['name']."', minimum height and width should be 150px.";
		}
		else
		{
			if(move_uploaded_file($tempFile,$targetFile)){
				
				if (strpos($targetFolder,"band_image/band_gallery/") !== false) {
					$upload_50 =$targetFolder."50/";
					$upload_101 =$targetFolder."101/";
					$upload_105 =$targetFolder."105/";
					$upload_160 =$targetFolder."160/";
					$upload_458_320 =$targetFolder."458_320/";
					$upload_738_275 =$targetFolder."738_275/";
					
					if(!dir($targetFolder."50/")){
						mkdir($targetFolder."50/");
					}
					$resizeObj_50 = new resize($upload_dir.$imgname); 
					$resizeObj_50 -> resizeImage(50, 50, 'exact');
					$resizeObj_50 -> saveImage($upload_50.$imgname,$upload_dir.$imgname, 100);
					
					if(!dir($targetFolder."101/")){
						mkdir($targetFolder."101/");
					}
					$resizeObj_101 = new resize($upload_dir.$imgname); 
					$resizeObj_101 -> resizeImage(101, 101, 'exact');
					$resizeObj_101 -> saveImage($upload_101.$imgname,$upload_dir.$imgname, 100);
					
					if(!dir($targetFolder."105/")){
						mkdir($targetFolder."105/");
					}
					$resizeObj_105 = new resize($upload_dir.$imgname); 
					$resizeObj_105 -> resizeImage(105, 105, 'exact');
					$resizeObj_105 -> saveImage($upload_105.$imgname,$upload_dir.$imgname, 100);
					
					if(!dir($targetFolder."160/")){
						mkdir($targetFolder."160/");
					}
					if($width >= 160 || $height >= 160){
						$resizeObj_160 = new resize($upload_dir.$imgname); 
						$resizeObj_160 -> resizeImage(160, 160, 'exact');
						$resizeObj_160 -> saveImage($upload_160.$imgname,$upload_dir.$imgname, 100);
					}else{
						$resizeObj_160 = new resize($upload_dir.$imgname); 
						$resizeObj_160 -> resizeImage($width, $height, 'exact');
						$resizeObj_160 -> saveImage($upload_160.$imgname,$upload_dir.$imgname, 100);
					}
					
					if(!dir($targetFolder."458_320/")){
						mkdir($targetFolder."458_320/");
					}
					if($width >= 458 || $height >= 320){
						$resizeObj_458_320 = new resize($upload_dir.$imgname); 
						$resizeObj_458_320 -> resizeImage(458, 320, 'exact');
						$resizeObj_458_320 -> saveImage($upload_458_320.$imgname,$upload_dir.$imgname, 100);
					}else{
						$resizeObj_458_320 = new resize($upload_dir.$imgname); 
						$resizeObj_458_320 -> resizeImage($width, $height, 'exact');
						$resizeObj_458_320 -> saveImage($upload_458_320.$imgname,$upload_dir.$imgname, 100);
					}
					
					if(!dir($targetFolder."738_275/")){
						mkdir($targetFolder."738_275/");
					}
					if($width >= 738 || $height >= 275){
						$resizeObj_738_275 = new resize($upload_dir.$imgname); 
						$resizeObj_738_275 -> resizeImage(738, 275, 'exact');
						$resizeObj_738_275 -> saveImage($upload_738_275.$imgname,$upload_dir.$imgname, 100);
					}else{
						$resizeObj_738_275 = new resize($upload_dir.$imgname); 
						$resizeObj_738_275 -> resizeImage($width, $height, 'exact');
						$resizeObj_738_275 -> saveImage($upload_738_275.$imgname,$upload_dir.$imgname, 100);
					}
				}
				echo $imgname;
			}else{
				echo 'Image contain error.';
			}
		}
	}else{
		echo 'Invalid file type.';
	}
}
?>