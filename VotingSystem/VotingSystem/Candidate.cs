namespace VotingSystem
{
    public class Candidate
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return Id + ". " + Name; 
        }
    }
}
