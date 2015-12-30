<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnnouncementHRFixedOdds_ja-JP.aspx.cs" Inherits="_HowToUse_Resources_HTML_HRFixedOdds_AnnouncementHRFixedOdds_ja_JP" %>

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
            imageName = "PT-Super-EN.png";
            break;
        case "3":
            imageName = "PT-Master-EN.png";
            break;      
        case "2":
            imageName = "PT-Agent-EN.png";
            break;    
    }
return imageName;
}
</script>

    <table style="margin-left:25px;">
        <thead>
            <tr>
                <td id="header_cont">
                    Horse Racing Fixed Odds FAQs
                </td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    <p style="font-size: 12px; font-weight: bold;">
                        1. Where to update Position Taking setting for Horse Racing Fixed Odds?</p>
                    <p style="font-size: 13px;">
                        This section is located at Sportsbook setting page, under Mix Sports Parlay.</p>
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
                        2. Where to update Bet Setting for Horse Racing Fixed Odds?</p>
                    <p style="font-size: 13px;">
                        Access Bet Setting section located within Sportsbook setting page.</p>
                </td>
            </tr>
            <tr>
                <td>
                      <img src="Images/betsetting-EN.png" alt="Smiley face" height="366" width="850" />
                </td>
            </tr>
            <tr>
                <td>
                    <p style="font-size: 12px; font-weight: bold;">
                        3. Where to update Commission setting for Horse Racing Fixed Odds Access Commission?</p>
                    <p style="font-size: 13px;">
                        Setting section located within Sportsbook setting page.</p>
                </td>
            </tr>
            <tr>
                <td>
                     <img src="Images/Comm-EN.png" alt="Smiley face" height="89" width="852" />
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>
