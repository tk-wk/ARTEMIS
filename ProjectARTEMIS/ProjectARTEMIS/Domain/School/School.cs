
    public class School
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string ColorCode { get; private set; }

        private School()
        {
        }

        public static School Create(string name, string description, string colorcode)
        {

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("School name cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(colorcode))
                throw new DomainException("Color code cannot be null or empty.");

            return new School
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                Description = description.Trim(),
                ColorCode = colorcode.Trim()
            };
        }
        public void UpdateInformation(string newName, string newDescription, string color)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new DomainException("School name cannot be empty during an update.");

            Name = newName.Trim();
            Description = newDescription.Trim();
            ColorCode = color.Trim();
        }
    }

