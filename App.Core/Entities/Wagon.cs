namespace App.Core.Entities
{
    public class Wagon
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }
        public int OccupiedSeats { get; set; }
    }
}
