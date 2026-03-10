namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface ISystemClocks
    {
        DateTime Now { get; }
    }
}
