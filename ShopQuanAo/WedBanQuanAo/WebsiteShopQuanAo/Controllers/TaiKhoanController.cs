using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteShopQuanAo.Models;
using System.Text.RegularExpressions;

namespace WebsiteShopQuanAo.Controllers
{
    public class TaiKhoanController : Controller
    {
        //
        // GET: /TaiKhoan/

        ShopQuanAoDataContext db = new ShopQuanAoDataContext();
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            //khai báo cac biến nhận gtri tu form f
            var username = f["username"];
            var matkhau = f["pw"];
            if (String.IsNullOrEmpty(username))
            {
                ViewData["Loi1"] = "Tên đăng nhập không được bỏ trống";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
            }

            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(matkhau))
            {
                TAIKHOAN tk = db.TAIKHOANs.SingleOrDefault(c => c.TENTK == username);
                if (tk != null)
                {
                    int quyen = int.Parse(tk.MAQUYEN.ToString());
                    Session["User"] = tk;
                    //Session["tdn"] = tttk.TENTK;
                    if(quyen == 1)
                    {
                        TAIKHOANKHACHHANG tttk = db.TAIKHOANKHACHHANGs.SingleOrDefault(c => c.TENTK == username);
                        Session["Customer"] = tttk;
                    }
                    return RedirectToAction("TatCaSanPhamHome", "SanPham");

                }
                else
                {
                    Response.Write("<script>alert('Tài Khoản hoặc mật khẩu không đúng !!!')</script>");
                }
            }
            return View();
        }

        public ActionResult DoiMatKhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection f)
        {
            var username = f["UserName"];
            var pass = f["Pass"];
            var newpass = f["NewPass"];
            var repass = f["RePass"];

            if (String.IsNullOrEmpty(newpass))
            {
                ViewData["LoiNewPass"] = "Vui lòng nhập mật khẩu mới!";
            }

            if (String.IsNullOrEmpty(repass))
            {
                ViewData["LoiRePass"] = "Vui lòng nhập lại mật khẩu mới!";
            }

            if (String.Equals(newpass, repass) == false)
            {
                ViewData["LoiRePass"] = "Mật khẩu nhập lại không đúng!";
            }
            else
            {
                TAIKHOAN tk = db.TAIKHOANs.SingleOrDefault(s=>s.TENTK == username);
                if (pass == tk.MATKHAU)
                {
                    tk.MATKHAU = newpass;
                    db.SubmitChanges();
                    Response.Write("<script>alert(Đổi mật khẩu thành công!!!')</script>");
                }
                else if (String.IsNullOrEmpty(pass))
                {
                    ViewData["LoiPass"] = " Vui lòng nhập mật khẩu hiện tại!";
                }
                else
                {
                    ViewData["LoiPass"] = "Mật khẩu hiện tại bạn nhập không đúng!";
                }
            }
            return View();
        }

        public ActionResult ThongTinTaiKhoan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThongTinTaiKhoan(FormCollection f)
        {
            var username = f["UserName"];
            var name = f["Ten"];
            var email = f["Email"];
            var sdt = f["SDT"];
            var gioitinh = f["GioiTinh"];
            var diachi = f["DiaChi"];
            var matkhau = f["KTPass"];
            

            TAIKHOAN tk = db.TAIKHOANs.SingleOrDefault(s=>s.TENTK == username);
            if (tk.MATKHAU == matkhau)
            {
                tk.HOTEN = name;
                tk.EMAIL = email;
                tk.SDT = sdt;
                tk.GTINH = gioitinh;
                tk.DCHI = diachi;
                db.SubmitChanges();
                Session["User"] = tk;
                Response.Write("<script>alert('Cập nhật thông tin thành công!!!')</script>");
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewBag.LoiPass = "Vui lòng nhập mật khẩu hiện tại !";
            }
            else
            {
                ViewData["LoiPass"] = "Mật khẩu bạn nhập vào không đúng !";
            }
            return View();

        }

        public ActionResult DangXuat()
        {
            Session["User"] = null;
            Session["tdn"] = null;
            Session["GiamGia"] = null;
            Session["Customer"] = null;
            return RedirectToAction("TatCaSanPhamHome", "SanPham");
        }

        
        public ActionResult DangKy()
        {
            return View();
        }


        [HttpPost]
        public ActionResult DangKy(TaiKhoanValid tkvl, TAIKHOAN tttk,TAIKHOANKHACHHANG tkkh, FormCollection f)
        {
            var username = f["Username"];
            var matkhau = f["Pass"];
            var remathau = f["RePass"];
            var hoten = f["Ten"];
            var email = f["Email"];
            var dienthoai = f["SDT"];
            var gioitinh = f["Gioitinh"];
            var diachi = f["DiaChi"];
            var ngaysinh = f["NgaySinh"];
            
            if(ModelState.IsValid)
            {
                TAIKHOAN ktratk = db.TAIKHOANs.SingleOrDefault(t => t.TENTK == username);
                if (String.Compare(matkhau,remathau)!=0)
                {
                    ViewData["LoiComfirmPass"] = "You re-entered the wrong password";
                    //Response.Write("<script>alert('Mật khẩu nhập lại không đúng!!!')</script>");
                }
                else if (ktratk != null)
                {
                    ViewData["LoiDaTonTaiTenTk"] = "This username already exists !";
                }
                else
                {
                    //gán giá trị cho đối tượng kh
                    tttk.TENTK = username;
                    tttk.MATKHAU = matkhau;
                    tttk.HOTEN = hoten;
                    tttk.NGSINH = DateTime.Parse(ngaysinh);
                    tttk.EMAIL = email;
                    tttk.SDT = dienthoai;
                    tttk.DCHI = diachi;
                    tttk.GTINH = gioitinh;
                    tttk.MAQUYEN = 1; // 2 = Quản lý , 1 = Khách hàng
                    db.TAIKHOANs.InsertOnSubmit(tttk);
                    tkkh.TENTK = username;
                    tkkh.LOAITK = "Đồng";
                    tkkh.ANH = "Bronze.jpg";
                    db.TAIKHOANKHACHHANGs.InsertOnSubmit(tkkh);
                    db.SubmitChanges();
                    //Response.Write("<script>alert('Đăng Ký Thành Công!!!')</script>");
                    return RedirectToAction("DangNhap", "TaiKhoan");
                }

            }
            return View(tkvl);
        }

        public ActionResult LichSuGiaoDich()
        {
            var tk = Session["User"] as TAIKHOAN;
            List<HOADON> lsthd = db.HOADONs.Where(t => t.TENTK == tk.TENTK).ToList();
            return View(lsthd);
        }

        public ActionResult XemChiTietHoaDon( int mahd)
        {
            return View(db.Sp_ChiTietGiaoDich(mahd).ToList());
        }
    }
}
