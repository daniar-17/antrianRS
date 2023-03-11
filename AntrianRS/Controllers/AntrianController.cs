using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using AntrianRS.Models;

namespace AntrianRS.Controllers
{
    
    public class AntrianController : Controller
    {
        public static List<DataAntrian> GetDataAntrian(string valueData, string checkData)
        {
            List<DataAntrian> users = new List<DataAntrian>();
            string fullPath = "Models/tempPasien.json";
            var jsonData = System.IO.File.ReadAllText(fullPath);
            var pasienList = JsonConvert.DeserializeObject<List<DataAntrian>>(jsonData) ?? new List<DataAntrian>();
            System.IO.File.WriteAllText(fullPath, "[]");
            
            if (checkData == "checkBersih")
            {
                users.Add(new DataAntrian
                {
                    Pasien = valueData,
                    Layanan = valueData,
                    Pembayaran = valueData,
                    Poli = valueData,
                    Dokter = valueData,
                    DataPasien = valueData
                });
                var JSONResult = JsonConvert.SerializeObject(users);
                System.IO.File.WriteAllText(fullPath, JSONResult);
            }
            else
            {
                foreach (var pasien in pasienList)
                {
                    users.Add(new DataAntrian
                    {
                        Pasien = (checkData == "checkPasien") ? valueData : pasien.Pasien,
                        Layanan = (checkData == "checkLayanan") ? valueData : pasien.Layanan,
                        Pembayaran = (checkData == "checkPembayaran") ? valueData : pasien.Pembayaran,
                        Poli = (checkData == "checkPoli") ? valueData : pasien.Poli,
                        Dokter = (checkData == "checkDokter") ? valueData : pasien.Dokter,
                        DataPasien = pasien.DataPasien
                    });
                    var JSONResult = JsonConvert.SerializeObject(users);
                    System.IO.File.WriteAllText(fullPath, JSONResult);
                }
            }
            return users;
        }

        public static string GetNoAntrian()
        {
            string itemAntrian2 = "";
            string fullPath = "Models/dataAntrian.json";
            var jsonData = System.IO.File.ReadAllText(fullPath);
            var pasienList = JsonConvert.DeserializeObject<List<DataAntrianAll>>(jsonData) ?? new List<DataAntrianAll>();
            List<DataAntrianAll> users = new List<DataAntrianAll>(pasienList);
            for (int idx = users.Count - 1; idx < users.Count; ++idx)
            {
                var noAntrian = users[idx].NoAntrian;
                var idAntrian = noAntrian.Replace('A', ' ').Trim();
                int itemAntrian = int.Parse(idAntrian) + 1;
                itemAntrian2 = "A" + itemAntrian;
            }
            return itemAntrian2;
        }

        public static List<DataPasien> GetDataAllPasien()
        {
            string fullPath = "Models/dataPasien.json";
            var jsonData = System.IO.File.ReadAllText(fullPath);
            var pasienList = JsonConvert.DeserializeObject<List<DataPasien>>(jsonData) ?? new List<DataPasien>();
            List<DataPasien> users2 = new List<DataPasien>(pasienList);
            return users2;
        }

