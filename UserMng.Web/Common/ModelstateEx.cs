using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UserMng.Web.Common
{
    public static class ModelstateEx
    {
        public static string GetErrors(this ModelStateDictionary Modelstate, string Sprator = "<br/>")
        {
            var Messages = string.Join(Sprator, Modelstate.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return Messages;
        }
    }
}
