using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Domain.Services.Response;
using Resources.GetResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SaleService(ISaleRepository saleRepository, IUnitOfWork unitOfWork)
        {
            _saleRepository = saleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaleResponse> DeleteAsync(Sale sale)
        {
            if (_saleRepository.GetById(sale.ID) == null)
                return new SaleResponse("Object With Specific ID Was Null");

            try
            {
                _saleRepository.Delete(sale);

                await _unitOfWork.CompleteAsync();

                return new SaleResponse(sale);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaleResponse($"An error occurred when deleting the Sale: {ex.Message}");
            }
        }

        public async Task<SaleResponse> DeleteByIDAsync(int id)
        {
            var existingSale = await _saleRepository.GetById(id);

            if (existingSale == null)
                return new SaleResponse("Was not found.");

            try
            {
                _saleRepository.Delete(existingSale);
                await _unitOfWork.CompleteAsync();

                return new SaleResponse(existingSale);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaleResponse($"An error occurred when deleting the Sale: {ex.Message}");
            }
        }

        public async Task<Sale> GetById(int id)
        {
            if (id != 0)
                return
                    await _saleRepository.GetById(id);

            return null;
        }

        public async Task<SaleResponse> InsertAsync(Sale sale)
        {
            try
            {
                var existingSale = await _saleRepository.GetById(sale.ID);
                if (existingSale != null)
                    return new SaleResponse("Invalid sale.");

                await _saleRepository.Insert(sale);
                await _unitOfWork.CompleteAsync();

                return new SaleResponse(sale);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaleResponse($"An error occurred when saving the Sale: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Sale>> ListAsync()
        {
            var sales = await _saleRepository.GetAll();

            return sales;
        }

        public async Task<SaleResponse> UpdateAsync(int id, Sale sale)
        {
            var existingSale = await _saleRepository.GetById(id);

            if (existingSale == null)
                return new SaleResponse("was not found.");

            existingSale.ConsultantID = sale.ConsultantID;
            existingSale.SaleDate = sale.SaleDate;

            try
            {
                _saleRepository.Update(existingSale);
                await _unitOfWork.CompleteAsync();

                return new SaleResponse(existingSale);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaleResponse($"An error occurred when updating the Sale: {ex.Message}");
            }
        }

        public async Task<IEnumerable<SaleConsultantResource>> GetSaleConsultantsAsync(DateTime StartDate, DateTime EndDate)
        {
            IEnumerable<Sale> sales = _saleRepository.GetAll().Result.Where(x => (x.SaleDate >= StartDate && x.SaleDate <= EndDate));
            List<SaleConsultantResource> resultList = new List<SaleConsultantResource>();
            int ProductSum = 0;
            int profit = 0;
            foreach (var sale in sales.ToList())
            {
                SaleConsultantResource res = new SaleConsultantResource();

                ProductSum = sale.SaleProducts.Sum(x => x.ProductQuantity);
                profit = sale.SaleProducts.Sum(x => x.ProductQuantity * x.Product.Price);
                res.SaleID = sale.ID;
                res.SaleDate = sale.SaleDate;
                res.ConsultantID = sale.ConsultantID;
                res.ConsultantFirstName = sale.Consultant.FirstName;
                res.ConsultantLastName = sale.Consultant.LastName;
                res.ConsultantPrivateID = sale.Consultant.PersonalID;
                res.ProductQuantity = ProductSum;
                res.ProductTotalMoney = profit;

                resultList.Add(res);
            }

            return resultList;
        }

        //ch
        public async Task<IEnumerable<SaleProductPriceResource>> GetSalesPrices(DateTime StartDate, DateTime EndDate, int MinPrice, int MaxPrice)
        {
            List<SaleProductPriceResource> resultList = new List<SaleProductPriceResource>();

            IEnumerable<Sale> sales = _saleRepository.GetAll().Result.
                                 Where((x => x.SaleDate >= StartDate && x.SaleDate <= EndDate && (x.SaleProducts
                               .Where(y => y.Product.Price >= MinPrice && y.Product.Price <= MaxPrice).Count() > 0)));

            int quantity = 0;

            foreach (var sale in sales)
            {
                SaleProductPriceResource res = new SaleProductPriceResource();
                quantity = sale.SaleProducts.Where(y => y.Product.Price >= MinPrice && y.Product.Price <= MaxPrice).Count();
                res.SaleID = sale.ID;
                res.SaleDate = sale.SaleDate;
                res.ConsultantID = sale.ConsultantID;
                res.FirstName = sale.Consultant.FirstName;
                res.LastName = sale.Consultant.LastName;
                res.PersonalID = sale.Consultant.PersonalID;
                res.DiffProductQuantity = quantity;
                resultList.Add(res);
            }

            return resultList;
        }
    }
}
