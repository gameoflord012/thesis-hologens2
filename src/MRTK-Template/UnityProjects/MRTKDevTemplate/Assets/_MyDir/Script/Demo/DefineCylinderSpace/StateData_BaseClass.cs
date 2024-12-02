public abstract class StateData_BaseClass
{
    internal StateData_BaseClass()
    {
        Initialize();
    }

    /// <summary>
    /// Call by default constructor for initialize, and don't provide any constructor in implementation
    /// </summary>
    abstract protected void Initialize();
}
