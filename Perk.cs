public class Perk
{
    public PerkType id;
    private bool applied;
    public int cost;

    private string description;

    public string Description
    {
        get
        {
            if(!applied)
                return string.Format("{0}\nCost: {1}",description, cost);
            else
                return string.Format("{0}\n{1}", description, "Already bought");
        }

        set
        {
            description = value;
        }
    }

    public bool Applied
    {
        get
        {
            return applied;
        }

        set
        {
            applied = value;
        }
    }

    public Perk(PerkType id, int cost, string description="")
    {
        this.id = id;
        this.cost = cost;
        this.Description = description;
    }
}
