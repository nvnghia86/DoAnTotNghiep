using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DataAccess;
using DataAccess.Models;
using ShopHungVuong.Web.Models;
using ShopHungVuong.Web.Models.Cart;

namespace ShopHungVuong.Web.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        #region Giỏ hàng
        //Lấy giỏ hàng 
        public List<Cart> GetCart()
        {
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tao list giỏ hàng (sessionCart)
                lstCart = new List<Cart>();
                Session["Cart"] = lstCart;
            }
            return lstCart;
        }
        //Thêm giỏ hàng
        public ActionResult AddCart(int iProductId)
        {
            Product product = db.Products.SingleOrDefault(n => n.ProductId == iProductId);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<Cart> lstCart = GetCart();
            //Kiểm tra sách này đã tồn tại trong session[Cart] chưa
            Cart sanpham = lstCart.Find(n => n.iProductId == iProductId);
            if (sanpham == null)
            {
                sanpham = new Cart(iProductId);
                //Add sản phẩm mới thêm vào list
                lstCart.Add(sanpham);
                return View();
            }
            else
            {
                sanpham.Amount++;
                return View();
            }
        }
        //Cập nhật giỏ hàng 
        public ActionResult CapNhatCart(int iMaSP, FormCollection f)
        {
            //Kiểm tra masp
            Product product = db.Products.SingleOrDefault(n => n.ProductId == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<Cart> lstCart = GetCart();
            //Kiểm tra sp có tồn tại trong session["Cart"]
            Cart sanpham = lstCart.SingleOrDefault(n => n.iProductId == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                sanpham.Amount = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("Cart");
        }
        //Xóa giỏ hàng
        public ActionResult XoaCart(int iMaSP)
        {
            //Kiểm tra masp
            Product product = db.Products.SingleOrDefault(n => n.ProductId == iMaSP);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<Cart> lstCart = GetCart();
            Cart sanpham = lstCart.SingleOrDefault(n => n.iProductId == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                lstCart.RemoveAll(n => n.iProductId == iMaSP);

            }
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Cart");
        }
        //Xây dựng trang giỏ hàng
        public ActionResult Cart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart> lstCart = GetCart();
            return View(lstCart);
        }
        //Tính tổng số lượng và tổng tiền
        //Tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart != null)
            {
                iTongSoLuong = lstCart.Sum(n => n.Amount);
            }
            return iTongSoLuong;
        }
        //Tính tổng thành tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart != null)
            {
                dTongTien = lstCart.Sum(n => n.TotalMoney);
            }
            return dTongTien;
        }
        //tạo partial giỏ hàng
        public ActionResult CartPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //Xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaCart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart> lstCart = GetCart();
            return View(lstCart);

        }
        #endregion
        #region Đặt hàng
        //Xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            //Kiểm tra đăng đăng nhập
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }
            //Kiểm tra giỏ hàng
            if (Session["Cart"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            //Thêm đơn hàng
            Order order = new Order();
            User user = (User)Session["TaiKhoan"];
            List<Cart> gh = GetCart();
            order.CustomerId = user.UserId;
            order.OrderDate = DateTime.Now;
            db.Orders.Add(order);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng
            //foreach (var item in gh)
            //{
            //    ChiTietDonHang ctDH = new ChiTietDonHang();
            //    ctDH.MaDonHang = ddh.MaDonHang;
            //    ctDH.MaSach = item.iMaSach;
            //    ctDH.SoLuong = item.iSoLuong;
            //    ctDH.DonGia = (decimal)item.dDonGia;
            //    db.ChiTietDonHangs.Add(ctDH);
            //}
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
