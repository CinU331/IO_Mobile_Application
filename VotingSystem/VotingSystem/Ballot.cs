using System.Collections.Generic;

namespace VotingSystem
{
    public class Ballot
    {
        public string name;
        public bool state;
        public string candidatesSize;
        public int id;

        public override string ToString()
        {
            return name;
        }
    }
}
