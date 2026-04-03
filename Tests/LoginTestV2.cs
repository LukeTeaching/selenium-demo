using SeleniumTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Tests
{
    public class LoginTestV2 : BaseTest
    {
        public static IEnumerable<TestCaseData> InvalidLoginData()
        {
            yield return new TestCaseData("wronguser", "SuperSecretPassword!")
                .Returns("Your username is invalid!")
                .SetName("Login_With_Wrong_Username"); 

            yield return new TestCaseData("tomsmith", "wrongpassword")
                .Returns("Your password is invalid!")
                .SetName("Login_With_Wrong_Password");

            yield return new TestCaseData("", "")
                .Returns("Your username is invalid!")
                .SetName("Login_With_Empty_Credentials");
        }

        [TestCaseSource(nameof(InvalidLoginData))]
        public string Test_InvalidLogin_UsingDataSource(string username, string password)
        {
            var loginPage = new LoginPage(driver);

            loginPage.GoTo();
            loginPage.LoginAs(username, password);

            // Chỉ cần trả về text thực tế, NUnit lo phần Assert
            return loginPage.GetFlashMessage();
        }
    }
}
