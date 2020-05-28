using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Domain.Services.Response;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SaleProductsService : ISaleProductService
    {
        private readonly ISaleProductRepository _saleProductRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SaleProductsService(ISaleProductRepository saleProductRepository, IUnitOfWork unitOfWork)
        {
            _saleProductRepository = saleProductRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaleProductsResponse> DeleteAsync(SaleProducts saleProduct)
        {
            if (_saleProductRepository.GetById(saleProduct.ID) == null)
                return new SaleProductsResponse("Object With Specific ID Was Null");

            try
            {
                _saleProductRepository.Delete(saleProduct);
                await _unitOfWork.CompleteAsync();

                return new SaleProductsResponse(saleProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaleProductsResponse($"An error occurred when deleting the SaleProduct: {ex.Message}");
            }
        }

        public async Task<SaleProductsResponse> DeleteByIDAsync(int id)
        {
            var existingProduct = await _saleProductRepository.GetById(id);

            if (existingProduct == null)
                return new SaleProductsResponse("Was not found.");

            try
            {
                _saleProductRepository.Delete(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new SaleProductsResponse(existingProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaleProductsResponse($"An error occurred when deleting the SaleProduct: {ex.Message}");
            }
        }

        public async Task<SaleProducts> GetById(int id)
        {
            if (id != 0)
                return
                    await _saleProductRepository.GetById(id);

            return null;
        }

        public async Task<SaleProductsResponse> InsertAsync(SaleProducts product)
        {
            try
            {
                var existingProduct = await _saleProductRepository.GetById(product.ID);
                if (existingProduct != null)
                    return new SaleProductsResponse("Invalid SaleProduct.");

                await _saleProductRepository.Insert(product);
                await _unitOfWork.CompleteAsync();

                return new SaleProductsResponse(product);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaleProductsResponse($"An error occurred when saving the SaleProduct: {ex.Message}");
            }
        }

        public async Task<IEnumerable<SaleProducts>> ListAsync()
        {
            var saleProducts = await _saleProductRepository.GetAll();

            return saleProducts;
        }

        public async Task<SaleProductsResponse> UpdateAsync(int id, SaleProducts product)
        {
            var existingProduct = await _saleProductRepository.GetById(id);

            if (existingProduct == null)
                return new SaleProductsResponse("was not found.");

            existingProduct.ProductID = product.ProductID;
            existingProduct.SaleID = product.SaleID;
            existingProduct.ProductQuantity = product.ProductQuantity;

            try
            {
                _saleProductRepository.Update(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new SaleProductsResponse(existingProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaleProductsResponse($"An error occurred when updating the SaleProduct: {ex.Message}");
            }
        }

        public async Task DeleteBySaleID(int SaleID)
        {
            var salesToDelete = _saleProductRepository.GetAll().Result.Where(x => x.SaleID == SaleID);

            foreach (var saleProduct in salesToDelete)
            {
                await _saleProductRepository.DeleteByID(saleProduct.ID);
            }
        }
    }
}
