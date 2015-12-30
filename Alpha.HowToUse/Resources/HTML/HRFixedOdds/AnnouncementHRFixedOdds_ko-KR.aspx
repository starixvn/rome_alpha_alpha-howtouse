<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnnouncementHRFixedOdds_ko-KR.aspx.cs" Inherits="_HowToUse_Resources_HTML_HRFixedOdds_AnnouncementHRFixedOdds_ko_KR" %>

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
            imageName = "PT-Super-KO.png";
            break;
        case "3":
            imageName = "PT-Master-KO.png";
            break;      
        case "2":
            imageName = "PT-Agent-KO.png";
            break;    
    }
return imageName;
}
</script>
    <table style="margin-left:25px;">
        <thead>
            <tr>
                <td id="header_cont">
                   경마 고정 배당률 자주 묻는 질문
                </td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    <p style="font-size: 12px; font-weight: bold;">
                       1. 어디서 경마 고정 배당률 포지션테이킹을 업데이트 할 수 있나요 ? </p>
                    <p style="font-size: 13px;">
                        스포츠북 세팅 페이지에서 믹스 스포츠 팔레이 목록에서 할 수 있습니다.</p>
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
                        2. 어디서 경마 고정 배당률 베팅설정을 업데이트 할수 있나요 ?</p>
                    <p style="font-size: 13px;">
                       스포츠북 세팅페이지내의 베팅설정 부분에서 할 수 있습니다. </p>
                </td>
            </tr>
            <tr>
                <td>
                    <img src="Images/betsetting-KO.png" alt="Smiley face" height="366" width="850" />
                </td>
            </tr>
            <tr>
                <td>
                    <p style="font-size: 12px; font-weight: bold;">
                        3. 어디서 경마 고정 배당률 커미션을 업데이트 할수 있나요 ?</p>
                    <p style="font-size: 13px;">
                        스포츠북 세팅페이지내의 커미션 설정부분에서 할 수 있습니다.</p>
                </td>
            </tr>
            <tr>
                <td>
                    <img src="Images/Comm-KO.png" alt="Smiley face" height="89" width="852" />
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>
