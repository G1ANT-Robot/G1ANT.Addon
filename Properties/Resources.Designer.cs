﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace G1ANT.Addon.Mscrm.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("G1ANT.Addon.Mscrm.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (function () {
        ///    try {
        ///        keys = Object.keys(Sys.Application._components);
        ///        containsAtLeastOneCommandContainer = false;
        ///        for (i = 0; i &lt; keys.length; i++) {
        ///            if (keys[i].startsWith(&apos;commandContainer&apos;) || keys[i].startsWith(&apos;crmRibbonData&apos;)) {
        ///                containsAtLeastOneCommandContainer = true;
        ///                key = keys[i];
        ///                component = Sys.Application._components[key];
        ///                isComponentInitialized = component.get_isInitialized();
        ///    [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AreCommandBarsInitialized {
            get {
                return ResourceManager.GetString("AreCommandBarsInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap crmicon {
            get {
                object obj = ResourceManager.GetObject("crmicon", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WatinSearchHelper = function () {
        ///    var earTagId = 1;
        ///
        ///    var getElementId = function (cssSelector, iFrame) {
        ///        currentFrame = getIframe(iFrame);
        ///        var resultId = &quot;_no_element_&quot;;
        ///        var el = $(currentFrame).contents().find(cssSelector);
        ///        if (el.length &gt; 0) {
        ///            var firstEl = el[0];
        ///            if (!firstEl.id) {
        ///                firstEl.id = &quot;_watin_search_&quot; + earTagId++;
        ///            }
        ///            resultId = firstEl.id;
        ///        }
        ///        return resultId;
        ///    [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string FindViaJquerySelector {
            get {
                return ResourceManager.GetString("FindViaJquerySelector", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $(document).ready(function () {
        ///    console.log(&quot;g1ant script injected on &quot; + (new Date()).format(&quot;dd/MM/yyyy hh:mm:ss&quot;));
        ///    function elementClicked(e) {
        ///        var elementIsCrmField = false;
        ///        var mscrmElement = getMscrmSetValueElement(e.target);
        ///        var mscrmFilter = &apos;id&apos;;
        ///        if (mscrmElement) {
        ///            elementIsCrmField = mscrmElement.action === &apos;setvalue&apos;;
        ///            mscrmElement = mscrmElement.element;
        ///        }
        ///        else {
        ///            var mscrmClickItem = getMscrmC [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MsCrmWizardInjection {
            get {
                return ResourceManager.GetString("MsCrmWizardInjection", resourceCulture);
            }
        }
    }
}
