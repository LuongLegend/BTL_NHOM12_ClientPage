﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_NHOM12_ClientPage.Models
{
    public class CartItem
    {
        public string productID { get; set; }
        public string productName { get; set; }
        public int price { get; set; }
        public string photo { get; set; }
        public int quantity { get; set; }
        public int sale_price { get; set; }
        public int min_product { get; set; }
        public List<string> bonus;
        public int toPrice
        {
            get
            {
                if (quantity >= min_product) return quantity * price - sale_price;
                else 
                return quantity * price;
            }
        }

    }
    public class ShoppingCart
    {
        public void CartAdd(CartItem CartItemCart)
        {
            if (HttpContext.Current.Session["Cart"] == null)
            {
                //khoi tao gio hang
                HttpContext.Current.Session["Cart"] = new List<CartItem>();
                List<CartItem> ListCartItem = HttpContext.Current.Session["Cart"] as List<CartItem>;
                ListCartItem.Add(CartItemCart);
                //gan nguoc lai gio hang
                HttpContext.Current.Session["Cart"] = ListCartItem;
            }
            else
            {
                List<CartItem> ListCartItem = HttpContext.Current.Session["Cart"] as List<CartItem>;
                //find id in Cart
                var findCartItemCart = (from CartItem in ListCartItem where CartItem.productID == CartItemCart.productID select CartItem).FirstOrDefault();
                //neu co san pham trong gio hang
                if (findCartItemCart != null)
                {
                    findCartItemCart.quantity++;
                    //gan nguoc lai vao HttpContext.Current.Session cart
                    HttpContext.Current.Session["Cart"] = ListCartItem;
                }
                //neu khong co thi them san pham nay vao gio hang
                else
                {
                    //them moi CartItemCart vao gio hang
                    ListCartItem.Add(CartItemCart);
                    //gan nguoc lai vao HttpContext.Current.Session cart
                    HttpContext.Current.Session["Cart"] = ListCartItem;
                }
            }
        }

        //xoa san pham khoi gio hang
        public void CartDelete(string productID)
        {
            List<CartItem> Cart = HttpContext.Current.Session["Cart"] as List<CartItem>;
            //find CartItem
            var CartItemCart = (from CartItem in Cart where CartItem.productID == productID select CartItem).FirstOrDefault();
            if (CartItemCart != null)
            {
                //xoa sp khoi Cart
                Cart.Remove(CartItemCart);
                //gan lai Cart vao session Cart
                HttpContext.Current.Session["Cart"] = Cart;
            }
        }
        //update so luong san pham
        public void CartUpdate(string productID, int new_quantity)
        {
            List<CartItem> Cart = HttpContext.Current.Session["Cart"] as List<CartItem>;
            var CartItem = (from item in Cart where item.productID == productID select item).FirstOrDefault();
            if (CartItem != null)
            {
                CartItem.quantity = new_quantity;
            }
        }
        //xoa toan bo gio hang
        public void CartDestroy()
        {
            List<CartItem> objCart = new List<CartItem>();
            HttpContext.Current.Session["Cart"] = objCart;
        }
        //tong tien thanh toan
        public int CartTotal()
        {
            List<CartItem> Cart = HttpContext.Current.Session["Cart"] as List<CartItem>;
            int total = 0;
            foreach (var item in Cart)
            {
                total += item.toPrice;
            }
            return total;
        }
    }
}