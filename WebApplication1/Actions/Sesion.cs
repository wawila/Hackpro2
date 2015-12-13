using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Actions;

namespace WebApplication1.Actions
{
    public class Sesion : Controller
    {
        string nombre;
        string apellido;
        string correo;
        bool isAdmin;

        public void Loged(string correo)
        {
            hackprodb_4Entities db = new hackprodb_4Entities();

            foreach (var user in db.tbl_usuario.Where(n => n.tbl_usuario_correo == correo) )
            {
                this.correo = correo;
                nombre = user.tbl_usuario_p_nombre;
                apellido = user.tbl_usuario_p_apellido;
                isAdmin = user.tbl_usuario_admin;

                System.Console.WriteLine("Correo " + correo);
                System.Console.WriteLine("Nombre " + nombre);
                System.Console.WriteLine("Apellido " + apellido);
                System.Console.WriteLine("Is Admin " + isAdmin);
            }

            ViewBag.correo = correo;
            ViewBag.nombre = nombre + " " + apellido;
            ViewBag.isAdmin = isAdmin;

        }
    }
}