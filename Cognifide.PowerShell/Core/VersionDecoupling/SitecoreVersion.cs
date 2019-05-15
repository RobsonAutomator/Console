﻿using System;
using System.Diagnostics;
using System.Linq;
using Sitecore.Configuration;

namespace Cognifide.PowerShell.Core.VersionDecoupling
{
    public static class SitecoreVersion
    {
        public static Version Current = GetVersionNumber();
        public static Version V71 = new Version(7, 1);
        public static Version V72 = new Version(7, 2);
        public static Version V75 = new Version(7, 5);
        public static Version V80 = new Version(8, 0);
        public static Version V81 = new Version(8, 1);
        public static Version V82 = new Version(8, 2);
        public static Version V90 = new Version(9, 0);
        public static Version V91 = new Version(9, 1);
        public static Version V92 = new Version(9, 2);

        public static Version GetVersionNumber()
        {
            if (Version.TryParse(About.Version, out var version))
            {
                return version;
            }
            if (Version.TryParse(About.GetVersionNumber(false), out version))
            {
                return version;
            }
            var kernel = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName()
                    .Name.Equals("Sitecore.Kernel", StringComparison.OrdinalIgnoreCase));

            if (kernel != null)
            {
                var fvi = FileVersionInfo.GetVersionInfo(kernel.Location);
                if (!Version.TryParse(fvi.FileVersion, out version))
                {
                    version = new Version(7, 0);
                }
            }
            else
            {
                version = new Version(7, 0);
            }
            return version;
        }


    }
}