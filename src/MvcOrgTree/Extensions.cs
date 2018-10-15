using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        private static string Branch<T>(this HtmlHelper<OrgNode<T>> html, OrgNode<T> node, bool isRoot = false)
        {
            string classNames = string.Empty;
            if (isRoot) classNames += "root";
            if (node.Children.Count == 0) classNames += "leaf";
            if (classNames.Length > 0) classNames = $" class='{classNames}'";

            string htmlOutput = string.Empty;
            htmlOutput += $"<li{classNames}>";
            htmlOutput += "<div class='nodecontent'>";
            //htmlOutput += new HtmlHelper<T>(html.ViewContext, html.ViewDataContainer).DisplayFor(t => t);
            htmlOutput += html.DisplayFor(x => node.Data);
            //htmlOutput += (node.Data).ToString();
            htmlOutput += "</div>";
            htmlOutput += html.Tree(node.Children);
            htmlOutput += "</li>";
            return htmlOutput;
        }

        public static MvcHtmlString Tree<T>(this HtmlHelper<OrgNode<T>> html, OrgNode<T> root)
        {
            string htmlOutput = string.Empty;
            htmlOutput += "<div class='centered orgchart'><ul>";
            htmlOutput += html.Branch(root, true);
            htmlOutput += "</ul></div>";
            return new MvcHtmlString(htmlOutput);
        }

        private static string Tree<T>(this HtmlHelper<OrgNode<T>> html, List<OrgNode<T>> tree)
        {
            string htmlOutput = string.Empty;

            if (tree.Count() > 0)
            {
                htmlOutput += "<ul>";
                foreach (var branch in tree)
                {
                    htmlOutput += html.Branch(branch);
                }
                htmlOutput += "</ul>";
            }

            return htmlOutput;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Credits to
    /// https://dzone.com/articles/css-flex-based-orgchart-with-zk
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class OrgNode<T>
    {
        public OrgNode(T nodeData) : this(nodeData, new List<OrgNode<T>>())
        { }

        public OrgNode(T nodeData, List<OrgNode<T>> children)
        {
            Data = nodeData;
            Children = children;
        }

        public T Data { get; private set; }
        public List<OrgNode<T>> Children { get; private set; }
        public bool IsLeaf { get; set; }
        public bool IsVertical { get; set; }
    }
}
