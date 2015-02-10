using OpenQA.Selenium;
using OpenQA;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System.Collections;
using System.IO;

namespace AutomationFramework
{
    class TestFrameworkDriver
    {
        //Variables
        private int step = 1;
        private bool stepPassed = true;

        //objects
        private static TestFrameworkDriver testDriver = new TestFrameworkDriver();
        private IWebDriver browserDriver;
        private ArrayList recordsteps = new ArrayList();


        public static void Main(string[] args)
        {

            testDriver.browserContructor("firefox");
            testDriver.Wait(testDriver.browserDriver);
            testDriver.Connect("http://www.target.com/", testDriver.browserDriver);
            /*
            testDriver.testBookaFlightNormal(testDriver.browserDriver);
            testDriver.writeResults(@"D:\Users\Louis Land\SCHOOL DOCUMENTS\Test results\TestResults.txt");

            testDriver.Connect("http://newtours.demoaut.com/", testDriver.browserDriver);

            testDriver.testBookaFlightValidation(testDriver.browserDriver);
            testDriver.TearDown(testDriver.browserDriver);
            testDriver.writeResults(@"D:\Users\Louis Land\SCHOOL DOCUMENTS\Test results\TestResults2.txt");
        */
            testDriver.bestBuyFinding(testDriver.browserDriver);
            testDriver.bestBuyAdding(testDriver.browserDriver);
            
        }

        //-----------------------------------------------------------------------------------------------------------------
        //The methods below are the connection and quit methods

        //This method will pass in a string
        //This string is used to tell the driver to connect where
        public void Connect(String connection, IWebDriver driver)
        {

            driver.Navigate().GoToUrl(connection);
        }

        public void Wait(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }


        //Just a simple constructor to which browser you will be using
        public void browserContructor(String caseSelector)
        {

            switch (caseSelector.ToUpper())
            {
                case "FIREFOX":
                    browserDriver = new FirefoxDriver();
                    break;

                case "CHROME":
                    browserDriver = new ChromeDriver();
                    break;

                case "IE":
                    browserDriver = new InternetExplorerDriver();
                    break;
            }
        }


        // This closes the driver down after the test has finished.
        public void TearDown(IWebDriver driver)
        {
            driver.Quit();
            stepReset();
        }


        //resets the step counter 
        public void stepReset()
        {
            step = 1;
        }

        //Clears the arraylist
        public void clearRecord()
        {
            recordsteps.Clear();
        }


        //-----------------------------------------------------------------------------------------------------------------------------
        //Expaction handling when an element is not found
        public void elementFailure()
        {
            String record = ("Step " + step + " failed because of the element does not exsist or the wrong element name was entered");
            Console.WriteLine(record);
            recordSteps(record);
            stepPassed = false;
        }


        //-----------------------------------------------------------------------------------------------------------------------------
        //Test validation feedback
        //Finish out to interact with a Test Management program to report automated tests

        //This allows the tester to know which steps that failed
        public void stepFailure(bool result)
        {

            String record = "Step " + step;

            if (stepPassed)
            {
                if (!result)
                {
                    record = record + " Failed";
                    Console.WriteLine(record);
                    recordSteps(record);
                }
                else
                {
                    record = record + " Passed";
                    Console.WriteLine(record);
                    recordSteps(record);
                }
            }
            else
            {
                stepPassed = true;
            }

            step++;
        }

        //--------------------------------------------------------------------------------------------------------------
        //This secton will state in the main driver of the automation framework
        //This is the recorder of the steps results section

        public void writeResults(String location)
        {
            System.IO.File.WriteAllLines(location, recordsteps.Cast<string>());
            clearRecord();
            step = 1;
        }

        //Adds the results of each step to a file. 
        public void recordSteps(String record)
        {
            recordsteps.Add(record);

        }


        //-------------------------------------------------------------------------------------------------------------
        //Below is a test script

