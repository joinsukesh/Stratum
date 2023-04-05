
namespace Stratum.Foundation.Accounts.Services
{
    using Sitecore.Security.Accounts;
    using Sitecore.Security.Authentication;
    using Sitecore.SecurityModel;
    using System;
    using System.Collections.Generic;
    using System.Web.Security;

    public class AccountsService
    {
        public bool IsAuthenticated()
        {
            return Sitecore.Context.User.IsAuthenticated;
        }

        public bool IsAdministrator()
        {
            return Sitecore.Context.User.IsAuthenticated && Sitecore.Context.User.IsAdministrator;
        }

        /// <summary>
        /// get logged in user's id.
        /// </summary>
        /// <returns>.</returns>
        public Guid? GetUserId()
        {
            var userId = (Guid?)null;

            if (IsAuthenticated())
            {
                MembershipUser membershipUser = System.Web.Security.Membership.GetUser(Sitecore.Context.User.Name);

                if (membershipUser != null)
                {
                    userId = new Guid(Convert.ToString(membershipUser.ProviderUserKey));
                }
            }

            return userId;
        }

        public string GetEmail(string usernameWithDomain, bool isAuthenticated)
        {
            string email = string.Empty;

            User user = User.FromName(usernameWithDomain, isAuthenticated);

            if (user != null)
            {
                Sitecore.Security.UserProfile userProfile = user.Profile;
                email = userProfile.Email;
            }

            return email;
        }

        public string GetFullName(string usernameWithDomain, bool isAuthenticated)
        {
            string fullName = string.Empty;

            User user = User.FromName(usernameWithDomain, isAuthenticated);

            if (user != null)
            {
                Sitecore.Security.UserProfile userProfile = user.Profile;
                fullName = userProfile.FullName;
            }

            return fullName;
        }

        public string GetLastName()
        {
            string lastName = string.Empty;

            if (IsAuthenticated())
            {
                User user = User.FromName(Sitecore.Context.User.Name, true);

                if (user != null)
                {
                    Sitecore.Security.UserProfile userProfile = user.Profile;
                    string[] arrName = userProfile.FullName.Split(null);

                    if (arrName != null && arrName.Length == 2)
                    {
                        lastName = arrName[1];
                    }
                }
            }

            return lastName;
        }

        public User GetUserByUsername(string usernameWithDomain, bool isAuthenticated)
        {
            User user = User.FromName(usernameWithDomain, isAuthenticated);
            return user;
        }

        public bool IsValidUser(string usernameWithDomain)
        {
            //return Membership.ValidateUser(username, password);
            return User.Exists(usernameWithDomain);
        }

        public void CreateUser(string usernameWithDomain, string firstName, string lastName,
            string email, string password, List<KeyValuePair<string, string>> customProfileProperties,
            out bool userAlreadyExists)
        {
            userAlreadyExists = true;

            if (!User.Exists(usernameWithDomain))
            {
                userAlreadyExists = false;
                Membership.CreateUser(usernameWithDomain, password, email);

                /// Edit the profile information
                User user = User.FromName(usernameWithDomain, true);
                user.Profile.ProfileItemId = Constants.Profile.ProfileItemId;
                Sitecore.Security.UserProfile userProfile = user.Profile;

                using (new SecurityDisabler())
                {
                    userProfile.FullName = string.Format("{0} {1}", firstName, lastName);
                    userProfile.Email = email;

                    if (customProfileProperties != null && customProfileProperties.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> kvp in customProfileProperties)
                        {
                            user.Profile.SetCustomProperty(kvp.Key, kvp.Value);
                        }
                    }

                    userProfile.Save();
                }
            }
        }

        public bool SignInUser(string usernameWithDomain, string password, bool persist, out bool isSignUpPending)
        {
            isSignUpPending = false;
            bool isLoginSuccessful = AuthenticationManager.Login(usernameWithDomain, password, persist);

            if (isLoginSuccessful)
            {
                if (System.Convert.ToString(GetCustomProfilePropertyValue(usernameWithDomain, Constants.Profile.ProfileProperties.IsSignUpComplete)) != "1")
                {
                    isLoginSuccessful = false;
                    isSignUpPending = true;
                    SignOutUser();
                }
            }

            return isLoginSuccessful;
        }

        public void SignOutUser()
        {
            AuthenticationManager.Logout();
        }

        public object GetCustomProfilePropertyValue(string usernameWithDomain, string propertyName)
        {
            User user = GetUserByUsername(usernameWithDomain, false);
            return user != null ? user.Profile.GetCustomProperty(propertyName) : null;
        }

        public void SetCustomProfilePropertyValues(string usernameWithDomain, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (keyValuePairs != null && keyValuePairs.Count > 0)
            {
                User user = GetUserByUsername(usernameWithDomain, true);

                if (user != null)
                {
                    user.Profile.ProfileItemId = Constants.Profile.ProfileItemId;

                    using (new SecurityDisabler())
                    {
                        foreach (KeyValuePair<string, string> kvp in keyValuePairs)
                        {
                            user.Profile.SetCustomProperty(kvp.Key, kvp.Value);
                        }
                        user.Profile.Save();
                    }
                }
            }
        }

        public string ConvertEmailToUsernameWithDomain(string email, string domain)
        {
            ///create username
            string usernameWithDomain = email.Replace(".", "").Replace("-", "").Replace("_", "").Replace("@", "");
            usernameWithDomain = string.Format(@"{0}\{1}", domain, usernameWithDomain);
            return usernameWithDomain;
        }

        public bool UpdatePassword(string usernameWithDomain, string oldPassword, string newPassword)
        {
            bool isPasswordUpdated = false;
            MembershipUser user = Membership.GetUser(usernameWithDomain);

            if (user != null)
            {
                using (new SecurityDisabler())
                {
                    isPasswordUpdated = user.ChangePassword(oldPassword, newPassword);
                }
            }

            return isPasswordUpdated;
        }

        public bool ResetPassword(string usernameWithDomain, string newPassword, List<KeyValuePair<string, string>> keyValuePairs = null)
        {
            bool isPasswordUpdated = false;
            MembershipUser user = Membership.GetUser(usernameWithDomain);

            if (user != null)
            {
                string oldPassword = user.ResetPassword();
                using (new SecurityDisabler())
                {
                    isPasswordUpdated = user.ChangePassword(oldPassword, newPassword);

                    if (isPasswordUpdated && keyValuePairs != null && keyValuePairs.Count > 0)
                    {
                        SetCustomProfilePropertyValues(usernameWithDomain, keyValuePairs);
                    }
                }
            }

            return isPasswordUpdated;
        }
    }
}
