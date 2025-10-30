namespace App.Core.Entities
{
    public class Train
    {
        public string Name { get; set; } = default!;
        public ICollection<Wagon> Wagons { get; set; } = new List<Wagon>();
    }
}