        //This test will ensure the user can book a flight
        public void testBookaFlightNormal(IWebDriver driver)
        {
            String currentURL = driver.Url;

            //Step 1: Entering in the login information
            interactTextFieldName(driver, "userName", "mercury");
            stepFailure(validStringText(driver, "userName", "mercury"));


            //This will enter in password for the login
            interactTextFieldName(driver, "password", "mercury");


            //Step 2: This will click the Sign-In
            interactNaviationButton(driver, "login");
            stepFailure(validNavigation(driver, currentURL));


            //Step 3: Clicks the continue button on the Flight Finder
            currentURL = driver.Url;
            interactNaviationButton(driver, "findFlights");
            stepFailure(validNavigation(driver, currentURL));


            //Step 4: clicks the continue button on the Select Flight Page
            currentURL = driver.Url;
            interactNaviationButton(driver, "reserveFlights");
            stepFailure(validNavigation(driver, currentURL));


            //Step 5: This is where the testing begins,
            //Than click the Secure Purchase button
            interactTextFieldName(driver, "passFirst0", "Buffy");
            stepFailure(validStringText(driver, "passFirst0", "Buffy"));


            //Step 6: Last Name passangers
            interactTextFieldName(driver, "passLast0", "Summers");
            stepFailure(validStringText(driver, "passLast0", "Summers"));


            //Step 7: checking the meal drop down
            interactDropDown(driver, "pass.0.meal", "Hindu");
            stepFailure(validDropDown(driver, "pass.0.meal", "Hindu"));


            //Step 8: Checking the credit card type
            interactDropDown(driver, "creditCard", "American Express");
            stepFailure(validDropDown(driver, "creditCard", "American Express"));


            //Step 9: Checking the credit card number
            interactTextFieldName(driver, "creditnumber", "123456789123456");
            stepFailure(validStringText(driver, "creditnumber", "123456789123456"));


            //Step 10: Checking expiration date month
            interactDropDown(driver, "cc_exp_dt_mn", "03");
            stepFailure(validDropDown(driver, "cc_exp_dt_mn", "03"));


            //Step 11: Checking expiration date your
            interactDropDown(driver, "cc_exp_dt_yr", "2010");
            stepFailure(validDropDown(driver, "cc_exp_dt_yr", "2010"));


            //Step 12: Checking the Billing Address
            interactTextFieldName(driver, "billAddress1", "143 S. Main");
            stepFailure(validStringText(driver, "billAddress1", "143 S. Main"));


            //Step 13: Checking the Billing City
            interactTextFieldName(driver, "billCity", "Salt Lake City");
            stepFailure(validStringText(driver, "billCity", "Salt Lake City"));


            //Checking the Billing State
            interactTextFieldName(driver, "billState", "UT");
            stepFailure(validStringText(driver, "billState", "UT"));


            //Checking the Billing Zip
            interactTextFieldName(driver, "billZip", "84111");
            stepFailure(validStringText(driver, "billZip", "84111"));


            //Checking Billing Country
            interactDropDown(driver, "billCountry", "UNITED STATES");
            stepFailure(validDropDown(driver, "billCountry", "UNITED STATES"));


            //Checking the Delivery Address
            interactTextFieldName(driver, "delAddress1", "143 S. Main");
            stepFailure(validStringText(driver, "delAddress", "143 S. Main"));


            //Checking the Delivery City
            interactTextFieldName(driver, "delCity", "Salt Lake City");
            stepFailure(validStringText(driver, "delCity", "Salt Lake City"));


            //Checking the Delivery State
            interactTextFieldName(driver, "delState", "UT");
            stepFailure(validStringText(driver, "delState", "UT"));


            //Checking the Delivery Zip
            interactTextFieldName(driver, "delZip", "84111");
            stepFailure(validStringText(driver, "delZip", "84111"));


            //Checking Delivery Country
            interactDropDown(driver, "delCountry", "UNITED STATES");
            stepFailure(validDropDown(driver, "delCountry", "UNITED STATES"));

            //Lets get ourselves a ticket!
            currentURL = driver.Url;
            interactNaviationButton(driver, "buyFlights");
            stepFailure(validNavigation(driver, currentURL));
        }


