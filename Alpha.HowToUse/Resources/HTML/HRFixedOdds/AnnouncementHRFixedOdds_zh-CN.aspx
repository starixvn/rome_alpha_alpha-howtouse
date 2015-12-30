<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnnouncementHRFixedOdds_zh-CN.aspx.cs" Inherits="_HowToUse_Resources_HTML_HRFixedOdds_AnnouncementHRFixedOdds_zh_CN" %>

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
            imageName = "PT-Super-CN.png";
            break;
        case "3":
            imageName = "PT-Master-CN.png";
            break;      
        case "2":
            imageName = "PT-Agent-CN.png";
            break;    
    }
return imageName;
}
</script>
    <table style="margin-left:25px;">
        <thead>
            <tr>
                <td id="header_cont">
                    固定赔率赛马常见疑问

                </td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    <p style="font-size: 12px; font-weight: bold;">
                        1. 在哪更新固定赔率赛马的占成数?</p>
                    <p style="font-size: 13px;">
                        这设定位于体育设置页面，混合体育过关占成设置之后</p>
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
                        2. 在哪更新固定赔率赛马的投注设定?</p>
                    <p style="font-size: 13px;">
                        这设定位于体育设置页面的投注设定栏.</p>
                </td>
            </tr>
            <tr>
                <td>
                     <img src="Images/betsetting-CN.png" alt="Smiley face" height="366" width="850" />
                </td>
            </tr>
            <tr>
                <td>
                    <p style="font-size: 12px; font-weight: bold;">
                        3. 在哪更新固定赔率赛马的佣金?</p>
                    <p style="font-size: 13px;">
                        这设定位于体育设置页面的佣金栏.</p>
                </td>
            </tr>
            <tr>
                <td>
                     <img src="Images/Comm-CN.png" alt="Smiley face" height="89" width="852" />
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>
