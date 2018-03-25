using System.Collections.Generic;

namespace CatalogueProducts
{
    public interface ICatalogueGateway
    {
        IEnumerable<Catalogue> RetrieveCatalogue();
    }
}