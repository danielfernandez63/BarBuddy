using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BarBuddy.Controllers
{
    public static class TwilioNotification
    {
        public static void TwilioMessage(string phoneNumber, string messageText)
        {

            string accountSid = MyKeys.GetAccountSidTwilio();
            string authToken = MyKeys.GetAuthTokenTwilio();

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: messageText,
                from: new Twilio.Types.PhoneNumber("+19206266861"),
                to: new Twilio.Types.PhoneNumber("+" + phoneNumber)
                );
        }

        public static void NotifyManagementInventory(string alcohol)
        {
            string phoneNumber = GetManagerNumber();
            var message = $"Please log in and check inventory, it has been reported as low. The reported type is:" + alcohol;
            TwilioMessage(phoneNumber, message);
        }

        public static string GetManagerNumber()
        {
            string ManagerNumber = "16086306751";
            return ManagerNumber;
        }
    }
}