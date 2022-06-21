using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteShopQuanAo.Models;

namespace WebsiteShopQuanAo.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        ShopQuanAoDataContext db = new ShopQuanAoDataContext();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //nếu lstGioHang  ch tồn tại thì khởi tạo
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult GioHangEmpty()
        {
            return View();
        }
        
        public ActionResult ThemGioHangSLL(int msp, string strURL,FormCollection f)
        {
            string strsl = f["txtSoLuong"];
            string size = f["size"];
            //neu khong chon size
            if(size == null)
            {
                return Redirect(strURL);
            }
            
            //lấy giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //kiem tra soluong nhap vao hop le
            KHO spkho = db.KHOs.SingleOrDefault(s => s.MASP == msp && s.SIZE == size);
            int slton = int.Parse(spkho.SOLUONG.ToString());
            if (!strsl.All(char.IsDigit))
            {
                ViewData["SoLuong"] = "So luong phai la so!!";
                return Redirect(strURL);
            }
            else if (int.Parse(strsl) <= 0)
            {
                ViewData["SoLuong"] = "So khong dc am!!";
                return Redirect(strURL);
            }
            else if (int.Parse(strsl) > slton)
            {
                ViewData["SoLuong"] = "So luong trong kho khong du!!";
                return Redirect(strURL);
            }
            
            //ktra sách có tồn tại trong sesson["GioHang"] chưa?
            GioHang SanPham = lstGioHang.Find(sp => sp.iMaSP == msp && sp.sSiZe == size);
            if (SanPham == null)
            {
                SanPham = new GioHang(msp,size);
                lstGioHang.Add(SanPham);
                SanPham.iSoLuong = int.Parse(strsl);
                SanPham.sSiZe = size;
                return Redirect(strURL);
            }
            else
            {
                SanPham.iSoLuong = SanPham.iSoLuong + int.Parse(strsl);
                SanPham.sSiZe = size;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tsl += lstGioHang.Sum(sp => sp.iSoLuong);
            }
            return tsl;
        }

        //tổng thành tiền
        private double TongThanhTien()
        {
            double ttt = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                ttt += lstGioHang.Sum(sp => sp.dThanhTien);
            }
            return ttt;
        }

        private double TienGiamGia_Code()
        {
            double ttt = 0;
            var gg = Session["GiamGia"] as GIAMGIA;
            if (Session["GiamGia"] == null)
            {
                ttt = 0;
            }
            else
            {
                ttt = TongThanhTien() * (double)gg.PHANTRAMGIAM / 100;
            }
            return ttt;
        }

        private double TongThanhToan()
        {
            return TongThanhTien() - TienGiamGia_Code() - TienGiamGia_LoaiTaiKhoan();
        }

        private double TienGiamGia_LoaiTaiKhoan()
        {
            double ttt = 0;
            var tk = Session["User"] as TAIKHOAN;
            TAIKHOANKHACHHANG tttk = db.TAIKHOANKHACHHANGs.SingleOrDefault(s => s.TENTK == tk.TENTK);
            string loaitk = tttk.LOAITK;
            if(loaitk == "Kim Cương")
            {
                ttt = TongThanhTien() * 10 / 100;
                
            }
            else if (loaitk == "Bạc")
            {
                ttt = TongThanhTien() * 2 / 100;
            }
            else if (loaitk == "Vàng")
            {
                ttt = TongThanhTien() * 5 / 100;
            }
            else
            {
                ttt = 0;
            } 
            return ttt;
        }




        //xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("GioHangEmpty", "GioHang");
            }
            else
            {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            ViewBag.DiscountCode = TienGiamGia_Code();
            ViewBag.TongThanhToan = TongThanhTien() - TienGiamGia_Code() ;
            return View(lstGioHang);
            }
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            //ViewBag.TongThanhTien = TongThanhTien();
            return PartialView();
        }

        public ActionResult XacNhanDonHang()
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                List<GioHang> lstGioHang = LayGioHang();
                ViewBag.TongSoLuong = TongSoLuong();
                ViewBag.TongThanhTien = TongThanhTien();
                ViewBag.DiscountCode = TienGiamGia_Code();
                ViewBag.DiscountAccount = TienGiamGia_LoaiTaiKhoan();
                ViewBag.TongThanhToan = TongThanhToan();
                return View(lstGioHang);
            }
        }


        public ActionResult ApDungMaGiamGia(FormCollection f)
        {
            var us = Session["User"] as TAIKHOAN;
            if (Session["User"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                string code = f["txtCode"];
                //kiem tra ma co ton` tai va con trong thoi gian su dung
                GIAMGIA gg = db.GIAMGIAs.SingleOrDefault(s => s.MAGIAMGIA == code);
                GIAMGIA gg_tg = db.GIAMGIAs.SingleOrDefault(s => s.TGBD < DateTime.Now && s.TGKT > DateTime.Now && s.MAGIAMGIA == code);
                if (gg != null && gg_tg !=null)
                {
                    int tkgg = db.TKGGs.Count(s => s.MAGIAMGIA == code && s.TENTK == us.TENTK);
                    if (tkgg == 0)
                    {
                        Session["GiamGia"] = gg;
                    }
                    else
                    {

                        ViewData["LoiMaGiamGia"] = "Code already used or has expired!";
                    }

                }
                else
                {
                    ViewData["LoiMaGiamGia"] = "Code does not exist!";
                }
                
            }
            return RedirectToAction("GioHang", "GioHang");
        }


        public ActionResult ThanhToanThanhCong()
        {
            return View();
        }

        public ActionResult ThanhToan(FormCollection f)
        {
            string diachigh = f["txtDiaChi"];
            var us = Session["User"] as TAIKHOAN;
            var gg = Session["GiamGia"] as GIAMGIA;
            List<GioHang> listCart = Session["GioHang"] as List<GioHang>;
            //tao hoa don
            HOADON hd = new HOADON();
            hd.TENTK = us.TENTK;
            hd.NGTAO = DateTime.Now;
            hd.DIACHIGH = diachigh;
            hd.TONGTIEN = listCart.Sum(t => t.dThanhTien);
            if (gg == null)
            {
                hd.MAGIAMGIA = string.Empty;
                hd.TIENGIAMGIA = 0;
            }
            else
            {
                hd.MAGIAMGIA = gg.MAGIAMGIA;
                hd.TIENGIAMGIA = TienGiamGia_Code();
            }
            hd.TIENGIAMTAIKHOAN = TienGiamGia_LoaiTaiKhoan();
            hd.THANHTOAN = TongThanhToan();
            db.HOADONs.InsertOnSubmit(hd);
            db.SubmitChanges();
            // kiem tra ma giam gia va them vao db
            if (gg != null)
            {
                TKGG tkgg = new TKGG();
                tkgg.TENTK = us.TENTK;
                tkgg.MAGIAMGIA = gg.MAGIAMGIA;
                db.TKGGs.InsertOnSubmit(tkgg);
            }

            List<HOADON> lsthd = db.HOADONs.OrderByDescending(t => t.MAHD).ToList();
            var hd_last = lsthd.First();
            int mhd = hd_last.MAHD;
            //them chi tiet
            //copy gio hang vào ct hoa don
            foreach (GioHang item in listCart)
            {
                CHITIETHD ct = new CHITIETHD();
                ct.MAHD = mhd;
                ct.MASP = item.iMaSP;
                ct.SIZE = item.sSiZe;
                ct.SOLUONG = item.iSoLuong;
                //ct.ThanhTien = item.dThanhTien;

                // insert vao database
                db.CHITIETHDs.InsertOnSubmit(ct);
                KHO kho = db.KHOs.SingleOrDefault(s => s.MASP == item.iMaSP && s.SIZE == item.sSiZe);
                kho.SOLUONG = kho.SOLUONG - item.iSoLuong;
            }
            db.SubmitChanges(); 
            CapNhatTaiKhoan();

            
            // thanh toan thanh cong
            List<GioHang> list = LayGioHang();
            list.Clear();
            Session["GiamGia"] = null;
            return RedirectToAction("ThanhToanThanhCong", "GioHang");
        }

        public ActionResult XoaGioHang(int MaSP, string size)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Single(s => s.iMaSP == MaSP && s.sSiZe == size);
            if (sp != null)
            {
                lstGioHang.RemoveAll(s => s.iMaSP == MaSP && s.sSiZe == size);
                if (lstGioHang.Count == 0)
                {
                    return RedirectToAction("GioHangEmpty", "GioHang");
                }
                else
                {
                    return RedirectToAction("GioHang", "GioHang");
                }
            }
            return View();

        }

        public ActionResult CapNhatGioHang(int MaSP,string size, FormCollection f)
        {
            
            //lấy giỏ hàng
            string strsl = (f["txtSoLuong"]);
            List<GioHang> lstGioHang = LayGioHang();
            KHO spkho = db.KHOs.SingleOrDefault(s =>s.MASP == MaSP && s.SIZE ==  size);
            int slton = int.Parse(spkho.SOLUONG.ToString());
            //ktra sách cần cap nhat có trog giỏ hàng ko?
            if (!strsl.All(char.IsDigit))
            {
                ViewData["SoLuong"] = "So luong phai la so!!";

            }
            else if (int.Parse(strsl)<= 0)
            {
                ViewData["SoLuong"] = "So khong dc am!!";
            }
            else if (int.Parse(strsl) > slton)
            {
                ViewData["SoLuong"] = "So luong trong kho khong du!!";
            }
            else
            {
                GioHang sp = lstGioHang.Single(s => s.iMaSP == MaSP && s.sSiZe == size);
                if (sp != null)
                {
                    sp.iSoLuong = int.Parse(strsl);
                }
            }
            return RedirectToAction("GioHang", "GioHang");
        }

        public void CapNhatTaiKhoan()
        {
            var us = Session["User"] as TAIKHOAN;
            
            List<HOADON> lsthd = db.HOADONs.Where(hd => hd.TENTK == us.TENTK).ToList();
            double tongtienhd = double.Parse(lsthd.Sum(hd => hd.THANHTOAN).ToString());
            TAIKHOANKHACHHANG tk = db.TAIKHOANKHACHHANGs.SingleOrDefault(s => s.TENTK == us.TENTK);
            if (tongtienhd >= 10000000)
            {
                tk.LOAITK = "Kim Cương";
                tk.ANH = "Diamond.jpg";
            }
            else if(tongtienhd >= 5000000)
            {
                tk.LOAITK = "Vàng";
                tk.ANH = "Gold.jpg";
            }
            else if (tongtienhd >= 1000000)
            {
                tk.LOAITK = "Bạc";
                tk.ANH = "Silver.jpg";
            }
            db.SubmitChanges();
        }
    }

}
