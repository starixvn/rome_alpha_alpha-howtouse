<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnnouncementHRFixedOdds_th-TH.aspx.cs" Inherits="_HowToUse_Resources_HTML_HRFixedOdds_AnnouncementHRFixedOdds_th_TH" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>HR Fixed Oddds Announcement</title>
    <link href="Style/AnnouncementHRFixedOdds.css" rel="stylesheet" type="text/css" />
</head>
<body>
<script runat=server>
private string getImageName(string roleId)
{
string imageName=null;
switch (roleId)
    {
        case "4":
            imageName = "PT-Super-TH.png";
            break;
        case "3":
            imageName = "PT-Master-TH.png";
            break;      
        case "2":
            imageName = "PT-Agent-TH.png";
            break;    
    }
return imageName;
}
</script>
    <table style="margin-left:25px;">
        <thead>
            <tr>
                <td id="header_cont">
                    แข่งม้าราคาคงที่ คำถามที่พบบ่อย
                </td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    <p style="font-size: 12px; font-weight: bold;">
                        1. ตั้งค่าหุ้นส่วนของ แข่งม้าราคาคงที่ได้ที่ไหน?</p>
                    <p style="font-size: 13px;">
                        เลือกรายการตั้งค่าที่หน้สากีฬา ด้านล่างของกีฬาบอลชุด</p>
                </td>
            </tr>
            <tr>
                <td>
                    <img src="Images/<%=getImageName(Request.QueryString["roleId"])%>" alt="Smiley face" height="472" width="365" />
                </td>
            </tr>
            <tr>
                <td>
                    <p style="font-size: 12px; font-weight: bold;">
                        2.  สามารตั้งค่าเดิมพันของแข่งม้าราคาคงที่ได้ที่ไหน?</p>
                    <p style="font-size: 13px;">
                        เข้าไปยังหน้าการตั้งค่าเดิมพันของหน้ากีฬา</p>
                </td>
            </tr>
            <tr>
                <td>
                    <img src="Images/betsetting-TH.png" alt="Smiley face" height="366" width="850" />
                </td>
            </tr>
            <tr>
                <td>
                    <p style="font-size: 12px; font-weight: bold;">
                        3.  สามารถตั้งค่าคอมมิชั่นของแข่งม้าราคาคงที่ได้ที่ไหน?</p>
                    <p style="font-size: 13px;">
                        เข้าไปยังหน้าการตั้งค่าคอมมิชั่นของหน้ากีฬา</p>
                </td>
            </tr>
            <tr>
                <td>
                    <img src="Images/Comm-TH.png" alt="Smiley face" height="89" width="852" />
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>
