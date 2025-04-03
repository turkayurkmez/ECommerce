using System.ComponentModel;

namespace ECommerce.Catalog.Domain.Entities
{
    public enum ProductStatus
    {
        //Active, InActive, OutOfStock and Discontinued
        [Description("Aktif")]
        Active = 1,

        [Description("Pasif")]
        InActive = 2,

        [Description("Stokta Yok")]
        OutOfStock = 3,

        [Description("Satış Dışı")]
        Discontinued = 4

    }
}
