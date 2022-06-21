using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestShopQuanAo
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void TestMethod1()
        //{
        //    KhongNhapUsernameTest khongNhapUsernameTest = new KhongNhapUsernameTest();
        //    khongNhapUsernameTest.SetUp();
        //    khongNhapUsernameTest.khongNhapUsername();
        //    khongNhapUsernameTest.TearDown();
        //}

        [TestMethod]
        [DataTestMethod]
        [DynamicData(nameof(TestData_DangKy), DynamicDataSourceType.Method)]
        public void TestMethod3(string pUsername, string pPw, string pRpw, string pHoTen, string pNgSinh, string pEmail, string pSDT, string pDiaChi)
        {
            TCDNTCTest dangki = new TCDNTCTest();
            dangki.SetUp();
            dangki.tCDNTC(pUsername, pPw, pRpw, pHoTen, pNgSinh, pEmail, pSDT, pDiaChi);
            dangki.TearDown();
        }
        #region bộ dữ liệu test
        static IEnumerable<object[]> TestData_DangKy()
        {

            //yield return new Object[] { "", "tin@cute123", "tin@cute123", "Võ Văn Tin", "01/01/2001", "tin@gmail.com", "0973713495", "Gia Lai" }; // nhập thiếu usename
            //yield return new Object[] { "tin", "", "tin@cute123", "Võ Văn Tin", "01/01/2001", "tin@gmail.com", "0973713495", "Gia Lai" }; // nhập thiếu pass
            //yield return new Object[] { "tin", "tin@cute123", "tin@cute12", "Võ Văn Tin", "01/01/2001", "tin@gmail.com", "0973713495", "Gia Lai" }; // repass sai
            //yield return new Object[] { "tin", "tin@cute", "tin@cute", "Võ Văn Tin", "01/01/2001", "tin@gmail.com", "0973713495", "Gia Lai" }; // pass k đủ độ dài
            //yield return new Object[] { "tin", "tin@cute123", "tin@cute123", "Võ Văn Tin", "01/01/2001", "tin@gmail.com", "0973aa13495", "Gia Lai" }; //sdt sai định dạng
            //yield return new Object[] { "tin", "tin@cute123", "tin@cute123", "Võ Văn Tin", "01/01/2001", "tingmail.com", "0973713495", "Gia Lai" }; //email sai định dạng
            //yield return new Object[] { "tai", "tai12345", "tai12345", "Đinh Phát Tài", "11/05/2001", "tai@gmail.com", "0362417182", "Tân Bình" }; // đk đúng

            //yield return new Object[] { "tai", "tai12345", "tai12345", "Đinh Phát Tài", "12/05/2001", "tai@gmail.com", "0362417182", "Tân Bình" }; // đk đúng
            yield return new Object[] { "tai1", "tai12345", "tai12345", "Đinh Phát Tài", "05/28/2001", "tai@gmail.com", "0362417182", "Tân Bình" }; // đk đúng
            
            //yield return new Object[] { "na12345", "na17052001", "na17052001", "Lê Thùy Na", "17/05/2001", "nalethuy@gmail.com", "0362417182", "Tân Bình" }; // tài khoản đã tồn tại

            
            //yield return new Object[] { "nacute", "na@17052001", "na@17052001", "Lê Thùy Na", "17/05/2001", "nalethuy@gmail.com", "0362417182", "Tân Bình" }; //đăng kí thành công

           
        }
        #endregion
        //[TestMethod]
        //public void TestMethod4()
        //{
        //    TestDNKhongNhapGiTest khongNhapUsernameTest = new TestDNKhongNhapGiTest();
        //    khongNhapUsernameTest.SetUp();
        //    khongNhapUsernameTest.testDNKhongNhapGi();
        //    khongNhapUsernameTest.TearDown();
        //}
        //[TestMethod]
        //public void TestMethod5()
        //{
        //    TestDNKhongNhapGiTest khongNhapUsernameTest = new TestDNKhongNhapGiTest();
        //    khongNhapUsernameTest.SetUp();
        //    khongNhapUsernameTest.testDNKhongNhapUsername();
        //    khongNhapUsernameTest.TearDown();
        //}
        //[TestMethod]
        //public void TestMethod6()
        //{
        //    TestDNKhongNhapGiTest khongNhapUsernameTest = new TestDNKhongNhapGiTest();
        //    khongNhapUsernameTest.SetUp();
        //    khongNhapUsernameTest.testDNKhongNhapPass();
        //    khongNhapUsernameTest.TearDown();
        //}
        //[TestMethod]
        //public void TestMethod7()
        //{
        //    TestDNKhongNhapGiTest khongNhapUsernameTest = new TestDNKhongNhapGiTest();
        //    khongNhapUsernameTest.SetUp();
        //    khongNhapUsernameTest.testDNSai();
        //    khongNhapUsernameTest.TearDown();
        //}


        // Test đăng nhập 

        [TestMethod]
        [DataTestMethod]
        [DynamicData(nameof(TestData_DangNhap), DynamicDataSourceType.Method)]
        public void TestMethod8(string pUsername, string pPw)
        {
            TestDNFullTest dn = new TestDNFullTest();
            dn.SetUp();
            dn.testDNFull(pUsername, pPw);
            dn.TearDown();
        }

        #region bộ dữ liệu test
        static IEnumerable<object[]> TestData_DangNhap()
        {
            yield return new Object[] { "", "123" }; // nhập thiếu usename
            yield return new Object[] { "TK001", "" }; // nhập thiếu pass

            yield return new Object[] { "TK0011", "123" }; // tài khoản không tồn tại

            yield return new Object[] { "TK001", "123" }; // nhập đúng
            yield return new Object[] { "TK002", "123" }; // nhập đúng
            yield return new Object[] { "TK003", "123" }; // nhập đúng

        }
        #endregion

        //test thêm vào giỏ hàng
        [TestMethod]
        public void TestMethod9()
        {
            TestMuaHangTCTest themgiohang = new TestMuaHangTCTest();
            themgiohang.SetUp();
            themgiohang.testMuaHangTC();
            themgiohang.TearDown();
        }

        //test thanh toán thành công
        [TestMethod]
        public void TestMethod10()
        {
            ThanhToanTest thanhtoan = new ThanhToanTest();
            thanhtoan.SetUp();
            thanhtoan.thanhToan();
            thanhtoan.TearDown();
        }


        //test tim kiếm nhập vào khoảng giá
        [TestMethod]
        public void TestMethod11()
        {
            TestTKNhapKhoangGiaTest timkiem = new TestTKNhapKhoangGiaTest();
            timkiem.SetUp();
            timkiem.testTKNhapKhoangGia();
            timkiem.TearDown();
        }

    }

}
