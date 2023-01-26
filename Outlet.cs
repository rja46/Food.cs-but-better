class Outlet
    {
        private static Random rnd = new Random();
        protected int visitsToday, xCoord, yCoord, capacity, maxCapacity;
        protected double dailyCosts;

        public Outlet(int xCoord, int yCoord, int maxCapacityBase)
        {
            this.xCoord = xCoord;
            this.yCoord = yCoord;
            capacity = Convert.ToInt32(maxCapacityBase * 0.6);
            maxCapacity = maxCapacityBase + Convert.ToInt32(rnd.NextDouble() * 50) - Convert.ToInt32(rnd.NextDouble() * 50);
            dailyCosts = maxCapacityBase * 0.2 + capacity * 0.5 + 100;
            NewDay();
        }
        
        public int GetCapacity()
        {
            return capacity;
        }

        public int GetX()
        {
            return xCoord;
        }

        public int GetY()
        {
            return yCoord;
        }

        public void AlterDailyCost(double amount)
        {
            dailyCosts += amount;
        }

        public int AlterCapacity(int change)
        {
            int oldCapacity = capacity;
            capacity += change;
            if (capacity > maxCapacity)
            {
                capacity = maxCapacity;
                return maxCapacity - oldCapacity;
            }
            else if (capacity < 0)
            {
                capacity = 0;
            }
            dailyCosts = maxCapacity * 0.2 + capacity * 0.5 + 100;
            return change;
        }

        public void IncrementVisits()
        {
            visitsToday++;
        }

        public void NewDay()
        {
            visitsToday = 0;
        }

        public double CalculateDailyProfitLoss(double avgCostPerMeal, double avgPricePerMeal)
        {
            return (avgPricePerMeal - avgCostPerMeal) * visitsToday - dailyCosts;
        }

        public string GetDetails()
        {
            string details = "";
            details = "Coordinates: (" + xCoord.ToString() + ", " + yCoord.ToString() + ")     Capacity: " + capacity.ToString() + "      Maximum Capacity: ";
            details += maxCapacity.ToString() + "      Daily Costs: " + dailyCosts.ToString() + "      Visits today: " + visitsToday.ToString();
            return details;
        }
    }