        //This test will check to see if the fields have any test validation
        public void testBookaFlightValidation(IWebDriver driver)
        {
            String currentURL = driver.Url;

            //Step 1: Entering in the login information
            interactTextFieldName(driver, "userName", "mercury");
            stepFailure(validStringText(driver, "userName", "mercury"));


            //Step 2: This will enter in password for the login
            interactTextFieldName(driver, "password", "mercury");


            //Step 3: This will click the Sign-In
            interactNaviationButton(driver, "login");
            stepFailure(validNavigation(driver, currentURL));


            //Step 4: Clicks the continue button on the Flight Finder
            currentURL = driver.Url;
            interactNaviationButton(driver, "findFlights");
            stepFailure(validNavigation(driver, currentURL));


            //Step 5: clicks the continue button on the Select Flight Page
            currentURL = driver.Url;
            interactNaviationButton(driver, "reserveFlights");
            stepFailure(validNavigation(driver, currentURL));


            //Step 6: This is where the testing begins,
            //Than click the Secure Purchase button
            interactTextFieldName(driver, "passFirst0", "Buffy@#%@");
            stepFailure(validOnlyAlphaNum(driver, "passFirst0"));


            //Step 7: Last Name passangers
            interactTextFieldName(driver, "passLast0", "Summers#$%#");
            stepFailure(validOnlyAlphaNum(driver, "passLast0"));


            //Step 8: checking the meal drop down
            interactDropDown(driver, "pass.0.meal", "Hindu");
            stepFailure(validDropDown(driver, "pass.0.meal", "Hindu"));


            //Checking the credit card type
            interactDropDown(driver, "creditCard", "American Express");
            stepFailure(validDropDown(driver, "creditCard", "American Express"));


            //Checking the credit card number
            interactTextFieldName(driver, "creditnumber", "123456789123456@#$aFA");
            stepFailure(validOnlyNum(driver, "creditnumber"));


            //Checking expiration date month
            interactDropDown(driver, "cc_exp_dt_mn", "03");
            stepFailure(validDropDown(driver, "cc_exp_dt_mn", "03"));


            //Checking expiration date your
            interactDropDown(driver, "cc_exp_dt_yr", "2010");
            stepFailure(validDropDown(driver, "cc_exp_dt_yr", "2010"));


            //Checking the Billing Address
            interactTextFieldName(driver, "billAddress1", "143 S. Main");
            stepFailure(validStringText(driver, "billAddress1", "143 S. Main"));


            //Checking the Billing City
            interactTextFieldName(driver, "billCity", "Salt Lake City");
            stepFailure(validStringText(driver, "billCity", "Salt Lake City"));


            //Checking the Billing State
            interactTextFieldName(driver, "billState", "UT#$13");
            stepFailure(validOnlyAlpha(driver, "billState"));
            Console.WriteLine("Billing State Step is " + step);

            //Checking the Billing Zip
            interactTextFieldName(driver, "billZip", "84111@#$AD");
            stepFailure(validOnlyNum(driver, "billZip"));


            //Checking Billing Country
            interactDropDown(driver, "billCountry", "UNITED STATES");
            stepFailure(validDropDown(driver, "billCountry", "UNITED STATES"));


            //Checking the Delivery Address
            interactTextFieldName(driver, "delAddress1", "143 S. Main");
            stepFailure(validStringText(driver, "delAddress1", "143 S. Main"));


            //Checking the Delivery City
            interactTextFieldName(driver, "delCity", "Salt Lake City");
            stepFailure(validStringText(driver, "delCity", "Salt Lake City"));


            //Checking the Delivery State
            interactTextFieldName(driver, "delState", "UT@#$ASD");
            stepFailure(validOnlyAlpha(driver, "delState"));


            //Checking the Delivery Zip
            interactTextFieldName(driver, "delZip", "84111@#$AD");
            stepFailure(validOnlyNum(driver, "delZip"));


            //Checking Delivery Country
            interactDropDown(driver, "delCountry", "UNITED STATES");
            stepFailure(validDropDown(driver, "delCountry", "UNITED STATES"));

            //Lets get ourselves a ticket!
            currentURL = driver.Url;
            interactNaviationButton(driver, "buyFlights");
            stepFailure(validNavigation(driver, currentURL));
        }

