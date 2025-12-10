
using Atata;
using NUnit.Framework;

namespace UITest
{

    [Url("/login")] 
    public class LoginPage : Page<LoginPage>
    {
        //rest
        [FindByXPath("/html/body/div[1]/div/div/form/div[1]/div/div[1]/div/input")]
        public TextInput<LoginPage> UserName { get; private set; }

        [FindByXPath("/html/body/div[1]/div/div/form/div[2]/div/div[1]/div/input")]
        public PasswordInput<LoginPage> Password { get; private set; }

        [FindByXPath("/html/body/div[1]/div/div/form/div[3]/div/button")]
        public Button<LoginPage> Login { get; private set; }
    }

    [Url("/")] // 假設成功登入後導航到 "/dashboard"
    public class DashboardPage : Page<DashboardPage>
    {
        [FindByXPath("/html/body/div[1]/div/section/section/header/div[1]/span/span[1]")]
        public Text<DashboardPage> HeaderTitle { get; private set; }
    }

    [TestFixture]
    public class Login
    {
        [SetUp]
        public void Setup()
        {
            //ApiChecker.WaitForApiToStart();
            AtataContext.Configure()
                .UseChrome() // 指定使用 Chrome 瀏覽器
                .WithArguments("start-maximized", "--ignore-certificate-errors") // 添加瀏覽器參數
                .UseBaseUrl("http://localhost:5175") // 配置 Vue 3 的基址
                .Build();
        }

        [Test]
        public void LoginTest()
        {
            var login = Go.To<LoginPage>()
                        .UserName.Set("admin")
                        .Password.Set("admin")
                        .Login.ClickAndGo<DashboardPage>();

            login.HeaderTitle.WaitTo.BeVisible();

            login.HeaderTitle.Should.Contain("首頁");
        }

        [TearDown]
        public void TearDown()
        {
            AtataContext.Current?.Dispose();
        }
    }
}
