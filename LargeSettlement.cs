class LargeSettlement : Settlement
    {
        public LargeSettlement(int extraXSize, int extraYSize, int extraHouseholds)
            : base()
        {
            xSize += extraXSize;
            ySize += extraYSize;
            startNoOfHouseholds += extraHouseholds;
            for (int count = 1; count < extraHouseholds + 1; count++)
            {
                AddHousehold();
            }
        }
    }