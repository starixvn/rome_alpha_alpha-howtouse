namespace Alpha.HowToUse
{
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using Fanex.Age.UI;
    using Sunplus.Agent.Model.BL.Definitions;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Model.Entities.Permission;

    public partial class Menu_Page : TplPage
    {
        public override void ProcessRequest(ref System.Collections.Hashtable p)
        {
        }

        public override object ValidateRequest(System.Collections.Hashtable p)
        {
            return null;
        }

        public override object InvokeData(System.Collections.Hashtable p)
        {
            return null;
        }

        public override void RenderContent(HtmlTextWriter writer)
        {
            Menu_Template tpl = new Menu_Template();

            if (CurrentUserSession.IsSuper || UserPermission.IsNeutral)
            {
                tpl.AgUI = null;
            }
            tpl.scriptcore = "<script src='/site-main/_GlobalResources/Js/Core.js' type='text/javascript'></script>";
            RenderMenu(ref tpl);
            writer.Write(tpl);
        }

        protected void RenderMenu(ref Menu_Template tpl)
        {
            int roleId = CurrentUserSession.RoleId;
            SiteMapNode rootNode = SiteMap.RootNode;
            SiteMapNode contextNode = null;

            if (roleId == (int)(UserTypes.Agent))
            {
                contextNode = rootNode.ChildNodes[0];
            }
            else if (roleId == (int)(UserTypes.Master))
            {
                contextNode = rootNode.ChildNodes[1];
            }
            else if (roleId == (int)(UserTypes.Super))
            {
                contextNode = rootNode.ChildNodes[2];
            }

            RenderMenu(contextNode, ref tpl);
        }

        protected void RenderMenu(SiteMapNode node, ref Menu_Template tpl)
        {
            if (!node.HasChildNodes || tpl.SubMenu == null)
            {
                return;
            }

            Menu_SubMenu_Block subBlock = new Menu_SubMenu_Block();

            int subMenuCount = node.ChildNodes.Count;
            tpl.SubMenu.Clear();

            if (tpl.AgUI != null)
            {
                tpl.AgUI.userguideurl = Regex.Replace(Response.ApplyAppPathModifier("~/index.aspx"),
                    "site-howtouse",
                    "site-userguide",
                    RegexOptions.IgnoreCase);
            }

            for (int i = 0; i < subMenuCount; i++)
            {
                SiteMapNode subMenu = node.ChildNodes[i];
                string expand = subMenu.GetAttribute("expand");

                if (!subMenu.HasChildNodes)
                {
                    continue;
                }

                if (RenderSubMenu(subMenu, ref subBlock, i, expand.Equals("true", System.StringComparison.OrdinalIgnoreCase)))
                {
                    tpl.SubMenu.Append(subBlock);
                }
            }
        }

        private bool RenderSubMenu(SiteMapNode subMenu, ref Menu_SubMenu_Block subBlock, int index, bool isExpand)
        {
            int itemCount = subMenu.ChildNodes.Count;
            subBlock.sub_display = isExpand ? "Bleft_ParentAc" : "Bleft_Parent";
            subBlock.item_display = isExpand ? "block" : "none";
            subBlock.sub_id = index.ToString();
            subBlock.sub_title = LocalResources.HowToUse.ResourceManager.GetString(subMenu.Title);
            Menu_SubMenu_MenuItem_Block itemBlock = new Menu_SubMenu_MenuItem_Block();
            subBlock.MenuItem.Clear();
            bool isShowSiteMapNode = false;

            for (int j = 0; j < itemCount; j++)
            {
                SiteMapNode item = subMenu.ChildNodes[j];

                // Agent, Master(DisableAutoM) don't have AutoPT
                if (item.Description.Equals("AutoPT", System.StringComparison.OrdinalIgnoreCase))
                {
                    if (CurrentUserSession.IsAgent || (CurrentUserSession.IsMaster && AppSettings.DisabledAutoMaster))
                    {
                        continue;
                    }
                }

                // Check Show View Log
                if (item.Description.Equals("ViewLog", System.StringComparison.OrdinalIgnoreCase))
                {
                    if (CurrentUserSession.IsSubAcc)
                    {
                        PageAccess pageAccess = CurrentUserSession.SubPageAccess;
                        if (!(pageAccess.CreateAccount || pageAccess.UpdateAccount))
                        {
                            continue;
                        }
                    }

                    if (!AppSettings.ShowViewLog)
                    {
                        continue;
                    }
                }

                // Check Show Casino
                if (item.Description.Equals("CasinoGuide", System.StringComparison.OrdinalIgnoreCase) && UserPermission.IsNeutral)
                {
                    continue;
                }

                // Check Show NumberGame
                if (item.Description.Equals("NumberGame", System.StringComparison.OrdinalIgnoreCase))
                {
                    if (UserPermission.IsNeutral ||
                        !UserPermission.IsShowNumberGame ||
                        CurrentUserSession.IsNumberGameDisabled)
                    {
                        continue;
                    }
                }

                RenderMenuItem(item, ref itemBlock, j, index);
                subBlock.MenuItem.Append(itemBlock);
                isShowSiteMapNode = true;
            }

            return isShowSiteMapNode;
        }

        private void RenderMenuItem(SiteMapNode menuItem, ref Menu_SubMenu_MenuItem_Block itemBlock, int index, int parentIndex)
        {
            itemBlock.item_title = LocalResources.HowToUse.ResourceManager.GetString(menuItem.Title);
            itemBlock.order = string.Format("{0}{1}", parentIndex + 1, index + 1);

            if (menuItem.Title.Equals("MemberWinLimit", System.StringComparison.OrdinalIgnoreCase))
            {
               itemBlock.item_url = string.Format("{0}/Resources/HTML/WinLimit/WinLimit_{1}.html", Request.ApplicationPath, CurrentUserSession.Language);
            }
            else
            {
                itemBlock.item_url = string.Format(
                    "Content.aspx?fw={0}&fh={1}&folder={2}",
                                            menuItem.GetAttribute("width"),
                                            menuItem.GetAttribute("height"),
                                            menuItem.Description);
            }
        }
    }
}