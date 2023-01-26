class Household
    {
        private static Random rnd = new Random(); 
        protected double chanceEatOutPerDay;
        protected int xCoord, yCoord, ID;
        protected static int nextID = 1; 
        

        public Household(int x, int y)
        {
            xCoord = x;
            yCoord = y;
            chanceEatOutPerDay = rnd.NextDouble(); 
            
            ID = nextID;
            nextID++; 
        }
        
        public string GetDetails()
        {
            string details;
            details = ID.ToString() + "     Coordinates: (" + xCoord.ToString() + ", " + yCoord.ToString() + ")     Eat out probability: " + chanceEatOutPerDay.ToString();
            return details;
        }
        
        public double GetChanceEatOut()
        {
            return chanceEatOutPerDay;
        }
        
        public int GetX()
        {
            return xCoord;
        }
        
        public int GetY()
        {
            return yCoord;
        }
    } 