﻿using System;
using Cognifide.PowerShell.Services;
using Sitecore.Globalization;
using Sitecore.Jobs;
using Sitecore.Security.Accounts;

namespace Cognifide.PowerShell.VersionSpecific.Services
{
    public class SpeJobOptions : IJobOptions
    {
        public User ContextUser { get; set; }
        public bool EnableSecurity { get; set; }
        public Language ClientLanguage { get; set; }
        public bool AtomicExecution { get; set; }
        public TimeSpan AfterLife { get; set; }

        public string JobName { get; set; }
        public string Category { get; set; }
        public object Obj { get; set; }
        public string MethodName { get; set; }
        public string SiteName { get; set; }
        public object[] Parameters { get; set; }

        public SpeJobOptions(string jobName, string category, string siteName, object obj, string methodName, object[] parameters)
        {
            this.JobName = jobName;
            this.Category = category;
            this.SiteName = siteName;
            this.Obj = obj;
            this.MethodName = methodName;
            this.Parameters = parameters;
        }

        public static implicit operator JobOptions(SpeJobOptions customOptions)
        {
            return new JobOptions(customOptions.JobName, customOptions.Category, customOptions.SiteName, customOptions.Obj, customOptions.MethodName, customOptions.Parameters)
            {
                ContextUser = customOptions.ContextUser,
                EnableSecurity = customOptions.EnableSecurity,
                ClientLanguage = customOptions.ClientLanguage,
                AtomicExecution = customOptions.AtomicExecution,
                AfterLife = customOptions.AfterLife
            };
        }
    }
}