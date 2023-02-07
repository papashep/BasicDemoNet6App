namespace BasicDemoNet6.Options;

public class EmailSettingsOptions
{
    public bool EnableEmailSystem { get; set; }
    public int EmailTimeoutInSeconds { get; set; }
    public List<string> EmailServers { get; set; } = new List<string>();
    public AdminOptions EmailAdmin { get; set; } = new AdminOptions();

    // Must have a constructor that does not take in any parameters
    // If you do not specify it C# will presume it exists, exactly as shown below.
    // This allows for instantiation of the class.
    public EmailSettingsOptions()
    {

    }
}


