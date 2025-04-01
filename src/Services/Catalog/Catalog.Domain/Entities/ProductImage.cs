using ECommerce.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Entities
{
    public class ProductImage : Entity<int>
    {
        public int ProductId { get;private set; }
        public string ImageUrl { get; private set; }

        //IsMain
        public bool IsMain { get; private set; }
        //Sort Order
        public int SortOrder { get; private set; }

        //Navigation properties
        public Product? Product { get; private set; }
        protected ProductImage()
        {
        }

        public ProductImage(int productId, string imageUrl, bool isMain, int sortOrder)
        {
            //check productId:
            if (productId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(productId), "Ürün ID değeri geçersiz:");
            }

            //check imageUrl:
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new ArgumentNullException(nameof(imageUrl), "Resim URL değeri boş olamaz:");
            }

            ProductId = productId;
            ImageUrl = imageUrl;
            IsMain = isMain;
            SortOrder = sortOrder;
        }

        public void SetAsMainImage()
        {
            IsMain = true;
        }

        public void SetAsNonMainImage()
        {
            IsMain = false;
        }

        //Change Sort Order
        public void ChangeSortOrder(int newSortOrder)
        {
            if (newSortOrder < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newSortOrder), "Sıralama değeri 0'den küçük olamaz:");
            }
            SortOrder = newSortOrder;
        }

        //Change Image URL

        public void ChangeImageUrl(string newImageUrl)
        {
            if (string.IsNullOrWhiteSpace(newImageUrl))
            {
                throw new ArgumentNullException(nameof(newImageUrl), "Resim URL değeri boş olamaz:");
            }
            ImageUrl = newImageUrl;
        }


    }
}