        //This script will test to see if an item can be searched for and clicked on
        public void bestBuyFinding(IWebDriver driver)
        {
            //getting the current url 
            String url = driver.Url;

            //checking to see if the url changed when the item was searched for
            interactTextFieldID(driver, "searchTerm", "flash drive");
            stepFailure(validNavigation(driver, url));
            url = driver.Url;

            //This will valid that the search actaully found the correct products
            interactNaviationLink(driver, "prodTitle-slp_medium-1-1");

        }

        //This script will test if I can add items to my cart
        public void bestBuyAdding(IWebDriver driver)
        {
            interactNaviationButton(driver, "addToCart");
            interactNaviationButton(driver, "checkOutLink");
        }

        //------------------------------------------------------------------------------------------------------------------------------
        //methods below are used to interact with the element on the webpage

        //this methods would be used to fill out a text field in the website
        /**
         * @Input
         * - driver = 
         * - element = 
         **/
        public void interactTextFieldName(IWebDriver driver, String element, String testData)
        {

            try
            {
                driver.FindElement(By.Name(element)).Clear();
                driver.FindElement(By.Name(element)).SendKeys(testData);
            }
            catch (NoSuchElementException)
            {
                elementFailure();
            }
        }

        //this methods would be used to fill out a text field in the website
        /**
         * @Input
         * - driver = 
         * - element = 
         **/
        public void interactTextFieldID(IWebDriver driver, String element, String testData)
        {

            try
            {
                driver.FindElement(By.Id(element)).Clear();
                driver.FindElement(By.Name(element)).SendKeys(testData);
                driver.FindElement(By.Id(element)).Submit();
            }
            catch (NoSuchElementException)
            {
                elementFailure();
            }
        }

        //This methods selects an option from a drop down menu by name
        public void interactDropDown(IWebDriver driver, String element, String testData)
        {

            try
            {
                SelectElement oSelection = new SelectElement(driver.FindElement(By.Name(element)));
                oSelection.SelectByText(testData);
            }
            catch (NoSuchElementException)
            {
                elementFailure();
            }
        }

        //This method clicks a button
        public void interactNaviationButton(IWebDriver driver, String element)
        {

            try
            {
                driver.FindElement(By.Name(element)).Click();
            }

            catch (NoSuchElementException)
            {
                elementFailure();
            }
        }

        //Navigate a button with href
        public void interactNaviationButtonHref(IWebDriver driver, String element)
        {

            try
            {
                driver.FindElement(By.CssSelector(element)).Click();
            }

            catch (NoSuchElementException)
            {
                elementFailure();
            }
        }

        //This method clicks a navigation link
        public void interactNaviationLink(IWebDriver driver, String element)
        {

            try
            {
                driver.FindElement(By.Id(element)).Click();
            }
            catch (NoSuchElementException)
            {
                elementFailure();
            }
        }


        //----------------------------------------------------------------------------------------------------------------------------
        //Methods below check text fields if they contain a numeric, char or special charcters

        //this medthod checks if it stayed the same
        public bool validStringText(IWebDriver driver, String element, String testData)
        {

            bool hasStr;

            try
            {
                String actual = driver.FindElement(By.Id(element)).GetAttribute("value");
                hasStr = String.Equals(actual, testData);
            }
            catch (NoSuchElementException)
            {
                hasStr = false;
                elementFailure();
            }
            return hasStr;

        }


