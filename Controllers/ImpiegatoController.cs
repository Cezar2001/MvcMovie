using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using System.Net;

namespace MvcMovie.Controllers
{
    public class ImpiegatoController : Controller
    {
        public IActionResult CreaForm()
        {
            Impiegato TempImpiegato = new Impiegato()
            {
                Id = 0,
                FullName = "",
                Gender = "",
                City = "",
                EmailAddress = "",
                PersonalWebSite = "",
                Photo = "",
                AlternateText = "",
            };
            return View(TempImpiegato);
        }

        [HttpGet]
        public IActionResult CreaFormFoto()
        {
            ImpiegatoFile TempImpiegato = new ImpiegatoFile()
            {
                Id = 0,
                FullName = "",
                Gender = "",
                City = "",
                EmailAddress = "",
                PersonalWebSite = "",
                Photo = "",
                AlternateText = "",
            };
            return View(TempImpiegato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreaScheda(Impiegato DatiImpiegato)
        {
            string sNomeImpiegato = DatiImpiegato.FullName.Split(" ")[0];
            string sCognomeImpiegato = DatiImpiegato.FullName.Split(" ")[1];
            Impiegato VeroImpiegato = new Impiegato()
            {
                Id = DatiImpiegato.Id,
                FullName = DatiImpiegato.FullName,
                Gender = DatiImpiegato.Gender,
                City = DatiImpiegato.City,
                EmailAddress = sNomeImpiegato + "." + sCognomeImpiegato + "@gmail.com",
                PersonalWebSite = "www." + sNomeImpiegato + "-" + sCognomeImpiegato + ".com",
                Photo = "/Img/fondo-pag-speciali.jpg",
                AlternateText = "Foto non disponibile",
            };
            return View(VeroImpiegato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreaSchedaFoto(ImpiegatoFile DatiImpiegato)
        {
            if (!ModelState.IsValid)
            {
                return View("CreaFormFoto", DatiImpiegato);
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            FileInfo fileInfo = new FileInfo(DatiImpiegato.File.FileName);
            
            string fileName = DatiImpiegato.FullName + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                DatiImpiegato.File.CopyTo(stream);
            }
            
            //DatiImpiegato.File.CopyTo()
            
            string sNomeImpiegato = DatiImpiegato.FullName.Split(" ")[0];
            string sCognomeImpiegato = DatiImpiegato.FullName.Split(" ")[1];
            
            ImpiegatoFile VeroImpiegato = new ImpiegatoFile()
            {
                Id = DatiImpiegato.Id,
                FullName = DatiImpiegato.FullName,
                Gender = DatiImpiegato.Gender,
                City = DatiImpiegato.City,
                EmailAddress = sNomeImpiegato + "." + sCognomeImpiegato + "@gmail.com",
                PersonalWebSite = "www." + sNomeImpiegato + "-" + sCognomeImpiegato + ".com",
                Photo = "/Files/" + fileName,
                AlternateText = "Foto non disponibile",
            };
            return View(VeroImpiegato);
        }

        public IActionResult CreaFormPut()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://localhost:7090/Impiegato/InserisciDirigente");
            
            var postData = "NomeDir=" + Uri.EscapeDataString("Mario");
            postData += "&CognomeDir=" + Uri.EscapeDataString("Rossi");
            var data = System.Text.Encoding.ASCII.GetBytes(postData);

            request.Method = "PUT";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            
            EsitoOperazionePut Esito = new EsitoOperazionePut();
            Esito.sEsito = responseString;
            ViewData["Message"] = "Risposta del server";
            return View(Esito);
        }

        [HttpPut]
        public string InserisciDirigente(string sData)
        {
            string sAppo = Request.Method;

            if(sAppo == "PUT")
                return "Dirigente inserito correttamente";
            else
                return "Errore inserimento dirigente";
        }
    }
}
