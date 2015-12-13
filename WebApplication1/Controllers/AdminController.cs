using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Actions;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        /*public ActionResult Index()
        {
            return View();
        }*/

        [HttpGet]
        public ActionResult Admin()
        {
            return View("Admin");
        }

        [HttpGet]
        public ActionResult EventForm()
        {
            return View("EventForm");
        }

        [HttpGet]
        public ActionResult ListaUsuarios()
        {
            GetListasUsuarios();
            return View("ListaUsuarios");
        }

        [HttpGet]
        public ActionResult ListaEventos()
        {
            GetListasEventos();
            return View("ListaEventos");
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }


        [HttpGet]
        public void GetListasUsuarios()
        {
            hackprodb_4Entities db = new hackprodb_4Entities();
            List<tbl_usuario> ListUsuario = db.tbl_usuario.ToList();
            var lista = "";

            foreach (tbl_usuario usu in ListUsuario)
            {
                if (!usu.tbl_usuario_activo)
                    continue;

                lista += "<tr>";
                lista += "<td> " + usu.tbl_usuario_id + " </td>";
                lista += "<td> " + usu.tbl_usuario_correo + " </td>";
                lista += "<td> " + usu.tbl_usuario_username + " </td>";
                lista += "<td> " + usu.tbl_usuario_p_nombre + " </td>";
                lista += "<td> " + usu.tbl_usuario_p_apellido + " </td>";
                lista += "<td> " + usu.tbl_usuario_genero + " </td>";
                lista += "<td> " + usu.tbl_usuario_celular + " </td>";
                lista += "<td> " + usu.tbl_usuario_ocupacion + " </td>";
                lista += "</tr>";
            }
            ViewBag.HtmlStr = lista;
        }


        [HttpGet]
        public void GetListasEventos()
        {
            hackprodb_4Entities edb = new hackprodb_4Entities();
            List<tbl_evento> ListEventos = edb.tbl_evento.ToList();
            var evento = "";

            foreach (tbl_evento eve in ListEventos)
            {
                if (!eve.tbl_evento_activo)
                    continue;

                evento += "<tr>";
                evento += "<td>" + eve.tbl_evento_id + "</td>";
                evento += "<td>" + eve.tbl_evento_nombre + "</td>";
                evento += "<td>" + eve.tbl_evento_desc + "</td>";
                evento += "<td>" + eve.tbl_evento_lugar + "</td>";
                evento += "<td>" + eve.tbl_evento_duracion + "</td>";
                evento += "<td>" + eve.tbl_evento_fecha_inicio + "</td>";
                evento += "<td>" + eve.tbl_evento_precio + "</td>";
                evento += "<td>" + eve.tbl_evento_url + "</td>";
                evento += "<td>";
                if (eve.tbl_evento_cal_jurado && eve.tbl_evento_cal_pueblo)
                {
                    evento += " J - P";
                }
                else if (eve.tbl_evento_cal_jurado && !eve.tbl_evento_cal_pueblo)
                {
                    evento += "J";
                }
                else if (!eve.tbl_evento_cal_jurado && eve.tbl_evento_cal_pueblo)
                {
                    evento += "P";
                }
                else
                {
                    evento += "N/A";
                }
                evento += "</tr>";
            }
            ViewBag.eventos = evento;
        }
}
}