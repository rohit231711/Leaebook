<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="Default4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px;border-color:gray;background-color:#eee\" > 
            <tr>
               <td  style=\";Font-size:20px;\"> 
         <b   style=\"background-color:#001c4a;  display: block;    padding: 7px; \"><img src=\"https://www.leaebook.com/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\" /><div style=\"margin-left: 55px; margin-top: -25px; color:White;\">LEA eBooks</div></b>
     
     <h3 style='font-family: Calibri , sans-serif; font-size: 18px; font-weight: normal;color: rgb(51, 51, 51);'>
     <b>&nbsp;LEA eBooks | &nbsp;Order information</b></h3><br>
     <span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51)
     &nbsp;&nbsp;&nbsp;&nbsp;Hello&nbsp;abc </span><br>
     <span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>
     &nbsp;&nbsp;&nbsp;&nbsp;Thank you for your order !</span><br>
     <span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>
     &nbsp;&nbsp;&nbsp;&nbsp;Please find below, the summary of your order:</span><br>
     <span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>
     <b>&nbsp;&nbsp;&nbsp;&nbsp;Order No: </b> + OrderID </span><br>
     <span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>
     <b>&nbsp;&nbsp;&nbsp;&nbsp;Tracking Number: </b>  billNumber +</span><br>
     <table width='550' height='120' cellspacing='0' cellpadding='5' border='0' style='border-bottom: solid 1px #ccc;'>
     <tbody>
       
        <tr style='margin-bottom: 10px;'>
        <td width='70' align='left'>
        <b style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Image</b></td>
        <td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;eBookName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>
        <td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>
        <td width='57' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Quantity&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>
        <td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold; color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;Price</td>");
        </tr>
      
            <tr style='margin-bottom: 10px;'>
            <td width='70' align='left'>
            <img border='0' title='' alt='' style='margin: 0px;height:150px;width:120px;' src="#"></td>
            <td width='270' valign='middle' style = 'padding-left: 22px;'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'> .net<br></span> </td>
            <td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>vrinsoft<br></span> </td>
            <td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>12<br></span> </td>
            <td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif;font-size: 11.0pt; font-weight: normal; color: rgb(62, 62, 62);'>10</td>
            </tr>

            </tbody>
            </table>
            <br />

        <table width='550' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>
        <tr>
        <td width='70' align='left'>  &nbsp;</td>
        <td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Book(s):</td>
         <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>qty</td>
        </tr>");
        </tbody></table><br />

        <table width='550' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>
        <tr>");
        <td width='70' align='left'>  &nbsp;</td>
        <td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Charged:</td>
         <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>price</td>
        </tr>");
        </tbody></table><br />

        <table width=\"600\" border=\"0\" align=\"center\" cellspacing='0' cellpadding='0' border=''0' ><tbody>"
      <tr>
      <td>
       <div style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51); text-align: justify;'>
      <span>&nbsp;&nbsp;NOTE :</span> <span>If you have any questions or further query then please contact us:<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; contactmail </div>
      </td>
      </tr>
      </tbody>
      </table>
      </td>
      </tr>
      </table>
        </div>
    </form>
</body>
</html>