        public IActionResult TambahDataPasien(DataPasien dataPasien)
        {
            string fullPath = "Models/dataPasien.json";
            var jsonData = System.IO.File.ReadAllText(fullPath);
            try
            {
                var pasienList = JsonConvert.DeserializeObject<List<DataPasien>>(jsonData) ?? new List<DataPasien>();
                System.IO.File.WriteAllText(fullPath, "[]");
                pasienList.Add(new DataPasien
                {
                    Nama = dataPasien.Nama,
                    NoHp = dataPasien.NoHp,
                    JenisKelamin = dataPasien.JenisKelamin,
                    Agama = dataPasien.Agama,
                    TglLahir = dataPasien.TglLahir
                });
                var JSONResult = JsonConvert.SerializeObject(pasienList);
                System.IO.File.WriteAllText(fullPath, JSONResult);
                return Json(new { success = true, message = "Saved Successfully" });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "Error while saving" });
            }
        }

        public IActionResult TambahDataAntrian(DataAntrianAll dataPasien)
        {
            string fullPath = "Models/dataAntrian.json";
            var jsonData = System.IO.File.ReadAllText(fullPath);
            try
            {
                var pasienList = JsonConvert.DeserializeObject<List<DataAntrianAll>>(jsonData) ?? new List<DataAntrianAll>();
                System.IO.File.WriteAllText(fullPath, "[]");
                pasienList.Add(new DataAntrianAll
                {
                    NoAntrian = dataPasien.NoAntrian,
                    Nama = dataPasien.Nama,
                    NoHp = dataPasien.NoHp,
                    JenisKelamin = dataPasien.JenisKelamin,
                    Agama = dataPasien.Agama,
                });
                var JSONResult = JsonConvert.SerializeObject(pasienList);
                System.IO.File.WriteAllText(fullPath, JSONResult);
                return Json(new { success = true, message = "Saved Successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "Error while saving" });
            }
        }

        public dynamic ViewDataAntrian(string valueData, string checkData)
        {
            var dataList = GetDataAntrian(valueData, checkData);
            foreach (var pasien in dataList)
            {
                ViewBag.Pasien = pasien.Pasien;
                ViewBag.Layanan = pasien.Layanan;
                ViewBag.Pembayaran = pasien.Pembayaran;
                ViewBag.Poli = pasien.Poli;
                ViewBag.Dokter = pasien.Dokter;
                ViewBag.DataPasien = pasien.DataPasien;
            }
            return ViewBag;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            string valueData = ""; string checkData = "checkBersih";
            ViewDataAntrian(valueData, checkData);
            return View();
        }

        [HttpGet("/pasien")]
        public IActionResult Pasien(string type_pasien, string data_pasien)
        {
            ViewDataAntrian(type_pasien, data_pasien);
            return View("~/Views/Antrian/Pasien.cshtml");
        }

        //[HttpGet("pasien_process")]
        //public IActionResult PasienProcess(string type_pasien, string data_pasien)
        //{
        //    ViewDataAntrian(type_pasien, data_pasien);
        //    return View("~/Views/Antrian/Pasien.cshtml");
        //}

        [HttpGet("layanan")]
        public IActionResult Layanan(string type_layanan, string data_layanan)
        {
            ViewDataAntrian(type_layanan, data_layanan);
            return View("~/Views/Antrian/Layanan.cshtml");
        }

        [HttpGet("metode_pembayaran")]
        public IActionResult MetodePembayaran(string type_pembayaran, string data_pembayaran)
        {
            ViewDataAntrian(type_pembayaran, data_pembayaran);
            return View("~/Views/Antrian/MetodePembayaran.cshtml");
        }

        [HttpGet("poli")]
        public IActionResult Poli(string type_poli, string data_poli)
        {
            ViewDataAntrian(type_poli, data_poli);
            return View("~/Views/Antrian/Poli.cshtml");
        }

        [HttpGet("dokter")]
        public IActionResult Dokter(string type_dokter, string data_dokter)
        {
            ViewDataAntrian(type_dokter, data_dokter);
            return View("~/Views/Antrian/Dokter.cshtml");
        }

        [HttpGet("data_pasien")]
        public IActionResult DataPasien()
        {
            string type_pasien = ""; string data_pasien = "checkDataPasien";
            ViewDataAntrian(type_pasien, data_pasien);
            ViewData["PaseinsList"] = GetDataAllPasien();
            ViewBag.NoAntrian = GetNoAntrian();
            return View("~/Views/Antrian/DataPasien.cshtml");
        }

        [HttpPost("pasien_process")]
        public IActionResult DataPasienProcess(DataPasien dataPasien)
        {
            TambahDataPasien(dataPasien);
            return View("~/Views/Antrian/DataPasien.cshtml");
        }

        [HttpPost("antrian_process")]
        public IActionResult TambahAntrian(DataAntrianAll dataPasien)
        {
            TambahDataAntrian(dataPasien);
            return View("~/Views/Antrian/DataPasien.cshtml");
        }
    }
}
