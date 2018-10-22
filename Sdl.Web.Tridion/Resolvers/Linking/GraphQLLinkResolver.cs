﻿using Sdl.Web.Common;
using Sdl.Web.Common.Interfaces;
using Sdl.Web.PublicContentApi;
using Sdl.Web.PublicContentApi.Utils;
using Sdl.Web.Tridion.PCAClient;

namespace Sdl.Web.Tridion.Linking
{
    /// <summary>
    /// Default Link Resolver implementation
    /// </summary>
    public class GraphQLLinkResolver : ILinkResolver
    {
        #region ILinkResolver Members

        /// <summary>
        /// Resolves a link URI (TCM URI or site URL) to a normalized site URL.
        /// </summary>
        /// <param name="sourceUri">The source URI (TCM URI or site URL)</param>
        /// <param name="resolveToBinary">Specifies whether a link to a Multimedia Component should be resolved directly to its Binary (<c>true</c>) or as a regular Component link.</param>
        /// <param name="localization">The context Localization (optional, since the TCM URI already contains a Publication ID, but this allows resolving in a different context).</param>
        /// <returns>The resolved URL.</returns>
        public string ResolveLink(string sourceUri, bool resolveToBinary = false, ILocalization localization = null)
        {
            if (sourceUri == null) return null;

            string url = null;
            if (sourceUri.IsCmUri())
            {
                var client = PCAClientFactory.Instance.CreateClient();
                var cmUri = new CmUri(sourceUri);
                switch (cmUri.ItemType)
                {
                    case ItemType.Component:
                        url = resolveToBinary ? 
                            client.ResolveBinaryLink(cmUri.Namespace, cmUri.PublicationId, cmUri.ItemId, null) : 
                            client.ResolveComponentLink(cmUri.Namespace, cmUri.PublicationId, cmUri.ItemId, null, null);
                        break;
                    case ItemType.Page:
                        url = client.ResolvePageLink(cmUri.Namespace, cmUri.PublicationId, cmUri.ItemId);
                        break;
                }              
            }
            else
            {
                url = sourceUri;
            }

            // Strip off default extension / page name
            if (url == null || !url.EndsWith(Constants.DefaultExtension)) return url;
            url = url.Substring(0, url.Length - Constants.DefaultExtension.Length);
            if (url.EndsWith("/" + Constants.DefaultExtensionLessPageName))
            {
                url = url.Substring(0, url.Length - Constants.DefaultExtensionLessPageName.Length);
            }
            return url;
        }
        #endregion
    }
}
