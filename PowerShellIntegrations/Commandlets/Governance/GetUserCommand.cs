﻿using System.Data;
using System.Linq;
using System.Management.Automation;
using Sitecore;
using Sitecore.Security.Accounts;
using Sitecore.Shell.Applications.Security.RoleManager;

namespace Cognifide.PowerShell.PowerShellIntegrations.Commandlets.Governance
{
    [Cmdlet(VerbsCommon.Get, "User", DefaultParameterSetName = "User from name")]
    [OutputType(new[] {typeof (User)})]
    public class GetUserCommand : BaseCommand
    {
        [Alias("Name")]
        [Parameter(ParameterSetName = "User from name", ValueFromPipeline = true,Mandatory = true, Position = 0)]
        public string Identity { get; set; }

        [Parameter(ParameterSetName = "User from name")]
        public Role Role { get; set; }

        [Parameter(ParameterSetName = "Current user", Mandatory = true)]
        public SwitchParameter Current { get; set; }

        [Parameter(ParameterSetName = "User from name")]
        public SwitchParameter Authenticated { get; set; }

        protected override void ProcessRecord()
        {
            if (Current)
            {
                WriteObject(Context.User);
            }
            else
            {
                string name = Identity;

                if (name.Contains('?') || name.Contains('*') || string.IsNullOrEmpty(name))
                {
                    var users = WildcardFilter(name, UserManager.GetUsers(), user => user.Name);
                    if (Role != null)
                    {
                        users = users.Where(user => user.IsInRole(Role));
                    }

                    WriteObject(users,true);
                    return;
                }

                if (!name.Contains(@"\") && !string.IsNullOrEmpty(name))
                {
                    name = @"sitecore\" + name;
                }

                if (User.Exists(name))
                    WriteObject(User.FromName(name, Authenticated));
                else
                {
                    WriteError(new ErrorRecord(new ObjectNotFoundException("User '" + name + "' could not be found"),
                        "user not found", ErrorCategory.ObjectNotFound, null));
                }
            }
        }
    }
}