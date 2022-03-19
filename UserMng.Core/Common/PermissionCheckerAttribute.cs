using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserMng.Core.Contracts;

namespace UserMng.Core.Common
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private int _permissionId = 0;
        private IPanelService _penelService;
        public PermissionCheckerAttribute(int PermissionId)
        {
            _permissionId = PermissionId;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {

            _penelService = (IPanelService)context.HttpContext.RequestServices.GetService(typeof(IPanelService));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string _userName = context.HttpContext.User.Identity.Name;
                if (!_penelService.CheckPermission(_permissionId, _userName))
                {
                    context.Result = new RedirectResult("/Home/Index");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Home/Index");
            }
        }
    }
}
