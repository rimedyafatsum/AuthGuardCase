namespace AuthGuardCase.Services.Employee.Settings
{
    public interface IDatabaseSettings
    {
        public string EmployeeCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}