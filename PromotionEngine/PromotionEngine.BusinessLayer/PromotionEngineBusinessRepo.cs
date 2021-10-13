using PromotionEngine.Models;
using PromotionEngine.PromotionEngine.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.PromotionEngine.BusinessLayer
{
    public class PromotionEngineBusinessRepo : IPromotionEngineBusinessRepo
    {
        private readonly IPromotionEngineDataRepo _dataRepo;
        const int CombinedPromo = 2;
        private CartBilling cb;

        public PromotionEngineBusinessRepo(IPromotionEngineDataRepo dataRepo)
        {
            _dataRepo = dataRepo;
        }

        public int CalculateTotalAmount(CartItem[] cartItems)
        {
            List<CartBilling> cartproductsBilling = new List<CartBilling>();
            foreach (var item in cartItems)
            {
                IEnumerable<Promotion> promotion = _dataRepo.GetPromotionByProductName(item);

                if (promotion.Count() == 0 || (promotion.Any(x => x.PromoProducts.Count >= CombinedPromo) && promotion.Count() == 1))
                {
                    cartproductsBilling.Add(new CartBilling() { Name = item.Name, Quantity = item.Quantity, Price = item.Quantity * _dataRepo.GetProductValue(item.Name) });
                }
                CalculatePromotionPerProduct(item, promotion, cartproductsBilling,cartItems);
            }
            return RemoveLowPromotionCoupon(cartproductsBilling);
        }

        public void CalculatePromotionPerProduct(CartItem item, IEnumerable<Promotion> promotionCode, List<CartBilling> cartproductsBilling, CartItem[] cartItems)
        {
            CartBilling cod = new CartBilling() { Name = item.Name, Quantity = item.Quantity };
            foreach (var promotion in promotionCode)
            {
                if (promotion.PromoProducts.Count >= CombinedPromo)
                {
                    CalculateMultiPromotionPerProduct(promotion, cartItems, cartproductsBilling);
                }
                else
                {
                    int tempValue = 0;
                    int temp = Convert.ToInt32(item.Quantity / promotion.PromoProducts[item.Name]) * promotion.Amount
                        + (item.Quantity % promotion.PromoProducts[item.Name]) * _dataRepo.GetProductValue(item.Name);
                    cod.Price = (tempValue == 0) ? temp : (tempValue <= temp) ? tempValue : temp;
                }
            }
             cartproductsBilling.Add(cod);
        }

        public void CalculateMultiPromotionPerProduct(Promotion promotion, CartItem[] items, List<CartBilling> cartproductsBilling)
        {
            int finalMinValue = 0, multiTotalValue = 0, minGroup = 0;
            var doublePromos = (promotion.PromoProducts.Keys).ToArray();
            var result = from cart in items where doublePromos.Contains(cart.Name) select cart;
            if (result.Count() == doublePromos.Count())
            {
                CartBilling cod = new CartBilling();
                foreach (var product in result)
                {
                    int minTemp = Convert.ToInt32(product.Quantity / promotion.PromoProducts[product.Name]);
                    finalMinValue = (minGroup == 0) ? minTemp : (minGroup <= minTemp) ? minGroup : minTemp;
                }
                multiTotalValue = promotion.Amount * finalMinValue;
                foreach (var product in result)
                {
                    multiTotalValue += (product.Quantity - (promotion.PromoProducts[product.Name] * finalMinValue)) * _dataRepo.GetProductValue(product.Name);
                    cod.Name += product.Name;
                }
                cod.Price = multiTotalValue;
                if (!cartproductsBilling.Any(x=>x.Name==cod.Name && x.Quantity==cod.Quantity && x.Price== cod.Price)) cartproductsBilling.Add(cod);
            }
        }

        public int RemoveLowPromotionCoupon(IEnumerable<CartBilling> cartproductsBilling)
        {
            int totalCount = 0;
            List<CartBilling> tempList = new List<CartBilling>();
            foreach (var item in cartproductsBilling)
            {
                totalCount = 0;
                for (int i = 0; i < item.Name.Length && item.Name.Length>1; i++)
                {
                    cb = cartproductsBilling.FirstOrDefault(x => x.Name == item.Name[i].ToString());
                    totalCount += cb.Price;
                    tempList.Add(cb);
                }
                if (totalCount >= item.Price  && totalCount >0  )
                {
                    cartproductsBilling=cartproductsBilling.Except(tempList.ToList());
                }
                else 
                {
                    tempList.Clear();
                    tempList.Add(item);
                    cartproductsBilling = totalCount > 0? cartproductsBilling.Except(tempList.ToList()): cartproductsBilling;
                }
            }

            return  (cartproductsBilling.Sum(x=>x.Price));
        }

    }
}
