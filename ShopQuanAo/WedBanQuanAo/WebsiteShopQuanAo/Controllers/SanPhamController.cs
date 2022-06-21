using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteShopQuanAo.Models;
using PagedList;
using PagedList.Mvc;

namespace WebsiteShopQuanAo.Controllers
{
    public class SanPhamController : Controller
    {
        //
        // GET: /Home/

        ShopQuanAoDataContext db = new ShopQuanAoDataContext();

        
        public ActionResult DanhMucPartial()
        {
            return View(db.LOAISPs.OrderBy(t => t.MALSP));
        }

        public ActionResult SanPhamTheoLoai(string maLoai, int? page)
        {
            var dsloai = db.LOAISPs.SingleOrDefault(t => t.MALSP == maLoai);
            string loai = dsloai.TENLOAI.ToString();
            var listSPTheoLoai = db.SANPHAMs.Where(t => t.MALSP == maLoai).OrderBy(t => t.MASP).ToList();
            @ViewBag.loai = loai;
            return View(listSPTheoLoai.ToPagedList(page ?? 1,9));
        }

        public ActionResult TatCaSanPham(int? page)
        {
            var listSP = db.SANPHAMs.OrderBy(t => t.MASP).ToList();
            return View(listSP.ToPagedList(page ?? 1, 9));
        }

        public ActionResult TatCaSanPhamHome(int? page)
        {
            var listSP = db.SANPHAMs.OrderBy(t => t.MASP).ToList();
            return View(listSP.ToPagedList(page ?? 1, 9));
        }
        public ActionResult XemChiTiet(int maSP)
        {
            return View(db.SANPHAMs.SingleOrDefault(t => t.MASP == maSP));
        }

        public ActionResult QuanLySanPham()
        {
            List<SANPHAM> lstSanPham = db.SANPHAMs.OrderBy(t => t.MASP).ToList();
            return View(lstSanPham);
        }

        public ActionResult CapNhatGiaSanPham(int mahang, FormCollection f)
        {
            SANPHAM sp = db.SANPHAMs.Single(t => t.MASP == mahang);
            if (sp != null)
            {
                sp.DONGIA = int.Parse(f["txtGia"]);
                db.SubmitChanges();
            }
            return RedirectToAction("QuanLySanPham", "SanPham");
        }

        public ActionResult XemChiTietKho(int mahang,FormCollection f)
        {
            List<KHO> lstkho = db.KHOs.Where(t => t.MASP == mahang).ToList();
            return View(lstkho);
        }

        public ActionResult CapNhatSoLuongKho(int mahang, FormCollection f,string strURL)
        {
            string size = f["txtSize"];
            KHO kho = db.KHOs.Single(t => t.MASP == mahang && t.SIZE == size);
            if (kho != null)
            {
                kho.SOLUONG = int.Parse(f["txtSoLuong"]);
                db.SubmitChanges();
            }
            return Redirect(strURL);
        }

        public ActionResult ThemVaoKho(int msp)
        {
            return View(db.SANPHAMs.Single(t=>t.MASP == msp));
        }

        [HttpPost]
        public ActionResult ThemVaoKho(int msp,FormCollection f)
        {
            var masp = f["MaSP"];
            var size = f["Size"];
            var sl = f["SoLuong"];
            KHO kho = new KHO();
            if (kho != null)
            {
                kho.MASP = msp;
                kho.SIZE = size;
                kho.SOLUONG = int.Parse(sl);
                db.KHOs.InsertOnSubmit(kho);
                db.SubmitChanges();
                return RedirectToAction("QuanLySanPham", "SanPham");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ThemSanPham()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemSanPham(FormCollection f)
        {
            var tensp = f["TenSP"];
            var dongia = f["DonGia"];
            var hinhanh = f["HinhAnh"];
            var loaisp = f["LoaiSP"];
            SANPHAM sp = new SANPHAM();
            if (sp != null)
            {
                sp.TENSP = tensp;
                sp.DONGIA = double.Parse(dongia);
                sp.HINHANH = hinhanh;
                sp.MALSP = loaisp;
                db.SANPHAMs.InsertOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("QuanLySanPham", "SanPham");
            }
            else
            {
                return View();
            }
        }


        public ActionResult ThongKeSanPham(int year)
        {
            var lstData = db.Sp_ThongKeSanPhamBanTheoNam(year).ToList();

            return Json(lstData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ThongKe()
        {
            return View();
        }


        public ActionResult TimKiem(int? page)
        {
            var listSP = db.SANPHAMs.OrderBy(t => t.MASP).ToList();
            return View(listSP.ToPagedList(page ?? 1, 9));
        }

        public ActionResult TimKiemGo(FormCollection f)
        {
            var listSP = new List<SANPHAM>();
            string txttimkiem = f["txtTimKiem"];
            string txtmax = f["txtMax"];
            string txtmin = f["txtMin"];
            int max = 0;
            int min = 0;
            if (String.IsNullOrEmpty(txtmax) && String.IsNullOrEmpty(txtmin))
            {
                 listSP = db.SANPHAMs.Where(t => t.TENSP.Contains(txttimkiem)).ToList();
            }
            else if (String.IsNullOrEmpty(txtmin))
            {
                 max = int.Parse(txtmax);
                 listSP = db.SANPHAMs.Where(t => t.TENSP.Contains(txttimkiem) && t.DONGIA <= max).ToList();
            }
            else if (String.IsNullOrEmpty(txtmax))
            {
                min = int.Parse(txtmin);
                listSP = db.SANPHAMs.Where(t => t.TENSP.Contains(txttimkiem) && t.DONGIA >= min).ToList();
            }
            else
            {
                min = int.Parse(txtmin);
                max = int.Parse(txtmax);
                listSP = db.SANPHAMs.Where(t => t.TENSP.Contains(txttimkiem) && t.DONGIA >= min && t.DONGIA <= max).ToList();
            }

            return View(listSP);
        }




    }
}
