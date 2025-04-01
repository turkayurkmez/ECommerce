using ECommerce.Common.Domain;

namespace Catalog.Domain.Entities
{
    public class Brand : AuditableEntity<int>, IAggregateRoot
    {
        //Name, Description, IsActive, LOGO, and navigation properties
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }
        public string Logo { get; private set; } = string.Empty;

        //Navigation properties

        public ICollection<Product> Products { get; private set; } = new List<Product>();

        protected Brand()
        {
        }

        public Brand(string name, string description, string logo)
        {
            //check name:
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Marka adı boş olamaz:");
            }
          
          
          
            Name = name;
            Description = description;
            IsActive = true;
            Logo = logo;
        }

        //Update Basic Info

        public void UpdateBasicInfo(string name, string description)
        {
            //check name:
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Marka adı boş olamaz:");
            }
            //check description:
            Name = name;
            Description = description;
            SetModifiedDate();
        }

        //Update Logo:

        public void UpdateLogo(string logo)
        {
            //check logo:
            Logo = logo ?? string.Empty;
            SetModifiedDate();
        }

        //Activate Brand

        public void Activate()
        {
            IsActive = true;
            SetModifiedDate();
        }

        //Deactivate Brand

        public void Deactivate()
        {
            IsActive = false;
            SetModifiedDate();
        }




        }
}