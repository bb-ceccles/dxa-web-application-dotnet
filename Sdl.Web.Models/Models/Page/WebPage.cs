﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sdl.Web.Mvc.Models
{
    /// <summary>
    /// Model for the data that is used to render a web page
    /// </summary>
    public class WebPage
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public Breadcrumb Breadcrumb { get; set; }
        //TODO: Are dictionaries better than explicit values? Somethings you may always have (description etc.)
        public Dictionary<string, string> Meta { get; set; }
        public Dictionary<string, Region> Regions { get; set; }
        public Header Header { get; set; }
        public Footer Footer { get; set; }

        /*Other stuff to consider:
         1. Specific css/js for the page based on its components/plugins
         2. Navigation (should it hang off the page, or be inherent in the app)
         3. Analytics variables
         4. Parent SG id(s) for building navigation state - could be part of breadcrumb
         5. Ads/regions for ads
         */
        public WebPage()
        {
            Regions = new Dictionary<string, Region>();
        }
    }
}