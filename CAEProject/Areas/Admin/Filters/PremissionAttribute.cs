using CAEProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace CAEProject.Areas.Admin.Filters
{
    public class PremissionAttribute : ActionFilterAttribute
    {
        Model1 db = new Model1();
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Controller.ViewBag.menu = "";
                HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl);
                return;
            }
            var Data = db.Premissions.ToList();
            StringBuilder sb = new StringBuilder();
            sb.Append(GetPremission(Data.Where(x => x.pid == null).ToList()));
            filterContext.Controller.ViewBag.menu = sb.ToString();
        }

        private string GetPremission(ICollection<Premission> list)
        {
            StringBuilder sb = new StringBuilder();
            var user = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;
            User userDate = JsonConvert.DeserializeObject<User>(user);
            foreach (Premission premission in list)
            {
                if (userDate.Role.Authority.IndexOf(premission.PValue, StringComparison.Ordinal) > -1)
                {
                    if (premission.pid == null)
                    {
                        if (premission.premissionSon.Count > 0)
                        {
                            sb.Append($"<li class='sub-menu'>");
                            sb.Append($"<a href='javascript:;' class=''>");
                            sb.Append($"<i class='icon_documents_alt'></i>");
                            sb.Append($"<span>{premission.Name}</span>");
                            sb.Append($"<span class='menu-arrow arrow_carrot-right'></span>");
                            sb.Append($"</a>");
                            sb.Append($"<ul class='sub'>");
                            sb.Append(GetPremission(premission.premissionSon));
                            sb.Append($"</ul>");
                            sb.Append($"</li>");
                        }
                        else
                        {
                            sb.Append($"<li>");
                            sb.Append($"<a class='' href='{premission.Url}'>");
                            sb.Append($"<i class='icon_document_alt'></i>");
                            sb.Append($"<span>{premission.Name}</span>");
                            sb.Append($"</a>");
                            sb.Append($"</li>");
                        }
                    }
                    else
                    {
                        sb.Append($"<li><a class='' href='{premission.Url}'>{premission.Name}</a></li>");
                    }
                }
            }
            return sb.ToString();
            }
        }
}