
    public class SocialMedia
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;

        private SocialMedia()
        {
            
        }

        public static SocialMedia Create(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new DomainException("Name cannot be empty.");

            return new SocialMedia
            {
                Id = Guid.NewGuid(),
                Name = name,
            };
        }

        public void UpdateInformation(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new DomainException("Name cannot be empty.");

            Name = name;
        }


    }

