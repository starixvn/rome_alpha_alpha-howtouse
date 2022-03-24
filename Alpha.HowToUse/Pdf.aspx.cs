namespace Alpha.HowToUse
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using Sunplus.Agent.Model.Entities;

    public partial class Pdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userSession = UserSession.Current;
            var filePath = BuildPdfFilePath(
                Server.MapPath,
                Request.QueryString["functionName"],
                GetRoleName(userSession.RoleId),
                MapLanguage(userSession.Language));

            var fileBuffer = new WebClient().DownloadData(filePath);

            if (fileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", fileBuffer.Length.ToString());
                Response.BinaryWrite(fileBuffer);
                Response.Flush();
                Response.End();
            }
        }

        private static string BuildPdfFilePath(
            Func<string, string> mapToPhysicalPath,
            string functionName,
            string roleName,
            string language)
        {
            var functionRoleLanguage = mapToPhysicalPath(BuildPdfFilePath(functionName, roleName, language));
            if (File.Exists(functionRoleLanguage))
            {
                return functionRoleLanguage;
            }

            functionRoleLanguage = mapToPhysicalPath(BuildPdfFilePath(functionName, roleName, "en-US"));
            if (File.Exists(functionRoleLanguage))
            {
                return functionRoleLanguage;
            }

            var functionLanguage = mapToPhysicalPath(BuildPdfFilePath(functionName, language));
            if (File.Exists(functionLanguage))
            {
                return functionLanguage;
            }

            functionLanguage = mapToPhysicalPath(BuildPdfFilePath(functionName, "en-US"));
            if (File.Exists(functionLanguage))
            {
                return functionLanguage;
            }

            throw new FileNotFoundException($"{functionName} not found.");
        }

        private static string BuildPdfFilePath(string functionName, params string[] varyParams)
        {
            var fileNameBuilder = new StringBuilder();
            fileNameBuilder.Append("~/Resources/PDF/").Append(functionName);

            if (varyParams != null)
            {
                foreach (var item in varyParams)
                {
                    fileNameBuilder.Append(".").Append(item);
                }
            }

            fileNameBuilder.Append(".pdf");

            return fileNameBuilder.ToString();
        }

        private static string GetRoleName(int roleId)
        {
            switch (roleId)
            {
                case 4:
                    return "Super";
                case 3:
                    return "Master";
                case 2:
                    return "Agent";
                default:
                    return string.Empty;
            }
        }

        private static string MapLanguage(string language)
        {
            var supportedLanguages = new[] { "th-TH", "vi-VN", "zh-CN", "zh-TW" };

            return supportedLanguages.Any(l => l.Equals(language))
                ? language
                : "en-US";
        }
    }
}