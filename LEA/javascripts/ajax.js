var xmlHttp;
var display_id;
var load_id;
var title1;
var url1;
var ext;

function show_result(url,disp_id,loader_id)
{
    load_id=loader_id;
	alert(load_id);
	document.getElementById(load_id).style.display='block';
	display_id = disp_id;
	alert(display_id);
	xmlHttp=CreateHttpObject();
	
	if(xmlHttp == null)
	{
		alert("Your browser doesn't support HTTP request");
		return 
	}

	
//	var url="page/search_user.php?uname="+fld.value
	xmlHttp.onreadystatechange=stateChanged 
	xmlHttp.open("GET",url,true)
	xmlHttp.send(null)
}
function showresult(url,disp_id,loader_id,t,dead_line,url2)
{
    
	
	
    load_id=loader_id;
	document.getElementById(load_id).style.display='block';
	display_id = disp_id;
	xmlHttp=CreateHttpObject();
	
	if(xmlHttp == null)
	{
		alert("Your browser doesn't support HTTP request");
		return 
	}
	var st_date = url;
	var st_date1 = document.getElementById(st_date).value;
	var timetype = t;
	var timetype1 = document.getElementById(timetype).value;
	var deadline_prj = dead_line;
	var deadline_prj1 = document.getElementById(deadline_prj).value;
	var u = url2;
	
	var u1 = document.getElementById(u).value;
	var ur=u1+"?end="+st_date1+"&timetp="+timetype1+"&deadln="+deadline_prj1;
//	var url="page/search_user.php?uname="+fld.value
   
	xmlHttp.onreadystatechange=stateChanged 
	xmlHttp.open("GET",ur,true)
	xmlHttp.send(null)
}

function add_result(url,disp_id,loader_id)
{	
		load_id=loader_id;
		document.getElementById(load_id).style.display='block';
	display_id = disp_id;
	xmlHttp=CreateHttpObject();
	
	if(xmlHttp == null)
	{
		alert("Your browser doesn't support HTTP request");
		return 
	}
	
	xmlHttp.onreadystatechange=addstateChanged 
	xmlHttp.open("GET",ur,true)
	xmlHttp.send(null)
}
function show_delete(url1,disp_id,loader_id)
{
		load_id=loader_id;
		document.getElementById(load_id).style.display='block';
	if(confirm("Are you sure you want to delete the record"))
   	{
      	var url=url1;  
  	 }
   else 
   {
	 document.getElementById(load_id).style.display='none';  
    return false;
   }

	
	display_id = disp_id;
	
	xmlHttp=CreateHttpObject();
	
	if(xmlHttp == null)
	{
		alert("Your browser doesn't support HTTP request");
		return 
	}

	
//	var url="page/search_user.php?uname="+fld.value
	xmlHttp.onreadystatechange=stateChanged 
	xmlHttp.open("GET",url,true)
	xmlHttp.send(null)
}


function addstateChanged()
{
	if(xmlHttp.readyState==4 || xmlHttp.readyState=="complete")
	{
		
		document.getElementById(display_id).innerHTML += xmlHttp.responseText;
		document.getElementById(load_id).style.display='none';
		
	}
}


function stateChanged()
{
	if(xmlHttp.readyState==4 || xmlHttp.readyState=="complete")
	{
	
		document.getElementById(display_id).innerHTML = xmlHttp.responseText;
		document.getElementById(load_id).style.display='none';
		if(xmlHttp.responseText=="Already Subscribed!")
		{
			document.getElementById("title").value="";
		}
	}
}

function CreateHttpObject()
{
	var xmlHttp=null;
	try
	{
		xmlHttp = new XMLHttpRequest();
	}
	catch(e)
	{

		try
		{
			xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
		}
		catch(e)
		{
			xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
		}
	}
	return xmlHttp;
}

