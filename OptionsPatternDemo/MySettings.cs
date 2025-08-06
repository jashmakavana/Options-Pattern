namespace OptionsPatternDemo;

public class MySettings
{
    public string MySetting { get; set; }
    public bool IsValid() => !string.IsNullOrEmpty(MySetting);
}
