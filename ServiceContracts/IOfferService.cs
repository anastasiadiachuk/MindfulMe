using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IOfferService
    {
        // Create
        void CreateOffer(Offer offer);

        // Read
        Offer GetOfferById(int offerId);
        List<Offer> GetAllOffers();

        // Update
        void UpdateOffer(Offer updatedOffer);

        // Delete
        void DeleteOffer(int offerId);
    }
}
