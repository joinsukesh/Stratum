namespace Stratum.Foundation.Common.Services
{
    using System;

    /// <summary>
    /// For all user/account related methods, use Foundation.Accounts.Services.AccountsService.
    /// This class is only for basic methods to be used only within this project.
    /// </summary>
    public class UserService
    {
        public bool IsAuthenticated()
        {
            return Sitecore.Context.User.IsAuthenticated;
        }

        /// <summary>
        /// get logged in user's id.
        /// </summary>
        /// <returns>.</returns>
        internal Guid? GetUserId()
        {
            var userId = (Guid?)null;

            if (IsAuthenticated())
            {
                System.Web.Security.MembershipUser membershipUser = System.Web.Security.Membership.GetUser(Sitecore.Context.User.Name);

                if (membershipUser != null)
                {
                    userId = new Guid(Convert.ToString(membershipUser.ProviderUserKey));
                }
            }

            return userId;
        }
        
    }
}
