class Simulation
    {
        private static Random rnd = new Random();
        protected Settlement simulationSettlement;
        protected int noOfCompanies;
        protected double fuelCostPerUnit, baseCostForDelivery;
        protected List<Company> companies = new List<Company>();
        protected int daysPassed = 0;

        public Simulation()
        {
            fuelCostPerUnit = 0.0098;
            baseCostForDelivery = 100;
            string choice;
            Console.Write("Enter L for a large settlement, anything else for a normal size settlement: ");
            choice = Console.ReadLine();
            if (choice == "L")
            {
                int extraX, extraY, extraHouseholds;
                Console.Write("Enter additional amount to add to X size of settlement: ");
                extraX = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter additional amount to add to Y size of settlement: ");
                extraY = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter additional number of households to add to settlement: ");
                extraHouseholds = Convert.ToInt32(Console.ReadLine());
                simulationSettlement = new LargeSettlement(extraX, extraY, extraHouseholds);
            }
            else
            {
                simulationSettlement = new Settlement();
            }
            Console.Write("Enter D for default companies, anything else to add your own start companies: ");
            choice = Console.ReadLine();
            if (choice == "D")
            {
                noOfCompanies = 3;
                Company company1 = new Company("AQA Burgers", "fast food", 100000, 200, 203, fuelCostPerUnit, baseCostForDelivery);
                companies.Add(company1);
                companies[0].OpenOutlet(300, 987);
                companies[0].OpenOutlet(500, 500);
                companies[0].OpenOutlet(305, 303);
                companies[0].OpenOutlet(874, 456);
                companies[0].OpenOutlet(23, 408);
                companies[0].OpenOutlet(412, 318);
                Company company2 = new Company("Ben Thor Cuisine", "named chef", 100400, 390, 800, fuelCostPerUnit, baseCostForDelivery);
                companies.Add(company2);
                Company company3 = new Company("Paltry Poultry", "fast food", 25000, 800, 390, fuelCostPerUnit, baseCostForDelivery);
                companies.Add(company3);
                companies[2].OpenOutlet(400, 390);
                companies[2].OpenOutlet(820, 370);
                companies[2].OpenOutlet(800, 600);
            }
            else
            {
                Console.Write("Enter number of companies that exist at start of simulation: ");
                noOfCompanies = Convert.ToInt32(Console.ReadLine());
                for (int count = 1; count < noOfCompanies + 1; count++)
                {
                    AddCompany();
                }
            }
        }

        public void DisplayMenu()
        {
            Console.WriteLine("\n*********************************");
            Console.WriteLine("**********    MENU     **********");
            Console.WriteLine("*********************************");
            Console.WriteLine("1. Display details of households");
            Console.WriteLine("2. Display details of companies");
            Console.WriteLine("3. Modify company");
            Console.WriteLine("4. Add new company");
            Console.WriteLine("6. Advance to next day");
            Console.WriteLine("7. Advance multiple days");
            Console.WriteLine("Q. Quit");
            Console.Write("\n Enter your choice: ");
        }

        private void DisplayCompaniesAtDayEnd()
        {
            string details;
            Console.WriteLine("\n**********************");
            Console.WriteLine("***** Companies: *****");
            Console.WriteLine("**********************\n");
            foreach (var c in companies)
            {
                Console.WriteLine(c.GetName());
                Console.WriteLine();
                details = c.ProcessDayEnd();
                Console.WriteLine(details + "\n");
            }
        }
        
        private void ProcessAddHouseholdsEvent()
        {
            int NoOfNewHouseholds = rnd.Next(1, 5);
            for (int i = 1; i < NoOfNewHouseholds + 1; i++)
            {
                simulationSettlement.AddHousehold();
            }
            Console.WriteLine(NoOfNewHouseholds.ToString() + " new households have been added to the settlement.");
        }

        private void ProcessCostOfFuelChangeEvent()
        {
            double fuelCostChange = rnd.Next(1, 10) / 10.0;
            int upOrDown = rnd.Next(0, 2);
            int companyNo = rnd.Next(0, companies.Count);
            if (upOrDown == 0)
            {
                Console.WriteLine("The cost of fuel has gone up by " + fuelCostChange.ToString() + " for " + companies[companyNo].GetName());
            }
            else
            {
                Console.WriteLine("The cost of fuel has gone down by " + fuelCostChange.ToString() + " for " + companies[companyNo].GetName());
                fuelCostChange *= -1;
            }
            companies[companyNo].AlterFuelCostPerUnit(fuelCostChange);
        }

        private void ProcessReputationChangeEvent()
        {
            double reputationChange = rnd.Next(1, 10) / 10.0;
            int upOrDown = rnd.Next(0, 2);
            int companyNo = rnd.Next(0, companies.Count);
            if (upOrDown == 0)
            {
                Console.WriteLine("The reputation of " + companies[companyNo].GetName() + " has gone up by " + reputationChange.ToString());
            }
            else
            {
                Console.WriteLine("The reputation of " + companies[companyNo].GetName() + " has gone down by " + reputationChange.ToString());
                reputationChange *= -1;
            }
            companies[companyNo].AlterReputation(reputationChange);
        }

        private void ProcessCostChangeEvent()
        {
            double costToChange = rnd.Next(0, 2);
            int upOrDown = rnd.Next(0, 2);
            int companyNo = rnd.Next(0, companies.Count);
            double amountOfChange;
            if (costToChange == 0)
            {
                amountOfChange = rnd.Next(1, 20) / 10.0;
                if (upOrDown == 0)
                {
                    Console.WriteLine("The daily costs for " + companies[companyNo].GetName() + " have gone up by " + amountOfChange.ToString());
                }
                else
                {
                    Console.WriteLine("The daily costs for " + companies[companyNo].GetName() + " have gone down by " + amountOfChange.ToString());
                    amountOfChange *= -1;
                }
                companies[companyNo].AlterDailyCosts(amountOfChange);
            }
            else
            {
                amountOfChange = rnd.Next(1, 10) / 10.0;
                if (upOrDown == 0)
                {
                    Console.WriteLine("The average cost of a meal for " + companies[companyNo].GetName() + " has gone up by " + amountOfChange.ToString());
                }
                else
                {
                    Console.WriteLine("The average cost of a meal for " + companies[companyNo].GetName() + " has gone down by " + amountOfChange.ToString());
                    amountOfChange *= -1;
                }
                companies[companyNo].AlterAvgCostPerMeal(amountOfChange);
            }
        }

        private void DisplayEventsAtDayEnd()
        {
            Console.WriteLine("\n***********************");
            Console.WriteLine("*****   Events:   *****");
            Console.WriteLine("***********************\n");
            double eventRanNo;
            eventRanNo = rnd.NextDouble();
            if (eventRanNo < 0.25)
            {
                eventRanNo = rnd.NextDouble();
                if (eventRanNo < 0.25)
                {
                    ProcessAddHouseholdsEvent();
                }
                eventRanNo = rnd.NextDouble();
                if (eventRanNo < 0.5)
                {
                    ProcessCostOfFuelChangeEvent();
                }
                eventRanNo = rnd.NextDouble();
                if (eventRanNo < 0.5)
                {
                    ProcessReputationChangeEvent();
                }
                eventRanNo = rnd.NextDouble();
                if (eventRanNo >= 0.5)
                {
                    ProcessCostChangeEvent();
                }
            }
            else
            {
                Console.WriteLine("No events.");
            }
        }

        public void ProcessDayEnd()
        {
            daysPassed++;
            double totalReputation = 0;
            List<double> reputations = new List<double>();
            int companyRNo, current, loopMax, x = 0, y = 0;
            foreach (var c in companies)
            {
                c.NewDay();
                totalReputation += c.GetReputationScore();
                reputations.Add(totalReputation);
            }
            loopMax = simulationSettlement.GetNumberOfHouseholds() - 1;
            for (int counter = 0; counter < loopMax + 1; counter++)
            {
                if (simulationSettlement.FindOutIfHouseholdEatsOut(counter, ref x, ref y))
                {
                    companyRNo = rnd.Next(1, Convert.ToInt32(totalReputation) + 1);
                    current = 0;
                    while (current < reputations.Count)
                    {
                        if (companyRNo < reputations[current])
                        {
                            companies[current].AddVisitToNearestOutlet(x, y);
                            break;
                        }
                        current++;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("******************");
            Console.WriteLine("**** Day " + daysPassed.ToString().PadLeft(3,'0') + ": ****");
            Console.WriteLine("******************");
            DisplayCompaniesAtDayEnd();
            DisplayEventsAtDayEnd();
        }

        private void AddCompany()
        {
            int balance, x = 0, y = 0;
            string companyName, typeOfCompany = "9";
            Console.Write("Enter a name for the company: ");
            companyName = Console.ReadLine();
            Console.Write("Enter the starting balance for the company: ");
            balance = Convert.ToInt32(Console.ReadLine());
            while (typeOfCompany != "1" && typeOfCompany != "2" && typeOfCompany != "3")
            {
                Console.Write("Enter 1 for a fast food company, 2 for a family company or 3 for a named chef company: ");
                typeOfCompany = Console.ReadLine();
            }
            if (typeOfCompany == "1")
            {
                typeOfCompany = "fast food";
            }
            else if (typeOfCompany == "2")
            {
                typeOfCompany = "family";
            }
            else
            {
                typeOfCompany = "named chief";
            }
            simulationSettlement.GetRandomLocation(ref x, ref y);
            Company newCompany = new Company(companyName, typeOfCompany, balance, x, y, fuelCostPerUnit, baseCostForDelivery);
            companies.Add(newCompany);
        }

        public int GetIndexOfCompany(string companyName)
        {
            int index = -1;
            for (int current = 0; current < companies.Count; current++)
            {
                if (companies[current].GetName().ToLower() == companyName.ToLower())
                {
                    return current;
                }
            }
            return index;
        }

        public void ModifyCompany(int index)
        {
            string choice;
            int outletIndex, x, y;
            bool closeCompany;
            Console.WriteLine("\n*********************************");
            Console.WriteLine("*******  MODIFY COMPANY   *******");
            Console.WriteLine("*********************************");
            Console.WriteLine("1. Open new outlet");
            Console.WriteLine("2. Close outlet");
            Console.WriteLine("3. Expand outlet");
            Console.WriteLine("4. Remove company");
            Console.Write("\nEnter your choice: ");
            choice = Console.ReadLine();
            if (choice == "2" || choice == "3")
            {
                Console.Write("Enter ID of outlet: ");
                outletIndex = Convert.ToInt32(Console.ReadLine());
                if (outletIndex > 0 && outletIndex <= companies[index].GetNumberOfOutlets())
                {
                    if (choice == "2")
                    {
                        closeCompany = companies[index].CloseOutlet(outletIndex - 1);
                        if (closeCompany)
                        {
                            Console.WriteLine("That company has now closed down as it has no outlets.");
                            companies.RemoveAt(index);
                        }
                    }
                    else
                    {
                        companies[index].ExpandOutlet(outletIndex - 1);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid outlet ID.");
                }
            }
            else if (choice == "1")
            {
                Console.Write("Enter X coordinate for new outlet: ");
                x = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Y coordinate for new outlet: ");
                y = Convert.ToInt32(Console.ReadLine());
                if (x >= 0 && x < simulationSettlement.GetXSize() && y >= 0 && y < simulationSettlement.GetYSize())
                {
                    companies[index].OpenOutlet(x, y);
                }
                else
                {
                    Console.WriteLine("Invalid coordinates.");
                }
            }
            else if (choice == "4")
            {
                for (int i = 0; i < companies[index].GetNumberOfOutlets();i++)
                {
                    closeCompany = companies[index].CloseOutlet(1);
                    if (closeCompany)
                    {
                        companies.RemoveAt(index);
                    }
                }
            }
            Console.WriteLine();
        }

        public void DisplayCompanies()
        {
            Console.WriteLine("\n*********************************");
            Console.WriteLine("*** Details of all companies: ***");
            Console.WriteLine("*********************************\n");
            foreach (var c in companies)
            {
                Console.WriteLine(c.GetDetails() + "\n");
            }
            Console.WriteLine();
        }

        public void proccessMultipleDays(int days)
        {
            for (int i = 0; i < days; i++)
            {
                ProcessDayEnd();
            }
        }

        public void Run()
        {
            int numOfDays = 0;
            string choice = "";
            int index;
            while (choice != "Q")
            {
                DisplayMenu();
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        simulationSettlement.DisplayHouseholds();
                        break;
                    case "2":
                        DisplayCompanies();
                        break;
                    case "3":
                        string companyName;
                        index = -1;
                        while (index == -1)
                        {
                            Console.Write("Enter company name: ");
                            companyName = Console.ReadLine();
                            index = GetIndexOfCompany(companyName);
                        }
                        ModifyCompany(index);
                        break;
                    case "4":
                        AddCompany();
                        break;
                    case "6":
                        ProcessDayEnd();
                        break;
                    case "7":
                        Console.Write("Enter the number of days: ");
                        numOfDays = Convert.ToInt32(Console.ReadLine());
                        proccessMultipleDays(numOfDays);
                        break;

                    case "Q":
                        Console.WriteLine("Simulation finished, press Enter to close.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }