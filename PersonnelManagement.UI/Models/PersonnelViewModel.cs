using System.Collections.Generic;

public class PersonnelViewModel
{
    // Static Fields
    public string Name { get; set; }
    public string PersonnelCode { get; set; }
    public string LastName { get; set; }

    // Dynamic Fields
    public Dictionary<long, string> DynamicFields { get; set; } // Key: Field ID, Value: Field Value
}
