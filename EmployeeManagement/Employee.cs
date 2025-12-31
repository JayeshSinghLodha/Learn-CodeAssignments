public class Employee
{
    public int Id { get; }
    public string Name { get; }
    public string Department { get; }

    private bool isWorking;

    public Employee(int id, string name, string department, bool isWorking)
    {
        Id = id;
        Name = name;
        Department = department;
        this.isWorking = isWorking;
    }

    public void Terminate()
    {
        isWorking = false;
    }

    public bool IsWorking()
    {
        return isWorking;
    }
}