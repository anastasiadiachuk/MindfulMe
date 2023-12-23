using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts;

namespace Services
{
    public class OfferService : IOfferService
    {
        private readonly ProgramDbContext _dbContext;

        public OfferService(ProgramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateOffer(Offer offer)
        {
            _dbContext.Offers.Add(offer);
            _dbContext.SaveChanges();
        }

        public Offer GetOfferById(int offerId)
        {
            return _dbContext.Offers.Find(offerId);
        }

        public List<Offer> GetAllOffers()
        {
            return _dbContext.Offers.ToList();
        }

        public void UpdateOffer(Offer updatedOffer)
        {
            var existingOffer = _dbContext.Offers.Find(updatedOffer.OfferId);

            if (existingOffer != null)
            {
                existingOffer.Description = updatedOffer.Description;
                existingOffer.Content = updatedOffer.Content;

                _dbContext.SaveChanges();
            }
        }

        public void DeleteOffer(int offerId)
        {
            var offerToDelete = _dbContext.Offers.Find(offerId);

            if (offerToDelete != null)
            {
                _dbContext.Offers.Remove(offerToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
