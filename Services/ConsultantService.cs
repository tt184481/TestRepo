using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Domain.Services.Response;
using Resources.GetResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ConsultantService : IConsultantService
    {
        private readonly IConsultantRepository _consultantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConsultantService(IConsultantRepository consultantRepository, IUnitOfWork unitOfWork)
        {
            _consultantRepository = consultantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ConsultantResponse> DeleteAsync(Consultant consultant)
        {
            if (_consultantRepository.GetById(consultant.ID) == null)
                return new ConsultantResponse("Object With Specific ID Was Null");

            try
            {
                _consultantRepository.Delete(consultant);
                await _unitOfWork.CompleteAsync();

                return new ConsultantResponse(consultant);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ConsultantResponse($"An error occurred when deleting the Consultant: {ex.Message}");
            }
        }

        public async Task<ConsultantResponse> DeleteByIDAsync(int id)
        {
            var existingConsultant = await _consultantRepository.GetById(id);

            if (existingConsultant == null)
                return new ConsultantResponse("Was not found.");

            try
            {
                _consultantRepository.Delete(existingConsultant);
                await _unitOfWork.CompleteAsync();

                return new ConsultantResponse(existingConsultant);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ConsultantResponse($"An error occurred when deleting the Consultant: {ex.Message}");
            }
        }

        public async Task<Consultant> GetById(int id)
        {
            if (id != 0)
                return 
                    await _consultantRepository.GetById(id);

            return null;
        }

        public async Task<ConsultantResponse> InsertAsync(Consultant consultant)
        {
            try
            {
                var existingConsultant = await _consultantRepository.GetById(consultant.ID);
                if (existingConsultant != null)
                    return new ConsultantResponse("Invalid consultant.");

                var c = await _consultantRepository.Insert(consultant);
                var rec = await _consultantRepository.GetById(c.RecommendatoryID);

                //Check recommendatory:
                if(rec == null || c.ID == c.RecommendatoryID || rec.RecommendatoryID == c.ID)
                    return new ConsultantResponse("Recommendatory error!!!");

                await _unitOfWork.CompleteAsync();

                return new ConsultantResponse(consultant);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ConsultantResponse($"An error occurred when saving the Consultant: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Consultant>> ListAsync()
        {
            var consultants = await _consultantRepository.GetAll();

            return consultants;
        }

        public async Task<ConsultantResponse> UpdateAsync(int id, Consultant consultant)
        {
            var existingConsultant = await _consultantRepository.GetById(id);

            if (existingConsultant == null)
                return new ConsultantResponse("was not found.");

            existingConsultant.FirstName = consultant.FirstName;
            existingConsultant.LastName = consultant.LastName;
            existingConsultant.PersonalID = consultant.PersonalID;
            existingConsultant.RecommendatoryID = consultant.RecommendatoryID;
            existingConsultant.Gender = consultant.Gender;
            existingConsultant.BirthDate = consultant.BirthDate;

            var rec = await _consultantRepository.GetById(existingConsultant.RecommendatoryID);
            //Check recommendatory:
            if (rec == null || existingConsultant.ID == existingConsultant.RecommendatoryID ||
                        rec.RecommendatoryID == existingConsultant.ID)
                return new ConsultantResponse("Recommendatory error!!!");

            try
            {
                _consultantRepository.Update(existingConsultant);
                await _unitOfWork.CompleteAsync();

                return new ConsultantResponse(existingConsultant);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ConsultantResponse($"An error occurred when updating the Consultant: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ConsultantSaleSumsResource>> GetConsultantSaleSums(DateTime? StartDate, DateTime? EndDate) 
        {
            if (StartDate == null)
                StartDate = DateTime.MinValue;
            if (EndDate == null)
                EndDate = DateTime.Now;

            IEnumerable<Consultant> consultants = _consultantRepository.GetAll().Result;
            Dictionary<Consultant, int> dict = new Dictionary<Consultant, int>();

            List<ConsultantSaleSumsResource> res = new List<ConsultantSaleSumsResource>();

            int sum = 0;
            int sumAll = 0;

            foreach (var item in consultants)
            {
                ConsultantSaleSumsResource resource = new ConsultantSaleSumsResource();

                sum = item.Sales.Where(x => x.SaleDate >= StartDate && x.SaleDate <= EndDate).Count();

                resource.ConsultantID = item.ID;
                resource.FirstName = item.FirstName;
                resource.LastName = item.LastName;
                resource.PrivateID = item.PersonalID;
                resource.BirthDate = item.BirthDate;
                resource.OwnSaleSum = sum;
                resource.AllSaleSum = 0;

                res.Add(resource);

                //save consultants and their quanitites of sales, then to calculate their sub consultatns quantities too;
                //not calculating whole sum of sales here, cause of not to calculate sum for same consultant several times;
                dict.Add(item, sum);
            }
            int counter = 0;
            foreach (var item in dict)
            {
                sumAll = item.Value;
                foreach (var i in findSubConsultants(item.Key.ID))
                {
                    sumAll += dict[i];
                }
                res[counter].AllSaleSum = sumAll;
                counter++;
            }
            
            return res.OrderByDescending(x => x.AllSaleSum); 
        }

        public async Task<IEnumerable<ConsultantPopularProductResource>> GetMostPopularProducts(DateTime? StartDate, DateTime? EndDate)
        {
            if (StartDate == null)
                StartDate = DateTime.MinValue;
            if (EndDate == null)
                EndDate = DateTime.Now;

            var consultants = _consultantRepository.GetAll().Result;
            IEnumerable<Sale> sales;
            List<ConsultantPopularProductResource> resultList = new List<ConsultantPopularProductResource>();
            foreach (var consultant in consultants)
            {
                sales = consultant.Sales.Where(x=>x.SaleDate>=StartDate && x.SaleDate<=EndDate);
                List<SaleProducts> saleProds = new List<SaleProducts>();
                foreach (var sale in sales)
                {
                    saleProds.AddRange(sale.SaleProducts);
                }
                //calculate quantity and profit of popular and profitable products and save that products;
                var pr = saleProds.GroupBy(x => x.Product)
                    .Select(x => new { Product = x.Key, Quantity = x.Sum(x => x.ProductQuantity) });
                
                int quantity = pr.Max(x => x.Quantity);
                Product popularProduct = pr.Where(x => x.Quantity == quantity).Select(x => x.Product).FirstOrDefault();
                int profit = pr.Max(x => x.Product.Price * x.Quantity);
                Product profitableProduct = pr.Where(x => x.Product.Price * x.Quantity == profit).Select(x => x.Product).FirstOrDefault();

                ConsultantPopularProductResource res = new ConsultantPopularProductResource();
                res.ConsultantID = consultant.ID; res.FirstName = consultant.FirstName; res.LastName = consultant.LastName;
                res.PrivateID = consultant.PersonalID; res.BirthDate = consultant.BirthDate; res.PopularProductCode = popularProduct.ProductCode;
                res.PopularProductName = popularProduct.Name; res.ProductQuantity = quantity; res.ProfitableProductCode = profitableProduct.ProductCode;
                res.ProfitableProductName = profitableProduct.Name; res.Profit = profit;

                resultList.Add(res);
            }

            return resultList.OrderByDescending(x=>x.Profit);
        }

        public async Task<IEnumerable<ConsultantsByProductResource>> GetConsultantsByProducts
                                                (DateTime StartDate, DateTime EndDate, int MinQuantity, long? ProductCode)
        {
            List<ConsultantsByProductResource> resultList = new List<ConsultantsByProductResource>();
            IEnumerable<Consultant> consultants = _consultantRepository.GetAll().Result.Where(x => x.Sales.
                                                 Where(y => y.SaleDate >= StartDate && y.SaleDate <= EndDate).Count() > 0);

            foreach (var consultant in consultants)
            {
                IEnumerable<Sale> allSales = consultant.Sales;
                List<SaleProducts> allSaleProducts = new List<SaleProducts>(); ;
                foreach (var sale in allSales)
                {
                    allSaleProducts.AddRange(sale.SaleProducts);
                }
                Dictionary<long, int> dict = new Dictionary<long, int>();
                foreach (var item in allSaleProducts.GroupBy(x => x.Product))
                {
                    if (ProductCode == null)
                    {
                        int q = item.Sum(x => x.ProductQuantity);
                        if (q >= MinQuantity)
                            dict.Add(item.Key.ProductCode, q);
                    } else {
                        if (item.Key.ProductCode == ProductCode) {
                            int q = item.Sum(x => x.ProductQuantity);
                            if (q >= MinQuantity)
                                dict.Add(item.Key.ProductCode, q);
                        }
                    }
                }
                if (dict.Count() > 0) {
                    ConsultantsByProductResource resultConsultant = new ConsultantsByProductResource();
                    resultConsultant.ConsultantID = consultant.ID; resultConsultant.FirstName = consultant.FirstName;
                    resultConsultant.LastName = consultant.LastName; resultConsultant.BirthDate = consultant.BirthDate;
                    resultConsultant.PrivateID = consultant.PersonalID; resultConsultant.Products = dict;

                    resultList.Add(resultConsultant);
                }
            }

            return resultList;
        }


        //Helper methods
        private List<Consultant> findSubConsultants(int recommendatory)
        {
            List<Consultant> consultants = _consultantRepository.GetAll().Result.ToList();
            List<Consultant> res = consultants.Where(y => y.RecommendatoryID == recommendatory).ToList();
            List<Consultant> resultList = new List<Consultant>();
            resultList.AddRange(res);
            List<Consultant> tmp = new List<Consultant>();
            tmp.AddRange(res);
            int counter = 0;

            while (true)
            {

                if (tmp.Count() == 0)
                    break;

                counter = resultList.Count();
                foreach (var consultant in tmp)
                {
                    List<Consultant> newList = consultants.Where(y => y.RecommendatoryID == consultant.ID).ToList();

                    if (newList.Count() > 0)
                    {
                        resultList.AddRange(newList);
                    }
                }

                tmp.Clear();
                if (resultList.Count() > counter)
                {
                    tmp.AddRange(resultList.GetRange(counter, resultList.Count() - counter));
                }
            }

            return resultList;
        }
    }
}
