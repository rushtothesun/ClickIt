namespace ClickIt;

public record ClickItRule(string Name, string Location, bool Enabled)
{
    public bool Enabled = Enabled;
}
