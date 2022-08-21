using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace LoginSendEmailTests
{

    public class Tests
    {
        private IWebDriver driver;

        private readonly By _logInInputField = By.XPath("//input[@name='login']");
        private readonly By _passwordInputField = By.XPath("//input[@name='pass']");
        private readonly By _submitButton = By.XPath("//input[@type='submit']");
        private readonly By _writeEmail = By.XPath("//a[@class='make_message']");
        private readonly By _emailToNameField = By.XPath("//textarea[@name='to']");
        private readonly By _emailToThemeField = By.XPath("//input[@name='subject']");
        private readonly By _emailToTextField = By.XPath("//textarea[@id='text']");
        private readonly By _emailSendButton = By.XPath("//input[@class='bold']");
        private readonly By _confirmSendMessage = By.XPath("//div[@class='content clear']");        

        private const string _login = "test_testing";
        private const string _password = "Test1234";
        private const string _emailToName = "wherever.test@gmail.com";
        private const string _emailToTheme = "Test an email";
        private const string _emailToText = "Hello, Test! Happy testing! Good luck!";        
        private const string expectedSendMessage = "Лист успішно відправлено адресатам";


        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://passport.i.ua/login/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            var logInInput = driver.FindElement(_logInInputField);
            logInInput.SendKeys(_login);

            var passwordInput = driver.FindElement(_passwordInputField);
            passwordInput.SendKeys(_password);

            var submitButton = driver.FindElement(_submitButton);
            submitButton.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var div = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_writeEmail));

            var writeAnEmail = driver.FindElement(_writeEmail);
            writeAnEmail.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            var emailToNameInput = driver.FindElement(_emailToNameField);
            emailToNameInput.SendKeys(_emailToName);

            var emailToThemeInput = driver.FindElement(_emailToThemeField); 
            emailToThemeInput.SendKeys(_emailToTheme);
            
            var emailToTextInput = driver.FindElement(_emailToTextField);
            emailToTextInput.SendKeys(_emailToText);

            var emailSend = driver.FindElement(_emailSendButton);
            emailSend.Click();

            var actualSendMessage = driver.FindElement(_confirmSendMessage).Text;

            Assert.That(actualSendMessage, Is.EqualTo(expectedSendMessage));            
           
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}