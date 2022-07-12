namespace MatchMake.Backend.Storage
{
    public abstract class Entity<TId>
    {

        public TId Id { get; set; }
        
        // public bool IsNew() { return Id == -1; }

        //protected Entity()
        //{
        //    Id = -1;
        //}

    }
}