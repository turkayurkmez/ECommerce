using ECommerce.Common.Domain;

namespace Catalog.Domain.Entities
{
    public class Category : AuditableEntity<int>, IAggregateRoot
    {
        //Name, Description, ParentCategoryId (nullable), Level, IsActive and navigation properties

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public int? ParentCategoryId { get; private set; }
        public int Level { get; private set; }

        public bool IsActive { get; private set; }

        //Navigation properties
        public Category? ParentCategory { get; private set; }
        //Child Categories
       public ICollection<Category> SubCategories { get; private set; }  = new List<Category>();
        //Products Navigation Property:
        public ICollection<Product> Products { get; private set; } = new List<Product>();

        protected Category()
        {
        }

        public Category(string name, string description, int? parentCategoryId, int level=1)
        {
            //check name:
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Kategori adı boş olamaz:");
            }
            //check description:
        
            //check level:
        
            Name = name;
            Description = description;
            ParentCategoryId = parentCategoryId;
            Level = level;
            IsActive = true;
        }

        //Update Basic Info

        public void UpdateBasicInfo(string name, string description)
        {
            //check name:
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Kategori adı boş olamaz:");
            }
            //check description:

            Name = name;
            Description = description;
            SetModifiedDate();
        }

        //Change Parent Category with level

        public void ChangeParentCategory(int? parentCategoryId, int level)
        {
            ParentCategoryId = parentCategoryId;
            Level = level;
            SetModifiedDate();
        }

        //Activate Category

        public void Activate()
        {
            IsActive = true;
            SetModifiedDate();
        }

        //Deactivate Category
        public void Deactivate()
        {
            IsActive = false;
            SetModifiedDate();
        }











        }
}