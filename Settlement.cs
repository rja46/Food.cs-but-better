class Settlement
    {
        private static Random rnd = new Random();
        protected int startNoOfHouseholds, xSize, ySize;
        protected List<Household> households = new List<Household>(); 
        
        public Settlement()
        {
            xSize = 1000;
            ySize = 1000;
            startNoOfHouseholds = 250;
            CreateHouseholds();
        }

        public int GetNumberOfHouseholds()
        {
            return households.Count;
        }
        
        public int GetXSize()
        {
            return xSize;
        }
        
        public int GetYSize()
        {
            return ySize;
        }
        
        public void GetRandomLocation(ref int x, ref int y)
        {
            x = Convert.ToInt32(rnd.NextDouble() * xSize);
            y = Convert.ToInt32(rnd.NextDouble() * ySize);
        }
        
        public void CreateHouseholds()
        {
            for (int count = 0; count < startNoOfHouseholds; count++)
            {
                AddHousehold();
            }
        }

        public void AddHousehold()
        {
            int x = 0, y = 0;
            GetRandomLocation(ref x, ref y);
            Household temp = new Household(x, y);
            households.Add(temp);
        }
        public void DisplayHouseholds()
        {
            Console.WriteLine("\n**********************************");
            Console.WriteLine("*** Details of all households: ***");
            Console.WriteLine("**********************************\n");
            foreach (var h in households)
            {
                Console.WriteLine(h.GetDetails());
            }
            Console.WriteLine();
        }
        public bool FindOutIfHouseholdEatsOut(int householdNo, ref int x, ref int y)
        {
            double eatOutRNo = rnd.NextDouble();
            x = households[householdNo].GetX();
            y = households[householdNo].GetY();
            if (eatOutRNo < households[householdNo].GetChanceEatOut())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }