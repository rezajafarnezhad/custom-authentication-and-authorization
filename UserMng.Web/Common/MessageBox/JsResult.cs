using Microsoft.AspNetCore.Mvc;

namespace UserMng.Web.Common.MessageBox
{
    public class JsResult : ContentResult
    {
        public JsResult(string script)
        {
            Content = script;
            ContentType = "application/javascript";
        }
    }
}
