using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Models;
using ShopHungVuong.Web.Models;

namespace ShopHungVuong.Web.Controllers
{
    public class ProductsController : Controller
    {
        private DataContext db = new DataContext();


        private static double GetRating()
        {
            int star5 = 12801;
            int star4 = 4982;
            int star3 = 1251;
            int star2 = 429;
            int star1 = 1265;

            double rating = (double)(5 * star5 + 4 * star4 + 3 * star3 + 2 * star2 + 1 * star1) / (star1 + star2 + star3 + star4 + star5);

            rating = Math.Round(rating, 1);

            return rating;
        }
        
        public ActionResult CreateProduct(ProductModelView model)
        {
            try
            {
                List<Product> list = db.Products.ToList();
                if (model.Id > 0)
                {
                    //update
                    Product product = db.Products.SingleOrDefault(x => x.ProductId == model.Id);
                    product.ProductId = model.Id;
                    product.Name = model.Name;
                    product.Description = model.Description;
                    product.ImportPrice = model.ImportPrice;
                    product.Price = model.Price;
                    product.Status = model.Status;
                    product.MainPhoto1 = model.MainPhoto1;
                    product.MainPhoto2 = model.MainPhoto2;
                    product.MainPhoto3 = model.MainPhoto3;
                    product.SecondaryPhoto1 = model.SecondaryPhoto1;
                    product.SecondaryPhoto2 = model.SecondaryPhoto2;
                    product.SecondaryPhoto3 = model.SecondaryPhoto3;
                    product.Color = model.Color;
                    product.Amount = model.Amount;
                    product.Point = model.Point;
                    product.ProductGroupId = model.ProductGroupId;
                    product.ProductGroup.Name = model.ProductGroupName;
                    product.GuaranteeProductId = model.GuaranteeProductId;
                    product.GuaranteeProduct.Time = model.GuaranteeProductTime;
                    product.PromotionId = model.PromotionId;
                    product.Promotion.Name = model.PromotionName;
                    product.Promotion.SaleOff = model.PromotionSaleOff;
                    product.ManufacturerId = model.ManufacturerId;
                    product.Manufacturer.Name = model.ManufacturerName;
                    db.SaveChanges();
                }
                else
                {
                    //Insert
                    Product product = new Product
                    {
                        Name = model.Name,
                        Description = model.Description,
                        ImportPrice = model.ImportPrice,
                        Price = model.Price,
                        Status = model.Status,
                        MainPhoto1 = model.MainPhoto1,
                        MainPhoto2 = model.MainPhoto2,
                        MainPhoto3 = model.MainPhoto3,
                        SecondaryPhoto1 = model.SecondaryPhoto1,
                        SecondaryPhoto2 = model.SecondaryPhoto2,
                        SecondaryPhoto3 = model.SecondaryPhoto3,
                        Color = model.Color,
                        Amount = model.Amount,
                        Point = model.Point,
                        ProductGroupId = model.ProductGroupId,
                        GuaranteeProductId = model.GuaranteeProductId,
                        PromotionId = model.PromotionId,
                        ManufacturerId = model.ManufacturerId
                };
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ListProduct()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult BestDeals()
        {
            List<ProductModelView> listProduct =
                db.Products.Select(x => new ProductModelView
                {
                    Id = x.ProductId,
                    Name = x.Name,
                    Description = x.Description,
                    ImportPrice = x.ImportPrice,
                    Price = x.Price,
                    Status = x.Status,
                    MainPhoto1 = x.MainPhoto1,
                    SecondaryPhoto1 = x.SecondaryPhoto1,
                    Amount = x.Amount,
                    Point = x.Point,
                    ProductGroupId = x.ProductGroupId,
                    ProductGroupName = x.ProductGroup.Name,
                    GuaranteeProductId = x.GuaranteeProductId,
                    GuaranteeProductTime = x.GuaranteeProduct.Time,
                    PromotionId = x.PromotionId,
                    PromotionName = x.Promotion.Name,
                    PromotionSaleOff = x.Promotion.SaleOff,
                    ManufacturerId = x.ManufacturerId,
                    ManufacturerName = x.Manufacturer.Name
                }).ToList();
            ViewBag.ProductList = listProduct;
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult Compare()
        {
            return View();
        }
        
        public ActionResult Feature()
        {

            List<ProductModelView> listProduct =
                db.Products.Select(x => new ProductModelView
                {
                    Id = x.ProductId,
                    Name = x.Name,
                    Description = x.Description,
                    ImportPrice = x.ImportPrice,
                    Price = x.Price,
                    Status = x.Status,
                    MainPhoto1 = x.MainPhoto1,
                    SecondaryPhoto1 = x.SecondaryPhoto1,
                    Amount = x.Amount,
                    Point = x.Point,
                    ProductGroupId = x.ProductGroupId,
                    ProductGroupName = x.ProductGroup.Name,
                    GuaranteeProductId = x.GuaranteeProductId,
                    GuaranteeProductTime = x.GuaranteeProduct.Time,
                    PromotionId = x.PromotionId,
                    PromotionName = x.Promotion.Name,
                    PromotionSaleOff = x.Promotion.SaleOff,
                    ManufacturerId = x.ManufacturerId,
                    ManufacturerName = x.Manufacturer.Name
                }).ToList();
            ViewBag.ProductList = listProduct;
            return View();
        }
        public ActionResult Shopgrid()
        {
            return View();
        }
        public ActionResult Trackorder()
        {
            return View();
        }
        public ActionResult Wishlist()
        {
            return View();
        }
        public ActionResult Store()
        {
            return View();
        }
        public ActionResult ProductDetail(int? id)
        {
            ProductModelView model = new ProductModelView();

                Product product = db.Products.SingleOrDefault(x => x.ProductId == id);
                model.Id = product.ProductId;
                model.Name = product.Name;
                model.Description = product.Description;
                model.ImportPrice = product.ImportPrice;
                model.Price = product.Price;
                model.Status = product.Status;
                model.MainPhoto1 = product.MainPhoto1;
                model.MainPhoto2 = product.MainPhoto2;
                model.MainPhoto3 = product.MainPhoto3;
                model.SecondaryPhoto1 = product.SecondaryPhoto1;
                model.SecondaryPhoto2 = product.SecondaryPhoto2;
                model.SecondaryPhoto3 = product.SecondaryPhoto3;
                model.Color = product.Color;
                model.Amount = product.Amount;
                model.Point = product.Point;
                model.ProductGroupId = product.ProductGroupId;
                model.ProductGroupName = product.ProductGroup.Name;
                model.GuaranteeProductId = product.GuaranteeProductId;
                model.GuaranteeProductTime = product.GuaranteeProduct.Time;
                model.PromotionId = product.PromotionId;
                model.PromotionName = product.Promotion.Name;
                model.PromotionSaleOff = product.Promotion.SaleOff;
                model.ManufacturerId = product.ManufacturerId;
                model.ManufacturerName = product.Manufacturer.Name;

            return View(model);
        }

        public ActionResult BestSell()
        {
            List<ProductModelView> listProduct =
                db.Products.Select(x => new ProductModelView
                {
                    Id = x.ProductId,
                    Name = x.Name,
                    Description = x.Description,
                    ImportPrice = x.ImportPrice,
                    Price = x.Price,
                    Status = x.Status,
                    MainPhoto1 = x.MainPhoto1,
                    Amount = x.Amount,
                    Point = x.Point,
                    ProductGroupId = x.ProductGroupId,
                    ProductGroupName = x.ProductGroup.Name,
                    GuaranteeProductId = x.GuaranteeProductId,
                    GuaranteeProductTime = x.GuaranteeProduct.Time,
                    PromotionId = x.PromotionId,
                    PromotionName = x.Promotion.Name,
                    PromotionSaleOff = x.Promotion.SaleOff,
                    ManufacturerId = x.ManufacturerId,
                    ManufacturerName = x.Manufacturer.Name
                }).ToList();
            ViewBag.ProductList = listProduct;
            return View();
        }
        public ActionResult RelatedProduct(int id)
        {
            List<ProductModelView> listProduct =
                db.Products.Select(x => new ProductModelView
                {
                    Id = x.ProductId,
                    Name = x.Name,
                    Description = x.Description,
                    ImportPrice = x.ImportPrice,
                    Price = x.Price,
                    Status = x.Status,
                    MainPhoto1 = x.MainPhoto1,
                    Amount = x.Amount,
                    Point = x.Point,
                    ProductGroupId = x.ProductGroupId,
                    ProductGroupName = x.ProductGroup.Name,
                    GuaranteeProductId = x.GuaranteeProductId,
                    GuaranteeProductTime = x.GuaranteeProduct.Time,
                    PromotionId = x.PromotionId,
                    PromotionName = x.Promotion.Name,
                    PromotionSaleOff = x.Promotion.SaleOff,
                    ManufacturerId = x.ManufacturerId,
                    ManufacturerName = x.Manufacturer.Name
                }).Where(x => x.ProductGroupId == id).ToList();
            ViewBag.ProductList = listProduct;
            return View();
           
        }
        public ActionResult MiniCart()
        {
            return View();
        }
    }
}