        //This method checks for special symbols
        public bool specTextCheck(IWebDriver driver, String element)
        {

            bool hasSpecial;

            try
            {
                hasSpecial = driver.FindElement(By.Id(element)).GetAttribute("value").Any(char.IsSymbol);
            }
            catch (NoSuchElementException)
            {
                hasSpecial = true;
                elementFailure();
            }
            return hasSpecial;
        }


        //This method checks for numeric
        public bool numTextCheck(IWebDriver driver, String element)
        {

            bool hasDigit;

            try
            {
                hasDigit = driver.FindElement(By.Id(element)).GetAttribute("value").Any(char.IsDigit);
            }
            catch (NoSuchElementException)
            {
                hasDigit = true;
                elementFailure();
            }
            return hasDigit;
        }

        //---------------------------------------------------------------------------------------------------------------------
        //Methods below use check text methods to see if the text fields run character validation
        //For example, the validNumOnly would be use for postal code fields to ensure the text fields only accepts numeric values

        //This method is used to check if inputs do a numeric data validation
        public bool charTextCheck(IWebDriver driver, String element)
        {
            bool hasLetter;

            try
            {
                hasLetter = driver.FindElement(By.Id(element)).GetAttribute("value").Any(char.IsLetter);
            }
            catch (NoSuchElementException)
            {
                hasLetter = true;
                elementFailure();
            }
            return hasLetter;
        }


        //A valid method to validate text field did not allow for the entry of numeric or special charaters
        public bool validOnlyAlpha(IWebDriver driver, String element)
        {

            bool onlyAlpha = false;

            try
            {
                if (numTextCheck(driver, element) || specTextCheck(driver, element))
                {
                    onlyAlpha = true;
                }
            }
            catch (NoSuchElementException)
            {
                elementFailure();
            }
            return onlyAlpha;
        }

        //A valid method to check if the fields does not allow special characters
        public bool validOnlyAlphaNum(IWebDriver driver, String element)
        {

            bool onlyAlphaNum = false;

            try
            {
                if (numTextCheck(driver, element))
                {
                    onlyAlphaNum = true;
                }
            }
            catch (NoSuchElementException)
            {
                elementFailure();
            }
            return onlyAlphaNum;
        }


        //A valid method to validate text field did not allow for the entry of numeric or special charaters
        public bool validOnlyNum(IWebDriver driver, String element)
        {

            bool onlyNum = false;

            try
            {
                if (specTextCheck(driver, element) || charTextCheck(driver, element))
                {
                    onlyNum = true;
                }
            }
            catch (NoSuchElementException)
            {
                elementFailure();
            }
            return onlyNum;
        }

        //-------------------------------------------------------------------------------------------------------------------------
        //This method belongs in the button class, checks if the webpage changed

        //This is used to valid for a button when click changes the page to the new link.
        // The boolean is set to true, if the links do not match, the validation is passed and is true
        //If the links are the same, it will return false, the validation is failed
        public bool validNavigation(IWebDriver driver, String testData)
        {

            bool diffURL = false;

            try
            {
                String actual = driver.Url;
                if (!String.Equals(actual, testData))
                {
                    diffURL = true;
                }
            }
            catch (NoSuchElementException)
            {
                elementFailure();
            }
            return diffURL;
        }

        //---------------------------------------------------------------------------------------------------------------------
        //This methods belongs in the drop down class, check if the correct value was selected in the drop down

        //Check the value in a drop down
        public bool validDropDown(IWebDriver driver, String element, String testData)
        {

            bool dropData;
            try
            {
                SelectElement oSelection = new SelectElement(driver.FindElement(By.Name(element)));
                dropData = testData.Equals(oSelection.SelectedOption.Text);
            }
            catch (NoSuchElementException)
            {
                dropData = false;
                elementFailure();
            }
            return dropData;

        }
    }
}
