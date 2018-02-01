namespace DevHub.DAL.Models
{
    public enum SpaceEnum
    {
        OpenSpace = 1,
        PrivateSpace = 2,
        ConferenceMeeting = 3
    }

    public enum BookStatusEnum
    {
        Pending,
        Confirmed,
        Forfeited
    }

    public enum BookingTypeEnum
    {
        Walkin,
        Facebook,
        DevHub
    }

    public enum FrequencyEnum
    {
        Hourly = 1,
        Daily = 2,
        Weekly = 3,
        Monthly = 4
    }

    public enum LogTypeEnum
    {
        TimeIn,
        TimeOut,
        NA
    }

    public enum ActionTypeEnum
    {
        Created = 1,
        Updated = 2
    }
}
