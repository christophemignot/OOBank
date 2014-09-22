namespace OOBankTest
{
    using System;

    public interface IDateProvider
    {
        DateTime GetDate();
    }

    public class DefaultDateProvider : IDateProvider
    {
        public DateTime GetDate()
        {
            return DateTime.Now;
        }
    }

    public class MockDateProvider : IDateProvider
    {
        public DateTime innerDate = new DateTime(2014, 02, 01, 08, 0, 0);
        public DateTime GetDate()
        {
            innerDate = innerDate.AddHours(1);
            return innerDate;
        }
    }
}